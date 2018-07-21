using System;
using System.Net.Http;
using System.Text;
using CodeWarsGame.Models;
using Newtonsoft.Json.Linq;

namespace CodeWarsGame.Api
{
    public class SubmitCodeRequest
    {
        private const int SubmissionTimeOut = 20;

        public static ApiResult Submit(string sourceCode, string input)
        {
            using (var client = new HttpClient())
            {
                var encoded = Uri.EscapeDataString(sourceCode);

                var uri =
                    $"http://api.paiza.io:80/runners/create?source_code={encoded}&language=csharp&input={input}&longpoll=true&longpoll_timeout={SubmissionTimeOut}&api_key=guest";
                var stringContent = new StringContent("", Encoding.UTF8, "application/json");

                var response = client.PostAsJsonAsync(uri, stringContent).Result.Content.ReadAsStringAsync().Result;
                var id = JObject.Parse(response)["id"].Value<string>();

                return GetResults(id, client);
            }
        }

        private static ApiResult GetResults(string id, HttpClient client)
        {
            var repeatCount = 3;

            while (repeatCount > 0)
            {
                var uri = new Uri($"http://api.paiza.io:80/runners/get_details?id={id}&api_key=guest");
                var response = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
                var status = JObject.Parse(response)["status"].Value<string>();
                var buildResult  = JObject.Parse(response)["build_result"].Value<string>();

                if (status == "completed")
                {
                    if (buildResult == "success")
                    {
                        var result = JObject.Parse(response)["stdout"].Value<string>().Replace("\n", ";");
                        if (result[result.Length - 1].ToString() == ";")
                        {
                            result = result.Remove(result.Length - 1);
                        }
                            
                        return ApiResult.Create(result.Trim(), true);
                    }

                    if (buildResult == "failure")
                    {
                        var result = JObject.Parse(response)["build_stdout"].Value<string>();
                        var error = JObject.Parse(response)["build_stderr"].Value<string>();
                        return ApiResult.Create($"{result}{error}", false);
                    }
                }

                repeatCount--;
            }

            return null;
        }
    }
}
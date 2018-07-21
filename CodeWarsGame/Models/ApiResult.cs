namespace CodeWarsGame.Models
{
    public sealed class ApiResult
    {
        public string Output { get; private set; }
        public bool Success { get; private set; }

        public static ApiResult Create(string output, bool success)
        {
            return new ApiResult
            {
                Success = success,
                Output = output
            };
        }
    }
}
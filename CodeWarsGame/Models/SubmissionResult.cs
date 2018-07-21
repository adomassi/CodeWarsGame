namespace CodeWarsGame.Models
{
    public sealed class SubmissionResult
    {
        public string Output { get; private set; }
        public bool Success { get; private set; }

        public static SubmissionResult Create(string output, bool success)
        {
            return new SubmissionResult
            {
                Output = output,  
                Success = success,
            };
        }
    }
}
using CodeWarsGame.Api;
using CodeWarsGame.Models;
using CodeWarsGame.Repositories;

namespace CodeWarsGame.Helpers
{
    public  class SubmissionValidation
    {
        public SubmissionResult ValidateSubmission(string sourceCode, int taskId)
        {
            using (var taskRepository = new TasksRepository())
            {
                var task = taskRepository.GetTaskById(taskId);
                var result = SubmitCodeRequest.Submit(sourceCode, task.InputParameter);

                if (result.Success)
                {
                    if (result.Output == task.OutputParameter)
                    {
                        return SubmissionResult.Create($"Success! Predefined input: {task.InputParameter}  Correct value from Api: {result.Output}", true);

                    }

                    return SubmissionResult.Create(
                        $"Fail! Expected value: {task.OutputParameter} Predefined input: {task.InputParameter}  Returned value from Api: {result.Output}", false);
                }

                return SubmissionResult.Create(result.Output, false);
            }
        }
    }
}
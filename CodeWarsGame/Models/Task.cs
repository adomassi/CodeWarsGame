using System.ComponentModel.DataAnnotations;

namespace CodeWarsGame.Models
{
    public sealed class Task
    {
        [Key]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string InputParameter { get; set; }
        public string OutputParameter { get; set; }
    }
}
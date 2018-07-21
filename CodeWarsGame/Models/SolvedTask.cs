using System.ComponentModel.DataAnnotations;

namespace CodeWarsGame.Models
{
    public class SolvedTask
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int PlayerId { get; set; }

        public virtual Task Task { get; set; }
        public virtual Player Player { get; set; }
    }
}
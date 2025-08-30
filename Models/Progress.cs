using LearnSphere.Models;

namespace LearnSphere.Models
{
    public class Progress
    {
        public int ProgressId { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string Milestone { get; set; }   // e.g., "Module 1 Completed"
        public int CompletionPercent { get; set; } // 0 - 100
    }
}

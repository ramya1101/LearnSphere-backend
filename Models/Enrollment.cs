using LearnSphere.Models;

namespace LearnSphere.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public DateTime EnrollDate { get; set; } = DateTime.UtcNow;
    }
}

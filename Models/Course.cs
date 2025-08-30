namespace LearnSphere.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }    // foreign key to User (role Mentor)
        public User Mentor { get; set; }
        public int DurationHours { get; set; }
        public string Syllabus { get; set; }
    }
}

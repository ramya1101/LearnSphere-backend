namespace LearnSphere.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationHours { get; set; }
        public string Syllabus { get; set; }
        public int MentorId { get; set; }
        public string MentorName { get; set; }
    }
}

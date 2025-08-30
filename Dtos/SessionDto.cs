namespace LearnSphere.Dtos
{
    public class SessionDto
    {
        public int SessionId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int MentorId { get; set; }
        public string MentorName { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}

namespace LearnSphere.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime EnrollDate { get; set; }
    }
}

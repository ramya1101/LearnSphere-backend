namespace LearnSphere.Dtos;
public class FeedbackDto
{
    public int FeedbackId { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; }

    // missing property
    public int MentorId { get; set; }
    public string MentorName { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}



//namespace LearnSphere.Dtos
//{
//    public class FeedbackDto
//    {
//        public int FeedbackId { get; set; }
//        public int CourseId { get; set; }
//        public string CourseTitle { get; set; }
//        public int StudentId { get; set; }

//        public string StudentName { get; set; }
//        public int Rating { get; set; }
//        public string Comment { get; set; }
//        public DateTime Date { get; set; }
//    }
//}

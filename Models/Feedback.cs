using LearnSphere.Models;
using System;

namespace LearnSphere.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int MentorId { get; set; }
        public User Mentor { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int Rating { get; set; } // 1–5
        public string Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}

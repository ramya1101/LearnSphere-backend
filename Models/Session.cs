using LearnSphere.Models;
using System;

namespace LearnSphere.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int MentorId { get; set; }
        public User Mentor { get; set; }

        public DateTime DateTime { get; set; }
        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled
    }
}

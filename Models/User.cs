using System.Collections.Generic;

namespace LearnSphere.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }    // full name
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }    // Admin, Mentor, Student
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

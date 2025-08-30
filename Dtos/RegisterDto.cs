using System.ComponentModel.DataAnnotations;

namespace LearnSphere.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = " ";

        [Required, EmailAddress]
        public string Email { get; set; } = " ";

        [Required, MinLength(6)]
        public string Password { get; set; } = " ";

        [Required]
        public string Role { get; set; } = " "; 
        
        // "Admin", "Mentor", "Student"
    }
}




//namespace LearnSphere.Dtos
//{
//    public class RegisterDto
//    {
//        [Required]
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string Role { get; set; } // Admin | Mentor | Student
//    }
//}

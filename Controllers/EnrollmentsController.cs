
using LearnSphere.Data;
using LearnSphere.Dtos;
using LearnSphere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        public EnrollmentsController(LearnSphereContext context) { _context = context; }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentDto dto)
        {
            var existing = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == dto.CourseId && e.StudentId == dto.StudentId);
            if (existing != null) return BadRequest("Already enrolled.");

            var enrollment = new Enrollment
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                EnrollDate = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            dto.EnrollmentId = enrollment.EnrollmentId;
            dto.EnrollDate = enrollment.EnrollDate;
            return Ok(dto);
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();

            var result = enrollments.Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                StudentId = e.StudentId,
                StudentName = e.Student.Name,
                EnrollDate = e.EnrollDate
            });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> Unenroll(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Unenrolled successfully" });
        }
    }
}



//using LearnSphere.Models;
//using LearnSphere.Data;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LearnSphere.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class EnrollmentsController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        public EnrollmentsController(LearnSphereContext context)
//        {
//            _context = context;
//        }

//        // POST: api/enrollments
//        // Students enroll in a course
//        [HttpPost]
//        [Authorize(Roles = "Student")]
//        public async Task<IActionResult> Enroll([FromBody] Enrollment enrollment)
//        {
//            var course = await _context.Courses.FindAsync(enrollment.CourseId);
//            var student = await _context.Users.FindAsync(enrollment.StudentId);

//            if (course == null || student == null || student.Role != "Student")
//                return BadRequest("Invalid course or student.");

//            // Prevent duplicate enrollment
//            var existing = await _context.Enrollments
//                .FirstOrDefaultAsync(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);
//            if (existing != null)
//                return BadRequest("Already enrolled.");

//            enrollment.EnrollDate = DateTime.UtcNow;

//            _context.Enrollments.Add(enrollment);
//            await _context.SaveChangesAsync();
//            return Ok(new { message = "Enrolled successfully", enrollment });
//        }

//        // GET: api/enrollments/student/{studentId}
//        [HttpGet("student/{studentId}")]
//        [Authorize(Roles = "Student")]
//        public async Task<IActionResult> GetEnrollmentsByStudent(int studentId)
//        {
//            var enrollments = await _context.Enrollments
//                .Where(e => e.StudentId == studentId)
//                .Include(e => e.Course)
//                .ToListAsync();

//            return Ok(enrollments);
//        }

//        // GET: api/enrollments/course/{courseId}
//        [HttpGet("course/{courseId}")]
//        [Authorize(Roles = "Mentor,Admin")]
//        public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
//        {
//            var enrollments = await _context.Enrollments
//                .Where(e => e.CourseId == courseId)
//                .Include(e => e.Student)
//                .ToListAsync();

//            return Ok(enrollments);
//        }

//        // DELETE: api/enrollments/{id}
//        [HttpDelete("{id}")]
//        [Authorize(Roles = "Student,Admin")]
//        public async Task<IActionResult> Unenroll(int id)
//        {
//            var enrollment = await _context.Enrollments.FindAsync(id);
//            if (enrollment == null) return NotFound();

//            _context.Enrollments.Remove(enrollment);
//            await _context.SaveChangesAsync();
//            return Ok(new { message = "Unenrolled successfully" });
//        }
//    }
//}

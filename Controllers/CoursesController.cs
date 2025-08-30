
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
    public class CoursesController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        public CoursesController(LearnSphereContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses.Include(c => c.Mentor).ToListAsync();

            var result = courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                DurationHours = c.DurationHours,
                Syllabus = c.Syllabus,
                MentorId = c.MentorId,
                MentorName = c.Mentor?.Name
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses.Include(c => c.Mentor).FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null) return NotFound();

            var dto = new CourseDto
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                DurationHours = course.DurationHours,
                Syllabus = course.Syllabus,
                MentorId = course.MentorId,
                MentorName = course.Mentor?.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Create([FromBody] CourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                DurationHours = dto.DurationHours,
                Syllabus = dto.Syllabus,
                MentorId = dto.MentorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            dto.CourseId = course.CourseId;
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.DurationHours = dto.DurationHours;
            course.Syllabus = dto.Syllabus;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Mentor,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Course deleted successfully" });
        }
    }
}




//using LearnSphere.Data;
//using LearnSphere.Dtos;
//using LearnSphere.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LearnSphere.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CoursesController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        public CoursesController(LearnSphereContext context) { _context = context; }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var courses = await _context.Courses.Include(c => c.Mentor).ToListAsync();

//            var result = courses.Select(c => new CourseDto
//            {
//                CourseId = c.CourseId,
//                Title = c.Title,
//                Description = c.Description,
//                DurationHours = c.DurationHours,
//                Syllabus = c.Syllabus,
//                MentorId = c.MentorId,
//                MentorName = c.Mentor?.Name
//            });

//            return Ok(result);
//        }


//        //// GET: api/courses
//        //[HttpGet]
//        //public async Task<IActionResult> GetAll()
//        //{
//        //    var courses = await _context.Courses.Include(c => c.Mentor).ToListAsync();
//        //    return Ok(courses);
//        //}

//        // GET: api/courses/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var course = await _context.Courses.Include(c => c.Mentor).FirstOrDefaultAsync(c => c.CourseId == id);
//            if (course == null) return NotFound();
//            return Ok(course);
//        }

//        // POST: api/courses
//        [HttpPost]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> Create([FromBody] Course course)
//        {
//            if (course == null) return BadRequest("Course data is required.");

//            _context.Courses.Add(course);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
//        }

//        // PUT: api/courses/{id}
//        [HttpPut("{id}")]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> Update(int id, [FromBody] Course updatedCourse)
//        {
//            var course = await _context.Courses.FindAsync(id);
//            if (course == null) return NotFound();

//            // Optional: enforce that only the course's mentor can update it
//            course.Title = updatedCourse.Title;
//            course.Description = updatedCourse.Description;
//            course.DurationHours = updatedCourse.DurationHours;
//            course.Syllabus = updatedCourse.Syllabus;

//            await _context.SaveChangesAsync();
//            return Ok(course);
//        }

//        // DELETE: api/courses/{id}
//        [HttpDelete("{id}")]
//        [Authorize(Roles = "Mentor,Admin")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var course = await _context.Courses.FindAsync(id);
//            if (course == null) return NotFound();

//            _context.Courses.Remove(course);
//            await _context.SaveChangesAsync();
//            return Ok(new { message = "Course deleted successfully" });
//        }

//        // GET: api/courses/by-mentor/{mentorId}
//        [HttpGet("by-mentor/{mentorId}")]
//        public async Task<IActionResult> GetByMentor(int mentorId)
//        {
//            var courses = await _context.Courses.Where(c => c.MentorId == mentorId).ToListAsync();
//            return Ok(courses);
//        }

//        // (Future) GET: api/courses/enrolled/{studentId}
//        // Requires Enrollments table to join courses
//        [HttpGet("enrolled/{studentId}")]
//        [Authorize(Roles = "Student")]
//        public async Task<IActionResult> GetEnrolledCourses(int studentId)
//        {
//            var courses = await _context.Courses
//                .Join(_context.Enrollments,
//                      course => course.CourseId,
//                      enroll => enroll.CourseId,
//                      (course, enroll) => new { course, enroll })
//                .Where(x => x.enroll.StudentId == studentId)
//                .Select(x => x.course)
//                .ToListAsync();

//            return Ok(courses);
//        }
//    }
//}









//using LearnSphere.Data;
//using LearnSphere.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//[ApiController]
//[Route("api/[controller]")]
//public class CoursesController : ControllerBase
//{
//    private readonly LearnSphereContext _context;
//    public CoursesController(LearnSphereContext context) { _context = context; }

//    [HttpGet]
//    public IActionResult GetAll() => Ok(_context.Courses.Include(c => c.Mentor).ToList());

//    [HttpPost]
//    [Authorize(Roles = "Mentor")]
//    public async Task<IActionResult> Create([FromBody] Course course)
//    {
//        _context.Courses.Add(course);
//        await _context.SaveChangesAsync();
//        return Ok(course);
//    }

//    // ... other endpoints
//}

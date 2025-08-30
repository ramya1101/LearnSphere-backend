
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
    public class FeedbackController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        public FeedbackController(LearnSphereContext context) { _context = context; }

        [HttpGet("course/{courseId}")]
        [Authorize]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var feedback = await _context.Feedback.Include(f => f.Student).Include(f => f.Course)
                                .Where(f => f.CourseId == courseId).ToListAsync();

            var result = feedback.Select(f => new FeedbackDto
            {
                FeedbackId = f.FeedbackId,
                CourseId = f.CourseId,
                CourseTitle = f.Course.Title,
                StudentId = f.StudentId,
                StudentName = f.Student.Name,
                Rating = f.Rating,
                Comment = f.Comment,
                Date = f.Date
            });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create([FromBody] FeedbackDto dto)
        {
            var feedback = new Feedback
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                MentorId = dto.MentorId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();

            dto.FeedbackId = feedback.FeedbackId;
            dto.Date = feedback.Date;
            return Ok(dto);
        }
    }
}



//using LearnSphere.Data;
//using LearnSphere.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LearnSphere.Api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class FeedbackController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        public FeedbackController(LearnSphereContext context) { _context = context; }

//        // GET: api/feedback/course/{courseId}
//        [HttpGet("course/{courseId}")]
//        [Authorize]
//        public async Task<IActionResult> GetByCourse(int courseId)
//        {
//            var feedback = await _context.Feedbacks
//                .Where(f => f.CourseId == courseId)
//                .Include(f => f.Student)
//                .ToListAsync();

//            return Ok(feedback);
//        }

//        // GET: api/feedback/mentor/{mentorId}
//        [HttpGet("mentor/{mentorId}")]
//        [Authorize]
//        public async Task<IActionResult> GetByMentor(int mentorId)
//        {
//            var feedback = await _context.Feedbacks
//                .Where(f => f.MentorId == mentorId)
//                .Include(f => f.Student)
//                .ToListAsync();

//            return Ok(feedback);
//        }

//        // POST: api/feedback
//        [HttpPost]
//        [Authorize(Roles = "Student")]
//        public async Task<IActionResult> Create([FromBody] Feedback feedback)
//        {
//            _context.Feedbacks.Add(feedback);
//            await _context.SaveChangesAsync();
//            return Ok(feedback);
//        }
//    }
//}

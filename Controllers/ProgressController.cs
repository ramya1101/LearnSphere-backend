
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
    public class ProgressController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        public ProgressController(LearnSphereContext context) { _context = context; }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var progress = await _context.Progresses.Include(p => p.Course).Include(p => p.Student)
                                .Where(p => p.StudentId == studentId).ToListAsync();

            var result = progress.Select(p => new ProgressDto
            {
                ProgressId = p.ProgressId,
                StudentId = p.StudentId,
                StudentName = p.Student.Name,
                CourseId = p.CourseId,
                CourseTitle = p.Course.Title,
                Milestone = p.Milestone,
                CompletionPercent = p.CompletionPercent
            });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Add([FromBody] ProgressDto dto)
        {
            var progress = new Progress
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Milestone = dto.Milestone,
                CompletionPercent = dto.CompletionPercent
            };

            _context.Progresses.Add(progress);
            await _context.SaveChangesAsync();

            dto.ProgressId = progress.ProgressId;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgressDto dto)
        {
            var progress = await _context.Progresses.FindAsync(id);
            if (progress == null) return NotFound();

            progress.Milestone = dto.Milestone;
            progress.CompletionPercent = dto.CompletionPercent;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }
    }
}



//using LearnSphere.Models;
//using LearnSphere.Data;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LearnSphere.Api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProgressController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        public ProgressController(LearnSphereContext context) { _context = context; }

//        // GET: api/progress/student/{studentId}
//        [HttpGet("student/{studentId}")]
//        [Authorize(Roles = "Student,Mentor,Admin")]
//        public async Task<IActionResult> GetByStudent(int studentId)
//        {
//            var progress = await _context.Progresses
//                .Where(p => p.StudentId == studentId)
//                .Include(p => p.Course)
//                .ToListAsync();

//            return Ok(progress);
//        }

//        // POST: api/progress
//        // Mentor updates student's progress
//        [HttpPost]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> AddProgress([FromBody] Progress progress)
//        {
//            _context.Progresses.Add(progress);
//            await _context.SaveChangesAsync();
//            return Ok(progress);
//        }

//        // PUT: api/progress/{id}
//        [HttpPut("{id}")]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> Update(int id, [FromBody] Progress updated)
//        {
//            var progress = await _context.Progresses.FindAsync(id);
//            if (progress == null) return NotFound();

//            progress.Milestone = updated.Milestone;
//            progress.CompletionPercent = updated.CompletionPercent;

//            await _context.SaveChangesAsync();
//            return Ok(progress);
//        }
//    }
//}

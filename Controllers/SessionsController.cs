
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
    public class SessionsController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        public SessionsController(LearnSphereContext context) { _context = context; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var sessions = await _context.Sessions.Include(s => s.Course).Include(s => s.Mentor).ToListAsync();

            var result = sessions.Select(s => new SessionDto
            {
                SessionId = s.SessionId,
                CourseId = s.CourseId,
                CourseTitle = s.Course.Title,
                MentorId = s.MentorId,
                MentorName = s.Mentor.Name,
                DateTime = s.DateTime,
                Status = s.Status
            });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Create([FromBody] SessionDto dto)
        {
            var session = new Session
            {
                CourseId = dto.CourseId,
                MentorId = dto.MentorId,
                DateTime = dto.DateTime,
                Status = dto.Status
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            dto.SessionId = session.SessionId;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> Update(int id, [FromBody] SessionDto dto)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();

            session.DateTime = dto.DateTime;
            session.Status = dto.Status;

            await _context.SaveChangesAsync();
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
//    public class SessionsController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        public SessionsController(LearnSphereContext context) { _context = context; }

//        // GET: api/sessions
//        [HttpGet]
//        [Authorize]
//        public async Task<IActionResult> GetAll()
//        {
//            var sessions = await _context.Sessions.Include(s => s.Course).Include(s => s.Mentor).ToListAsync();
//            return Ok(sessions);
//        }

//        // GET: api/sessions/{id}
//        [HttpGet("{id}")]
//        [Authorize]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var session = await _context.Sessions.Include(s => s.Course).Include(s => s.Mentor)
//                            .FirstOrDefaultAsync(s => s.SessionId == id);
//            if (session == null) return NotFound();
//            return Ok(session);
//        }

//        // POST: api/sessions
//        [HttpPost]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> Create([FromBody] Session session)
//        {
//            _context.Sessions.Add(session);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetById), new { id = session.SessionId }, session);
//        }

//        // PUT: api/sessions/{id}
//        [HttpPut("{id}")]
//        [Authorize(Roles = "Mentor")]
//        public async Task<IActionResult> Update(int id, [FromBody] Session updatedSession)
//        {
//            var session = await _context.Sessions.FindAsync(id);
//            if (session == null) return NotFound();

//            session.DateTime = updatedSession.DateTime;
//            session.Status = updatedSession.Status;

//            await _context.SaveChangesAsync();
//            return Ok(session);
//        }

//        // DELETE: api/sessions/{id}
//        [HttpDelete("{id}")]
//        [Authorize(Roles = "Mentor,Admin")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var session = await _context.Sessions.FindAsync(id);
//            if (session == null) return NotFound();

//            _context.Sessions.Remove(session);
//            await _context.SaveChangesAsync();
//            return Ok(new { message = "Session deleted successfully" });
//        }
//    }
//}

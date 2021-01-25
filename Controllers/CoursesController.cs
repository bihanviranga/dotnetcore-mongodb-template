using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetcore_mongodb_template.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore_mongodb_template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService service)
        {
            _courseService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(string id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Create(Course course)
        {
            await _courseService.CreateAsync(course);
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Course updatedCourse)
        {
            var queriedCourse = await _courseService.GetByIdAsync(id);
            if (queriedCourse == null)
            {
                return NotFound();
            }
            await _courseService.UpdateAsync(id, updatedCourse);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var queriedCourse = await _courseService.GetByIdAsync(id);
            if (queriedCourse == null)
            {
                return NotFound();
            }
            await _courseService.DeleteAsync(id);
            return NoContent();
        }
    }
}

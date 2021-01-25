using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetcore_mongodb_template.Models;
using dotnetcore_mongodb_template.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore_mongodb_template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public StudentsController(StudentService eService, CourseService cService)
        {
            _studentService = eService;
            _courseService = cService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            if (student.Courses.Count > 0)
            {
                var coursesList = new List<Course>();
                foreach (var courseId in student.Courses)
                {
                    var course = await _courseService.GetByIdAsync(courseId);
                    if (course != null)
                    {
                        coursesList.Add(course);
                    }
                }
                student.CourseList = coursesList;
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _studentService.CreateAsync(student);
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Student updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedStudent = await _studentService.GetByIdAsync(id);
            if (queriedStudent == null)
            {
                return NotFound();
            }
            await _studentService.UpdateAsync(id, updatedStudent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var queriedStudent = await _studentService.GetByIdAsync(id);
            if (queriedStudent == null)
            {
                return NotFound();
            }
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}

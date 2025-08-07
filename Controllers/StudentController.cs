using CoreWebApiCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly WebApiCrudContext contex;

        public StudentController(WebApiCrudContext contex)
        {
            this.contex = contex;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await contex.Students.ToListAsync();
            return Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await contex.Students.FindAsync(id);
            if (student == null) {
                return BadRequest();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task <ActionResult<Student>> CreateStudent(Student student)
        {
            await contex.Students.AddAsync(student);
            await contex.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingStudent = await contex.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound("Student not found");
            }

        
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Standard = student.Standard;
            

            try
            {
                await contex.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }

            return Ok(existingStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std =  await contex.Students.FindAsync(id);
            contex.Students.Remove(std);
            await contex.SaveChangesAsync();
            return Ok(std);
        }


    }
}

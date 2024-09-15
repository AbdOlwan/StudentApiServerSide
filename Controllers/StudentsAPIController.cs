using StudentsAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDataAccessLayer;

namespace StudentsAPI.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        // =================================================
        // Get All Students : 
        // =================================================
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = StudentApiBusinessLayer.Student.GetAllStudents();
            if(students == null || students.Count == 0)
            {
                return NotFound("No Students in Database");
            }
            return Ok(students);
        }
        // =================================================
        // Get Passed Students : 
        // =================================================

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var PassedStudents = StudentApiBusinessLayer.Student.GetPassedStudents();
            if(PassedStudents == null || PassedStudents.Count == 0)
            {
                return NotFound("No Passed Students");
            }
            return Ok(PassedStudents);
        }

        // =================================================
        // Get AVG Grades : 
        // =================================================
        [HttpGet("AVG", Name = "GetAVG")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAvgGrade()
        {
           

            var AvgGrads = StudentApiBusinessLayer.Student.GetAvgeGrades();
            return Ok(AvgGrads);
        }


        // =================================================
        // Get  Student By Id: 
        // =================================================
        [HttpGet("{Id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentById(int Id)
        {
            if(Id < 0)
            {
                return BadRequest($"No Student With Id {Id}");
            }

            var Student = StudentApiBusinessLayer.Student.Find(Id);

            if ( Student ==null)
            {
                return NotFound("No Students");
            }

            return Ok(Student.DTO);
        }

        // =================================================
        // Add New Student : 
        // =================================================
        [HttpPost(Name = "AddNew")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<StudentDTO> AddNewStudent(StudentDTO newstudent)
        {
            if(newstudent == null || string.IsNullOrEmpty(newstudent.Name) || newstudent.Age<0 || newstudent.Grade < 0)
            {
                return BadRequest("Not Accepted");
            }
              
            var Student = new StudentApiBusinessLayer.Student(new StudentDTO(  newstudent.Id, newstudent.Name,  newstudent.Age,  newstudent.Grade ));
            Student.Save(); 

            newstudent.Id =Student.Id;

            return CreatedAtRoute("GetStudentById", new { Id = newstudent.Id }, newstudent );



        }

        // =================================================
        // Delete Student By Id: 
        // =================================================
        //here we use HttpDelete method
        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            // var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            // StudentDataSimulation.StudentsList.Remove(student);

            if (StudentApiBusinessLayer.Student.DeleteStudent(id))

                return Ok($"Student with ID {id} has been deleted.");
            else
                return NotFound($"Student with ID {id} not found. no rows deleted!");
        }

        // =================================================
        // Update Student By Id: 
        // =================================================

        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO UpdateStudent)
        {
            if (id < 0 || UpdateStudent == null || string.IsNullOrEmpty(UpdateStudent.Name) || UpdateStudent.Age < 0 || UpdateStudent.Grade<0)
            {
                return BadRequest("Not Accepted");
            }

            var Student = StudentApiBusinessLayer.Student.Find(id);
            if (Student == null)
            {
                return NotFound($"Student With Id '{id}' Not Found!");
            }

            Student.Name = UpdateStudent.Name;
            Student.Age = UpdateStudent.Age;
            Student.Grade = UpdateStudent.Grade;
            if (Student.Save())
            {
                return Ok(Student.DTO);
            }
            else
            {
                return StatusCode(500, new { message = "Error Updateing Student" });
            }


           
        }
    }
}

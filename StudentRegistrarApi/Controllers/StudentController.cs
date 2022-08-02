using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrarApi.ApiModels.Request;
using StudentRegistrarApi.ApiModels.Update;
using StudentRegistrarApi.Data;
using StudentRegistrarApi.Models;
using StudentRegistrarApi.Service;
using System.Net;

namespace StudentRegistrarApi.Controllers
{
    [Route("studentApi/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        private IMapper _mapper;

        public StudentController(
            IStudentService studentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _studentService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _studentService.GetById(id);
            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetByStudentId(string studentId)
        {
            var user = _studentService.GetByStudentId(studentId);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            _studentService.Create(model);
            return Ok(new { message = "Student created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _studentService.Update(id, model);
            return Ok(new { message = "Student updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return Ok(new { message = "Student deleted" });
        }
        [HttpPost]
        public string UploadImage([FromForm] IFormFile file)
        {
            try
            {
                string FileName = file.FileName;
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", FileName);
                file.CopyTo(new FileStream(imagePath, FileMode.Create));
                return "File Uploaded Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

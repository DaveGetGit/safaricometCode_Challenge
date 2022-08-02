using AutoMapper;
using StudentRegistrarApi.ApiModels.Request;
using StudentRegistrarApi.ApiModels.Update;
using StudentRegistrarApi.Data;
using StudentRegistrarApi.Models;

namespace StudentRegistrarApi.Service
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        Student GetById(int id);
        Student GetByStudentId(string studentId);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
    public class StudentService : IStudentService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students;
        }

        public Student GetById(int id)
        {
            return getStudent(id);
        }  
        public Student GetByStudentId(string studentId)
        {
            return getStudentById(studentId);
        }

        public void Create(CreateRequest model)
        {
            // validate
            if (_context.Students.Any(x => x.StudentId == model.StudentId))
                throw new AppException("Student with the Student Id '" + model.StudentId + "' already exists");

            // map model to new user object
            var user = _mapper.Map<Student>(model);
            // save user
            _context.Students.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getStudent(id);

            // validate
            if (model.StudentId != user.StudentId && _context.Students.Any(x => x.StudentId == model.StudentId))
                throw new AppException("Student with the Student Id '" + model.StudentId + "' already exists");


            // copy model to user and save
            _mapper.Map(model, user);
            _context.Students.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getStudent(id);
            _context.Students.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private Student getStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            return student;
        }   
        private Student getStudentById(string studentId)
        {
            var user = _context.Students.Where(a=>a.StudentId.Equals(studentId)).FirstOrDefault();
            if (user == null) throw new KeyNotFoundException("Student not found");
            return user;
        }
    }
}

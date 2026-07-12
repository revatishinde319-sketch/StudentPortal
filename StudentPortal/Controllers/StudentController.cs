using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StudentModelView viewModel)
        {
            var students = new Student()
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            _dbContext.students.Add(students);
            _dbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var StudentList = _dbContext.students.ToList();
            return View(StudentList);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
             var student = _dbContext.students.Find(id);
             return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student viewmodel)
        {
            var students = _dbContext.students.Find(viewmodel.Id);
            if(students is not null)
            {
                students.Name = viewmodel.Name;
                students.Email = viewmodel.Email;
                students.Phone = viewmodel.Phone;
                _dbContext.students.Update(students);
                _dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }               
        }
        [HttpPost]
        public IActionResult Delete(Student viewmodel)
        {

            var student = _dbContext.students.FirstOrDefault(s => s.Id == viewmodel.Id);
            if (student != null)
            {
                _dbContext.students.Remove(student);
                _dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        
        }

        [HttpPost]
        public async Task<IActionResult> Delete1(Student viewmodel)
        {
            var student = await _dbContext.students.FirstOrDefaultAsync(s=>s.Id == viewmodel.Id);
            if (student != null)
            {
                _dbContext.students.Remove(student);
                _dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }

        }


    }
}

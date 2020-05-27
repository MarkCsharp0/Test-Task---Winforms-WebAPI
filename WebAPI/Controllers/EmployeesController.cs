using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        EmployeesContext db;
        // Конструктор контроллера
        public EmployeesController(EmployeesContext context)
        {
            db = context;
            // Заполним БД данными для демонстрации
            if (!db.Employees.Any())
            {
                db.Employees.Add(new Employee { FirstName = "Tom", LastName = "Tom", Address = "Tom", 
                    BirthDate = new DateTime(3,3,3), Patronymic="Tom", AboutMe="Good boy", Department="Nauka" });
                db.Employees.Add(new Employee
                {
                    FirstName = "Mike",
                    LastName = "Menshikh",
                    Address = "Mike",
                    BirthDate = new DateTime(2000, 9, 10),
                    Patronymic = "Mike",
                    AboutMe = "Good boy",
                    Department = "Nauka"
                });
                db.SaveChanges();
            }
        }


        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        // Метод контроллера, удаляющий сущность с заданным id
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            Employee employee = db.Employees.FirstOrDefault(x => x.Id == id);
            // Если сотрудник не найден, то вернем информацию об этом
            if (employee == null)
            {
                return NotFound();
            }
            // Удаляем найденного сотрудника из БД
            db.Employees.Remove(employee);
            // Сохраняем изменения
            await db.SaveChangesAsync();
            // Возврат HTTP Response со статусом ОК
            return Ok(employee);
        }


        [HttpGet]
        // GET api/employees

        // Возврат всех сотрудников из БД
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await db.Employees.ToListAsync();
        }

        // GET api/employees/{id}
        [HttpGet("{id}")]

        // Возврат сотрудника с заданным ID
        public async Task<ActionResult<Employee>> Get(int id)
        {
            Employee employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                return NotFound();
            return new ObjectResult(employee);
        }

        // POST api/employees
        [HttpPost]

        // Помещение нового сотрудника в БД
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            // Возвращаем клиенту ответ, что данные некорректны
            if (employee == null)
            {
                return BadRequest();
            }
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        // PUT api/employees/
        [HttpPut]
        // Замена старых данных сотрудника новыми данными
        public async Task<ActionResult<Employee>> Put(Employee employee)
        {
            // Возвращаем клиенту ответ, что данные некорректны
            if (employee == null)
            {
                return BadRequest();
            }
            // Возвращаем ответ со статусом NotFound в случае, если сотрудник с таким
            // ID не обнаружен
            if (!db.Employees.Any(x => x.Id == employee.Id))
            {
                return NotFound();
            }

            db.Update(employee);
            await db.SaveChangesAsync();
            return Ok(employee);
        }

        
    }
}

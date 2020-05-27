using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    // Класс для подключения к БД с помощью ORM
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeesContext(DbContextOptions<EmployeesContext> options)
            : base(options)
        {
            // Проверка на существование БД, если её нет, то Entity Framework создаст её
            Database.EnsureCreated();
        }

    }
}

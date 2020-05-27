using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    // Сущность сотрудника
    public class Employee
    {
        public int Id { get; set; }

        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

        public string Department { get; set; }
        public string AboutMe { get; set; }

        public override string ToString()
        {
            var str = "Имя: " + FirstName + "\n";
            str += "Фамилия: " + LastName + "\n";
            str += "Отчество: " + Patronymic + "\n";
            str += "Отдел: " + Department + "\n";
            str += "Обо мне: " + AboutMe + "\n";
            str += "Дата рождения: " + BirthDate.ToString() + "\n";
            str += "Адрес: " + Address + "\n";
            return str;
        }

    }
}

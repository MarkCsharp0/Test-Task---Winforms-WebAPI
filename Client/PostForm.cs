using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebAPI.Models;

namespace Client
{
    public partial class PostForm : Form
    {
        public Employee employee { get; set; }

        // 1 - создание, 2 - обновление
        public string typeOperation { get; set; }

        // Передаем в конструктор формы информацию о том, создаем мы сотрудника или обновляем
        public PostForm(string type = "1")
        {
            InitializeComponent();
            typeOperation = type;
        }

        // Проверка того, является ли поле пустым
        private int Validate(string data)
        {
            if (data.Length == 0)
            {
                return 1;
            }
            return 0;
        }

        // Проверка введенных данных на корректность и сохранение введенной
        // информации в классе
        private void button1_Click(object sender, EventArgs e)
        {
            var data = new List<string>();
            data.Add(richTextBox1.Text);
            data.Add(textBox1.Text);
            data.Add(textBox2.Text);
            data.Add(textBox3.Text);
            data.Add(textBox4.Text);
            data.Add(textBox5.Text);       
            int cntErrors = 0;
            foreach (var value in data)
            {
                cntErrors += Validate(value);
            }
            if (cntErrors > 0)
            {
                MessageBox.Show("Заполните пустые поля в форме");
                
                return;
            }

            var emp = new Employee
            {
                AboutMe = data[0],
                Address = data[4],
                BirthDate = dateTimePicker1.Value
            ,
                Department = data[5],
                FirstName = data[1],
                LastName = data[2],
                Patronymic = data[3]
            };
            // Если у нас операция обновления, то ID является необходимым полем
            if (typeOperation == "2")
            {
                if (textBox6.TextLength == 0)
                {
                    MessageBox.Show("Введите ID");
                    return;
                }
                emp.Id = int.Parse(textBox6.Text);
            }

            employee = emp;
            // Закрываем форму

            this.Close();

        }
    }
}

using System;
using System.Windows.Forms;


namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Создание нового сотрудника
        private async void button5_Click(object sender, EventArgs e)
        {
            var form = new PostForm();
            // Если данные прошли валидацию и была нажата кнопка отправить
            form.ShowDialog();
            if (form.employee != null)
            {
                printMessageAboutLoading();
                var res = await RestHelper.Post(form.employee);
                MessageBox.Show("Статус операции добавления сотрудника " + "-" + res, "POST");
            }


        }

        // Получить данные всех сотрудников из базы данных
        private async void button2_Click(object sender, EventArgs e)
        {
            printMessageAboutLoading();
            // отправка запроса 
            var res = await RestHelper.GETALL();
            if (res == null)
            {
                MessageBox.Show("InternalServerError", "GET ALL");
            }
            else
            {
                // Используем полученный массив в качестве источника данных
                MessageBox.Show("Загружены все данные с сервера", "GET ALL");
                dataGridView1.DataSource = res;
                label3.Text = "Amount of elements: " + res.Count;
            }
            
          
        }

        // Получение данных сотрудника с выбранным id
        private async void button3_Click(object sender, EventArgs e)
        {

           
            // Если поле с ID на форме пустое, то отменяем операцию, перед этим выведем сообщение
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Введите ID выбираемого элемента", "GET");
                return;
            }
            printMessageAboutLoading();
            // Осуществляем запрос с помощью метода Get
            var res = await RestHelper.Get(textBox2.Text);
            // Если в ответе не было данных сотрудника, то выведем информацию
            // что он не найден
            if (res == null)
            {
                MessageBox.Show("Статус операции добавления сотрудника с id " + textBox2.Text + "-" + "NotFound", "GET");
            }
            // Иначе покажем информацию о сотруднике
            else
            {
                MessageBox.Show(res.ToString(), "GET");
            }
            textBox2.Text = "";
        }


        private void printMessageAboutLoading()
        {
            MessageBox.Show("Ваш запрос отправлен на сервер, ждите ответа");
        }

        // Удаление данных сотрудника с выбранным id
        private async void button1_Click(object sender, EventArgs e)
        {
            
            // Если поле с ID на форме пустое, то отменяем операцию, перед этим выведем сообщение
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Введите ID удаляемого элемента", "DELETE");
                return;
            }
            printMessageAboutLoading();
            // Выполним операцию и покажем код ответа
            var res = await RestHelper.Delete(textBox1.Text);
            MessageBox.Show("Статус операции удаления сотрудника с id " + textBox1.Text + "-" + res, "DELETE");
            textBox1.Text = "";
        }

        // Обновление
        private async void button4_Click(object sender, EventArgs e)
        {
           
            
            var form = new PostForm("2");
            // Если данные прошли валидацию и была нажата кнопка отправить

            // Делаем запрос на обновление и показываем код ответа от сервера
            form.ShowDialog();
            if (form.employee != null)
            {
                printMessageAboutLoading();
                var res = await RestHelper.Update(form.employee);
                MessageBox.Show("Статус операции обновления данных сотрудника с id " + form.employee.Id + "-" + res, "UPDATE");
            }
            
            }
          
        }
    }

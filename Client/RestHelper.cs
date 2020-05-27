using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebAPI.Models;

namespace Client
{
    public static class RestHelper
    {
        // Неизменная часть URL, по которому будут отправлять запросы
        private static readonly string baseURL = "http://localhost:5000/api/"; 
        public static async Task<List<Employee>> GETALL()
        {

            try
            {
                // Создаем сущность HttpClient для возможности отправить запрос
                using (HttpClient client = new HttpClient())
                {
                    // Создаем сущность HttpResponseMessage, в котором будет находится HTTP Request
                    // Также делаем вызов на получение данных с помощью метода GetAsync
                    using (HttpResponseMessage res = await client.GetAsync(baseURL + "employees"))
                    {
                        
                        // Создаем сущность класса HttpContent, в который помещаем сущность ответа
                        using (HttpContent content = res.Content)
                        {
                            // Получаем необходимые данные в виде List<Employee>
                            var data = await content.ReadAsAsync<List<Employee>>();
                            return data;
                            
                        }
                    }

                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }



        
        public static async Task<Employee> Get(string id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage res = await client.GetAsync(baseURL + "employees/" + id))
                    {
                     
                        using (HttpContent content = res.Content)
                        {

                            // Если объект не найден, то вернем null
                            if (res.StatusCode.ToString() == "NotFound")
                            {
                                return null;
                            }
                            // Преобразуем полученные данные в Employee
                            var data = await content.ReadAsAsync<Employee>();
                            return data;
                        }
                    }

                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }


        public static async Task<string> Post(Employee employee)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Делаем POST запрос, посылая данные о сотруднике в формате JSON
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(baseURL + "employees", employee))
                    {
                       
                        using (HttpContent content = res.Content)
                        {
                            // Возвращаем код ответа
                            return res.StatusCode.ToString();
                        }
                    }

                }
            } catch (HttpRequestException)
            {
                return HttpStatusCode.InternalServerError.ToString();
            }
        }


        public static async Task<string> Update(Employee employee)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Делаем PUT запрос, посылая данные о сотруднике в формате JSON
                    using (HttpResponseMessage res = await client.PutAsJsonAsync(baseURL + "employees", employee))
                    {
                        using (HttpContent content = res.Content)
                        {
                            return res.StatusCode.ToString();
                        }
                    }
                }
            } catch(HttpRequestException)
            {
                return HttpStatusCode.InternalServerError.ToString();
            }
        }

        public static async Task<string> Delete(string id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Отправляем запрос на удаление элемента с заданным ID
                    using (HttpResponseMessage res = await client.DeleteAsync(baseURL + "employees/" + id))
                    {

                        using (HttpContent content = res.Content)
                        {

                            // Возвращаем код ответа
                            return res.StatusCode.ToString();
                        }
                    }

                }
            } catch (HttpRequestException)
            {
                return HttpStatusCode.InternalServerError.ToString();
            }
            
        }
    }
}

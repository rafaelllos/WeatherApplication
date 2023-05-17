using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Функция выполняется при нажатии на кнопку button1
        private void button1_Click(object sender, EventArgs e)
        {
            // Переменная с типом данных string, которая принимает значение textBox1,
            // куда пользователь вводит город
            string userInput = textBox1.Text; 

            // Переменная api, хранящая значение GET-запроса к сервису weatherapi
            string api = "http://api.weatherapi.com/v1/current.json?key=105d39cfc3424073b16103714231605&q=" + userInput;

            // Объект httpWebRequest создан по образу класса WebRequest,
            // который обрабатывает запрос, описанный в переменной api
            WebRequest httpWebRequest = WebRequest.Create(api);

            // Объект httpWebResponse создан по образу класса WebResponse,
            // который получает данные по запросу
            WebResponse httpWebResponse = httpWebRequest.GetResponse();

            string response; // переменная для хранения данных по запросу

            // Чтение данных с запроса и запись их в переменную response
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            // Переменная weatherResponse, которая конвертирует данные из JSON-формата в объект
            dynamic weatherResponse = JsonConvert.DeserializeObject(response);

            // Заполняет новыми данными поля lables
            label1.Text = "Город: " + weatherResponse.location.name;
            label2.Text = "Температура: " + weatherResponse.current.temp_c + "°C";
            label3.Text = "Дата и время: " + weatherResponse.location.localtime;
            label4.Text = "Скорость ветра: " + Math.Round(Convert.ToDecimal(weatherResponse.current.wind_kph / 3.6)) + " м/c";
        }
    }
}

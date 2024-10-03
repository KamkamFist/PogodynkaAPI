using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PogodynkaAPI
{
    public partial class Form1 : Form
    {
        public class Current
        {
            public string time { get; set; }
            public double temperature_2m { get; set; }
            public int relative_humidity_2m { get; set; }
            public double rain { get; set; }
            public double showers { get; set; }
        }

        public class Root
        {
            public Current current { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            Xdd();
        }

        //private async void Form1_Load(object sender, EventArgs e)
        //{
        //   await GetWeatherDataAsync();
        //}

        private async Task GetWeatherDataAsync()
        {
            using HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.open-meteo.com/") };

          
                var response = await client.GetAsync("/v1/forecast?latitude=54.52&longitude=18.41&current=temperature_2m,relative_humidity_2m,rain,showers");
                response.EnsureSuccessStatusCode();

                var root = await response.Content.ReadFromJsonAsync<Root>();

                if (root?.current != null)
                {
                    label1.Text = $"Temperatura: {root.current.temperature_2m} °C\n" +
                                  $"Wilgotnoœæ: {root.current.relative_humidity_2m} %\n" +
                                  $"Opady: {root.current.rain} mm";

                    label2.Text = $"Ostatnie przeladowanie: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                }
                //else
                //{
                //   label1.Text = "Nie uda³o siê pobraæ danych pogodowych.";
                //}
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await GetWeatherDataAsync();
        }

        //int czas = 0;
        private void Load(object sender, EventArgs e)
        {
            timer1.Tick += async (s, e) =>
            {
                await GetWeatherDataAsync();
            };
           //czas++;
            //label2.Text = czas.ToString();
        }

        async void Xdd()
        {
            await GetWeatherDataAsync();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

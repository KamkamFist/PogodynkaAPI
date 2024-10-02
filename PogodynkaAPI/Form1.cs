using System.Net.Http.Json;
using static PogodynkaAPI.Form1;

namespace PogodynkaAPI
{
    public partial class Form1 : Form
    {
        public class Current
        {
            public string time { get; set; }
            public int interval { get; set; }
            public double temperature_2m { get; set; }
            public int relative_humidity_2m { get; set; }
            public int rain { get; set; }
            public int showers { get; set; }
        }

        public class CurrentUnits
        {
            public string time { get; set; }
            public string interval { get; set; }
            public string temperature_2m { get; set; }
            public string relative_humidity_2m { get; set; }
            public string rain { get; set; }
            public string showers { get; set; }
        }

        public class Root
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double generationtime_ms { get; set; }
            public int utc_offset_seconds { get; set; }
            public string timezone { get; set; }
            public string timezone_abbreviation { get; set; }
            public int elevation { get; set; }
            public CurrentUnits current_units { get; set; }
            public Current current { get; set; }
        }





        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.open-meteo.com/") };

            var response = await client.GetAsync("/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,relative_humidity_2m,rain,showers");
            response.EnsureSuccessStatusCode();

            var root = await response.Content.ReadFromJsonAsync<Root>();

            if (root?.current != null)
            {
                label1.Text = $"Temperatura: {root.current.temperature_2m} °C\n" +
                              $"Wilgotnoœæ: {root.current.relative_humidity_2m} %\n" +
                              $"Opady: {root.current.rain} mm";
            }
            else
            {
                label1.Text = "Brak danych.";
            }
        }
    }
}

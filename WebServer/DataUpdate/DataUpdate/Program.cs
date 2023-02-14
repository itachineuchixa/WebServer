using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataUpdate
{
    internal class Program
    {
        static async Task<string> Get_info()
        {
            HttpClient httpClient = new HttpClient();
            // получаем ответ
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/get_city_info");
            return await response.Content.ReadAsStringAsync();
        }

        static async Task<string> Get_city(string city_id)
        {
            HttpClient httpClient = new HttpClient();
            // получаем ответ
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/get_city/" + city_id);
            return await response.Content.ReadAsStringAsync();
        }

        static async Task<string> Update(string city_id)
        {
            HttpClient httpClient = new HttpClient();
            // получаем ответ
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/update_city_info/" + city_id);
            return await response.Content.ReadAsStringAsync();
        }
        static async void Main(string[] args)
        {
            Console.WriteLine("НЕгорыыыыы");
            while (1 == 1)
            {
                Console.WriteLine("1111111111111111111");
                string dat = await Get_info();
                List<CityInfo> infos = JsonConvert.DeserializeObject<List<CityInfo>>(dat);
                Console.WriteLine(dat);
                for (int i = infos.Count(); i < infos.Count(); i++)
                {
                    Console.WriteLine(infos[i].CityId.ToString());
                    string c_city = await Update(infos[i].CityId.ToString());
                    Console.WriteLine(c_city);

                }
                /*System.Threading.Thread.Sleep(60000);*/
            }
        }
        public partial class CityInfo
        {
            public long Id { get; set; }

            public double[] MaxWeather { get; set; } = null!;

            public double[] MinWeather { get; set; } = null!;

            public double[] PeriodWeather { get; set; } = null!;

            public double CurrentWeather { get; set; }

            public long CityId { get; set; }

            public virtual string City { get; set; } = null!;
        }
    }
}

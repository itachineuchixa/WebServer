using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.ObjectPool;
using System.Collections;
using System.Net;
using System.Text;
using WebServer;
using Newtonsoft.Json;
using Npgsql;

namespace WebServer
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double elevation { get; set; }
        public string feature_code { get; set; }
        public string country_code { get; set; }
        public int admin1_id { get; set; }
        public string timezone { get; set; }
        public int country_id { get; set; }
        public string country { get; set; }
        public string admin1 { get; set; }
        public int? population { get; set; }
    }

    public class Root2
    {
        public List<Result> results { get; set; }
        public double generationtime_ms { get; set; }
    }
    public class CurrentWeather
    {
        public double temperature { get; set; }
        public double windspeed { get; set; }
        public double winddirection { get; set; }
        public int weathercode { get; set; }
        public string time { get; set; }
    }

    public class Daily
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m_max { get; set; }
        public List<double> temperature_2m_min { get; set; }
    }

    public class DailyUnits
    {
        public string time { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
    }

    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<int> weathercode { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string weathercode { get; set; }
    }

    public class Root
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public CurrentWeather current_weather { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
        public DailyUnits daily_units { get; set; }
        public Daily daily { get; set; }
    }


    public class Utilities
    {

        bool chkDBExists(string connectionStr, string dbname)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                using (NpgsqlCommand command = new NpgsqlCommand
                    ($"SELECT DATNAME FROM pg_catalog.pg_database WHERE DATNAME = '{dbname}'", conn))
                {
                    try
                    {
                        conn.Open();
                        var i = command.ExecuteScalar();
                        conn.Close();
                        if (i.ToString().Equals(dbname)) //always 'true' (if it exists) or 'null' (if it doesn't)
                            return true;
                        else return false;
                    }
                    catch (Exception e) { return false; }
                }
            }
        }
        public void create_db()
        {
            const string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=1234;";
            const string connStr2 = "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather;";


            var m_conn = new NpgsqlConnection(connStr); // db connction
            var m_conn2 = new NpgsqlConnection(connStr2); // table connection

            // creating a database in Postgresql
            var m_createdb_cmd = new NpgsqlCommand("CREATE DATABASE weather;", m_conn);

            string script = "--\r\n-- PostgreSQL database dump\r\n--\r\n\r\n-- Dumped from database version 15.1\r\n-- Dumped by pg_dump version 15.1\r\n\r\nSET statement_timeout = 0;\r\nSET lock_timeout = 0;\r\nSET idle_in_transaction_session_timeout = 0;\r\nSET client_encoding = 'UTF8';\r\nSET standard_conforming_strings = on;\r\nSELECT pg_catalog.set_config('search_path', '', false);\r\nSET check_function_bodies = false;\r\nSET xmloption = content;\r\nSET client_min_messages = warning;\r\nSET row_security = off;\r\n\r\nSET default_tablespace = '';\r\n\r\nSET default_table_access_method = heap;\r\n\r\n--\r\n-- Name: cities; Type: TABLE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE TABLE public.cities (\r\n    id bigint NOT NULL,\r\n    city character varying(89) NOT NULL,\r\n    longitude double precision,\r\n    latitude double precision\r\n);\r\n\r\n\r\nALTER TABLE public.cities OWNER TO postgres;\r\n\r\n--\r\n-- Name: cities_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE SEQUENCE public.cities_id_seq\r\n    START WITH 1\r\n    INCREMENT BY 1\r\n    NO MINVALUE\r\n    NO MAXVALUE\r\n    CACHE 1;\r\n\r\n\r\nALTER TABLE public.cities_id_seq OWNER TO postgres;\r\n\r\n--\r\n-- Name: cities_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER SEQUENCE public.cities_id_seq OWNED BY public.cities.id;\r\n\r\n\r\n--\r\n-- Name: city_info; Type: TABLE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE TABLE public.city_info (\r\n    id bigint NOT NULL,\r\n    max_weather double precision[] NOT NULL,\r\n    min_weather double precision[] NOT NULL,\r\n    period_weather double precision[] NOT NULL,\r\n    current_weather double precision NOT NULL,\r\n    city_id bigint NOT NULL\r\n);\r\n\r\n\r\nALTER TABLE public.city_info OWNER TO postgres;\r\n\r\n--\r\n-- Name: city_info_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE SEQUENCE public.city_info_id_seq\r\n    START WITH 1\r\n    INCREMENT BY 1\r\n    NO MINVALUE\r\n    NO MAXVALUE\r\n    CACHE 1;\r\n\r\n\r\nALTER TABLE public.city_info_id_seq OWNER TO postgres;\r\n\r\n--\r\n-- Name: city_info_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER SEQUENCE public.city_info_id_seq OWNED BY public.city_info.id;\r\n\r\n\r\n--\r\n-- Name: user_info; Type: TABLE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE TABLE public.user_info (\r\n    id bigint NOT NULL,\r\n    user_id bigint NOT NULL,\r\n    city_id bigint NOT NULL\r\n);\r\n\r\n\r\nALTER TABLE public.user_info OWNER TO postgres;\r\n\r\n--\r\n-- Name: user_info_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE SEQUENCE public.user_info_id_seq\r\n    START WITH 1\r\n    INCREMENT BY 1\r\n    NO MINVALUE\r\n    NO MAXVALUE\r\n    CACHE 1;\r\n\r\n\r\nALTER TABLE public.user_info_id_seq OWNER TO postgres;\r\n\r\n--\r\n-- Name: user_info_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER SEQUENCE public.user_info_id_seq OWNED BY public.user_info.id;\r\n\r\n\r\n--\r\n-- Name: users; Type: TABLE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE TABLE public.users (\r\n    id bigint NOT NULL,\r\n    username character varying(50) NOT NULL,\r\n    password character varying(50) NOT NULL\r\n);\r\n\r\n\r\nALTER TABLE public.users OWNER TO postgres;\r\n\r\n--\r\n-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres\r\n--\r\n\r\nCREATE SEQUENCE public.users_id_seq\r\n    START WITH 1\r\n    INCREMENT BY 1\r\n    NO MINVALUE\r\n    NO MAXVALUE\r\n    CACHE 1;\r\n\r\n\r\nALTER TABLE public.users_id_seq OWNER TO postgres;\r\n\r\n--\r\n-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;\r\n\r\n\r\n--\r\n-- Name: cities id; Type: DEFAULT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.cities ALTER COLUMN id SET DEFAULT nextval('public.cities_id_seq'::regclass);\r\n\r\n\r\n--\r\n-- Name: city_info id; Type: DEFAULT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.city_info ALTER COLUMN id SET DEFAULT nextval('public.city_info_id_seq'::regclass);\r\n\r\n\r\n--\r\n-- Name: user_info id; Type: DEFAULT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.user_info ALTER COLUMN id SET DEFAULT nextval('public.user_info_id_seq'::regclass);\r\n\r\n\r\n--\r\n-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);\r\n\r\n\r\n--\r\n-- Data for Name: cities; Type: TABLE DATA; Schema: public; Owner: postgres\r\n--\r\n\r\n\r\n\r\n--\r\n-- Data for Name: city_info; Type: TABLE DATA; Schema: public; Owner: postgres\r\n--\r\n\r\n\r\n\r\n--\r\n-- Data for Name: user_info; Type: TABLE DATA; Schema: public; Owner: postgres\r\n--\r\n\r\n\r\n\r\n--\r\n-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres\r\n--\r\n\r\n\r\n\r\n--\r\n-- Name: cities_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres\r\n--\r\n\r\nSELECT pg_catalog.setval('public.cities_id_seq', 14, true);\r\n\r\n\r\n--\r\n-- Name: city_info_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres\r\n--\r\n\r\nSELECT pg_catalog.setval('public.city_info_id_seq', 7, true);\r\n\r\n\r\n--\r\n-- Name: user_info_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres\r\n--\r\n\r\nSELECT pg_catalog.setval('public.user_info_id_seq', 19, true);\r\n\r\n\r\n--\r\n-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres\r\n--\r\n\r\nSELECT pg_catalog.setval('public.users_id_seq', 8, true);\r\n\r\n\r\n--\r\n-- Name: cities cities_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.cities\r\n    ADD CONSTRAINT cities_pkey PRIMARY KEY (id);\r\n\r\n\r\n--\r\n-- Name: city_info city_info_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.city_info\r\n    ADD CONSTRAINT city_info_pkey PRIMARY KEY (id);\r\n\r\n\r\n--\r\n-- Name: user_info user_info_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.user_info\r\n    ADD CONSTRAINT user_info_pkey PRIMARY KEY (id);\r\n\r\n\r\n--\r\n-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.users\r\n    ADD CONSTRAINT users_pkey PRIMARY KEY (id);\r\n\r\n\r\n--\r\n-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.users\r\n    ADD CONSTRAINT users_username_key UNIQUE (username);\r\n\r\n\r\n--\r\n-- Name: city_info city_info_city_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.city_info\r\n    ADD CONSTRAINT city_info_city_id_fkey FOREIGN KEY (city_id) REFERENCES public.cities(id);\r\n\r\n\r\n--\r\n-- Name: user_info user_info_city_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.user_info\r\n    ADD CONSTRAINT user_info_city_id_fkey FOREIGN KEY (city_id) REFERENCES public.cities(id);\r\n\r\n\r\n--\r\n-- Name: user_info user_info_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres\r\n--\r\n\r\nALTER TABLE ONLY public.user_info\r\n    ADD CONSTRAINT user_info_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);\r\n\r\n\r\n--\r\n-- PostgreSQL database dump complete\r\n--\r\n\r\n";
            var m_createtbl_cmd = new NpgsqlCommand(script);

            m_createtbl_cmd.Connection = m_conn2;

            // 3.. Make connection and create

            // open connection to create DB
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();

            // open connection to create table
            m_conn2.Open();
            m_createtbl_cmd.ExecuteNonQuery();
            m_conn2.Close();
        }



                static async Task<string> Main()
        {
            HttpClient httpClient = new HttpClient();
            var dt = DateTime.Now;
            DateTime month = dt.AddMonths(-1);
            // получаем ответ
            using HttpResponseMessage response = await httpClient.GetAsync("https://api.open-meteo.com/v1/forecast?latitude=55.71&longitude=38.95&hourly=temperature_2m,weathercode&daily=temperature_2m_max,temperature_2m_min&current_weather=true&timezone=auto&start_date=" + month.ToString("yyyy-MM-dd") + "&end_date=" + DateTime.Now.ToString("yyyy-MM-dd"));
            // получаем ответ
            return await response.Content.ReadAsStringAsync();
        }


        static async Task<string> get_data(string latitude, string longtitude)
        {
            HttpClient httpClient = new HttpClient();
            var dt = DateTime.Now;
            DateTime month = dt.AddMonths(-1);
            // получаем ответ
            using HttpResponseMessage response = await httpClient.GetAsync("https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longtitude + "&hourly=temperature_2m,weathercode&daily=temperature_2m_max,temperature_2m_min&current_weather=true&timezone=auto&start_date=" + month.ToString("yyyy-MM-dd") + "&end_date=" + DateTime.Now.ToString("yyyy-MM-dd"));
            // получаем ответ
            return await response.Content.ReadAsStringAsync();
        }


        static async Task<string> GetGEO(string city)
        {
            HttpClient httpClient = new HttpClient();
            // получаем ответ
            using HttpResponseMessage response = await httpClient.GetAsync("https://geocoding-api.open-meteo.com/v1/search?name=" + city + "&language=ru");
            // получаем ответ
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Mine()

        {
            const string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=1234;";
            if (chkDBExists(connStr, "weather") != true)
            {
                create_db();
            }
            return await Main();
        }

        public double get_current(string city_id)
        {
            var res = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
            return res[0].CurrentWeather;
        }

        public async Task<Dictionary<int, string[]>> get_geo(string geo)
        {
            string data = await GetGEO(geo);
            Root2 datas = JsonConvert.DeserializeObject<Root2>(data);
            string[] cities = new string[datas.results.Count()];
            if (datas.results.Count() == 1)
            
            {
                var results = new Dictionary<int, string[]>();
                string[] coordinates = { datas.results[0].name, datas.results[0].longitude.ToString(), datas.results[0].latitude.ToString() };
                results.Add(1, coordinates);
                return results;
            }

            for (int i = 0; i < cities.Length; i++)
            {
                cities[i] = datas.results[i].name;
            }
            string[] longtitude = new string[datas.results.Count()];
            for (int i = 0; i < longtitude.Length; i++)
            {
                longtitude[i] = datas.results[i].longitude.ToString();
            }
            string[] latitude = new string[datas.results.Count()];
            for (int i = 0; i < latitude.Length; i++)
            {
                latitude[i] = datas.results[i].latitude.ToString();
            }
            var result = new Dictionary<int, string[]>();
            for (int i = 1; i < cities.Length; i++)
            {
                string[] coordinates = { cities[i], longtitude[i], latitude[i] };
                result.Add(i, coordinates);
            }
            return result;
        }

        public string Login(string username, string password)
        {
            var res = WeatherContext.GetContext().Users.Where(x => x.Password == password && x.Username == username).ToList();
            if (res.Count == 1) return "Success";
            else return "Failed";
        }




        public List<City> get_cities(string user_id)
        {
            var ss = WeatherContext.GetContext().UserInfos.Where(x => x.UserId == int.Parse(user_id)).ToList();
            long[] cc = new long[ss.Count()];
            for (int i = 0; i < ss.Count(); i++)
            {
                cc[i] = ss[i].CityId;
            }
            var stupid = WeatherContext.GetContext().Cities.Where(x => cc.Contains(x.Id)).ToList();
            List<City> cits = new List<City>();
            for (int i = 0; i < stupid.Count(); i++)
            {
                City cur = new City();
                cur.City1 = stupid[i].City1;
                cur.Longitude = stupid[i].Longitude;
                cur.Latitude = stupid[i].Latitude;
                cur.Id = stupid[i].Id;
                cits.Add(cur);
            } return cits;
        }

        public string get_user_id(string username, string password)
        {
            var res = WeatherContext.GetContext().Users.Where(x => x.Password == password && x.Username == username).ToList();
            if (res.Count == 1) return res[0].Id.ToString();
            else return "Failed";
        }

        public async Task<string> add_city(string city, string longtitude, string latitude, string user_id)
        {
            var res = WeatherContext.GetContext().Cities.Where(x => x.City1 == city && x.Longitude == double.Parse(longtitude.Replace(".", ",")) && x.Latitude == double.Parse(latitude.Replace(".", ","))).ToList();
            if (res.Count == 1)
            {
                UserInfo info = new UserInfo();
                info.UserId = int.Parse(user_id);
                info.CityId = res[0].Id;
                WeatherContext.GetContext().UserInfos.Add(info);
                WeatherContext.GetContext().SaveChanges();
                return "City already exist";
            }
            else {
                City area = new City();
                area.City1 = city;
                area.Longitude = double.Parse(longtitude.Replace(".", ","));
                area.Latitude = double.Parse(latitude.Replace(".", ","));
                WeatherContext.GetContext().Cities.Add(area);
                WeatherContext.GetContext().SaveChanges();
                var current_area = WeatherContext.GetContext().Cities.Where(x => x.City1 == city && x.Longitude == double.Parse(longtitude.Replace(".", ",")) && x.Latitude == double.Parse(latitude.Replace(".", ","))).ToList();
                CityInfo city_infos = new CityInfo();
                var data = await get_data(current_area[0].Latitude.ToString().Replace(",", "."), current_area[0].Longitude.ToString().Replace(",", "."));
                UserInfo info = new UserInfo();
                Root datas =  JsonConvert.DeserializeObject<Root>(data);
                List<double> max = datas.daily.temperature_2m_max; /*data.Split("\"temperature_2m_max\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
                /*                double[] max = new double[max_weath.Length];*/
                List<double> min = datas.daily.temperature_2m_min; /*data.Split("\"temperature_2m_min\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
                /*                double[] min = new double[min_weath.Length];*/
                double current_weath = datas.current_weather.temperature;
                /*double current_weath = double.Parse(data.Split("\"current_weather\":{\"temperature\":")[1].Split(",")[0].Replace(".", ","));*/
                List<double> period = datas.hourly.temperature_2m; /*data.Split("\"temperature_2m\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
                /*double[] period = new double[per.Length];*/
                city_infos.CurrentWeather = current_weath;
                city_infos.MaxWeather = max.ToArray();
                city_infos.MinWeather = min.ToArray();
                city_infos.PeriodWeather = period.ToArray();
                city_infos.CityId = current_area[0].Id;
                info.UserId = int.Parse(user_id);
                info.CityId = current_area[0].Id;
                WeatherContext.GetContext().CityInfos.Add(city_infos);
                WeatherContext.GetContext().SaveChanges();
                WeatherContext.GetContext().UserInfos.Add(info);
                WeatherContext.GetContext().SaveChanges();

                return "Success"; }
        }

        public string register(string username, string password)
        {
            var res = WeatherContext.GetContext().Users.Where(x => x.Username == username).ToList();
            try
            {
                if (res.Count == 0)
                { User user = new User();
                    user.Username = username;
                    user.Password = password;
                    WeatherContext.GetContext().Users.Add(user);
                    WeatherContext.GetContext().SaveChanges();
                    return "Success";
                }
                else return "Username is already exist";
            }
            catch
            {
                return "Error";
            }
        }

        public string delete(string user_id, string city_id)
        {
            var res = WeatherContext.GetContext().UserInfos.Where(x => x.UserId == int.Parse(user_id) && x.CityId == int.Parse(city_id)).ToList();
            WeatherContext.GetContext().UserInfos.Remove(res[0]);
            WeatherContext.GetContext().SaveChanges();
            return "Success";
        }


        public double[] get_maximum(string city_id)
        {
            var res = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
            return res[0].MaxWeather;
        }

        public double[] get_minimum(string city_id)
        {
            var res = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
            return res[0].MinWeather;
        }

        public double[] get_graph(string city_id)
        {
            var res = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
            return res[0].PeriodWeather;
            //WeatherContext.GetContext().User.Add(_currentUser);
        }

        public List<UserInfo> get_info()
{
    var res = WeatherContext.GetContext().UserInfos.ToList();
    return res;
    //WeatherContext.GetContext().User.Add(_currentUser);
    }

        public List<int> get_city_info()
{
    var ss = WeatherContext.GetContext().CityInfos.ToList();
        List<int> cits = new List<int>();
    for (int i = 0; i<ss.Count(); i++)
    {
        cits.Add(Convert.ToInt32(ss[i].CityId));
    }

    return cits;
/*    var res = WeatherContext.GetContext().CityInfos.ToList();
    return res;*/
    //WeatherContext.GetContext().User.Add(_currentUser);
}

        public List<City> get_city(string city_id)
{
    var res = WeatherContext.GetContext().Cities.Where(x => x.Id == int.Parse(city_id)).ToList();
    return res;
    //WeatherContext.GetContext().User.Add(_currentUser);
}

        public async Task<string> update_city_info(string city_id)
{
    List<CityInfo> cc = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
        List<City> current_area = WeatherContext.GetContext().Cities.Where(x => x.Id == int.Parse(city_id)).ToList();
            var data = await get_data(current_area[0].Latitude.ToString().Replace(",", "."), current_area[0].Longitude.ToString().Replace(",", "."));
            Root datas = JsonConvert.DeserializeObject<Root>(data);
            List<double> max = datas.daily.temperature_2m_max; /*data.Split("\"temperature_2m_max\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
            /*                double[] max = new double[max_weath.Length];*/
            List<double> min = datas.daily.temperature_2m_min; /*data.Split("\"temperature_2m_min\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
            /*                double[] min = new double[min_weath.Length];*/
            double current_weath = datas.current_weather.temperature;
            /*double current_weath = double.Parse(data.Split("\"current_weather\":{\"temperature\":")[1].Split(",")[0].Replace(".", ","));*/
            List<double> period = datas.hourly.temperature_2m; /*data.Split("\"temperature_2m\":[")[1].Split("]")[0].Replace(",", ";").Replace(".", ",").Split(";");*/
            /*double[] period = new double[per.Length];*/
            cc[0].CurrentWeather = current_weath;
            cc[0].MaxWeather = max.ToArray();
            cc[0].MinWeather = min.ToArray();
            cc[0].PeriodWeather = period.ToArray();
            WeatherContext.GetContext().SaveChanges();
            return "Success";
                //WeatherContext.GetContext().User.Add(_currentUser);
            }






















    }
}
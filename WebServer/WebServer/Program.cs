using System;
using System.Net.Http;
using System.Xml.Linq;
using WebServer;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

Utilities utility = new Utilities();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async () =>
{
    return await utility.Mine();
});
app.MapGet("/get_current/{city_id}", async (string city_id) =>
{
    var res = WeatherContext.GetContext().CityInfos.Where(x => x.CityId == int.Parse(city_id)).ToList();
    return utility.get_current(city_id);
});

app.MapGet("/get_geo/{geo}", async (string geo) =>
{
    return await utility.get_geo(geo);
});

app.MapGet("/login/{username}/{password}", async (string username, string password) =>
{
    return utility.Login(username, password);
});

app.MapGet("/get_cities/{user_id}", async (string user_id) =>
{
    return utility.get_cities(user_id);
});


app.MapGet("/get_user_id/{username}/{password}", async (string username, string password) =>
{
    return utility.get_user_id(username, password);
});

app.MapGet("/add_city/{city}/{longtitude}/{latitude}/{user_id}", async (string city, string longtitude, string latitude, string user_id) =>
{
    return await utility.add_city(city, longtitude, latitude, user_id);
});


app.MapGet("/register/{username}/{password}", async (string username, string password) =>
{
    return utility.register(username, password);
});

app.MapGet("/delete/{user_id}/{city_id}", async (string user_id, string city_id) =>
{
    return utility.delete(user_id, city_id);
});


app.MapGet("/get_maximum/{city_id}", async (string city_id) =>
{
    return utility.get_maximum(city_id);
});

app.MapGet("/get_minimum/{city_id}", async (string city_id) =>
{
    return utility.get_minimum(city_id);
});


app.MapGet("/get_graph/{city_id}", async (string city_id) =>
{
    return utility.get_graph(city_id);
});

app.MapGet("/get_info/", async () =>
{
    return utility.get_info();
});

app.MapGet("/get_city_info/", async () =>
{
    return utility.get_city_info();
});

app.MapGet("/get_city/{city_id}", async (string city_id) =>
{
    return utility.get_city(city_id);
});

app.MapGet("/update_city_info/{city_id}", async (string city_id) =>
{
    return await utility.update_city_info(city_id);
});

app.Run();



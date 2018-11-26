using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
 
namespace ConnectedPart
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WebClient())
            {
                string result = client.DownloadString("https://swapi.co/api/people/1/");
                People person = JsonConvert.DeserializeObject<People>(result);

                string result2 = client.DownloadString("https://swapi.co/api/planets/3/");
                Planet planet = JsonConvert.DeserializeObject<Planet>(result2);

                string result3 = client.DownloadString("https://swapi.co/api/starships/9/");
                Starships starship = JsonConvert.DeserializeObject<Starships>(result3);

                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.
                        ConnectionStrings["StarWarsConnectionString"].
                        ConnectionString;
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = $"insert into People values " +
                        $"('{person.name}', '{person.height}'" +
                        $"'{person.mass}', '{person.hair_color}'" +
                        $"'{person.skin_color}', '{person.eye_color}'" +
                        $"'{person.birth_year}', '{person.gender}'" +
                        $"'{person.homeworld}')";

                   
                    command.CommandText = $"insert into Planets values " +
                        $"('{planet.name}', '{planet.rotation_period}'" +
                        $"'{planet.diameter}', '{planet.climate}'" +
                        $"'{planet.gravity}', '{planet.terrain}'" +
                        $"'{planet.surface_water}', '{planet.population}'" +
                        $"'{planet.residents}')";

                  
                    command.CommandText = $"insert into Starships values " +
                        $"('{starship.name}', '{starship.model}'" +
                        $"'{starship.manufacturer}', '{starship.cost_in_credits}'" +
                        $"'{starship.length}', '{starship.max_atmosphering_speed}'" +
                        $"'{starship.crew}', '{starship.passengers}'" +
                        $"'{starship.cargo_capacity}', '{starship.consumables}'" +
                        $"'{starship.hyperdrive_rating}', '{starship.MGLT}'" +
                        $"'{starship.starship_class}', '{starship.pilots}')";

                    command.ExecuteNonQuery();

                    SqlCommand selectCommand = new SqlCommand();
                    selectCommand.Connection = connection;
                    selectCommand.CommandText = "select * from People";
                    selectCommand.CommandText = "select * from Planets";
                    selectCommand.CommandText = "select * from Starships";
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    while(reader.NextResult())
                    {
                        People gotPerson = new People
                        {
                            name = reader["name"].ToString(),
                            height = reader["height"].ToString(),
                            mass = reader["mass"].ToString(),
                            hair_color = reader["hair_color"].ToString(),
                            skin_color = reader["skin_color"].ToString(),
                            eye_color = reader["eye_color"].ToString(),
                            birth_year = reader["birth_year"].ToString(),
                            gender = reader["gender"].ToString(),
                            homeworld = reader["homeworld"].ToString()                        
                        };

                        Planet gotPlanet = new Planet
                        {
                            name = reader["name"].ToString(),
                            rotation_period = reader["rotation_period"].ToString(),
                            diameter = reader["diameter"].ToString(),
                            climate = reader["climate"].ToString(),
                            gravity = reader["gravity"].ToString(),
                            terrain = reader["terrain"].ToString(),
                            surface_water = reader["surface_water"].ToString(),
                            population = reader["population"].ToString(),
                           
                        };

                        Starships gotStarships = new Starships
                        {
                            name = reader["name"].ToString(),
                            model = reader["model"].ToString(),
                            manufacturer = reader["manufacturer"].ToString(),
                            cost_in_credits = reader["cost_in_credits"].ToString(),
                            length = reader["length"].ToString(),
                            max_atmosphering_speed = reader["max_atmosphering_speed"].ToString(),
                            crew = reader["crew"].ToString(),
                            passengers = reader["passengers"].ToString(),
                            cargo_capacity = reader["cargo_capacity"].ToString(),
                            consumables = reader["consumables"].ToString(),
                            hyperdrive_rating = reader["hyperdrive_rating"].ToString(),
                            MGLT = reader["MGLT"].ToString(),
                            starship_class = reader["starship_class"].ToString(),                         
                        };
                    }                  
                }
            }
        }
    }
}

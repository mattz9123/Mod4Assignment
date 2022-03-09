using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationTemplate.Services
{
    public class JSONRepository : IRepository
    {
        public void Read(string json)
        {
            Movie m = JsonConvert.DeserializeObject<Movie>(json);

            Console.WriteLine(m.Id);
            Console.WriteLine(m.Title);
            Console.WriteLine(m.Genres);

        }



        public string Write()
        {
            Movie movie = new Movie();
            movie.Id = 1;
            movie.Title = "Batman";
            movie.Genres = new string[] { "Action|Adventure" };

            string json = JsonConvert.SerializeObject(movie);
            return json;
        }
    }
}

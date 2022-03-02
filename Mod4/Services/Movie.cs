using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTemplate.Services;

namespace ApplicationTemplate.Services
{
    public class Movie : Media
    {
        public string[] Genres { get; set; }

        public override void Display(string file)
        {
            StreamReader sr = new StreamReader(file);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                Console.WriteLine(line);
            }sr.Close();
        }

        
    }
}

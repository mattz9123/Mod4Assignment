using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTemplate.Services
{
    public abstract class Media
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public abstract void Display(string file);
    }
}

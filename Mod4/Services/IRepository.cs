using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTemplate.Services
{
    internal interface IRepository
    {
        void Read(string json);

        string Write();
    }
}

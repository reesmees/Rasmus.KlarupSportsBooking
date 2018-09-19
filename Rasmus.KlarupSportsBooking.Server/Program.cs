using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.Business;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(9999);
            server.Start();
        }
    }
}

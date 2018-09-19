using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasmus.KlarupSportsBooking.DataAccess
{
    [Flags]
    public enum Weekday : int
    {
        none = 0,
        Mandag = 1,
        Tirsdag = 2,
        Onsdag = 4,
        Torsdag = 8,
        Fredag = 16,
        Lørdag = 32,
        Søndag = 64
    }
}

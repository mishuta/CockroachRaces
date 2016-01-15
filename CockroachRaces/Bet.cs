using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockroachRaces
{
    public class Bet
    {
        public int bugId {get; set;}
        public int betSize {get; set;}
        public Bet(int n, int s)
        {
            bugId = n;
            betSize = s;
        }
    }
}

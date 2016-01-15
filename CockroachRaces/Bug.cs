using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockroachRaces
{
    public class Bug
    {
        public int id { get; set; }
        public int X { get; set; }
        public int speed { get; set; }


        public Bug(int n, int start)
        {
            id = n;
            X = start;
        }
        public int Move()
        {
            X += speed;
            return X;
        }
    }
}

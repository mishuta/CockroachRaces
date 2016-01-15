using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockroachRaces
{
    class GameController
    {
        public List<Gambler> Gamblers;
        public List<Bug> Bugs;
        public GameController()
        {
            Gamblers = new List<Gambler>(3);
            Bugs = new List<Bug>(4);
            for (int i = 1; i <= 3; i++)
                Gamblers.Add(new Gambler(i));
            for (int i = 1; i <= 4; i++)
                Bugs.Add(new Bug(i, 20));
            
        }

        public void resetBets()
        {
            foreach (Gambler g in Gamblers)
                g.currentBet = new Bet(0,0);
        }
        public void resetBugs(int startPoint)
        {
            foreach (Bug b in Bugs)
            {
                b.X = startPoint;
                b.speed = 0;
            }  
        }
    }
}

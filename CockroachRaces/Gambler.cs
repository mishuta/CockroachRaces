using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockroachRaces
{
    public class Gambler
    {
        public int id {get; set;}
        public int money {get; set;}
        public Bet currentBet;
        public Gambler(int n)
        {
            id = n;
            money = 100;
            currentBet = new Bet(0, 0);
        }
        public int bet(int bugId, int betSize)
        {
            if (betSize <= 0)
                return 0;
            if (currentBet.betSize > 0)
                return 2;
            else
                if (betSize <= money)
                    {
                        money -= betSize;
                        currentBet.betSize += betSize;
                        currentBet.bugId = bugId;
                        return 1;
                    }
                else
                    return 0;             
        }

        public bool getPrize(int n, int sizePrize)
        {
            if (n == currentBet.bugId)
            {
                money += sizePrize;
                return true;
            }
            else
                return false;
           
        }
    
    }
}

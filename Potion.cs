using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Potion : Equipment
    {
        public float healAmount = 30;

        public float Heal()
        {
            return healAmount;
        }
    }
}

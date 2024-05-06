using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Potion : Equipment
    {
        public float healAmount = 30;

        public Potion()
        {
            EquipData = new EquipmentData(0, "일반 포션", 0, 0, "어디서든 볼 수 있는 일반 포션이다.", 100, false, false);
        }
        public float Heal()
        {
            return healAmount;
        }
    }
}

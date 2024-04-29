using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class Weapon : Equipment
    {
        public Weapon() { }

        public Weapon(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled) : base(name, ATK, DEF, Description, Price, isEquipped, isSelled)
        {
            type = Define.EquipType.Weapon;
        }

    }
}

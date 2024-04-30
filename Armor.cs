using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Armor : Equipment
    {
        public Armor() { }

        public Armor(int oid, string name, int atk, int def, string description, int price, bool isEquipped, bool isSelled) : base(oid, name, atk, def, description, price, isEquipped, isSelled)
        {
            Type = Define.EquipType.Armor;
        }
    }
}

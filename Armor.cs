using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Armor : Equipment
    {
        public Armor(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled) : base(name, ATK, DEF, Description, Price, isEquipped, isSelled)
        {
            type = EquipType.Armor;
        }

        public override void Equip(Equipment equip)
        {
            base.Equip(equip);

            GameManager.onEquipArmor?.Invoke(equip);
        }

        public override void Detach(Equipment equip)
        {
            base.Detach(equip);

            GameManager.onDetachArmor?.Invoke(equip);
        }
    }
}

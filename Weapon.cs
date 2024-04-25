using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Weapon : Equipment
    {
        public Weapon(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled) : base(name, ATK, DEF, Description, Price, isEquipped, isSelled)
        {
            type = EquipType.Weapon;
        }

        public override void Equip(Equipment equip)
        {
            base.Equip(equip);

            GameManager.onEquipWeapon?.Invoke(equip);
        }

        public override void Detach(Equipment equip)
        {
            base.Detach(equip);

            GameManager.onDetachWeapon?.Invoke(equip);
        }

    }
}

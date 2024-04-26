using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class PlayerStatus
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Chad { get; set; }

        public int ATK { get; set; }

        public int DEF { get; set; }

        public int VIT { get; set; }

        public int Gold { get; set; }

        public PlayerStatus()
        {
            GameManager.onEquipWeapon += SetWeaponAbility;
            GameManager.onEquipArmor += SetArmorAbility;

            GameManager.onDetachWeapon += SetWeaponAbility;
            GameManager.onDetachArmor += SetArmorAbility;

        }
        public PlayerStatus(string name, int level, string chad, int atk, int def, int vit, int gold)
        {
            Name = name;
            Level = level;
            Chad = chad;
            ATK = atk;
            DEF = def;
            VIT = vit;
            Gold = gold;

            GameManager.onEquipWeapon += SetWeaponAbility;
            GameManager.onEquipArmor += SetArmorAbility;

            GameManager.onDetachWeapon += SetWeaponAbility;
            GameManager.onDetachArmor += SetArmorAbility;
        }

        public void LevelUp(int level)
        {
            Level += level;
            ATK += 1;
            DEF += 1;

        }

        public void SetWeaponAbility(Equipment weapon)
        {
            if (weapon.isEquipped)
            {
                ATK += weapon.ATK;
            }
            else
            {
                ATK -= weapon.ATK;

            }
        }
        public void SetArmorAbility(Equipment armor)
        {
            if (armor.isEquipped)
            {
                DEF += armor.DEF;

            }
            else
            {
                DEF -= armor.DEF;

            }
        }

    }
}

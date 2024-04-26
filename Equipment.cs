using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public enum EquipType
    {
        Weapon,
        Armor
    }
    public interface IEquipable
    {
        void Equip(Equipment equip);
        void Detach(Equipment equip);
    }

    [System.Serializable]
    public class Equipment
    {
        public string Name { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }

        public bool isEquipped = false;
        public bool isSelled = false;

        public EquipType type;


        public Equipment() { }

        public Equipment(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled)
        {
            if(!GameManager.isLoaded)
            {
                Name = name;
                this.ATK = ATK;
                this.DEF = DEF;
                this.Description = Description;
                this.Price = Price;
                this.isEquipped = isEquipped;
                this.isSelled = isSelled;
            }
        }

        public string IsEquipped()
        {
            if (isEquipped)
            {
                return "[E]";
            }
            else
            {
                return "";
            }
        }

        public string IsSelled()
        {
            if (isSelled)
            {
                return "구매완료";
            }
            else
            {
                return Price.ToString() + " G";
            }
        }

        public virtual void Equip(Equipment equip)
        {
            isEquipped = true;
            if(type == EquipType.Weapon)
            {
                GameManager.onEquipWeapon?.Invoke(equip);

            }
            else if (type == EquipType.Armor)
            {
                GameManager.onEquipArmor?.Invoke(equip);

            }
        }

        public virtual void Detach(Equipment equip)
        {
            isEquipped = false;
            if (type == EquipType.Weapon)
            {
                GameManager.onDetachWeapon?.Invoke(equip);

            }
            else if (type == EquipType.Armor)
            {
                GameManager.onDetachArmor?.Invoke(equip);

            }
        }
    }
}

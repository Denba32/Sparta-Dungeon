using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{

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

        public Define.EquipType type;


        public Equipment() { }

        public Equipment(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled)
        {
            if(!GameManager.Instance.isLoaded)
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
            if(type == Define.EquipType.Weapon)
            {
                GameManager.Instance.Event.onEquipWeapon?.Invoke(equip);

            }
            else if (type == Define.EquipType.Armor)
            {
                GameManager.Instance.Event.onEquipArmor?.Invoke(equip);

            }
        }

        public virtual void Detach(Equipment equip)
        {
            isEquipped = false;
            if (type == Define.EquipType.Weapon)
            {
                GameManager.Instance.Event.onDetachWeapon?.Invoke(equip);

            }
            else if (type == Define.EquipType.Armor)
            {
                GameManager.Instance.Event.onDetachArmor?.Invoke(equip);

            }
        }
    }
}

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


    public abstract class Equipment
    {
        public string Name { get; private set; }
        public int ATK { get; private set; }
        public int DEF { get; private set; }

        public string Description { get; private set; }
        public int Price { get; private set; }

        public EquipType type;
        public bool isEquipped = false;
        public bool isSelled = false;



        public Equipment(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled)
        {
            Name = name;
            this.ATK = ATK;
            this.DEF = DEF;
            this.Description = Description;
            this.Price = Price;
            this.isEquipped = isEquipped;
            this.isSelled = isSelled;
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
        }

        public virtual void Detach(Equipment equip)
        {
            isEquipped = false;
        }
    }
}

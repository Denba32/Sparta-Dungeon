

namespace Sparta_Dungeon
{

    public interface IEquipable
    {
        void Equip(Equipment equip);
        void Detach(Equipment equip);
    }

    public class Equipment
    {
        public EquipmentData EquipData { get; set; }
        public Define.EquipType Type { get; set; }
        public Equipment() { }
        public Equipment(int oid, string name, int atk, int def, string description, int price, bool isEquipped, bool isSelled)
        {
            
            EquipData = new EquipmentData();
            EquipData.Oid = oid;
            EquipData.Name = name;
            EquipData.Atk = atk;
            EquipData.Def = def;
            EquipData.Description = description;
            EquipData.Price = price;
            EquipData.IsEquipped = isEquipped;
            EquipData.IsSelled = isSelled;
        }

        public void ShowItemInfo(bool isSelect)
        {
            if(isSelect)
            {
                if (Type == Define.EquipType.Weapon)
                {
                    Console.WriteLine($"{IsEquipped()}{EquipData.Name}  | 공격력 +{EquipData.Atk}  | {EquipData.Description}");
                }
                else if (Type == Define.EquipType.Armor)
                {
                    Console.WriteLine($"{IsEquipped()}{EquipData.Name}  | 방어력 +{EquipData.Def}  | {EquipData.Description}");
                }
            }
            else
            {
                if (Type == Define.EquipType.Weapon)
                {
                    Console.WriteLine($"   {IsEquipped()}{EquipData.Name}  | 공격력 +{EquipData.Atk}  | {EquipData.Description}");
                }
                else if (Type == Define.EquipType.Armor)
                {
                    Console.WriteLine($"   {IsEquipped()}{EquipData.Name}  | 방어력 +{EquipData.Def}  | {EquipData.Description}");
                }
            }

        }

        public string IsEquipped()
        {
            if (EquipData.IsEquipped)
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
            if (EquipData.IsSelled)
            {
                return "구매완료";
            }
            else
            {
                return EquipData.Price.ToString() + " G";
            }
        }

        public virtual void Equip(Equipment equip)
        {
            EquipData.IsEquipped = true;
            //if(Type == Define.EquipType.Weapon)
            //{
            //    GameManager.Instance.Event.EquipWeapon(equip);
            //}
            //else if (Type == Define.EquipType.Armor)
            //{
            //    GameManager.Instance.Event.EquipArmor(equip);
            //}
        }

        public virtual void Detach(Equipment equip)
        {
            EquipData.IsEquipped = false;
            //if (Type == Define.EquipType.Weapon)
            //{
            //    GameManager.Instance.Event.DetachWeapon(equip);

            //}
            //else if (Type == Define.EquipType.Armor)
            //{
            //    GameManager.Instance.Event.DetachArmor(equip);

            //}
        }
    }
}

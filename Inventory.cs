using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class Inventory
    {
        public List<Equipment> items { get; private set; } = new List<Equipment>();
        public Equipment? Weapon { get; set; }
        public Equipment? Armor { get; set; }

        public Inventory()
        {
            if(!GameManager.isLoaded)
            {
                Armor armor = new Armor("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, false, true);
                Weapon spear = new Weapon("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true);
                Weapon oldSword = new Weapon("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600, false, true);

                Equip(spear);
                Equip(armor);

                SetItem(spear);
                SetItem(armor);
                SetItem(oldSword);
            }


            Store.onBuyItem += SetItem;
        }

        public int GetItemCount()
        {
            return items.Count;
        }


        public void SetItem(Equipment item)
        {
            items.Add(item);
        }

        public Equipment? GetItem(int index)
        {
            if (items.Count > 0)
                return items[index];
            else
                return null;
        }
        public void SellItem(Equipment item)
        {
            Detach(item);
            item.isSelled = false;

            items.Remove(item);
        }

        public string ShowAllItem()
        {
            string data = String.Empty;

            if (GetItemCount() > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == EquipType.Weapon)
                    {
                        data += $"- {i + 1} {items[i].IsEquipped()}{items[i].Name}   | 공격력 +{items[i].ATK} | {items[i].Description}\n";
                    }
                    else
                    {
                        data += $"- {i + 1} {items[i].IsEquipped()}{items[i].Name}   | 방어력 +{items[i].DEF} | {items[i].Description}\n";

                    }
                }

                return data;
            }
            else
            {
                return "";
            }
        }

        public string ShowAllItemData()
        {
            string data = "";
            foreach (var item in items)
            {
                data += $"{item.Name},{item.ATK},{item.DEF}, {item.Description},{item.Price},{Enum.GetName(item.type)},{item.isEquipped},{item.isSelled},";
            }
            data += "\n";
            return data;
        }

        public string ShowAllSellItem()
        {
            string data = String.Empty;

            if (GetItemCount() > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == EquipType.Weapon)
                    {
                        data += $"- {i + 1} {items[i].Name}   | 공격력 +{items[i].ATK} | {items[i].Description}   | {items[i].Price * 0.85} G\n";
                    }
                    else
                    {
                        data += $"- {i + 1} {items[i].Name}   | 방어력 +{items[i].DEF} | {items[i].Description}   | {items[i].Price * 0.85} G\n";

                    }
                }
                return data;
            }
            else
            {
                return "";
            }
        }

        public void Equip(Equipment equip)
        {
            if (!equip.isEquipped)
            {
                equip.isEquipped = true;

                if (equip.type == EquipType.Weapon)
                {
                    if (Weapon == equip) return;

                    equip.Equip(equip);
                    Weapon = equip;

                }
                else if (equip.type == EquipType.Armor)
                {
                    if (Armor == equip) return;

                    equip.Equip(equip);
                    Armor = equip;
                }
            }
        }

        /*
         * 1.아이템이 장착되어있는 것일 경우
         * 2.장착되어있지 않을경우
         */
        public void SelectItem(int count)
        {
            int selNum = count - 1;

            if (items[selNum].isEquipped)
            {
                // 무기일 경우
                if (items[selNum].type == EquipType.Weapon)
                {
                    if (Weapon.Name ==items[selNum].Name)
                    {
                        Detach(items[selNum]);
                    }
                }
                // 방어구일 경우
                else
                {
                    if (Armor.Name == items[selNum].Name)
                    {
                        Detach(items[selNum]);
                    }
                }
            }
            else
            {
                if (items[selNum].type == EquipType.Weapon)
                {
                    // 아이템이 존재할 경우
                    if (Weapon != null)
                    {
                        Detach(Weapon);
                        Equip(items[selNum]);
                    }
                    else
                    {
                        Equip(items[selNum]);
                    }
                }
                else
                {
                    if (Armor != null)
                    {
                        Detach(Armor);
                        Equip(items[selNum]);
                    }
                    else
                    {
                        Equip(items[selNum]);
                    }
                }
            }
        }
        private void Detach(Equipment equip)
        {
            if (equip.isEquipped)
            {
                if (equip.type == EquipType.Weapon)
                {
                    if (Weapon.Name == equip.Name)
                    {
                        Weapon = null;
                        equip.Detach(equip);
                    }
                }
                else if (equip.type == EquipType.Armor)
                {
                    if (Armor.Name == equip.Name)
                    {
                        Armor = null;
                        equip.Detach(equip);
                    }
                }
            }
        }
        public int GetWeaponAbility()
        {
            if (Weapon == null)
                return 0;

            return Weapon.ATK;
        }

        public int GetArmorAbility()
        {
            if (Armor == null)
                return 0;

            return Armor.DEF;
        }

    }

}

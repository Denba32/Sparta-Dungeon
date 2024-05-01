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
    

        public Inventory()
        {
            GameManager.Instance.Event.onSellItem += SetItem;
            GameManager.Instance.Event.onShowItems += ShowItems;
        }
        
        public void Init()
        {
            Armor armor = new Armor(2, "무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, false, true);
            Weapon spear = new Weapon(6, "스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true);
            Weapon oldSword = new Weapon(4, "낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600, false, true);

            Equip(spear);
            Equip(armor);

            SetItem(spear);
            SetItem(armor);
            SetItem(oldSword);
        }
        public void ShowItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].ShowItemInfo();
            }
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
            item.EquipData.IsSelled = false;

            items.Remove(item);
        }

        public string ShowAllItem()
        {
            string data = String.Empty;

            if (GetItemCount() > 0)
            {
                for (int i = 0; i < items.Count; i++) 
                { 
                 
                }
                return data;
            }
            else
            {
                return "";
            }
        }

        public string ShowAllSellItem()
        {
            string data = String.Empty;

            if (GetItemCount() > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Type == Define.EquipType.Weapon)
                    {
                        data += $"- {i + 1} {items[i].EquipData.Name}   | 공격력 +{items[i].EquipData.Atk} | {items[i].EquipData.Description}   | {items[i].EquipData.Price * 0.85} G\n";
                    }
                    else
                    {
                        data += $"- {i + 1} {items[i].EquipData.Name}   | 방어력 +{items[i].EquipData.Def} | {items[i].EquipData.Description}   | {items[i].EquipData.Price * 0.85} G\n";

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
            // 장비를 미장착중일 때
            if (!equip.EquipData.IsEquipped)
            {
                equip.EquipData.IsEquipped = true;

                if (equip.Type == Define.EquipType.Weapon)
                {
                    if (GameManager.Instance.Player.Weapon == equip) return;

                    equip.Equip(equip);
                    GameManager.Instance.Player.Weapon = equip;

                }
                else if (equip.Type == Define.EquipType.Armor)
                {
                    if (GameManager.Instance.Player.Armor == equip) return;

                    equip.Equip(equip);
                    GameManager.Instance.Player.Armor = equip;
                }
            }
            // 장비를 장착 중일 때 Swap
            else
            {
                if (equip.Type == Define.EquipType.Weapon)
                {
                    Detach(GameManager.Instance.Player.Weapon);
                    GameManager.Instance.Player.Weapon = equip;
                }
                else if (equip.Type == Define.EquipType.Armor)
                {
                    Detach(GameManager.Instance.Player.Armor);
                    GameManager.Instance.Player.Armor = equip;
                }
                
                equip.Equip(equip);
            }
        }

        /*
         * 1.아이템이 장착되어있는 것일 경우
         * 2.장착되어있지 않을경우
         */

        public void SelectItem(int count)
        {
            int selNum = count - 1;

            if (items[selNum].EquipData.IsEquipped)
            {
                // 무기일 경우
                if (items[selNum].Type == Define.EquipType.Weapon)
                {
                    if (GameManager.Instance.Player.Weapon.EquipData.Oid ==items[selNum].EquipData.Oid)
                    {
                        Detach(items[selNum]);
                    }
                }
                // 방어구일 경우
                else
                {
                    if (GameManager.Instance.Player.Armor.EquipData.Oid == items[selNum].EquipData.Oid)
                    {
                        Detach(items[selNum]);
                    }
                }
            }
            else
            {
                if (items[selNum].Type == Define.EquipType.Weapon)
                {
                    // 아이템이 존재할 경우
                    if (GameManager.Instance.Player.Weapon != null)
                    {
                        Detach(GameManager.Instance.Player.Weapon);
                        Equip(items[selNum]);
                    }
                    else
                    {
                        Equip(items[selNum]);
                    }
                }
                else
                {
                    if (GameManager.Instance.Player.Armor != null)
                    {
                        Detach(GameManager.Instance.Player.Armor);
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
            if (equip.EquipData.IsEquipped)
            {
                if (equip.Type == Define.EquipType.Weapon)
                {
                    if (GameManager.Instance.Player.Weapon.EquipData.Oid == equip.EquipData.Oid)
                    {
                        items.Find(x => x == equip).EquipData.IsEquipped = false;
                        GameManager.Instance.Player.Weapon = null;
                        equip.Detach(equip);
                    }
                }
                else if (equip.Type == Define.EquipType.Armor)
                {
                    if (GameManager.Instance.Player.Armor.EquipData.Oid == equip.EquipData.Oid)
                    {
                        items.Find(x => x == equip).EquipData.IsEquipped = false;

                        GameManager.Instance.Player.Armor = null;
                        equip.Detach(equip);
                    }
                }
            }
        }
        public int GetWeaponAbility()
        {
            if (GameManager.Instance.Player.Weapon == null)
                return 0;

            return GameManager.Instance.Player.Weapon.EquipData.Atk;
        }

        public int GetArmorAbility()
        {
            if (GameManager.Instance.Player.Armor == null)
                return 0;

            return GameManager.Instance.Player.Armor.EquipData.Def;
        }

    }

}

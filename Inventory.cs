namespace Sparta_Dungeon
{
    [System.Serializable]
    public class Inventory
    {
        public List<Equipment> items { get; set; } = new List<Equipment>();
        
        public Inventory()
        {
            GameManager.Instance.Event.onDetach += Detach;
            GameManager.Instance.Event.onShowItems += ShowItems;
            GameManager.Instance.Event.onShowSelectorItemList += ShowSelectItems;
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


        /*
         * 인벤토리 진입 시,
         * 소유하고 있는 장비를 출력
         */
        public void ShowItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].ShowItemInfo(false);
            }
        }

        /*
         * 인벤토리 장비 변경 진입 시
         * 소유하고 있는 장비와 번호를 출력
         */
        private void ShowSelectItems(bool istrue)
        {
            for (int i = 0; i < GameManager.Instance.Player.Inven.items.Count; i++)
            {
                if (istrue)
                {
                    Console.SetCursorPosition(1, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[ヰ]");
                    GameManager.Instance.UI.ConsoleColorReset();
                }
                else
                {
                    Console.SetCursorPosition(0, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[ ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{1 + i}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" ]");
                    GameManager.Instance.UI.ConsoleColorReset();
                }
                if (GameManager.Instance.Player.Inven.items[i].EquipData.IsEquipped)
                {
                    Console.SetCursorPosition(6, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("E");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("]");
                    GameManager.Instance.UI.ConsoleColorReset();
                }
                else
                {
                    Console.SetCursorPosition(6, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ]");
                    GameManager.Instance.UI.ConsoleColorReset();
                }
                if (GameManager.Instance.Player.Inven.items[i].Type == Define.EquipType.Weapon)
                {
                    Console.SetCursorPosition(10, 8 + i);
                    Console.Write($"| {GameManager.Instance.Player.Inven.items[i].EquipData.Name}");
                    Console.SetCursorPosition(28, 8 + i);
                    Console.Write($"|  공격력 : {GameManager.Instance.Player.Inven.items[i].EquipData.Atk}");
                    Console.SetCursorPosition(44, 8 + i);
                    Console.Write($"|  {GameManager.Instance.Player.Inven.items[i].EquipData.Description}");
                    Console.SetCursorPosition(99, 8 + i);
                }
                else
                {
                    Console.SetCursorPosition(10, 8 + i);
                    Console.Write($"| {GameManager.Instance.Player.Inven.items[i].EquipData.Name}");
                    Console.SetCursorPosition(28, 8 + i);
                    Console.Write($"|  방어력 : {GameManager.Instance.Player.Inven.items[i].EquipData.Def}");
                    Console.SetCursorPosition(44, 8 + i);
                    Console.WriteLine($"|  {GameManager.Instance.Player.Inven.items[i].EquipData.Description}");
                    Console.SetCursorPosition(99, 8 + i);
                }

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

        /*
         * 상점에 진입 시,
         * 인벤토리에 진입하여
         * 판매할 아이템을 출력
         */
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

        // 장비를 장착하는 코메서드
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
        public void Detach(Equipment equip)
        {
            if (equip == null)
            {
                return;
            }
            else if (equip.EquipData.IsEquipped)
            {
                if (equip.Type == Define.EquipType.Weapon)
                {
                    if (GameManager.Instance.Player.Weapon.EquipData.Oid == equip.EquipData.Oid)
                    {
                        items.Find(x => x.EquipData.Oid == equip.EquipData.Oid).EquipData.IsEquipped = false;
                        equip.Detach(equip);
                        GameManager.Instance.Player.Weapon = null;
                        
                    }
                }
                else if (equip.Type == Define.EquipType.Armor)
                {
                    if (GameManager.Instance.Player.Armor.EquipData.Oid == equip.EquipData.Oid)
                    {
                        items.Find(x => x.EquipData.Oid == equip.EquipData.Oid).EquipData.IsEquipped = false;
                        equip.Detach(equip);
                        GameManager.Instance.Player.Armor = null;
                        
                    }
                }
            }
        }


        /*
         * 선택한 아이템을
         * 장착하게 도와주는 코드
         */
        public void SelectItem(int count)
        {
            int selNum = count - 1;

            // 선택한 장비가 장착 중이라고 뜬다면
            if (items[selNum].EquipData.IsEquipped)
            {
                // 무기일 경우
                if (items[selNum].Type == Define.EquipType.Weapon)
                {
                    Detach(items[selNum]);
                }
                // 방어구일 경우
                else
                {
                    Detach(items[selNum]);
                }
            }
            // 
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

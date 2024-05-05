using System;

namespace Sparta_Dungeon
{
    public class Store
    {
        public List<Equipment> items { get; set; } = new List<Equipment>();

        public Store()
        {
            GameManager.Instance.Event.onSellItem += Sell;
            GameManager.Instance.Event.onBuy += Buy;
            GameManager.Instance.Event.onShowShopList += ShowAllItemData;
            Init();

        }

        public void Init()
        {
            items.Add(new Armor(1, "수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
            items.Add(new Armor(2, "무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, false, true));
            items.Add(new Armor(3, "스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            items.Add(new Weapon(4, "낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, true));
            items.Add(new Weapon(5, "청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
            items.Add(new Weapon(6, "스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true));
        }

        public void ShowAllItemData(bool istrue)
        {
            for(int i = 0; i < items.Count; i++)
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
                if (items[i].Type == Define.EquipType.Weapon)
                {
                    Console.SetCursorPosition(6 , 8 + i);
                    Console.Write($"{items[i].EquipData.Name}");
                    Console.SetCursorPosition(22, 8 + i);
                    Console.Write($"|  공격력 : { items[i].EquipData.Atk}" );
                    Console.SetCursorPosition(38, 8 + i);
                    Console.Write($"|  {items[i].EquipData.Description}");
                    Console.SetCursorPosition(93, 8 + i);
                    Console.Write("|  ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{items[i].EquipData.Price}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(102, 8 + i);
                    Console.WriteLine("G");
                }
                else
                {
                    Console.SetCursorPosition(6, 8 + i);
                    Console.Write($"{items[i].EquipData.Name}");
                    Console.SetCursorPosition(22, 8 + i);
                    Console.Write($"|  방어력 : {items[i].EquipData.Def}");
                    Console.SetCursorPosition(38, 8 + i);
                    Console.Write($"|  {items[i].EquipData.Description}");
                    Console.SetCursorPosition(93, 8 + i);
                    Console.Write("|  ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{items[i].EquipData.Price}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(102, 8 + i);
                    Console.WriteLine("G");
                }
                
            }
        }

        // 플레이어가 상점으로부터 아이템을 구매할 때 사용하는 메서드
        public void Buy(int index)
        {
            if (items.Count > 0)
            {
                if (index - 1 >= items.Count)
                {
                    GameManager.Instance.UI.ErrorText();
                }
                else if (items[index - 1].EquipData.Price > GameManager.Instance.Player.PlayerData.Gold)
                {
                    GameManager.Instance.UI.SystemText("   소지금이 부족합니다!!!", ConsoleColor.Red, 400);
                }
                else
                {
                    if(items[index - 1].Type == Define.EquipType.Weapon)
                    {
                        GameManager.Instance.Player.SetGold(-items[index - 1].EquipData.Price);
                        GameManager.Instance.Player.Inven.items.Add(new Weapon(items[index - 1].EquipData.Oid, items[index - 1].EquipData.Name, items[index - 1].EquipData.Atk, 0, items[index - 1].EquipData.Description, items[index - 1].EquipData.Price, false, false));
                    }
                    else
                    {
                        GameManager.Instance.Player.SetGold(-items[index - 1].EquipData.Price);
                        GameManager.Instance.Player.Inven.items.Add(new Armor(items[index - 1].EquipData.Oid, items[index - 1].EquipData.Name, 0, items[index - 1].EquipData.Def, items[index - 1].EquipData.Description, items[index - 1].EquipData.Price, false, false));
                    }
                    GameManager.Instance.UI.SystemText("   아이템을 구매했습니다!",ConsoleColor.Green, 400);
                }
            }
        }

        // 플레이어가 상점으로부터 아이템을 판매할 때 사용하는 메서드
        public void Sell(int index)
        {
            if (index - 1 >= GameManager.Instance.Player.Inven.items.Count)
            {
                GameManager.Instance.UI.ErrorText();
            }
            else
            {
                GameManager.Instance.Player.SetGold( + (int)(items[index - 1].EquipData.Price * 0.8f));
                GameManager.Instance.Event.Detached(GameManager.Instance.Player.Inven.items[index - 1]);
                GameManager.Instance.Player.Inven.items.RemoveAt(index - 1);
                GameManager.Instance.UI.SystemText("   아이템을 판매했습니다!", ConsoleColor.Green, 400);
            }
        }
    }
}

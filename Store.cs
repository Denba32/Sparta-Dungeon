using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    internal class Store
    {
        public List<Equipment> items { get; set; } = new List<Equipment>();

        public static event Action<Equipment>? onBuyItem;

        public Store()
        {
            if(!GameManager.isLoaded)
            {
                items.Add(new Armor("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
                items.Add(new Armor("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, false, true));
                items.Add(new Armor("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
                items.Add(new Weapon("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, true));
                items.Add(new Weapon("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
                items.Add(new Weapon("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true));
            }

            GameManager.player.onSellItem += Sell;

        }
        public string ShowAllItemData()
        {
            string data = "";
            foreach (var item in items)
            {
                data += $"{item.Name},{item.ATK},{item.DEF},{item.Description},{item.Price},{Enum.GetName(item.type)},{item.isEquipped},{item.isSelled},";
            }
            data += "\n";
            return data;
        }

        // 플레이어가 상점으로부터 아이템을 구매할 때 사용하는 메서드
        public void Buy(int index)
        {
            if (items.Count > 0)
            {
                if (items[index - 1].isSelled)
                {
                    GameManager.isBuyed = true;
                    return;
                }
                if (items[index - 1].Price > GameManager.player.Status.Gold)
                {
                    GameManager.isEmpty = true;
                    return;
                }
                else
                {
                    GameManager.player.SetGold(-items[index - 1].Price);
                    items[index - 1].isSelled = true;
                }
            }
            onBuyItem?.Invoke(items[index - 1]);
        }

        // 플레이어가 상점으로부터 아이템을 판매할 때 사용하는 메서드
        public void Sell(Equipment equipment)
        {
            foreach (var item in items)
            {
                if (equipment.Name == item.Name)
                {
                    item.isSelled = false;
                    break;
                }

            }

        }




        public int GetItemCount() => items.Count;
        // 상점에서 소유하고 있는 총 아이템 정보에 대해서 출력
        public string ShowItemlist(bool isBuy)
        {
            string data = "";

            if (GetItemCount() > 0)
            {
                if (isBuy)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].type == EquipType.Weapon)
                        {
                            data += $"- {i + 1} {items[i].Name}    | 공격력 +{items[i].ATK}  | {items[i].Description}  | {items[i].IsSelled()}\n";
                        }
                        else
                        {
                            data += $"- {i + 1} {items[i].Name}    | 방어력 +{items[i].DEF}  | {items[i].Description}  | {items[i].IsSelled()}\n";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].type == EquipType.Weapon)
                        {
                            data += $"- {items[i].Name}    | 공격력 +{items[i].ATK}  | {items[i].Description}  | {items[i].IsSelled()}\n";
                        }
                        else
                        {
                            data += $"- {items[i].Name}    | 방어력 +{items[i].DEF}  | {items[i].Description}  | {items[i].IsSelled()}\n";
                        }
                    }
                }

            }
            else
            {
                return "";
            }

            return data;


        }

        // 판매 여부에 따라 결과를 출력해주는 메서드
        public string IsSelled(Equipment item)
        {
            if (item.isSelled)
            {
                return "구매완료";
            }
            else
            {
                return item.Price.ToString();
            }
        }
    }
}

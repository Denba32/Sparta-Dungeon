namespace Sparta_Dungeon
{
    public class Store
    {
        public List<Equipment> items { get; set; } = new List<Equipment>();


        public Store()
        {
            GameManager.Instance.Event.onSellItem += Sell;
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

        public string ShowAllItemData()
        {
            string data = "";
            foreach (var item in items)
            {
                data += $"{item.EquipData.Name},{item.EquipData.Atk},{item.EquipData.Def},{item.EquipData.Description},{item.EquipData.Price},{Enum.GetName(item.Type)},{item.EquipData.IsEquipped},{item.EquipData.IsSelled},";
            }
            data += "\n";
            return data;
        }

        // 플레이어가 상점으로부터 아이템을 구매할 때 사용하는 메서드
        public void Buy(int index)
        {
            if (items.Count > 0)
            {
                if (items[index - 1].EquipData.IsSelled)
                {
                    return;
                }
                if (items[index - 1].EquipData.Price > GameManager.Instance.Player.PlayerData.Gold)
                {
                    return;
                }
                else
                {
                    GameManager.Instance.Player.SetGold(-items[index - 1].EquipData.Price);
                    items[index - 1].EquipData.IsSelled = true;
                }
            }
            GameManager.Instance.Event.BuyItem(items[index - 1]);
        }

        // 플레이어가 상점으로부터 아이템을 판매할 때 사용하는 메서드
        public void Sell(Equipment equipment)
        {
            foreach (var item in items)
            {
                if (equipment.EquipData.Oid == item.EquipData.Oid)
                {
                    item.EquipData.IsSelled = false;
                    break;
                }

            }

        }


        public int GetItemCount() => items.Count;
        // 상점에서 소유하고 있는 총 아이템 정보에 대해서 출력
       

        // 상점이 가지고 있는 아이템 판매
        public string ShowItemlist(bool isBuy)
        {
            string data = "";

            if (GetItemCount() > 0)
            {
                if (isBuy)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Type == Define.EquipType.Weapon)
                        {
                            data += $"- {i + 1} {items[i].EquipData.Name}    | 공격력 +{items[i].EquipData.Atk}  | {items[i].EquipData.Description}  | {items[i].IsSelled()}\n";
                        }
                        else
                        {
                            data += $"- {i + 1} {items[i].EquipData.Name}    | 방어력 +{items[i].EquipData.Def}  | {items[i].EquipData.Description}  | {items[i].IsSelled()}\n";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Type == Define.EquipType.Weapon)
                        {
                            data += $"- {items[i].EquipData.Name}    | 공격력 +{items[i].EquipData.Atk}  | {items[i].EquipData.Description}  | {items[i].IsSelled()}\n";
                        }
                        else
                        {
                            data += $"- {items[i].EquipData.Name}    | 방어력 +{items[i].EquipData.Def}  | {items[i].EquipData.Description}  | {items[i].IsSelled()}\n";
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
            if (item.EquipData.IsSelled)
            {
                return "구매완료";
            }
            else
            {
                return item.EquipData.Price.ToString();
            }
        }
    }
}

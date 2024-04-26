using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class Player
    {
        public event Action<Equipment>? onSellItem;

        // 이름
        public string Name { get; set; }

        // 스테이터스
        public PlayerStatus Status { get; set; }

        // 인벤토리
        public Inventory Inven { get; set; }

        public Player() { } 
        public Player(string name)
        {
            Name = name;

            Status = new PlayerStatus(Name, 1, "전사", 10, 5, 100, 1500);
            Inven = new Inventory();
        }

        public void SetGold(int gold)
        {
            Status.Gold += gold;
        }
        public void Sell(int index)
        {
            if (Inven.GetItemCount() > 0)
            {
                if (Inven.GetItem(index-1) != null)
                {
                    Equipment? equipment = Inven.GetItem(index - 1);
                    SetGold((int)(equipment.Price * 0.85));
                    Inven.SellItem(equipment);

                    onSellItem?.Invoke(equipment);
                }
            }
        }
        // 장비 장착 상태까지 포함하여 스테이터스 표시 메서드

        // 모든 스테이터스 정보를 포맷화하여 출력
        public string ShowAllStatus()
        {
            if (Inven.Weapon != null && Inven.Armor != null)
            {
                return $"이름 : {Name}\n" +
                        $"Lv. {Status.Level.ToString("D2")}\n" +
                        $"Chad ( {Status.Chad} )\n" +
                        $"공격력 : {Status.ATK} (+{Inven.GetWeaponAbility()})\n" +
                        $"방어력 : {Status.DEF} (+{Inven.GetArmorAbility()})\n" +
                        $"체력 : {Status.VIT}\n" +
                        $"Gold : {Status.Gold} G\n";
            }
            else if (Inven.Weapon != null && Inven.Armor == null)
            {
                return $"이름 : {Name}\n" +
                        $"Lv. {Status.Level.ToString("D2")}\n" +
                        $"Chad ( {Status.Chad} )\n" +
                        $"공격력 : {Status.ATK} (+{Inven.GetWeaponAbility()})\n" +
                        $"방어력 : {Status.DEF}\n" +
                        $"체력 : {Status.VIT}\n" +
                        $"Gold : {Status.Gold} G\n";
            }
            else if (Inven.Weapon == null && Inven.Armor != null)
            {
                return $"이름 : {Name}\n" +
                        $"Lv. {Status.Level.ToString("D2")}\n" +
                        $"Chad ( {Status.Chad} )\n" +
                        $"공격력 : {Status.ATK}\n" +
                        $"방어력 : {Status.DEF} (+{Inven.GetArmorAbility()})\n" +
                        $"체력 : {Status.VIT}\n" +
                        $"Gold : {Status.Gold} G\n";
            }
            else
            {
                return $"이름 : {Name}\n" +
                        $"Lv. {Status.Level.ToString("D2")}\n" +
                        $"Chad ( {Status.Chad} )\n" +
                        $"공격력 : {Status.ATK}\n" +
                        $"방어력 : {Status.DEF}\n" +
                        $"체력 : {Status.VIT}\n" +
                        $"Gold : {Status.Gold} G\n";

            }

        }

        // 데미지를 주고 출력
        public int Damage(int damage)
        {
            Status.VIT -= damage;
            if (Status.VIT <= 0)
            {
                GameManager.state = GameManager.GameState.End;
                Status.VIT = 0;
                return 0;
            }
            return Status.VIT;
        }

        // 던전 보상 및 출력
        public int RewardGold(int gold)
        {
            Status.Gold += gold;
            return Status.Gold;
        }

        public void Rest()
        {
            if (Status.Gold >= 500)
            {
                SetGold(-500);
                Status.VIT = 100;
                GameManager.isHealed = true;
            }
            else
            {
                GameManager.isEmpty = true;
            }
        }
    }
}

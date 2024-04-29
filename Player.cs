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
        public void Player1(string name)
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
        public void ShowAllStatus()
        {
            Player1("ㅇㅇ");

            Console.WriteLine($"   Lv. {Status.Level.ToString("D2")}");
            Console.WriteLine($"   이  름 : {Status.Name}");
            Console.WriteLine($"   직  업 : {Status.Chad}");
            if (Inven.Weapon != null)
            {
                Console.WriteLine($"   공격력 : {Status.ATK + Inven.GetWeaponAbility()}");
            }
            else
            {
                Console.WriteLine($"   공격력 : {Status.ATK}");
            }
            if (Inven.Armor != null)
            {
                Console.WriteLine($"   방어력 : {Status.DEF + Inven.GetArmorAbility()}");
            }
            else
            {
                Console.WriteLine($"   방어력 : {Status.DEF}");
            }
            Console.WriteLine($"   체  력 : {Status.VIT}");
            Console.WriteLine($"    Gold  : {Status.Gold}");

        }

        // 데미지를 주고 출력
        public int Damage(int damage)
        {
            Status.VIT -= damage;
            if (Status.VIT <= 0)
            {
                GameManager.Instance.state = Define.GameState.End;
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
                GameManager.Instance.isHealed = true;
            }
            else
            {
                GameManager.Instance.isEmpty = true;
            }
        }
    }
}

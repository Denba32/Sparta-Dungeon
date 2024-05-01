using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    public class Player
    {
        private PlayerData? playerData;
        private Inventory? inventory;

        public Equipment? Weapon { get; set; }
        public Equipment? Armor { get; set; }


        public PlayerData PlayerData
        {
            get
            {
                if (playerData == null)
                {
                    if (GameManager.Instance.Data.FileExists(typeof(PlayerData)))
                    {
                        playerData = GameManager.Instance.Data.Load<PlayerData>();
                        
                    }
                    else
                    {
                        PlayerData = new PlayerData("플레이어", 1, "전사", 10, 5, 100, 1500);
                   }
                }
                return playerData;
            }
            set
            {
                playerData = value;
            }
        }


        // 인벤토리
        public Inventory Inven
        {
            get
            {
                if(inventory == null)
                {
                    if(GameManager.Instance.Data.FileExists(typeof(Inventory)))
                    {
                        inventory = GameManager.Instance.Data.Load<Inventory>();
                        InitEquip();
                    }
                    else
                    {
                        inventory = new Inventory();
                        inventory.Init();
                    }
                }
                return inventory;
            }
            set
            {
                inventory = value;
            }
        }

        private void InitEquip()
        {
            foreach(var item in inventory.items)
            {
                if(item.EquipData.IsEquipped)
                {
                    if(item.Type == Define.EquipType.Weapon)
                    {
                        Weapon = item;
                    }
                    else if(item.Type == Define.EquipType.Armor)
                    {
                        Armor = item;
                    }

                }
            }
        }

        public void SetGold(int gold)
        {
            PlayerData.Gold += gold;
        }
        public void Sell(int index)
        {
            if (Inven.GetItemCount() > 0)
            {
                if (Inven.GetItem(index-1) != null)
                {
                    Equipment? equipment = Inven.GetItem(index - 1);
                    SetGold((int)(equipment.EquipData.Price * 0.85));
                    Inven.SellItem(equipment);

                    if(equipment.Type == Define.EquipType.Weapon)
                    {
                        GameManager.Instance.Event.DetachWeapon(equipment);

                    }
                    else if(equipment.Type == Define.EquipType.Armor)
                    {
                        GameManager.Instance.Event.DetachArmor(equipment);
                    }
                }
            }
        }
        // 장비 장착 상태까지 포함하여 스테이터스 표시 메서드

        // 모든 스테이터스 정보를 포맷화하여 출력
        public void ShowAllStatus()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"   이  름 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Name}");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"   직  업 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{PlayerData.Chad}]");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   공격력 : ");
            if (Weapon != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{PlayerData.Atk}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" + ({Inven.GetWeaponAbility()})");
                Console.WriteLine("");
                ;            }
            else if (Weapon != null && Armor == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{PlayerData.Atk}");
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   방어력 : ");
            if (Armor != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{PlayerData.Def}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" + ({Inven.GetWeaponAbility()})");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{PlayerData.Def}");
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   체  력 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Vit}");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   소지금 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Gold} G");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // 데미지를 주고 출력
        public int Damage(int damage)
        {
            PlayerData.SetVit(-damage);
            if (PlayerData.Vit <= 0)
            {
                
                return 0;
            }
            return PlayerData.Vit;
        }

        // 던전 보상 및 출력
        public int RewardGold(int gold)
        {
            PlayerData.Gold += gold;
            return PlayerData.Gold;
        }

        public void Rest()
        {
            if (PlayerData.Gold >= 500)
            {
                SetGold(-500);
                PlayerData.Vit = 100;
                GameManager.Instance.isHealed = true;
            }
            else
            {
                GameManager.Instance.isEmpty = true;
            }
        }
    }
}

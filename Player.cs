using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class Player
    {
        private PlayerData? playerData;
        private Inventory? inventory;

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
                    SetGold((int)(equipment.Price * 0.85));
                    Inven.SellItem(equipment);

                    if(equipment.type == Define.EquipType.Weapon)
                    {
                        GameManager.Instance.Event.DetachWeapon(equipment);

                    }
                    else if(equipment.type == Define.EquipType.Armor)
                    {
                        GameManager.Instance.Event.DetachArmor(equipment);
                    }
                }
            }
        }
        // 장비 장착 상태까지 포함하여 스테이터스 표시 메서드

        // 모든 스테이터스 정보를 포맷화하여 출력
        public string ShowAllStatus()
        {
            // 장비를 장착 중일 때
            if (Inven.Weapon != null && Inven.Armor != null)
            {
                return $"이름 : {PlayerData.Name}\n" +
                        $"Lv. {PlayerData.Level.ToString("D2")}\n" +
                        $"Chad ( {PlayerData.Chad} )\n" +
                        $"공격력 : {PlayerData.Atk} (+{Inven.GetWeaponAbility()})\n" +
                        $"방어력 : {PlayerData.Def} (+{Inven.GetArmorAbility()})\n" +
                        $"체력 : {PlayerData.Vit}\n" +
                        $"Gold : {PlayerData.Gold} G\n";
            }
            // 무기만 장착 중일 때
            else if (Inven.Weapon != null && Inven.Armor == null)
            {
                return $"이름 : {PlayerData.Name}\n" +
                        $"Lv. {PlayerData.Level.ToString("D2")}\n" +
                        $"Chad ( {PlayerData.Chad} )\n" +
                        $"공격력 : {PlayerData.Atk} (+{Inven.GetWeaponAbility()})\n" +
                        $"방어력 : {PlayerData.Def}\n" +
                        $"체력 : {PlayerData.Vit}\n" +
                        $"Gold : {PlayerData.Gold} G\n";
            }
            // 방어구만 장착중일 때
            else if (Inven.Weapon == null && Inven.Armor != null)
            {
                return $"이름 : {PlayerData.Name}\n" +
                        $"Lv. {PlayerData.Level.ToString("D2")}\n" +
                        $"Chad ( {PlayerData.Chad} )\n" +
                        $"공격력 : {PlayerData.Atk}\n" +
                        $"방어력 : {PlayerData.Def} (+{Inven.GetArmorAbility()})\n" +
                        $"체력 : {PlayerData.Vit}\n" +
                        $"Gold : {PlayerData.Gold} G\n";
            }
            // 장비를 장착중이지 않을 때
            else
            {
                return $"이름 : {PlayerData.Name}\n" +
                        $"Lv. {PlayerData.Level.ToString("D2")}\n" +
                        $"Chad ( {PlayerData.Chad} )\n" +
                        $"공격력 : {PlayerData.Atk}\n" +
                        $"방어력 : {PlayerData.Def}\n" +
                        $"체력 : {PlayerData.Vit}\n" +
                        $"Gold : {PlayerData.Gold} G\n";

            }

        }

        // 데미지를 주고 출력
        public int Damage(int damage)
        {
            PlayerData.SetVit(-damage);
            if (PlayerData.Vit <= 0)
            {
                GameManager.Instance.state = Define.GameState.End;
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

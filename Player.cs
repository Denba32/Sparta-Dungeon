﻿

namespace Sparta_Dungeon
{
    public class Player : IDamagable
    {
        private PlayerData? playerData;
        private Inventory? inventory;

        public Equipment? Weapon { get; set; }
        public Equipment? Armor { get; set; }

        public List<Potion> potions = new List<Potion>();

        public SkillController skillController = new SkillController();

        public int count = 0;
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
                        PlayerData = new PlayerData("플레이어", 1, "전사", 10.0f, 5.0f, 100.0f, 50, 0 ,1500);
                   }
                }
                GameManager.Instance.Event.onReward += Reward;

                return playerData;
            }
            set
            {
                playerData = value;
            }
        }
        public void Attack(IDamagable damagable)
        {
            damagable.Damage(PlayerData.Atk);
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

        public void UsePotion()
        {
            if (potions[0] != null)
            {
                PlayerData.Vit += potions[0].Heal();
                // 회복을 완료했습니다.
            }
            else
            {
                // TODO 포션이 없음을 출력함
            }
        }
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
                Console.WriteLine($" + ({Inven.GetArmorAbility()})");
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


        /// <summary>
        /// 스킬 컨트롤러를 참조하여 스킬을 사용합니다.
        /// 단일 대상을 공격합니다.
        /// </summary>
        /// <param name="skillNum" 스킬 등록 번호></param>
        /// <param name="target" 공격할 대상></param>
        public void UseSkill(int skillNum, IDamagable target)
        {
            skillController.ExecuteSkill(PlayerData.Chad, skillNum, target, PlayerData.Atk);
        }

        /// <summary>
        /// 스킬 컨트롤러를 참조하여 스킬을 사용합니다.
        /// 여러 대상을 공격합니다.
        /// </summary>
        /// <param name="skillNum" 스킬 등록 번호></param>
        /// <param name="target" 공격할 대상></param>
        public void UseSkill(int skillNum, List<IDamagable> targets)
        {
            skillController.ExecuteSkill(PlayerData.Chad, skillNum, targets, PlayerData.Atk);
        }

        // 선택지 때 본인의 직업을 고르는 부분
        public void SetChad(string chad)
        {
            if(chad.Equals("전사"))
            {
                PlayerData = new PlayerData(PlayerData.Name, 1, "전사", 6.0f, 7.0f, 100.0f, 50, 0, 1500);
            }
            else if(chad.Equals("궁수"))
            {
                PlayerData = new PlayerData(PlayerData.Name, 1, "궁수", 8.0f, 4.0f, 100.0f, 50 ,0, 1500);
            }
        }


        // 던전 보상 및 출력
        public int RewardGold(int gold)
        {
            PlayerData.Gold += gold;
            return PlayerData.Gold;
        }

        public void Reward(int exp)
        {
            PlayerData.Exp += exp;
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

        public void LevelUp()
        {
            int requireExp;
            if(GameManager.Instance.Data.levelDict.TryGetValue(PlayerData.Level, out requireExp))
            {
                if (PlayerData.Exp >= requireExp)
                {
                    PlayerData.Level++;
                    PlayerData.Atk += 0.5f;
                    PlayerData.Def += 1.0f;
                }
            }
        }

        // 플레이어가 데미지를 입었을 때의 함수
        public void Damage(float damage)
        {
            if(PlayerData.Vit > 0)
            {
                PlayerData.Vit -= damage;
            }
            else
            {
                // TODO 사망했을 때의 코드
                // GameManager.Instance.Scece.게임 끝내는 코드
                PlayerData.Vit = 0;
            }

        }

        public void SkDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }
}

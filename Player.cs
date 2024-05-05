
namespace Sparta_Dungeon
{
    public class Player : IDamagable
    {
        private PlayerData? playerData;
        private Inventory? inventory;

        private Equipment? weapon;

        private Equipment? armor;

        private bool isLoad = false;

        // public List<Potion> potions = new List<Potion>();

        private SkillController skillController;

        public int count = 0;
        
        public Player()
        {
            skillController = new SkillController(this);
        }
        public PlayerData PlayerData
        {
            get
            {
                if (playerData == null)
                {
                    // 데이터가 존재하는 경우
                    if (GameManager.Instance.Data.FileExists(typeof(PlayerData)))
                    {
                        playerData = GameManager.Instance.Data.Load<PlayerData>();
                    }

                    // 데이터가 존재하지 않는 경우
                    else
                    {
                        PlayerData = new PlayerData("플레이어", 1, "전사", 10.0f, 5.0f, 100.0f, 50, 0, 1500);
                    }

                    GameManager.Instance.Event.onSelectSkill += ShowAllSkill;
                    GameManager.Instance.Event.onCheckManaCount += skillController.CheckRequireMP;

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
                if (inventory == null)
                {
                    if (GameManager.Instance.Data.FileExists(typeof(Inventory)))
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


        public Equipment? Weapon
        {
            get => weapon;
            set
            {
                // 무기가 있던 상태에서
                if (weapon != null)
                {
                    if (value == null)
                    {
                        PlayerData.MinusWeaponAbility(weapon);
                    }
 
                    // 지금과는 다른 무기가 들어올 경우
                    else if (weapon.EquipData.Oid != value.EquipData.Oid)
                    {
                        // 기존 무기의 공격력을 뺀 후
                        PlayerData.MinusWeaponAbility(weapon);
                        // 넣으려는 무기의 공격력을 추가
                        PlayerData.PlusWeaponAbility(value);
                    }
                    // 동일한 장비 장착 시도 시
                    else if (weapon.EquipData.Oid == value.EquipData.Oid)
                    {
                        // 그냥 넘어갑니다.
                    }
                    // null을 넣으려고 할 시

                }
                // 빈 상태에서
                else
                {
                    // 새로운 무기 장착 시
                    if (value != null)
                    {
                        if(isLoad)
                        {
                            isLoad = false;
                        }
                        else
                        {
                            PlayerData.PlusWeaponAbility(value);

                        }
                    }
                    // 빈 상태에서 빈 상태를 넣으려 할 시
                    else if (weapon == value)
                    {
                        // 넘어감
                    }
                }
                weapon = value;
            }
        }

        public Equipment? Armor
        {
            get => armor;
            set
            {
                // 무기가 있던 상태에서
                if (armor != null)
                {
                    if (value == null)
                    {
                        PlayerData.MinusArmorAbility(armor);
                    }
                    // 지금과는 다른 무기가 들어올 경우
                    else if (armor != value)
                    {
                        // 기존 무기의 공격력을 뺀 후
                        PlayerData.MinusArmorAbility(armor);
                        // 넣으려는 무기의 공격력을 추가
                        PlayerData.PlusArmorAbility(value);
                    }
                    // 동일한 장비 장착 시도 시
                    else if (armor == value)
                    {
                        // 그냥 넘어갑니다.
                    }
                    // null을 넣으려고 할 시

                }
                // 빈 상태에서
                else
                {
                    // 새로운 무기 장착 시
                    if (value != null)
                    {
                        if (isLoad)
                        {
                            isLoad = false;
                        }
                        else
                        {
                            PlayerData.PlusArmorAbility(value);
                        }
                    }
                    // 빈 상태에서 빈 상태를 넣으려 할 시
                    else if (armor == value)
                    {
                        // 넘어감
                    }
                }

                armor = value;
            }


        }
        private void InitEquip()
        {
            foreach (var item in inventory.items)
            {
                if (item.EquipData.IsEquipped)
                {
                    if (item.Type == Define.EquipType.Weapon)
                    {
                        isLoad = true;

                        Weapon = item;
                    }
                    else if (item.Type == Define.EquipType.Armor)
                    {
                        isLoad = true;

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
                if (Inven.GetItem(index - 1) != null)
                {
                    Equipment? equipment = Inven.GetItem(index - 1);
                    SetGold((int)(equipment.EquipData.Price * 0.85));
                    Inven.SellItem(equipment);
                }
            }
        }
        /*
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
        */
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
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{PlayerData.Atk}");
                Console.ForegroundColor = ConsoleColor.Magenta;
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
                Console.WriteLine("");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{PlayerData.Def}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   체  력 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Vit}");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   마  나 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Mp}");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   소지금 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{PlayerData.Gold} G");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Attack(IDamagable damagable)
        {
            damagable.Damage(PlayerData.Atk);
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

        public void ShowAllSkill()
        {
            if(skillController.skillBook.TryGetValue(PlayerData.Chad, out List<ISkillExecutable> skillBook))
            {
                for(int i = 0; i < skillBook.Count; i++)
                {
                    skillBook[i].ShowSkill(i);
                }
            }
        }



        // 선택지 때 본인의 직업을 고르는 부분
        public void SetChad(string chad)
        {
            if (chad.Equals("전사"))
            {
                PlayerData = new PlayerData(PlayerData.Name, 1, "전사", 6.0f, 7.0f, 100.0f, 50, 0, 1500);
            }
            else if (chad.Equals("궁수"))
            {
                PlayerData = new PlayerData(PlayerData.Name, 1, "궁수", 8.0f, 4.0f, 100.0f, 50, 0, 1500);
            }
        }


        // 던전 보상 및 출력
        public void RewardGold(int gold)
        {
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{gold} Gold\n");
            PlayerData.Gold += gold;
        }

        public void Reward(int exp)
        {
            Console.WriteLine($"던전에서 몬스터 {exp}마리를 잡았습니다.");

            int prev = PlayerData.Exp;
            PlayerData.Exp += exp;

            Console.WriteLine($"exp {prev} -> {PlayerData.Exp}\n");
            LevelUp();
        }

        public void Rest()
        {
            if (PlayerData.Gold >= 500)
            {
                SetGold(-500);
                PlayerData.Vit = 100;
            }
            else
            {
            }
        }

        public void LevelUp()
        {
            int requireExp;
            if (GameManager.Instance.Data.levelDict.TryGetValue(PlayerData.Level, out requireExp))
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
            if (PlayerData.Vit > 0)
            {
                float prevHP = PlayerData.Vit;
                float finalDamage = (PlayerData.Def * 0.25f) - damage;

                if (finalDamage < 0)
                {
                    finalDamage = MathF.Abs((float)MathF.Ceiling(finalDamage));
                }
                else if (finalDamage > 0)
                {
                    finalDamage = 0;

                }
                Console.WriteLine($"{PlayerData.Name} 을(를) 맞췄습니다.  [데미지 : {finalDamage}]");
                PlayerData.Vit -= finalDamage;

                GameManager.Instance.UI.DamagedPlayerUI(prevHP.ToString());

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

        public bool IsDead()
        {
            return PlayerData.Vit <= 0;
        }
    }
}
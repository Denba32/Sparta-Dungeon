
namespace Sparta_Dungeon
{
    public enum Difficulty
    {
        None,
        Easy,
        Normal,
        Hard
    }

    internal class GameManager
    {
        // 무기가 장착되었을 때의 Event Action
        public static Action<Equipment>? onEquipWeapon;

        // 무기가 해제되었을 때의 Event Action
        public static Action<Equipment>? onDetachWeapon;

        // 방어구가 장착되었을 때의 Event Action
        public static Action<Equipment>? onEquipArmor;

        // 방어구가 해제되었을 때의 Event Action
        public static Action<Equipment>? onDetachArmor;


        // 간편하게 접근하기 위한 플레이어
        public static Player player = new Player("플레이어");

        // 간편하게 접근하기 위한 상점
        public static Store store = new Store();

        // 간편하게 접근하기 위한 던전
        public static Dungeon dungeon = new Dungeon();


        // 화면별 게임 상태
        public enum GameState
        {
            None,
            Main,
            Status,
            Inventory,
            ItemManagement,
            Store,
            BuyItem,
            SellItem,
            Dungeon,
            DungeonClear,
            DungeonFailed,
            Hospital,
            End
        }

        public static GameState state = GameState.Main;


        // Error 발생 시 True로 만들어서 입력란 밑에 에러를 발생시키는 코드
        public static bool isError = false;

        // 이미 구매한 상품을 구매 시도 시 입력란 밑에 경고를 알려주는 코드
        public static bool isBuyed = false;

        // 구매 시도 시 골드가 부족한 경우 처리
        public static bool isEmpty = false;

        // 힐링 성공 시 힐링을 성공했음을 알림
        public static bool isHealed = false;
        static void Main(string[] args)
        {
            // 거슬리는 커서의 움직임을 없애기 위해서 Visible을 False로
            Console.CursorVisible = false;

            // GameManager 속 전체적인 흐름
            // END상태가 될 경우 게임이 끝난다.
            while (state != GameState.End)
            {
                if (state == GameState.Main)
                {
                    Display("", "", GameState.Main);
                }

                else if (state == GameState.Status)
                {
                    Display(player.ShowAllStatus(), "", GameState.Status);
                }

                else if (state == GameState.Inventory)
                {
                    Display("", "", GameState.Inventory);
                }

                else if(state == GameState.ItemManagement)
                {
                    Display(player.Inven.ShowAllItem(), "",GameState.ItemManagement);
                }

                else if (state == GameState.Store)
                {
                    Display(store.ShowItemlist(false), "", GameState.Store);
                }
                
                else if(state == GameState.BuyItem)
                {
                    Display(store.ShowItemlist(true), "", GameState.BuyItem);

                }

                else if(state == GameState.SellItem)
                {
                    Display(player.Inven.ShowAllSellItem(), "", GameState.SellItem);

                }
                
                else if(state == GameState.Dungeon)
                {
                    Display("", "", GameState.Dungeon);
                }
                else if(state == GameState.DungeonClear)
                {
                    Display("", dungeon.Reward(player, true), GameState.DungeonClear);

                }
                else if(state == GameState.DungeonFailed)
                {
                    Display("", dungeon.Reward(player, false), GameState.DungeonFailed);
                }
                else if(state == GameState.Hospital)
                {
                    Display("", "", GameState.Hospital);

                }

                // Display 후 선택 기회 전달
                SelectOption();
            }
            Display("", "", GameState.End);
        }

        // 선택지를 고르는 메서드
        public static void SelectOption()
        {
            int selNum;

            if (state == GameState.Main)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        // Main -> Status
                        case 1:
                            state = GameState.Status;
                            break;
                        // Main -> Inventory
                        case 2:
                            state = GameState.Inventory;
                            break;
                        // Main -> Store
                        case 3:
                            state = GameState.Store;
                            break;

                        // Main -> Dungeon
                        case 4:
                            state = GameState.Dungeon;
                                break;

                        case 5:
                            state = GameState.Hospital;
                            break;

                        // 그 외 숫자 입력
                        default:
                            isError = true;
                            break;
                    }
                }
                // 숫자 외의 값을 입력했을 경우
                else
                {
                    isError = true;
                }
            }
            // 스테이터스 선택지
            else if (state == GameState.Status)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        // Status -> Main
                        case 0:
                            state = GameState.Main;
                            break;
                        // Status -> Error
                        default:
                            isError = true;
                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }

            // 인벤토리 선택지
            else if (state == GameState.Inventory)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        case 0:
                            state = GameState.Main;
                            break;
                        case 1:
                            state = GameState.ItemManagement;

                            break;

                        default:
                            isError = true;
                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }

            // 아이템 관리
            else if (state == GameState.ItemManagement)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    if(player.Inven.GetItemCount() >= selNum && selNum != 0)
                    {
                        player.Inven.SelectItem(selNum);

                    }
                    else if(selNum > player.Inven.GetItemCount())
                    {
                        isError = true;
                    }
                    else if(selNum == 0)
                    {
                        state = GameState.Main;
                    }
                }
                else
                {
                    isError = true;
                }
            }

            // 스토어
            else if (state == GameState.Store)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        case 0:
                            state = GameState.Main;
                            break;

                        case 1:
                            state = GameState.BuyItem;
                            break;

                        case 2:
                            state = GameState.SellItem;
                            break;

                        default:
                            isError = true;
                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }
            
            // 아이템 구매 시
            else if(state == GameState.BuyItem)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    if(store.GetItemCount() >= selNum && selNum != 0)
                    {
                        store.Buy(selNum);
                        state = GameState.Store;
                    }
                    else if(selNum > store.GetItemCount())
                    {
                        isError = true;
                        state = GameState.Store;
                    }
                    else if (selNum == 0)
                    {
                        state = GameState.Main;
                    }

                }
                else
                {
                    isError = true;
                }
            }

            // 아이템 판매 시
            else if (state == GameState.SellItem)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    if (player.Inven.GetItemCount() >= selNum && selNum != 0)
                    {
                        player.Sell(selNum);
                        state = GameState.Store;
                    }
                    else if (selNum > player.Inven.GetItemCount())
                    {
                        isError = true;
                        state = GameState.Store;
                    }
                    else if (selNum == 0)
                    {
                        state = GameState.Main;
                    }

                }
                else
                {
                    isError = true;
                }
            }

            // 던전 입장
            else if (state == GameState.Dungeon)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        // Dungeon -> Main
                        case 0:
                            state = GameState.Main;
                            break;
                        case 1:
                            dungeon.GoToDungeon(player, Difficulty.Easy);
                            break;
                        // Dungeon -> Normal
                        case 2:
                            dungeon.GoToDungeon(player, Difficulty.Normal);
                            break;
                        // Dungeon -> Hard
                        case 3:
                            dungeon.GoToDungeon(player, Difficulty.Hard);

                            break;
                        // Dungeon -> Error
                        default:
                            isError = true;
                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }
            
            // 던전 클리어
            else if (state == GameState.DungeonClear)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        // DungeonClear -> Main
                        case 0:
                            state = GameState.Main;
                            break;
  
                        default:
                            isError = true;
                            state = GameState.Main;

                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }

            // 던전 실패
            else if (state == GameState.DungeonFailed)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        // DungeonClear -> Main
                        case 0:
                            state = GameState.Main;
                            break;

                        default:
                            isError = true;
                            state = GameState.Main;

                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }

            // 휴식처
            else if (state == GameState.Hospital)
            {
                if (int.TryParse(Console.ReadLine(), out selNum))
                {
                    switch (selNum)
                    {
                        case 1:
                            // TODO 회복
                            player.Rest();
                            break;
                        case 0:
                            state = GameState.Main;
                            break;

                        default:
                            isError = true;
                            state = GameState.Main;

                            break;
                    }
                }
                else
                {
                    isError = true;
                }
            }
        }

        // 화면 정보 표시 메서드
        public static void Display(string text1 = " ", string text2 =" ", GameState state = GameState.None)
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);

            switch (state)
            {
                case GameState.None:
                    Console.Write(text1);
                    break;

                    // Main 화면
                case GameState.Main:

                    Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n" +
                        "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n" +
                        "1. 상태 보기\n" +
                        "2. 인벤토리\n" +
                        "3. 상점\n" +
                        "4. 던전입장\n" +
                        "5. 휴식하기\n\n" +
                        "원하시는 행동을 입력해주세요\n" +
                        ">>");

                    break;
                    // 상태창 화면
                case GameState.Status:

                    Console.Write($"상태 보기\n" +
                                    $"캐릭터의 정보가 표시됩니다.\n\n" +
                                    $"{text1}\n" +
                                    $"0. 나가기\n\n" +
                                    $"원하시는 행동을 입력해주세요.\n" +
                                    $">>");


                    break;
                    // 인벤토리 화면
                case GameState.Inventory:

                    Console.Write($"인벤토리\n" +
                                    "보유 중인 아이템을 관리할 수 있습니다.\n\n" +
                                    "[아이템 목록]\n" +
                                    $"{text1}\n\n" +
                                    "1. 장착 관리\n" +
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                    // 인벤토리 장비 관리 화면
                case GameState.ItemManagement:
                    Console.Write($"인벤토리 - 장착 관리\n" +
                                    "보유 중인 아이템을 관리할 수 있습니다.\n\n" +
                                    "[아이템 목록]\n" +
                                    $"{text1}\n\n" +
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                    // 상점 화면
                case GameState.Store:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("상점\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("필요한 아이템을 얻을 수 있는 상점입니다.\n\n" +
                                  "[보유 골드]\n");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"{player.Status.Gold} G\n\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"[아이템 목록]\n" +
                                    $"{text1}\n" +
                                    $"1. 아이템 구매\n" +
                                    $"2. 아이템 판매\n" +
                                    $"0. 나가기\n\n" +
                                    $"원하시는 행동을 입력해주세요.\n" +
                                    $">>");
                                    
                    break;
                    // 상점 아이템 구매 화면
                case GameState.BuyItem:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("상점 - 아이템 구매\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("필요한 아이템을 얻을 수 있는 상점입니다.\n\n" +
                                  "[보유 골드]\n");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"{player.Status.Gold} G\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[아이템 목록]\n" +
                                    $"{text1}\n" +
                                    $"0. 나가기\n\n" +
                                    $"원하시는 행동을 입력해주세요.\n" +
                                    $">>");
                    break;
                    // 상점 아이템 판매 화면
                case GameState.SellItem:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("상점 - 아이템 판매\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("필요한 아이템을 얻을 수 있는 상점입니다.\n\n" +
                                    "[보유 골드]\n");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"{player.Status.Gold} G\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[아이템 목록]\n" +
                                    $"{text1}\n" +
                                    $"0. 나가기\n\n" +
                                    $"원하시는 행동을 입력해주세요.\n" +
                                    $">>");
                    break;

                case GameState.Dungeon:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("던전입장\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n" +
                                    "1. 쉬운 던전 | 방어력 5 이상 권장\n" +
                                    "2. 일반 던전 | 방어력 11 이상 권장\n" +
                                    "3. 어려운 던전 | 방어력 17 이상 권장\n" +
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                case GameState.DungeonClear:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("던전 클리어\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("축하합니다!!\n" +
                                    $"{text2}\n\n" + 
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                case GameState.DungeonFailed:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("던전 실패\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("TIP : 좀 더 좋은 장비를 착용한다면?\n" +
                                    $"{text2}\n" + 
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                case GameState.Hospital:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("휴식하기\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"500 G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Status.Gold} G)\n\n" +
                                    $"1. 휴식하기\n" +
                                    "0. 나가기\n\n" +
                                    "원하시는 행동을 입력해주세요.\n" +
                                    ">>");
                    break;
                case GameState.End:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("유다희....\n\n\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("TIP : 좀 더 좋은 장비를 착용한다면?\n");
                    break;
            }

            // 에러 처리, 오류가 발생했을 시 처리
            if (isError)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);
                isError = false;
            }
            // 이미 구매 상품 처리
            if (isBuyed)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n이미 구매한 아이템입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                isBuyed = false;
            }
            if(isEmpty)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nGold가 부족합니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                isEmpty = false;
            }
            if(isHealed)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n휴식을 완료했습니다.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                isHealed = false;
            }
        }


    }

    #region ========== Player ==========

    // 플레이어
    [Serializable]
    public class Player
    {
        public event Action<Equipment>? onSellItem;

        // 이름
        public string Name { get; set; }

        // 스테이터스
        public PlayerStatus Status { get;  set; }

        // 인벤토리
        public Inventory Inven { get;  set; }

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
                if(Inven.GetItem(index) != null)
                {
                    Equipment equipment = Inven.GetItem(index-1);
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
            if(Status.VIT <= 0)
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
            if(Status.Gold >= 500)
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
    [Serializable]
    public class PlayerStatus
    {
        public string Name { get;  set; }
        public int Level { get;  set; }
        public string Chad { get;  set; }

        public int ATK { get;  set; }

        public int DEF { get;  set; }

        public int VIT { get; set; }

        public int Gold { get;  set; }


        public PlayerStatus(string name, int level, string chad, int atk, int def, int vit, int gold)
        {
            Name = name;
            Level = level;
            Chad = chad;
            ATK = atk;
            DEF = def;
            VIT = vit;
            Gold = gold;


            GameManager.onEquipWeapon += SetWeaponAbility;
            GameManager.onEquipArmor += SetArmorAbility;

            GameManager.onDetachWeapon += SetWeaponAbility;
            GameManager.onDetachArmor += SetArmorAbility;

        }

        public void LevelUp(int level)
        {
            Level += level;
            ATK += 1;
            DEF += 1;

        }

        public void SetWeaponAbility(Equipment weapon)
        {
            if (weapon.isEquipped)
            {
                ATK += weapon.ATK;
            }
            else
            {
                ATK -= weapon.ATK;

            }
        }
        public void SetArmorAbility(Equipment armor)
        {
            if (armor.isEquipped)
            {
                DEF += armor.DEF;

            }
            else
            {
                DEF -= armor.DEF;

            }
        }
    }

    #endregion

    #region ========== Item ==========
    public interface IEquipable
    {
        void Equip(Equipment equip);
        void Detach(Equipment equip);
    }

    public enum EquipType
    {
        Weapon,
        Armor
    }

    [Serializable]
    public abstract class Equipment : IEquipable
    {
        public string Name { get; private set; }
        public int ATK { get; private set; }
        public int DEF { get; private set; }

        public string Description { get; private set; }
        public int Price { get; private set; }

        public EquipType type;
        public bool isEquipped = false;
        public bool isSelled = false;



        public Equipment(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled)
        {
            Name = name;
            this.ATK = ATK;
            this.DEF = DEF;
            this.Description = Description;
            this.Price = Price;
            this.isEquipped = isEquipped;
            this.isSelled = isSelled;
        }

        public string IsEquipped()
        {
            if(isEquipped)
            {
                return "[E]";
            }
            else
            {
                return "";
            }
        }

        public string IsSelled()
        {
            if (isSelled)
            {
                return "구매완료";
            }
            else
            {
                return Price.ToString() + " G";
            }
        }

        public virtual void Equip(Equipment equip)
        {
            isEquipped = true;
        }

        public virtual void Detach(Equipment equip)
        {
            isEquipped = false;
        }
    }

    [Serializable]
    public class Weapon : Equipment
    {
        public Weapon(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled) : base(name, ATK, DEF, Description, Price, isEquipped, isSelled)
        {
            type = EquipType.Weapon;
        }

        public override void Equip(Equipment equip)
        {
            base.Equip(equip);

            GameManager.onEquipWeapon?.Invoke(equip);
        }

        public override void Detach(Equipment equip)
        {
            base.Detach(equip);

            GameManager.onDetachWeapon?.Invoke(equip);
        }
    }

    [Serializable]
    public class Armor : Equipment
    {
        public Armor(string name, int ATK, int DEF, string Description, int Price, bool isEquipped, bool isSelled) : base(name, ATK, DEF, Description, Price, isEquipped, isSelled)
        {
            type = EquipType.Armor;
        }

        public override void Equip(Equipment equip)
        {
            base.Equip(equip);

            GameManager.onEquipArmor?.Invoke(equip);
        }

        public override void Detach(Equipment equip)
        {
            base.Detach(equip);

            GameManager.onDetachArmor?.Invoke(equip);
        }
    }
    
    [Serializable]
    public class Inventory
    {
        List<Equipment> items = new List<Equipment>();

        public Equipment? Weapon { get; set; }
        public Equipment? Armor { get; set; }

        public Inventory()
        {
            Armor armor = new Armor("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, false, true);
            Weapon spear = new Weapon("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true);
            Weapon oldSword = new Weapon("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600, false, true);

            Equip(spear);
            Equip(armor);

            SetItem(spear);
            SetItem(armor);
            SetItem(oldSword);


            Store.onBuyItem += SetItem;
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
            item.isSelled = false;

            items.Remove(item);
        }

        public string ShowAllItem()
        {
            string data = String.Empty;
            
            if(GetItemCount() > 0)
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == EquipType.Weapon)
                    {
                        data += $"- {i+1} {items[i].IsEquipped()}{items[i].Name}   | 공격력 +{items[i].ATK} | {items[i].Description}\n";
                    }
                    else
                    {
                        data += $"- {i+1} {items[i].IsEquipped()}{items[i].Name}   | 방어력 +{items[i].DEF} | {items[i].Description}\n";

                    }
                }

                return data;
            }
            else
            {
                return "";
            }
        }

        public string ShowAllItemData()
        {
            string data = "";
            foreach(var item in items)
            {
                data += $"{item.Name},{item.ATK},{item.DEF}, {item.Description},{item.Price},{Enum.GetName(item.type)},{item.isEquipped},{item.isSelled},";
            }
            data += "\n";
            return data;
        }

        public string ShowAllSellItem()
        {
            string data = String.Empty;

            if (GetItemCount() > 0)
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == EquipType.Weapon)
                    {
                        data += $"- {i + 1} {items[i].Name}   | 공격력 +{items[i].ATK} | {items[i].Description}   | {items[i].Price * 0.85} G\n";
                    }
                    else
                    {
                        data += $"- {i + 1} {items[i].Name}   | 방어력 +{items[i].DEF} | {items[i].Description}   | {items[i].Price * 0.85} G\n";

                    }
                }
                return data;
            }
            else
            {
                return "";
            }
        }

        public void Equip(Equipment equip)
        {
            if (!equip.isEquipped)
            {
                equip.isEquipped = true;

                if (equip.type == EquipType.Weapon)
                {
                    if (Weapon == equip) return;

                    equip.Equip(equip);
                    Weapon = equip;

                }
                else if (equip.type == EquipType.Armor)
                {
                    if (Armor == equip) return;

                    equip.Equip(equip);
                    Armor = equip;
                }
            }
            else
            {
                if(Weapon == null)
                {
                    if(equip.type == EquipType.Weapon)
                    {
                        equip.Equip(equip);
                    }
                }
                else if(Armor == null)
                {
                    if (equip.type == EquipType.Armor)
                    {
                        equip.Equip(equip);
                    }
                }
            }
        }

        /*
         * 1.아이템이 장착되어있는 것일 경우
         * 2.장착되어있지 않을경우
         */
        public void SelectItem(int count)
        {
            int selNum = count-1;

            if (items[selNum].isEquipped)
            {
                // 무기일 경우
                if (items[selNum].type == EquipType.Weapon)
                {
                    if(Weapon == items[selNum])
                    {
                        Detach(items[selNum]);
                    }
                }
                // 방어구일 경우
                else
                {
                    if (Armor == items[selNum])
                    {
                        Detach(items[selNum]);
                    }
                }
            }
            else
            {
                if (items[selNum].type == EquipType.Weapon)
                {
                    // 아이템이 존재할 경우
                    if(Weapon != null)
                    {
                        Detach(Weapon);
                        Equip(items[selNum]);
                    }
                    else
                    {
                        Equip(items[selNum]);
                    }
                }
                else
                {
                    if(Armor != null)
                    {
                        Detach(Armor);
                        Equip(items[selNum]);
                    }
                    else
                    {
                        Equip(items[selNum]);
                    }
                }
            }
        }
        public void Detach(Equipment equip)
        {
            if (equip.isEquipped)
            {
                if (equip.type == EquipType.Weapon)
                {
                    if (Weapon == equip)
                    {
                        Weapon = null;
                        equip.Detach(equip);
                    }
                }
                else if (equip.type == EquipType.Armor)
                {
                    if (Armor == equip)
                    {
                        Armor = null;
                        equip.Detach(equip);
                    }
                }
            }
        }
        public int GetWeaponAbility()
        {
            if (Weapon == null)
                return 0;

            return Weapon.ATK;
        }

        public int GetArmorAbility()
        {
            if (Armor == null)
                return 0;

            return Armor.DEF;
        }

    }


    #endregion

    #region ========== Store ==========
    [Serializable]
    public class Store
    {
        List<Equipment> items = new List<Equipment>();

        public static event Action<Equipment>? onBuyItem;

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
                if (items[index-1].isSelled)
                {
                    GameManager.isBuyed = true;
                    return;
                }
                if (items[index-1].Price > GameManager.player.Status.Gold)
                {
                    GameManager.isEmpty = true;
                    return;
                }
                else
                {
                    GameManager.player.SetGold(-items[index - 1].Price);
                    items[index-1].isSelled = true;
                }
            }
            onBuyItem?.Invoke(items[index - 1]);
        }

        // 플레이어가 상점으로부터 아이템을 판매할 때 사용하는 메서드
        public void Sell(Equipment equipment)
        {
            bool isFind = false;
            foreach(var item in items)
            {
                if(equipment.Name == item.Name)
                {
                    item.isSelled = false;
                    isFind = true;
                    break;
                }
                else
                {
                    isFind = false;
                }
            }

        }


        public Store()
        {
            items.Add(new Armor("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
            items.Add(new Armor("무쇠 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 2200, false, true));
            items.Add(new Armor("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            items.Add(new Weapon("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, true));
            items.Add(new Weapon("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
            items.Add(new Weapon("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200, false, true));

            GameManager.player.onSellItem += Sell;
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
                            data += $"- {i+1} {items[i].Name}    | 공격력 +{items[i].ATK}  | {items[i].Description}  | {items[i].IsSelled()}\n";
                        }
                        else
                        {
                            data += $"- {i+1} {items[i].Name}    | 방어력 +{items[i].DEF}  | {items[i].Description}  | {items[i].IsSelled()}\n";
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
            if(item.isSelled)
            {
                return "구매완료";
            }
            else
            {
                return item.Price.ToString();
            }
        }
    }
    #endregion

    #region ========== Dungeon ==========
    
    [Serializable]
    public class Dungeon
    {
        Difficulty difficulty = Difficulty.None;
        public int ClearProbability { get; private set; }
        public void GoToDungeon(Player player, Difficulty difficulty)
        {
            this.difficulty = difficulty;

            switch (difficulty)
            {
                case Difficulty.None:
                    break;

                case Difficulty.Easy:
                    if (player.Status.DEF >= 5)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if(IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;
                case Difficulty.Normal:
                    if (player.Status.DEF >= 11)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if (IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;
                case Difficulty.Hard:
                    if (player.Status.DEF >= 17)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if (IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;

            }
        }

        public bool IsClear()
        {
            Random random = new Random();
            int result = random.Next(0, 100);

            if (ClearProbability == 100)
            {
                return true;
            }

            if (result < ClearProbability)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public string Reward(Player player, bool result)
        {
            if(result)
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        return $"쉬운 던전을 클리어 하였습니다.\n\n" +
                            $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (5 - player.Status.DEF)), (35 + (5 - player.Status.DEF))))}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(1000 + (int)(1000 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";

                    case Difficulty.Normal:
                        return $"일반 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                        $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (11 - player.Status.DEF)), (35 + (11 - player.Status.DEF))))}\n" +
                        $"Gold {player.Status.Gold} -> {player.RewardGold(1700 + (int)(1700 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";
                    case Difficulty.Hard:
                        return $"하드 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                        $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (17 - player.Status.DEF)), (35 + (17 - player.Status.DEF))))}\n" +
                        $"Gold {player.Status.Gold} -> {player.RewardGold(2500 + (int)(2500 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";
                }
            }
            else
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        return $"쉬운 던전을 실패 하였습니다.\n\n" +
                            $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";

                    case Difficulty.Normal:
                        return $"일반 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";
                    case Difficulty.Hard:
                        return $"하드 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";
                }

                return "";
            }
            return "";
        }

    }


    #endregion


}

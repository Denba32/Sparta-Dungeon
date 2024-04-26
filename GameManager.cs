
namespace Sparta_Dungeon
{

    internal class GameManager
    {
        #region ========== Property & Action & Funcs ==========

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

        public static GameState state = GameState.Main;


        // Error 발생 시 True로 만들어서 입력란 밑에 에러를 발생시키는 코드
        public static bool isError = false;

        // 이미 구매한 상품을 구매 시도 시 입력란 밑에 경고를 알려주는 코드
        public static bool isBuyed = false;

        // 구매 시도 시 골드가 부족한 경우 처리
        public static bool isEmpty = false;

        // 힐링 성공 시 힐링을 성공했음을 알림
        public static bool isHealed = false;

        public static bool isLoaded = false;

        // 선택지를 고르는 메서드
        public void SelectOption()
        {
            int selNum;
            if (int.TryParse(Console.ReadLine(), out selNum))
            {
                if (state == GameState.Main)
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

                // 스테이터스 선택지
                else if (state == GameState.Status)
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

                // 인벤토리 선택지
                else if (state == GameState.Inventory)
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

                // 아이템 관리
                else if (state == GameState.ItemManagement)
                {
                    if (player.Inven.GetItemCount() >= selNum && selNum != 0)
                    {
                        player.Inven.SelectItem(selNum);

                    }
                    else if (selNum > player.Inven.GetItemCount())
                    {
                        isError = true;
                    }
                    else if (selNum == 0)
                    {
                        state = GameState.Inventory;
                    }
                }

                // 스토어
                else if (state == GameState.Store)
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

                // 아이템 구매 시
                else if (state == GameState.BuyItem)
                {
                    if (store.GetItemCount() >= selNum && selNum != 0)
                    {
                        store.Buy(selNum);
                    }
                    else if (selNum > store.GetItemCount())
                    {
                        isError = true;
                    }
                    else if (selNum == 0)
                    {
                        state = GameState.Store;
                    }
                }

                // 아이템 판매 시
                else if (state == GameState.SellItem)
                {
                    if (player.Inven.GetItemCount() >= selNum && selNum != 0)
                    {
                        player.Sell(selNum);
                    }
                    else if (selNum > player.Inven.GetItemCount())
                    {
                        isError = true;
                    }
                    else if (selNum == 0)
                    {
                        state = GameState.Store;
                    }
                }

                // 던전 입장
                else if (state == GameState.Dungeon)
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

                // 던전 클리어
                else if (state == GameState.DungeonClear)
                {
                    switch (selNum)
                    {
                        // DungeonClear -> Main
                        case 0:
                            state = GameState.Main;
                            break;

                        default:
                            isError = true;

                            break;
                    }
                }

                // 던전 실패
                else if (state == GameState.DungeonFailed)
                {
                    switch (selNum)
                    {
                        // DungeonClear -> Main
                        case 0:
                            state = GameState.Main;
                            break;

                        default:
                            isError = true;
                            break;
                    }
                }

                // 휴식처
                else if (state == GameState.Hospital)
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

                            break;
                    }
                }
            }
            else
            {
                isError = true;
            }
        }


        // 화면 정보 표시 메서드
        public void Display(string text1 = " ", string text2 = " ", GameState state = GameState.None)
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
            if (isEmpty)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nGold가 부족합니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                isEmpty = false;
            }
            if (isHealed)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n휴식을 완료했습니다.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                isHealed = false;
            }
        }

        #endregion
        static void Main(string[] args)
        {
            // 거슬리는 커서의 움직임을 없애기 위해서 Visible을 False로
            Console.CursorVisible = false;

            GameManager gameManager = new GameManager();
            DataManager dataManager = new DataManager();

            if (dataManager.Load<Player>() != null)
            {
                isLoaded = true;

                player = dataManager.Load<Player>();
                store = dataManager.Load<Store>();

            }
            else
            {
                isLoaded = false;
            }
            if (dataManager.Load<Store>() != null)
            {
                store = dataManager.Load<Store>();
            }

            // GameManager 속 전체적인 흐름
            // END상태가 될 경우 게임이 끝난다.
            while (state != GameState.End)
            {
                dataManager.Save(GameManager.player);
                dataManager.Save(GameManager.store);

                if (state == GameState.Main)
                {
                    gameManager.Display("", "", GameState.Main);
                }

                else if (state == GameState.Status)
                {
                    gameManager.Display(player.ShowAllStatus(), "", GameState.Status);
                }

                else if (state == GameState.Inventory)
                {
                    gameManager.Display("", "", GameState.Inventory);
                }

                else if (state == GameState.ItemManagement)
                {
                    gameManager.Display(player.Inven.ShowAllItem(), "", GameState.ItemManagement);
                }

                else if (state == GameState.Store)
                {
                    gameManager.Display(store.ShowItemlist(false), "", GameState.Store);
                }

                else if (state == GameState.BuyItem)
                {
                    gameManager.Display(store.ShowItemlist(true), "", GameState.BuyItem);

                }
                else if (state == GameState.SellItem)
                {
                    gameManager.Display(player.Inven.ShowAllSellItem(), "", GameState.SellItem);

                }
                else if (state == GameState.Dungeon)
                {
                    gameManager.Display("", "", GameState.Dungeon);
                }
                else if (state == GameState.DungeonClear)
                {
                    gameManager.Display("", dungeon.Reward(player, true), GameState.DungeonClear);

                }
                else if (state == GameState.DungeonFailed)
                {
                    gameManager.Display("", dungeon.Reward(player, false), GameState.DungeonFailed);
                }
                else if (state == GameState.Hospital)
                {
                    gameManager.Display("", "", GameState.Hospital);

                }
                // Display 후 선택 기회 전달
                gameManager.SelectOption();
            }
            gameManager.Display("", "", GameState.End);
        }
    }
}

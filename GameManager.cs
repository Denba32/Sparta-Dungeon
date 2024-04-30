using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;

namespace Sparta_Dungeon
{
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padLock = new object();

        GameManager()
        {

        }

        public static GameManager Instance
        {
            get
            {
                lock (padLock)
                {
                    if(instance == null)
                    {
                        instance = new GameManager();
                    }
                }
                return instance;
            }
        }

        private DataManager data = new DataManager();

        private UIManager ui = new UIManager();
        private EventManager _event = new EventManager();
        private Player player = new Player();

        public DataManager Data { get => Instance.data; }
        public UIManager UI { get => Instance.ui; }
        public EventManager Event { get => Instance._event; }
        public Player Player { get => Instance.player; }

        public SceneManager Scene = new SceneManager();

        public Define.GameState state = GameState.Main;


        // Error 발생 시 True로 만들어서 입력란 밑에 에러를 발생시키는 코드
        public bool isError = false;

        // 이미 구매한 상품을 구매 시도 시 입력란 밑에 경고를 알려주는 코드
        public bool isBuyed = false;

        // 구매 시도 시 골드가 부족한 경우 처리
        public bool isEmpty = false;

        // 힐링 성공 시 힐링을 성공했음을 알림
        public bool isHealed = false;

        public bool isLoaded = false;


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
                    //if (store.GetItemCount() >= selNum && selNum != 0)
                    //{
                    //    store.Buy(selNum);
                    //}
                    //else if (selNum > store.GetItemCount())
                    //{
                    //    isError = true;
                    //}
                    //else if (selNum == 0)
                    //{
                    //    state = GameState.Store;
                    //}
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
                            // dungeon.GoToDungeon(player, Difficulty.Easy);
                            break;
                        // Dungeon -> Normal
                        case 2:
                            // dungeon.GoToDungeon(player, Difficulty.Normal);
                            break;
                        // Dungeon -> Hard
                        case 3:
                            // dungeon.GoToDungeon(player, Difficulty.Hard);

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
            GameStart();
        }

        public void GameStart()
        {
            Scene.StartScene();
            while (true)
            {
                Data.Save<PlayerData>(Player.PlayerData);
                Data.Save<Inventory>(Player.Inven);
                // TODO 자동 저장, 플레이어 정보, 인벤 정보, 상점 정보,
                // 던전 클리어 횟수 정보
                Scene.TownScene();
            }
        }
    }
}

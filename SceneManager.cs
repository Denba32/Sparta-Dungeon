using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparta_Dungeon.Define;

namespace Sparta_Dungeon
{
    public class SceneManager
    {
        // 시작 화면
        public void StartScene()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }

            GameManager.Instance.UI.SelectGuide(3);
            Console.WriteLine("       아무키나 눌러서 시작하세요");

            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }


            Console.ReadKey();
        }
        // 로그인 화면
        public void LoginScene()
        {
            GameManager.Instance.UI.TiteleText("로그인");
            GameManager.Instance.UI.LoreText("캐릭터를 생성합니다.");

            GameManager.Instance.UI.InputText("당신의 이름을 입력해주세요");
            GameManager.Instance.Player.PlayerData.Name = Console.ReadLine();

            GameManager.Instance.UI.TiteleText("로그인");
            GameManager.Instance.UI.LoreText("캐릭터를 생성합니다.");

            GameManager.Instance.UI.SelectGuide(5);
            Console.WriteLine($"   당신의 이름은 {GameManager.Instance.Player.PlayerData.Name}");
            Console.WriteLine("   이 이름으로 하시겠습니까?");

            GameManager.Instance.UI.SelectGuide(2);
            Console.Write("   0.아니오");
            Console.Write("       1.예");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        LoginScene();
                        break;
                    case 1:
                        SelectChar();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        LoginScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                LoginScene();
            }
        }
        // 캐릭터 선택 화면
        public void SelectChar()
        {
            GameManager.Instance.UI.TiteleText("캐릭터 선택");
            GameManager.Instance.UI.LoreText("원하는 캐릭터를 선택합니다.");

            GameManager.Instance.UI.SelectGuide(10);
            Console.WriteLine("   1.전사");
            Console.WriteLine("   2.궁수");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 1:
                        GameManager.Instance.Player.SetChad("전사");
                        break;
                    case 2:
                        GameManager.Instance.Player.SetChad("궁수");
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        SelectChar();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                SelectChar();
            }
        }
        // 마을 화면
        public void TownScene()
        {
            
            GameManager.Instance.UI.TiteleText("마을", ConsoleColor.Green);            
            GameManager.Instance.UI.LoreText("스파르타 마을에 오신 여러분 환영합니다.", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(4);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   1.상태 보기");
            Console.WriteLine("   2.인벤토리");
            Console.WriteLine("   3.상점");
            Console.WriteLine($"   4.던전 입장 : (현재 {GameManager.Instance.UI.dungeonFloor}층)");
            Console.WriteLine("   5.휴식하기");

            GameManager.Instance.UI.InputText(ConsoleColor.Green);

            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 1:
                        StatusScene();
                        break;
                    case 2:
                        InventoryScene();
                        break;
                    case 3:
                        StoreScene();
                        break;
                    case 4:
                        DungeonSelectScene();
                        break;
                    case 5:
                        HospitalScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
            }
        }
        // 상태 화면
        void StatusScene()
        {
            GameManager.Instance.UI.TiteleText("상태 보기", ConsoleColor.Cyan);
            GameManager.Instance.UI.LoreText("캐릭터의 정보가 표시됩니다.");

            GameManager.Instance.Player.ShowAllStatus();

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        StatusScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                StatusScene();
            }
        }
        // 인벤토리 화면
        void InventoryScene()
        {
            GameManager.Instance.UI.TiteleText("인벤토리");
            GameManager.Instance.UI.LoreText("보유중인 아이템을 관리할 수 있습니다.", "[아이템 목록]");

            GameManager.Instance.Event.ShowItemList();

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.장착 관리");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        ItemManageScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        InventoryScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                InventoryScene();
            }
        }
        // 인벤토리 관리화면(기능 추가 필요)
        void ItemManageScene()
        {
            GameManager.Instance.UI.TiteleText("인벤토리");
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력해서 장착/해제할 수 있습니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)
            //기능 추가 필요(아이템 장착 기능)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        InventoryScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        ItemManageScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                ItemManageScene();
            }
        }
        // 상점 화면(기능 추가 필요)
        void StoreScene()
        {
            GameManager.Instance.UI.TiteleText("상점");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("필요한 아이템을 얻을 수 있는 상점입니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(2);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.아이템 구매");
            Console.WriteLine("   2.아이템 판매");

            //기능 추가 필요(아이템 목록)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        BuyItemScene();
                        break;
                    case 2:
                        SellItemScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        StoreScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                StoreScene();
            }
        }
        // 아이템 구매 화면(기능 추가 필요)
        void BuyItemScene()
        {
            GameManager.Instance.UI.TiteleText("상점 - 아이템 구매");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 구매합니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            // 기능 추가 필요(아이템 구매)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        BuyItemScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                BuyItemScene();
            }
        }
        // 아이템 판매 화면(기능 추가 필요)
        void SellItemScene()
        {
            GameManager.Instance.UI.TiteleText("상점 - 아이템 판매");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 판매합니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            //기능 추가 필요(아이템 판매)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        SellItemScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                SellItemScene();
            }
        }
        // 던전 선택창
        void DungeonSelectScene()
        {
            GameManager.Instance.UI.TiteleText("던전 입구");
            GameManager.Instance.UI.LoreText("던전에 입장할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine($"   1.던전 입장 : (현재 {GameManager.Instance.UI.dungeonFloor}층)");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        BattleScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        DungeonSelectScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                DungeonSelectScene();
            }
        }
        // 전투 화면(제작중)
        void BattleScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("!!!전투!!!");
                
                //몬스터 

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattelActionText();

                GameManager.Instance.UI.NowFloorText();

                GameManager.Instance.UI.InputText();
                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    switch (int.Parse(input))
                    {
                        case 0:
                            return;
                        case 1:
                            BattleAttackScene();
                            break;
                        case 2:
                            BattleItemScene();
                            break;
                        case 3:
                            WinScene();
                            return;
                        case 4:
                            DieScene();
                            return;
                        default:
                            GameManager.Instance.UI.ErrorText();
                            break;
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }
        }
        void BattleAttackScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("공격하기");
                // 적 표시 기능 추가하기

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattelAttackText();

                GameManager.Instance.UI.NowFloorText();

                GameManager.Instance.UI.InputText();
                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    switch (int.Parse(input))
                    {
                        case 0:
                            return;
                        case 1:
                            BattleLogScene();
                            return;
                        default:
                            GameManager.Instance.UI.ErrorText();
                            break;
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }

            }
        }
        //전투중 아이템 선택창
        void BattleItemScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("아이템 사용");
                //적 표시 기능 추가하기

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattleItemText();

                GameManager.Instance.UI.NowFloorText();

                while (true)
                {
                    GameManager.Instance.UI.InputText();
                    string input = Console.ReadLine();

                    if (GameManager.Instance.UI.IsNumTest(input))
                    {
                        switch (int.Parse(input))
                        {
                            case 0:
                                return;
                            default:
                                GameManager.Instance.UI.ErrorText();
                                break;
                        }
                    }
                    else
                    {
                        GameManager.Instance.UI.ErrorText();
                    }
                }
            }
        }
        void BattleLogScene()
        {
            GameManager.Instance.UI.TiteleText("!!!적의 행동!!!");
            //적 표시 기능 추가하기

            GameManager.Instance.UI.PlayerUI();

            GameManager.Instance.UI.BattleLogText();

            GameManager.Instance.UI.NowFloorText();

            GameManager.Instance.UI.InputText("아무 키나 눌러서 다음 화면으로");
            Console.ReadKey();

        }
        // 휴식 화면(기능 추가 필요)
        void HospitalScene()
        {
            GameManager.Instance.UI.TiteleText("휴식하기");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("500 G로 체력을 화복할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.휴식하기");

            //기능 추가 필요(체력 회복)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        GameManager.Instance.UI.SystemText("   미구현!!!");
                        HospitalScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        HospitalScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                HospitalScene();
            }
        }
        // 승리 화면(기능 추가 필요)
        void WinScene()
        {
            GameManager.Instance.UI.TiteleText("!!!승리!!!");
            GameManager.Instance.UI.LoreText("전투에서 승리하셨습니다!");

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.마을로 돌아가기");
            Console.WriteLine("   1.다음 층으로");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        GameManager.Instance.UI.dungeonFloor++;
                        BattleScene();                        
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        WinScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                WinScene();
            }
        }
        // 사망화면(기능 추가 필요)
        void DieScene()
        {
            GameManager.Instance.UI.dungeonFloor = 1;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            GameManager.Instance.UI.TiteleText("YOU DIED...");
            GameManager.Instance.UI.DieText();
            GameManager.Instance.UI.InputText("아무키나 눌러서 마을로...");
            
            // 사망 패널티등등 구현하기

            Console.ReadKey();
        }

        
    }
}

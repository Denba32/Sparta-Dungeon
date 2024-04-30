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
        public Define.GameState state = GameState.Main;
        private Player player = new Player();

        UIManager Ui = new UIManager();

        //씬 템플릿
        public void Templet()
        {
            Ui.TiteleText("제목을 입력");
            Ui.LoreText("설명을 입력");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    default:
                        Ui.ErrorText();

                        break;
                }
            }
            else
            {
                Ui.ErrorText();

            }
        }
        //시작 화면
        public void StartScene()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }

            Ui.SelectGuide(3);
            Console.WriteLine("       아무키나 눌러서 시작하세요");

            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }


            Console.ReadKey();
        }

        //로그인 화면
        public void LoginScene()
        {
            Ui.TiteleText("로그인");
            Ui.LoreText("");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요

            Ui.InputText();
            string input = Console.ReadLine();

        }
        //마을 화면
        public void TownScene()
        {
            Ui.TiteleText("마을");
            Ui.LoreText("스파르타 마을에 오신 여러분 환영합니다.","이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            Ui.SelectGuide(4);
            Console.WriteLine("   1.상태 보기");
            Console.WriteLine("   2.인벤토리");
            Console.WriteLine("   3.상점");
            Console.WriteLine("   4.던전 입장");
            Console.WriteLine("   5.휴식하기");

            Ui.InputText();


            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
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
                        Ui.ErrorText();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
            }
        }
        //스탯 화면(기능 추가 필요)
        public void StatusScene()
        {
            Ui.TiteleText("상태 보기");
            Ui.LoreText("캐릭터의 정보가 표시됩니다.");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(캐릭터 정보 출력)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    default:
                        Ui.ErrorText();
                        StatusScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                StatusScene();
            }
        }
        //인벤토리 화면(기능 추가 필요)
        public void InventoryScene()
        {
            Ui.TiteleText("인벤토리");
            Ui.LoreText("보유중인 아이템을 관리할 수 있습니다.","[아이템 목록]");

            Ui.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.장착 관리");

            //기능 추가 필요(아이템 목록)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        ItemManageScene();
                        break;
                    default:
                        Ui.ErrorText();
                        InventoryScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                InventoryScene();
            }
        }
        //인벤토리 관리화면(기능 추가 필요)
        public void ItemManageScene()
        {
            Ui.TiteleText("인벤토리");
            Ui.LoreText("원하는 아이템의 번호를 입력해서 장착/해제할 수 있습니다.","[아이템 목록]");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)
            //기능 추가 필요(아이템 장착 기능)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        InventoryScene();
                        break;
                    default:
                        Ui.ErrorText();
                        ItemManageScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                ItemManageScene();
            }
        }
        //상점 화면(기능 추가 필요)
        public void StoreScene()
        {
            Ui.TiteleText("상점");
            Ui.GoldText();
            Ui.LoreText("필요한 아이템을 얻을 수 있는 상점입니다.", "[아이템 목록]");

            Ui.SelectGuide(2);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.아이템 구매");
            Console.WriteLine("   2.아이템 판매");

            //기능 추가 필요(아이템 목록)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
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
                        Ui.ErrorText();
                        StoreScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                StoreScene();
            }
        }
        //아이템 구매 화면(기능 추가 필요)
        public void BuyItemScene()
        {
            Ui.TiteleText("상점 - 아이템 구매");
            Ui.GoldText();
            Ui.LoreText("원하는 아이템의 번호를 입력하면 구매합니다.", "[아이템 목록]");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            //기능 추가 필요(아이템 구매)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        Ui.ErrorText();
                        BuyItemScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                BuyItemScene();
            }
        }
        //아이템 판매 화면(기능 추가 필요)
        public void SellItemScene()
        {
            Ui.TiteleText("상점 - 아이템 판매");
            Ui.GoldText();
            Ui.LoreText("원하는 아이템의 번호를 입력하면 판매합니다.", "[아이템 목록]");

            Ui.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            //기능 추가 필요(아이템 판매)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        Ui.ErrorText();
                        SellItemScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                SellItemScene();
            }
        }
        //던전 선택창(던전 추가 필요)
        public void DungeonSelectScene()
        {
            Ui.TiteleText("던전 입구");
            Ui.LoreText("던전에 입장할 수 있습니다.");

            Ui.SelectGuide(3);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.쉬운 던전");
            Console.WriteLine("   2.보통 던전");
            Console.WriteLine("   3.어려운 던전");

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        Ui.SystemText("   미구현!!!");
                        DungeonSelectScene();
                        break;
                    case 2:
                        Ui.SystemText("   미구현!!!");
                        DungeonSelectScene();
                        break;
                    case 3:
                        Ui.SystemText("   미구현!!!");
                        DungeonSelectScene();
                        break;
                    default:
                        Ui.ErrorText();
                        DungeonSelectScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                DungeonSelectScene();
            }
        }
        //휴식 화면(기능 추가 필요)
        public void HospitalScene()
        {
            Ui.TiteleText("휴식하기");
            Ui.GoldText();
            Ui.LoreText("500 G로 체력을 화복할 수 있습니다.");

            Ui.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.휴식하기");

            //기능 추가 필요(체력 회복)

            Ui.InputText();
            string input = Console.ReadLine();

            if (Ui.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        Ui.SystemText("   미구현!!!");
                        HospitalScene();
                        break;
                    default:
                        Ui.ErrorText();
                        HospitalScene();
                        break;
                }
            }
            else
            {
                Ui.ErrorText();
                HospitalScene();
            }
        }
    }
}

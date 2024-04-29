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
                        state = GameState.Inventory;
                        break;
                    case 3:
                        state = GameState.Store;
                        break;
                    case 4:
                        state = GameState.Dungeon;
                        break;
                    case 5:
                        state = GameState.Hospital;
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
        //스탯 화면
        public void StatusScene()
        {
            Ui.TiteleText("상태 보기");
            Ui.LoreText("캐릭터의 정보가 표시됩니다.");
            player.ShowAllStatus();
            string input = Console.ReadLine();
        }
    }
}

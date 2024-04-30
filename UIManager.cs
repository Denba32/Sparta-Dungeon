using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Sparta_Dungeon.Define;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    public class UIManager
    {
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
                    Console.Write($"500 G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.Instance.Player.Status.Gold} G)\n\n" +
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
        }

        //매개변수에 씬의 제목을 입력시 화면 최상단에 출력
        public void TiteleText(string str)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(8, 1);
            Console.WriteLine(str);
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
        }
        //매개변수에 씬의 설명을 입력하면 화면 상단에 출력
        public void LoreText(string str, string str2 = null)
        {
            Console.SetCursorPosition(3, 4);
            Console.WriteLine(str);
            Console.WriteLine("");
            if (str2 != null)
            {
                Console.SetCursorPosition(3, 6);
                Console.WriteLine(str2);
                Console.WriteLine("");
            }
        }
        //선택지의 최대 번호를 입력시 선택지들을 입력텍스트의 바로 위로 출력되게 해줌
        public void SelectGuide(int num)
        {
            Console.SetCursorPosition(0, 22 - num);
        }
        //입력 텍스트를 화면 하단에 출력
        public void InputText()
        {

            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("   원하시는 번호를 입력해주세요");
            Console.WriteLine("");
            Console.Write("   현재 입력 : ");
            Console.CursorVisible = false;
        }
        //input이 int인지 검사
        public bool IsNumTest(string str)
        {
            int num;
            return int.TryParse(str, out num);
        }
        //매개변수에 입력된 문자열을 하단에 출력
        public void SystemText(string str)
        {
            Console.SetCursorPosition(0, 28);
            Console.WriteLine(str);
            Thread.Sleep(300);
        }
        //오류메세지를 하단에 출력
        public void ErrorText()
        {
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("   잘못된 입력입니다!!!");
            Thread.Sleep(300);
        }
        public void GoldText()
        {
            Console.SetCursorPosition(30, 6);
            Console.WriteLine($"[보유중인 골드] : {0} G");
        }
    }
}
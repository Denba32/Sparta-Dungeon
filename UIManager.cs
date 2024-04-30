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
        //로그인 화면에 출력하는 입력텍스트 양식
        public void LoginText()
        {

            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("   당신의 이름을 입력해주세요");
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
        //보유 골드를 화면 우상단에 표시
        public void GoldText()
        {
            Console.SetCursorPosition(30, 6);
            Console.WriteLine($"[보유중인 골드] : {GameManager.Instance.Player.PlayerData.Gold} G");
        }
        //사망메세지
        public void DieText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("유다희....\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("TIP : 좀 더 좋은 장비를 착용한다면?");
        }
    }
}
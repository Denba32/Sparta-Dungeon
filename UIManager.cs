using System.Drawing;

namespace Sparta_Dungeon
{
    public class UIManager
    {
        // 던전 층 수로 쓸 변수
        public int dungeonFloor = 1;

        // 매개변수에 씬의 제목을 입력시 화면 최상단에 출력
        public void TiteleText(string str, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.Black)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = color;
            Console.BackgroundColor = color2;
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
            ConsoleColorReset();
        }
        // 매개변수에 씬의 설명을 입력하면 화면 상단에 출력
        public void LoreText(string str, string str2 = null, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.White)
        {
            Console.SetCursorPosition(3, 4);
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.WriteLine("");
            ConsoleColorReset();
            if (str2 != null)
            {
                Console.SetCursorPosition(3, 6);
                Console.ForegroundColor = color2;
                Console.WriteLine(str2);
                Console.WriteLine("");
                ConsoleColorReset();
            }
        }
        // 선택지의 최대 번호를 입력시 선택지들을 입력텍스트의 바로 위로 출력되게 해줌
        public void SelectGuide(int num)
        {
            Console.SetCursorPosition(0, 22 - num);
        }
        // 입력 텍스트를 화면 하단에 출력
        public void InputText(ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("   원하시는 행동의 번호를 입력해주세요");
            Console.WriteLine("");
            Console.Write("   현재 입력 : ");
            Console.CursorVisible = false;
            ConsoleColorReset();
        }
        // 입력 텍스트를 원하는 글자로 화면 하단에 출력
        public void InputText(string str, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine($"   {str}");
            Console.WriteLine("");
            Console.Write("   현재 입력 : ");
            Console.CursorVisible = false;
        }
        // input이 int인지 검사
        public bool IsNumTest(string str)
        {
            int num;
            return int.TryParse(str, out num);
        }
        // 매개변수에 입력된 문자열을 하단에 출력
        public void SystemText(string str, ConsoleColor color = ConsoleColor.White, int num = 300)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(0, 28);
            Console.WriteLine(str);
            Thread.Sleep(num);
            ConsoleColorReset();
        }
        // 퀘스트 보상 출력
        public void RewardText(int num, ConsoleColor color = ConsoleColor.Cyan, ConsoleColor color2 = ConsoleColor.Yellow, int num2 = 400)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(3, 28);
            Console.Write("퀘스트를 클리어하여, ");
            Console.ForegroundColor = color2;
            Console.Write(num);
            Console.ForegroundColor = color;
            Console.WriteLine("G를 획득하셨습니다!!!");
            Thread.Sleep(num2);
            ConsoleColorReset();
        }
        // 오류메세지를 하단에 출력
        public void ErrorText()
        {
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("   잘못된 입력입니다!!!");
            Thread.Sleep(300);
        }
        // 내용을 받아서 출력하는 메서드
        public void ErrorText(string str) 
        {
            Console.SetCursorPosition(0, 28);
            Console.WriteLine($"   {str}");
            Thread.Sleep(300);
        }
        // 보유 골드를 화면 우상단에 표시
        public void GoldText()
        {
            Console.SetCursorPosition(30, 6);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[보유중인 골드] : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{GameManager.Instance.Player.PlayerData.Gold}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" G");
            ConsoleColorReset();
        }
        // 몬스터 정보 출력
        public void EnemyText()
        {
            Console.SetCursorPosition(0, 4);
            GameManager.Instance.Event.EnterDungeon();
            Console.WriteLine("");
        }
        // 캐릭터 정보 출력
        public void PlayerUI()
        {
            Console.SetCursorPosition(0, 9);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(3, 10 );
            Console.WriteLine($"[{GameManager.Instance.Player.PlayerData.Name}]");
            Console.SetCursorPosition(3, 12);
            Console.WriteLine($"Lv . {GameManager.Instance.Player.PlayerData.Level}    ({GameManager.Instance.Player.PlayerData.Chad})");
            Console.SetCursorPosition(3, 13);
            Console.WriteLine($"HP  {GameManager.Instance.Player.PlayerData.Vit}");
            Console.SetCursorPosition(0, 14);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
        }

        public void DamagedPlayerUI(string prevHP)
        {
            Console.SetCursorPosition(0, 9);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }
            Console.SetCursorPosition(3, 10);
            Console.WriteLine($"[{GameManager.Instance.Player.PlayerData.Name}]");
            Console.SetCursorPosition(3, 12);
            Console.WriteLine($"Lv . {GameManager.Instance.Player.PlayerData.Level}    ({GameManager.Instance.Player.PlayerData.Chad})");
            Console.SetCursorPosition(3, 13);
            Console.WriteLine($"HP  {prevHP} -> {GameManager.Instance.Player.PlayerData.Vit}");
            Console.SetCursorPosition(0, 14);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }

        }
        // 던전의 층을 표시
        public void NowFloorText()
        {
            Console.SetCursorPosition(3, 22);
            Console.WriteLine($"현재 층 수 : {dungeonFloor}");
        }
        // 사망메세지
        public void DieText()
        {
            List<string> list = new List<string>();
            list.Add("TIP : 좀 더 좋은 장비를 착용한다면?");
            list.Add($"{GameManager.Instance.Player.PlayerData.Name}!!! 어서 일어서거라!!! 일어서!!! 어서 일어서!!!");
            list.Add("허~~~~~~~~접, 이정도밖에 안되는거야?");
            list.Add("TIP : 레벨을 올려서 물리로 때리면 된다.");

            Random random = new Random();
            int num = random.Next(0,4);
            Console.SetCursorPosition(8, 5);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("유다희......");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(3, 10);
            Console.WriteLine(list[num]);
        }


        #region ========== 선택지 문구를 띄워주는 메서드 ==========

        // 전투 선택지를 띄워줌
        public void BattelActionText()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine($"   1.공격한다.");
            Console.WriteLine($"   2.스킬.");

            Console.WriteLine($"   0.도망간다.");


        }
        // 전투 - 공격 선택지
        public void BattelAttackText()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine($"   0.돌아간다.");
            Console.SetCursorPosition(0, 16);
            // 공격 스킬등 추가 
        }
     
        // 전투 - 아이템 사용 선택지
        public void BattleItemText()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine($"   0.돌아간다.");
            Console.SetCursorPosition(0, 16);
            // 아이템 목록 추가
        }

        public void BattleNextText()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine($"   0.다음.");
            Console.SetCursorPosition(0, 16);
        }

        // 플레이어의 공격 후 몬스터의 데미지 결과를 출력
        public void BattleResultText(string result)
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(result);
            Console.WriteLine("");
        }

        #endregion

        public void BattleLogText()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("여기에 플레이어의 행동을 출력");
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("여기부터 아래로 적의 행동을 출력");
        }
        public void BattleLogText(string text)
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(text);
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("여기에 플레이어의 행동을 출력");
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("여기부터 아래로 적의 행동을 출력");
        }
        public void ConsoleColorReset()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
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
                    Console.Write($"{GameManager.Instance.Player.Status.Gold} G\n\n");
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
                    Console.Write($"{GameManager.Instance.Player.Status.Gold} G\n\n");
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
                    Console.Write($"{GameManager.Instance.Player.Status.Gold} G\n\n");
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
            // display >>

            // 에러 처리, 오류가 발생했을 시 처리
            if (GameManager.Instance.isError)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);
                GameManager.Instance.isError = false;
            }
            // 이미 구매 상품 처리
            if (GameManager.Instance.isBuyed)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n이미 구매한 아이템입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                GameManager.Instance.isBuyed = false;
            }
            if (GameManager.Instance.isEmpty)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nGold가 부족합니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                GameManager.Instance.isEmpty = false;
            }
            if (GameManager.Instance.isHealed)
            {
                // 입력칸 아래에 작성
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n휴식을 완료했습니다.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top - 1);

                GameManager.Instance.isHealed = false;
            }
        }

    }
}
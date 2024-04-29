
using static Sparta_Dungeon.Define;

namespace Sparta_Dungeon
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // 거슬리는 커서의 움직임을 없애기 위해서 Visible을 False로
            Console.CursorVisible = false;

            GameManager.Instance.GameStart();
        }
    }
}

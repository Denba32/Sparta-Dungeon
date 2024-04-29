
using static Sparta_Dungeon.Define;

namespace Sparta_Dungeon
{

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameManager.Instance.GameStart();
            }
        }
    }
}

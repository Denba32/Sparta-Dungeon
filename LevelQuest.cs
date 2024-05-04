using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    internal class LevelQuest : Quest
    {
        public int count = 0;
        public int RequireCount { get; set; }
        public LevelQuest(string title, string description, int reward, int require) : base(title, description, reward)
        {
            Type = Define.QuestType.LevelupQuest;
            State = Define.QuestState.CanAccept;
            RequireCount = require;
        }
        public override void Notify()
        {
            if (GameManager.Instance.Player.PlayerData.Level >= RequireCount)
            {
                State = Define.QuestState.ClearQuest;
            }
        }
        public override void RequireText(int num)
        {

            Console.SetCursorPosition(4, 14);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("아마도 ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"레벨");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("을 ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{RequireCount}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" 까지 올리고 와야할 것 같다.");
            Console.ForegroundColor = ConsoleColor.White;
            if (State == Define.QuestState.AcceptQuest)
            {
                Console.SetCursorPosition(6, 16);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"[진행 상황] : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{GameManager.Instance.Player.PlayerData.Level} / {RequireCount} ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Lv");
            }
        }
    }
}

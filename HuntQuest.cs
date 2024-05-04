using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class HuntQuest : Quest
    {
        public int count = 0;
        public int RequireCount { get; set; }
        public string Target {  get; set; }

        public HuntQuest(string title, string description, int reward, string enemyName,int requireCount) : base(title, description, reward)
        {
            Type = Define.QuestType.HuntQuest;
            State = Define.QuestState.CanAccept;
            Target = enemyName;
            RequireCount = requireCount;
        }
        public override void Notify()
        {
            DungeonData dungeon = new DungeonData();

            // 전투가 끝났을 시타겟과 같은 이름을 가진 몬스터의 갯수를 세서 카운트에 더해주기만 하면 됨

            if (State == Define.QuestState.CanAccept)
            {
                count = 0;
            }
            else if (RequireCount >= count)
            {
                count = RequireCount;
                State = Define.QuestState.ClearQuest;
            }
        }
        public override void RequireText(int num)
        {
            DungeonData dungeon = new DungeonData();
            string target = "";

            for (int i = 0; i < dungeon.enemies.Count; i++)
            {
                if (Target == dungeon.enemies[i].Name)
                {
                    target = dungeon.enemies[i].Name;
                    break;
                }
            }

            Console.SetCursorPosition(4, 14);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("아마도 ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"[{target}]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" 을(를) ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{RequireCount}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" 마리 잡아 오라고 하는 것 같다.");
            Console.ForegroundColor = ConsoleColor.White;
            if(State == Define.QuestState.AcceptQuest)
            {
                Console.SetCursorPosition(6, 16);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"[진행 상황] : [{target}] ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{0} / {RequireCount} ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("마리");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class QuestManager
    {
        public List<Quest> quests = new List<Quest>();
        public QuestManager()
        {
            quests.Add(new HuntQuest("마을, 위협, 미니언, 처치", "마을, 위협, 미니언, 많은, 미니언, 5마리, 처치.",1500 , "미니언", 5));
            quests.Add(new EquipQuest("장비, 착용", "바깥, 위험, 장비, 무쇠갑옷, 착용, 안전.", 500, 2));
            quests.Add(new LevelQuest("레벨, 수련", "수련, 필요, 강함, 좋음, 레벨, 5, 필요.", 2500, 5));
        }

        public void ShowQuestList()
        {
            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i].State == Define.QuestState.RewardQuest)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(0, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[ ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{1 + i}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" ]");
                    Console.SetCursorPosition(6, 8 + i);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(quests[i].Title);
                }
                else
                {
                    Console.SetCursorPosition(0, 8 + i);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[ ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{1 + i}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" ]");
                    GameManager.Instance.UI.ConsoleColorReset();
                    Console.SetCursorPosition(6, 8 + i);
                    Console.WriteLine(quests[i].Title);
                }
            }
        }
        public void ShowQuestInfo(int num)
        {
            Console.SetCursorPosition(4, 8);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{quests[num - 1].Title}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(4, 10);
            Console.WriteLine(quests[num - 1].Description);

            quests[num - 1].RequireText(num);
            Console.SetCursorPosition(5, 12);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"보수금 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{quests[num - 1].Reward} ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("G");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void QuestSelectText(int num)
        {
            Console.SetCursorPosition(3, 19);
            if (quests[num - 1].State == Define.QuestState.CanAccept)
            {
                Console.SetCursorPosition(3, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7, 19);
                Console.Write("수락한다.");
                Console.SetCursorPosition(17, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(21, 19);
                Console.WriteLine("거절한다.");

            }
            else if (quests[num - 1].State == Define.QuestState.AcceptQuest)
            {
                Console.SetCursorPosition(3, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7, 19);
                Console.Write("포기한다.");
                Console.SetCursorPosition(17, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.SetCursorPosition(21, 19);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("퀘스트를 완료한다.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (quests[num - 1].State == Define.QuestState.ClearQuest)
            {
                Console.SetCursorPosition(3, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7, 19);
                Console.Write("포기한다.");
                Console.SetCursorPosition(17, 19);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]");
                Console.SetCursorPosition(21, 19);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("퀘스트를 완료한다.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("");
        }
    }
}

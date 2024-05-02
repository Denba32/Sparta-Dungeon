using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class QuestManager
    {
        List<Quest> quests = new List<Quest>();
        public QuestManager()
        {
            quests.Add(new HuntQuest("마을, 위협, 미니언, 처치", "마을, 위협, 미니언, 많은, 미니언, 5마리, 처치.", 5));
            quests.Add(new EquipQuest("장비, 착용", "바깥, 위험, 장비, 무쇠갑옷, 착용, 안전.", new Armor(2, "무쇠 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200, true, false)));
            quests.Add(new LevelQuest("레벨, 수련", "수련, 필요, 강함, 좋음, 레벨, 5, 필요.", 5));
        }

        public void ShowQuestList()
        {
            int num = 1;
            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i].State != Define.QuestState.RewardQuest)
                {
                    
                    int line = 7 + num;
                    Console.SetCursorPosition(3, line);
                    Console.WriteLine($"[{num}] {quests[i].Title}");
                    quests[i].QuestNum = num;
                    num++;
                }              
            }
        }
    }
}

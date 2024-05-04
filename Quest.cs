using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Quest
    {
        public int QuestNum = 0;
        public string Title { get; set; }
        public string Description { get; set; }
        public int Reward { get; set; }
        public Define.QuestType Type { get; set; }
        public Define.QuestState State { get; set; }

        public Quest(string title, string description, int reward)
        {
            Title = title;
            Description = description;
            Reward = reward;
         }
        public virtual void Notify()
        {

        }
        public void Clear()
        {
            if (State == Define.QuestState.ClearQuest)
            {
                State = Define.QuestState.RewardQuest;
                GameManager.Instance.Player.PlayerData.Gold += Reward;
            }
        }
        public virtual void RequireText(int num)
        {

        }
    }
}

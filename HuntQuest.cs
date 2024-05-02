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

        public HuntQuest(string title, string description, int requireCount) : base(title, description)
        {
            Type = Define.QuestType.HuntQuest;
            State = Define.QuestState.CanAccept;
            RequireCount = requireCount;
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override void Notify()
        {
            base.Notify();
        }
    }
}

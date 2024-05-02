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
        public LevelQuest(string title, string description, int require) : base(title, description)
        {
            Type = Define.QuestType.LevelupQuest;
            State = Define.QuestState.CanAccept;
            RequireCount = require;
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

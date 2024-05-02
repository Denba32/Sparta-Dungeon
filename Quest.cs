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
        public Define.QuestType Type { get; set; }
        public Define.QuestState State { get; set; }

        public Quest(string title, string description)
        { 
            Title = title;
            Description = description;
        }
        public virtual void Notify()
        {

        }
        public virtual void Clear()
        {

        }
    }
}

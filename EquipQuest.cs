using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    internal class EquipQuest : Quest
    {
        public Equipment RequireEquip {  get; set; }

        public EquipQuest(string title, string description, Equipment equipment) : base(title, description)
        {
            Type = Define.QuestType.EquipQuest;
            State = Define.QuestState.CanAccept;
            RequireEquip = equipment;
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

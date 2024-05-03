using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    internal class EquipQuest : Quest
    {
        public int RequireEquip {  get; set; }

        public EquipQuest(string title, string description, int oid) : base(title, description)
        {
            Type = Define.QuestType.EquipQuest;
            State = Define.QuestState.CanAccept;
            RequireEquip = oid;
        }

        public override void Clear()
        {
            for (int i = 0; i < GameManager.Instance.Player.Inven.items.Count; i++)
            {
                if (GameManager.Instance.Player.Inven.items[i].EquipData.IsEquipped == true)
                {
                    int num = GameManager.Instance.Player.Inven.items[i].EquipData.Oid;
                }                
            }
        }

        public override void Notify()
        {
            base.Notify();
        }
    }
}

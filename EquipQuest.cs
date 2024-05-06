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

        public EquipQuest(string title, string description,int reward, int oid) : base(title, description, reward)
        {
            Type = Define.QuestType.EquipQuest;
            State = Define.QuestState.CanAccept;
            RequireEquip = oid;
        }
        public override void Notify()
        {
            for (int i = 0; i < GameManager.Instance.Player.Inven.items.Count; i++)
            {
                if (GameManager.Instance.Player.Inven.items[i].EquipData.IsEquipped == true)
                {
                    int num = GameManager.Instance.Player.Inven.items[i].EquipData.Oid;
                    if (num == RequireEquip)
                    {
                        State = Define.QuestState.ClearQuest;
                        return;
                    }
                    else
                    {
                        State = Define.QuestState.AcceptQuest;
                    }
                }
            }
        }
        public override void RequireText(int num)
        {
            Store store = new Store(true);
            string target = "";

            for (int i = 0; i < store.items.Count; i++)
            {
                if (RequireEquip == store.items[i].EquipData.Oid)
                {
                    target = store.items[i].EquipData.Name;
                    break;
                }
            }

            Console.SetCursorPosition(4, 14);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("아마도 ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"[{target}]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" 을(를) 장비하고 오라고 하는 것 같다.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

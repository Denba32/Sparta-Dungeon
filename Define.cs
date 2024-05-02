using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Define
    {
        public enum Difficulty
        {
            None,
            Easy,
            Normal,
            Hard
        }

        public enum EquipType
        {
            Weapon,
            Armor
        }

        public enum QuestState
        {
            CanAccept,
            AcceptQuest,
            ProcessQuest,
            ClearQuest,
            RewardQuest
        }

        public enum QuestType
        {
            HuntQuest,
            EquipQuest,
            LevelupQuest
        }
        public enum Turn
        {
            Player,
            Enemy
        }
        public enum BattleSituation
        {
            Progress,
            Victory,
            Defeat
        }

    }
}

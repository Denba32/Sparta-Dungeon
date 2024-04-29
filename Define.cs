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


        public enum GameState
        {
            None,
            Main,
            Status,
            Inventory,
            ItemManagement,
            Store,
            BuyItem,
            SellItem,
            Dungeon,
            DungeonClear,
            DungeonFailed,
            Hospital,
            End
        }
    }
}

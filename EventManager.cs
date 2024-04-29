using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class EventManager
    {
        // 무기가 장착되었을 때의 Event Action
        public Action<Equipment>? onEquipWeapon;

        // 무기가 해제되었을 때의 Event Action
        public Action<Equipment>? onDetachWeapon;

        // 방어구가 장착되었을 때의 Event Action
        public Action<Equipment>? onEquipArmor;

        // 방어구가 해제되었을 때의 Event Action
        public Action<Equipment>? onDetachArmor;
    }
}
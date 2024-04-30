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
        public event Action<Equipment>? onEquipWeapon;

        public void EquipWeapon(Equipment weapon)
        {
            onEquipWeapon?.Invoke(weapon);
        }
        // 무기가 해제되었을 때의 Event Action
        public event Action<Equipment>? onDetachWeapon;

        public void DetachWeapon(Equipment weapon)
        {
            onDetachWeapon?.Invoke(weapon);
        }

        // 방어구가 장착되었을 때의 Event Action
        public event Action<Equipment>? onEquipArmor;

        public void EquipArmor(Equipment armor)
        {
            onEquipArmor?.Invoke(armor);
        }

        // 방어구가 해제되었을 때의 Event Action
        public event Action<Equipment>? onDetachArmor;

        public void DetachArmor(Equipment armor)
        {
            onDetachArmor?.Invoke(armor);
        }

        // 플레이어가 아이템을 팔 때의 Event Action
        public event Action<Equipment>? onSellItem;

        public void SellItem(Equipment equipment)
        {
            onSellItem?.Invoke(equipment);
        }
        // 플레이어가 상점에서 아이템을 팔 때 Event Action
        public event Action<Equipment>? onBuyItem;

        public void BuyItem(Equipment equipment) 
        {
            onBuyItem?.Invoke(equipment);
        }

    }
}
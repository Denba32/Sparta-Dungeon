﻿namespace Sparta_Dungeon
{    public class Weapon : Equipment
    {
        public Weapon() { }

        public Weapon(int oid, string name, int atk, int def, string description, int price, bool isEquipped, bool isSelled) : base(oid, name, atk, def, description, price, isEquipped, isSelled)
        {
            Type = Define.EquipType.Weapon;
        }
    }
}

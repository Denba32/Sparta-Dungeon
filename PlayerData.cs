﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class PlayerData
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Level { get;  set; }

        [JsonProperty]
        public string Chad { get;  set; }

        [JsonProperty]
        public float Atk { get;  set; }

        [JsonProperty]
        public float Def { get;  set; }

        [JsonProperty]
        public float Vit { get;  set; }

        [JsonProperty]
        public int Mp { get; set; }

        [JsonProperty]
        public int Exp { get; set; }

        [JsonProperty]
        public int Gold { get;  set; }

        public PlayerData() { }
        public PlayerData(string name, int level, string chad, float atk, float def, float vit, int mp, int exp, int gold)
        {
            Name = name;
            Level = level;
            Chad = chad;
            Atk = atk;
            Def = def;
            Vit = vit;
            Mp = mp;
            Exp = exp;
            Gold = gold;

        }

        #region Property Setter
        
        public void SetVit(int vit)
        {
            Vit += vit;
        }
        public void SetGold(int gold)
        {
            Gold += gold;
        }

        #endregion


        // 플레이어의 레벨이 1오를 때 마다 공격력이랑 방어력이 1씩 오름
        public void LevelUp(int level)
        {
            Level += level;
            Atk += level;
            Def += level;

        }

        #region Add Event Function
        public void PlusWeaponAbility(Equipment? weapon)
        {
            if (weapon != null)
            {
                if (weapon.EquipData.IsEquipped)
                {
                    Atk += weapon.EquipData.Atk;
                }
            }

        }

        public void MinusWeaponAbility(Equipment? weapon)
        {
            if(weapon != null)
            {
                if (!weapon.EquipData.IsEquipped)
                {
                    Atk -= weapon.EquipData.Atk;

                }
            }

        }

        public void PlusArmorAbility(Equipment? armor)
        {
            if(armor != null)
            {
                if (armor.EquipData.IsEquipped)
                {
                    Def += armor.EquipData.Def;
                }
            }

        }

        public void MinusArmorAbility(Equipment? armor)
        {
            if(armor != null)
            {
                if (!armor.EquipData.IsEquipped)
                {
                    Def -= armor.EquipData.Def;

                }
            }

        }

        #endregion
    }
}
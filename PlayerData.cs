using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Mp { get; set; }

        [JsonProperty]
        public int Exp { get; set; }

        [JsonProperty]
        public int Gold { get;  set; }

        public PlayerData()
        {
            GameManager.Instance.Event.onEquipWeapon += PlusWeaponAbility;
            GameManager.Instance.Event.onEquipArmor += PlusArmorAbility;

            GameManager.Instance.Event.onDetachWeapon += MinusWeaponAbility;
            GameManager.Instance.Event.onDetachArmor += MinusArmorAbility;
        }

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

            GameManager.Instance.Event.onEquipWeapon += PlusWeaponAbility;
            GameManager.Instance.Event.onEquipArmor += PlusArmorAbility;

            GameManager.Instance.Event.onDetachWeapon += MinusWeaponAbility;
            GameManager.Instance.Event.onDetachArmor += MinusArmorAbility;
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
        private void PlusWeaponAbility(Equipment weapon)
        {
            if (weapon.EquipData.IsEquipped)
            {
                Atk += weapon.EquipData.Atk;
            }
        }

        private void MinusWeaponAbility(Equipment weapon)
        {
            if(!weapon.EquipData.IsEquipped)
            {
                Atk -= weapon.EquipData.Atk;

            }
        }

        private void PlusArmorAbility(Equipment armor)
        {
            if (armor.EquipData.IsEquipped)
            {
                Def += armor.EquipData.Def;
            }
        }

        private void MinusArmorAbility(Equipment armor)
        {
            if (!armor.EquipData.IsEquipped)
            {
                Def -= armor.EquipData.Def;

            }
        }

        #endregion
    }
}

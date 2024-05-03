using Newtonsoft.Json;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class EnemyData
    {
        public string Name {  get; set; }

        public float Level { get; set; }

        public float Atk { get; set; }

        public float Vit { get; set; }

        public int DropGold { get; set; }
        
        public Equipment? DropItem { get; set; }

        public EnemyData(string name, float level, float atk, float vit, int dropGold, Equipment? dropItem = null)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Vit = vit;
            DropGold = dropGold;
            DropItem = dropItem;
        }

    }
}
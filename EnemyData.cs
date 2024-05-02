using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class EnemyData
    {
        [JsonProperty]
        public string Name {  get; set; }
        [JsonProperty]
        public float Level { get; set; }
        [JsonProperty]
        public float Atk { get; set; }
        [JsonProperty]
        public float Vit { get; set; }
        [JsonProperty]
        public char LifeYn { get; set; }

        public EnemyData(string name, float level, float atk, float vit, char lifeYn)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Vit = vit;
            LifeYn = lifeYn;
        }

    }
}

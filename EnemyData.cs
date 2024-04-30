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
        public int Level { get; set; }
        [JsonProperty]
        public int Atk { get; set; }
        [JsonProperty]
        public int Vit { get; set; }

        public EnemyData(string name, int level, int atk, int vit) 
        {
            Name = name;
            Level = level;
            Atk = atk;
            Vit = vit;
        }

    }
}

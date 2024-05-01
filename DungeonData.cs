
using Newtonsoft.Json;
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class DungeonData
    {
        [JsonProperty]
        public int Scale { get; set; }
        public Random rand = new Random();
        [JsonProperty]
        public List<Enemy> enemies { get; set; } = new List<Enemy>();
        public DungeonData()
        {
            enemies.Add(new Enemy("미니언", 1, 15, 3, 'Y'));
            enemies.Add(new Enemy("대포미니언", 5, 25, 7, 'Y'));
            enemies.Add(new Enemy("공허충", 3, 10, 12, 'Y'));
            enemies.Add(new Enemy("내셔남작", 6, 40, 20, 'Y'));
        }

    }
}

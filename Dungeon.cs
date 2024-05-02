
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    public interface IDamagable
    {
        void Damage(float damage);
        void SkDamage(float skdamage);
    }
    public class Dungeon
    {
        private DungeonData? dungeonData;

        public DungeonData DungeonData
        {
            get
            {
                if (DungeonData == null)
                {
                    if (GameManager.Instance.Data.FileExists(typeof(DungeonData)))
                    {
                        DungeonData = GameManager.Instance.Data.Load<DungeonData>();
                        
                    }
                    else
                    {
                        DungeonData = new DungeonData();
                   }
                }
                return DungeonData;
            }
            set
            {
                DungeonData = value;
            }
        }

        public string ShowEnemies()
        {
            string data = string.Empty;

            for(int i=0 ; i<DungeonData.Scale ; i++)
            {
                int randIdx = DungeonData.rand.Next(0, 4);
                string level = DungeonData.enemies[randIdx].GetLevel().ToString();
                string name = DungeonData.enemies[randIdx].GetName();
                string vit = DungeonData.enemies[randIdx].GetVit().ToString();

                data += $"{i + 1} Lv.{level} {name} HP {vit}\n";
            }

            return data;
        }

    }
}

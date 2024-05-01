using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class EnemyRespawner
    {
        public int Scale;
        public List<EnemyData> enemies { get; set; } = new List<EnemyData>();

        public EnemyRespawner()
        {
            enemies.Add(new EnemyData("미니언", 1, 15, 2));
            enemies.Add(new EnemyData("대포미니언", 5, 25, 7));
            enemies.Add(new EnemyData("공허충", 3, 10, 12));
            enemies.Add(new EnemyData("내셔남작", 6, 40, 20));

            Scale = 3;
        }

        public string ShowEnemies()
        {
            Random rand = new Random();
            string data = String.Empty;

            for(int i=0 ; i<Scale ; i++)
            {
                int randIdx = rand.Next(0, 4);
                data += $"   {i + 1} Lv.{enemies[randIdx].Level} {enemies[randIdx].Name} HP {enemies[randIdx].Vit}\n";
            }

            return data;
        }
    }
}
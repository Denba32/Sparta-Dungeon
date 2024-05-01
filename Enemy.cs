using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sparta_Dungeon
{
    public class Enemy : IDamagable
    {
        private EnemyData? enemyData;
        private Random random;

        public Enemy(string name, float level, float atk, float vit)
        {
            enemyData = new EnemyData(name, level, atk, vit);
            random = new Random();
        }
        public void Attack()
        {
            player.Damage(enemyData.Atk);
        }
        public void Damage(float damage)
        {
            if (random.Next(0, 100) < 10)
            {

            }
            else
            {
                enemyData.Vit -= damage;
                if (enemyData.Vit <= 0)
                {

                }
            }
        }
    }
}

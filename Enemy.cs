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

        public Enemy(string name, float level, float atk, float vit, char lifeYn)
        {
            enemyData = new EnemyData(name, level, atk, vit, lifeYn);
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
                // TODO 공격을 회피하는 작업을 수행합니다.
            }
            else
            {
                enemyData.Vit -= damage;
                if (enemyData.Vit <= 0)
                {
                    Dead();
                }
            }
        }
        public void SkDamage(float Skdamage)
        {
            if (enemyData.Vit <= 0)
            {
                return;
            }
            enemyData.Vit -= Skdamage;
            
            if (enemyData.Vit <= 0)
            {
                Dead();
            }
        }
        public void Dead()
        {
            // TODO 적을 처리하는 작업을 수행합니다.
        }
        public string GetName()
        {
            return enemyData.Name;
        }
        public float GetLevel()
        {
            return enemyData.Level;
        }
        public float GetAtk()
        {
            return enemyData.Atk;
        }
        public float GetVit()
        {
            return enemyData.Vit;
        }

    }
}

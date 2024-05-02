using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return;
            }

            float minDamage = damage - (damage * 0.1f);
            float maxDamage = damage + (damage * 0.1f);

            damage = minDamage + (float)random.NextDouble() * (maxDamage - minDamage);
            // 최소 데미지 - 최대 데미지 예를 들어 최소가 9 최대가 11일 경우 2가 나오고 이를 랜덤으로 생성( 0, 1, 2 중 하나) + 최소 데미지 = 데미지
            // 데미지 = 최소데미지 + 0, 최소데미지 + 1, 최소데미지 +2 > 0 일경우 9데미지, 1일경우 10 데미지, 2일 경우 11데미지 가 나옴으로서 10%의 오차를 가지게 함
            float lastdamage = (float)Math.Round(damage);

            if (random.Next(0, 110) < 15)
            {
                lastdamage = lastdamage * 1.6f;
            }
            enemyData.Vit -= lastdamage;
            if (enemyData.Vit <= 0)
            {
                Dead();
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
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
            GameManager.Instance.Player.Damage(enemyData.Atk);
            Console.WriteLine("플레이어가 데미지를 입었습니다.");
        }

        public void Damage(float damage)
        {
            if (random.Next(0, 100) < 10)
            {
                Console.WriteLine("회피했습니다.");
                // TODO 공격을 회피하는 작업을 수행합니다.
                return;
            }

            if (random.Next(0, 100) < 15)
            {
                damage *= 1.6f;
                Console.WriteLine("치명타 발생");
            }

            enemyData.Vit -= damage;
            Console.WriteLine(damage + "만큼 피해를 입습니다.\n" +
                                $"{enemyData.Name}의 체력은 {enemyData.Vit}가 되었습니다.");
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

            Console.WriteLine(Skdamage + "만큼 스킬 피해를 입습니다.\n" +
$"{enemyData.Name}의 체력은 {enemyData.Vit}가 되었습니다.");
            if (enemyData.Vit <= 0)
            {
                Dead();
            }
        }
        public void Dead()
        {
            // TODO 적을 처리하는 작업을 수행합니다.
            Console.WriteLine($"{enemyData.Name}이 사망했습니다.");
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
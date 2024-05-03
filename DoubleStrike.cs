using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class DoubleStrike : WarriorSkill, ISkillExecutable
    {
        public DoubleStrike(string name, int requireMP, string description, Player player) : base(name, requireMP, description, player)
        {
        }

        public void Execute(IDamagable damagable, float damage)
        {

        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            if(player.PlayerData.Mp >= RequireMP)
            {
                player.PlayerData.Mp -= RequireMP;
                Console.WriteLine("더블 스트라이크!");
                Random random = new Random();
                int count = 0;
                while(count < 2)
                {
                    int enemyIndex = random.Next(0, damagable.Count);

                    // 안죽은 경우
                    if (!damagable[enemyIndex].IsDead())
                    {
                        damagable[enemyIndex].SkDamage(damage);
                        count++;
                    }
                    // 죽은 경우
                    else
                    {
                        if (GameManager.Instance.Event.EnemyAllDie())
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}

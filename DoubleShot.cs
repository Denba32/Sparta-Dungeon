using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class DoubleShot : ArcherSkill, ISkillExecutable
    {
        public DoubleShot(string name, int requireMP, string description, Player player) : base(name, requireMP, description, player)
        {
        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            if (player.PlayerData.Mp >= RequireMP)
            {
                player.PlayerData.Mp -= RequireMP;
                Console.WriteLine("더블 샷!");
                Random random = new Random();
                int count = 0;
                while (count < 2)
                {
                    int enemyIndex = random.Next(0, damagable.Count);
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

        public void Execute(IDamagable damagable, float damage)
        {
            throw new NotImplementedException();
        }
    }
}

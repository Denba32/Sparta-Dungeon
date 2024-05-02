using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class DoubleShot : ArcherSkill, ISkillExecutable
    {
        public DoubleShot(string name, int requireMP, string description) : base(name, requireMP, description)
        {
        }


        public void Execute(List<IDamagable> damagable, float damage)
        {
            Console.WriteLine("더블 샷!");
            Random random = new Random();
            for(int i = 0; i < 2; i++)
            {
                int targetNum = random.Next(0, damagable.Count);
                damagable[targetNum].SkDamage(damage * 1.5f);
            }
        }

        public void Execute(IDamagable damagable, float damage)
        {
            throw new NotImplementedException();
        }
    }
}

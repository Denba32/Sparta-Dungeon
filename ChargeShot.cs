using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class ChargeShot : ArcherSkill, ISkillExecutable
    {
        public ChargeShot(string name, int requireMP, string description) : base(name, requireMP, description)
        {
        }

        public void Execute(IDamagable damagable, float damage)
        {
            Console.WriteLine("더블 샷!");
            damagable.SkDamage(damage);
        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            throw new NotImplementedException();

        }
    }
}
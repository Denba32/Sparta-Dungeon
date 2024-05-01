using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class AlphaStrike : WarriorSkill, ISkillExecutable
    {
        public AlphaStrike(string name, int requireMP, string description) : base(name, requireMP, description)
        {

        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            throw new NotImplementedException();

        }

        public void Execute(IDamagable damagable, float damage)
        {
            Console.WriteLine("알파 스트라이크!");
            damagable.SkDamage(damage);
        }


        //public override void Execute()
        //{
        //    base.Execute();
        //    Console.WriteLine("알파 스트라이크!");
        //}
    }
}

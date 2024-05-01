using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class WarriorSkill : Skill
    {
        public WarriorSkill(string name, int requireMP, string description) : base(name, requireMP, description)
        {

        }

        //public override void Execute()
        //{
        //    base.Execute();
        //    Console.WriteLine("워리어 스킬");
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class ArcherSkill : Skill
    {
        public ArcherSkill(string name, int requireMP, string description) : base(name, requireMP, description)
        {
        }
    }
}

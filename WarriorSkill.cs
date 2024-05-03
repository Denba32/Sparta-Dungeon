using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class WarriorSkill : Skill
    {
        public WarriorSkill(string name, int requireMP, string description, Player player) : base(name, requireMP, description, player)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class AlphaStrike : WarriorSkill, ISkillExecutable
    {
        public AlphaStrike(string name, int requireMP, string description, Player player) : base(name, requireMP, description, player)
        {

        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            throw new NotImplementedException();

        }

        public void Execute(IDamagable damagable, float damage)
        {
            if (player.PlayerData.Mp >= RequireMP)
            {
                player.PlayerData.Mp -= RequireMP;

                Console.WriteLine("알파 스트라이크!\n");
                damagable.SkDamage(damage * 2);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class ChargeShot : ArcherSkill, ISkillExecutable
    {
        public ChargeShot(string name, int requireMP, string description, Player player) : base(name, requireMP, description, player)
        {
        }

        public void Execute(IDamagable damagable, float damage)
        {
            if(player.PlayerData.Mp >= RequireMP)
            {
                player.PlayerData.Mp -= RequireMP;

                Console.WriteLine("더블 샷!\n");
                damagable.SkDamage(damage * 2);
            }

        }

        public void Execute(List<IDamagable> damagable, float damage)
        {
            throw new NotImplementedException();

        }

    }
}
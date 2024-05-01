using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public interface ISkillExecutable
    {
        void Execute(IDamagable damagable, float damage);
        void Execute(List<IDamagable> damagable, float damage);
    }

    public abstract class Skill
    {
        public string Name { get; set; }
        public int RequireMP { get; set; }
        public float Damage { get; set; } 
        public string Description { get; set; }

        public Skill(string name, int requireMP, string description)
        {
            Name = name;
            this.RequireMP = requireMP;
            Description = description;
        }

        //// Execute -> 스킬 사용
        //public virtual void Execute()
        //{
        //    Console.WriteLine("스킬");
        //}

        public void ShowSkill()
        {
            string data = $"{Name} - MP {RequireMP}\n" +
                $"{Description}";
        }
    }
}

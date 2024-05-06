
namespace Sparta_Dungeon
{
    public interface ISkillExecutable
    {
        bool CheckMP(int mp);
        void ShowSkill(int num);
        void Execute(IDamagable damagable, float damage);
        void Execute(List<IDamagable> damagable, float damage);
    }

    public abstract class Skill
    {
        public string Name { get; set; }
        public int RequireMP { get; set; }
        public float Damage { get; set; } 
        public string Description { get; set; }

        protected Player player;

        public Skill(string name, int requireMP, string description, Player player)
        {
            Name = name;
            this.RequireMP = requireMP;
            Description = description;
            this.player = player;
        }
        
        // 스킬 사용 전 MP가 문제 없는지 체크
        public bool CheckMP(int mp)
        {
            if(mp >= RequireMP)
            {
                return true;
            }
            else
            {
                GameManager.Instance.UI.ErrorText("MP가 부족합니다.");
                return false;
            }
        }
        public void ShowSkill(int num)
        {
            Console.WriteLine($"  {num + 1}. {Name} - MP {RequireMP}\n" +
                $"     {Description}");
        }
    }
}

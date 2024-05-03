
namespace Sparta_Dungeon
{
    public class SkillController
    {
        Player player;
        // <직업이름, 스킬 종류>
        public Dictionary<string, List<ISkillExecutable>> skillBook = new Dictionary<string, List<ISkillExecutable>>();
    
        public SkillController(Player player)
        {
            this.player = player;

            List<ISkillExecutable> warriorSkill = new List<ISkillExecutable>();
            List<ISkillExecutable> archerSkill = new List<ISkillExecutable>();

            warriorSkill.Add(new AlphaStrike("알파 스트라이크", 10, "공격력 * 2 로 하나의 적을 공격합니다.", player));
            warriorSkill.Add(new DoubleStrike("더블 스트라이크", 15, "공격력 * 1.5로 2명의 적을 랜덤으로 공격합니다.", player));

            archerSkill.Add(new ChargeShot("차지 샷", 10, "공격력 * 2 로 하나의 적을 공격합니다.", player));
            archerSkill.Add(new DoubleShot("더블 샷", 15, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", player));

            skillBook.Add("전사", warriorSkill);
            skillBook.Add("궁수", archerSkill);

            // GameManager.Instance.Event.onCheckManaCount += CheckRequireMP;
        }


        /// <summary>
        /// 플레이어가 입력한 숫자를 토대로
        /// 플레이어가 스킬을 발동하는 메서드
        /// 단일 대상 스킬
        /// </summary>
        /// <param name="chad" 플레이어의 직업></param>
        /// <param name="skillNum" 플레이어가 선택한 스킬 번호></param>
        /// <param name="damagable" 적 여려명 대상></param>
        /// <param name="atk" 플레이어의 공격력></param>
        /// 
        public void ExecuteSkill(string chad, int skillNum, IDamagable damagable, float atk)
        {
            if (skillBook.TryGetValue(chad, out var skill))
            {
                skill[skillNum - 1].Execute(damagable, atk);
            }

        }

        /// <summary>
        /// 플레이어가 입력한 숫자를 토대로
        /// 플레이어가 스킬을 발동하는 메서드
        /// </summary>
        /// <param name="chad" 플레이어의 직업></param>
        /// <param name="skillNum" 플레이어가 선택한 스킬 번호></param>
        /// <param name="damagable" 적 여려명 대상></param>
        /// <param name="atk" 플레이어의 공격력></param>
        /// 
        public void ExecuteSkill(string chad, int skillNum, List<IDamagable> damagable, float atk)
        {
            if(skillBook.TryGetValue(chad, out var skill))
            {
                skill[skillNum-1].Execute(damagable, atk);
            }

        }

        public void CheckRequireMP(int sel)
        {
            if(skillBook.TryGetValue(player.PlayerData.Chad, out List<ISkillExecutable> skill))
            {
                skill[sel-1].CheckMP(player.PlayerData.Mp);
            }
        }
    }
}

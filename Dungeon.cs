
using System.Numerics;

namespace Sparta_Dungeon
{

    /*
     * 코드 기능
     * 
     * IDamagable 인터페이스는
     * 데미지를 입힐 수 있는
     * 대상에게 부착하는 인터페이스입니다.
     * 
     * 기능은 2가지 입니다.
     * 1. 일반 공격 처리
     * 2. 스킬 공격 처리
     * 
     * 단, 스킬 공격 처리는,
     * 플레이어가 몬스터에게 스킬로 가하는 데미지로
     * 플레이어에서는 구현을 하지만
     * 이용을 하지 않습니다.
     */
    public interface IDamagable
    {
        bool IsDead();
        void Damage(float damage);
        void SkDamage(float damage);
    }

    /*
     * Dungeon 클래스는
     * 
     * 플레이어가 던전에 진입할 시
     * 적(Enemy)를 랜덤한 수와 랜덤의 적으로 소환하여
     * 플레이어를 저지합니다.
     * 
     * 또한, 플레이어와 적(Enemy)가
     * 각자의 턴에 요청하는 행동을 처리하고
     * 이를 관리합니다.
     */
    public class Dungeon
    {
        /*
         * 코드 기능
         * 
         * 던전의 생성자를 통해서
         * EventManager에서 다루는 event Action들에
         * 상황에 맞는 함수를 호출할 수 있게 함수를 대입
         * 
         * [변경점]
         * 기존에는 던전은 그저 빈 생성자만 호출했지만
         * 턴을 전반적으로 관리하는 Dungeon에서
         * 각 플레이어, 적 별 턴에 대한 행동에 대한 동작을
         * 던전에서 관리
         * 
         */
        public Dungeon()
        {
            GameManager.Instance.Event.onRespawnEnemy += SpawnEnemy;

            GameManager.Instance.Event.onEnterDungeon += ShowEnemies;
            GameManager.Instance.Event.onSelectEnemy += ShowSelectEnemies;
            GameManager.Instance.Event.onPlayerAttack += AttackToEnemy;
            GameManager.Instance.Event.onPlayerSkillAttack += SkillAttackEnemy;
            GameManager.Instance.Event.onPlayerRangeSkillAttack += SkillAttackEnemy;
            GameManager.Instance.Event.onCheckAttackCount += CheckCount;
            GameManager.Instance.Event.onEnemyAttack += AttacktoPlayer;

            GameManager.Instance.Event.onEnemyAllDie += IsAllDie;
            GameManager.Instance.Event.onReward += Reward;
        }

        private DungeonData dungeonData = new DungeonData();


        /*
         * 코드 기능
         * 
         * 적(Enemy)를 랜덤으로 리스폰시키는 메서드
         * 
         * [변경점]
         * 1. 기존 ShowEnemies에 Respawn까지 적용되어 있던 코드를 분리
         * 2. 총 1 ~ 4 사이의 적(Enemy)를 스폰해야 했는데
         *      1 ~ 3 사이의 적(Enemy)를 스폰하던 부분을 수정
         */
        public void SpawnEnemy()
        {
            dungeonData.respawnList.Clear();
            dungeonData.Scale = dungeonData.rand.Next(1, 5);
            for (int i = 0; i < dungeonData.Scale; i++)
            {
                int randIdx = dungeonData.rand.Next(0, dungeonData.enemies.Count);
                dungeonData.respawnList.Add(new Enemy(dungeonData.enemies[randIdx]));
            }
        }

        public bool IsAllDie()
        {
            int count = 0;
            for(int i = 0; i < dungeonData.respawnList.Count; i++)
            {
                if(dungeonData.respawnList[i].isDead)
                {
                    count++;
                }
            }
            return dungeonData.respawnList.Count == count ? true : false;
        }

        public void Reward()
        {
            int rewardGold = 0;

            foreach(Enemy enemy in dungeonData.respawnList)
            {
                rewardGold += enemy.GetDropGold();
            }

            // 해당 플레이어에게 경험치를 부여
            GameManager.Instance.Player.Reward(dungeonData.respawnList.Count);

            GameManager.Instance.Player.RewardGold(rewardGold);


        }

        #region ========== Player Turn ==========


        /*
         * 코드 기능
         * 
         * 스폰된 모든 몬스터의 정보를 출력하는 메서드
         * 
         * [변경점]
         * 기존 던전에서 포맷팅으로 출력해온 정보를
         * 적(Enemy)가 스스로의 정보를 출력
         * 해당 메서드를 실행함으로써 가독성 간결화
         */
        public void ShowEnemies()
        {
            string data = "";
            for (int i = 0; i < dungeonData.Scale; i++)
            {
                data += dungeonData.respawnList[i].ShowEnemyInfo();
            }
            Console.WriteLine(data);
        }

        /*
         * 코드 기능
         * 
         * List에 있는 모든 적이 플레이어에게 공격을 가하는 코드
         * 1. for문을 돌면서 생존한 몬스터가 있을 경우 모든 공격을 돌고
         *    다시 배틀씬으로 돌아갑니다.
         * 2. 만약 모든 for문이 continue로 무시당할 경우
         *    모든 몬스터는 죽은걸로 판정
         *    던전을 클리어 상태로 넘김니다.
         *    
         * 3. 만약 0을 입력 시 플레이어가 사망 상태일 경우
         *    씬을 사망씬으로 전환합니다.
         *    
         */
        public void AttacktoPlayer()
        {
            for (int i = 0; i < dungeonData.respawnList.Count; i++)
            {
                if (dungeonData.respawnList[i].isDead)
                {
                    continue;
                }

                dungeonData.respawnList[i].Attack();

                while (i <= dungeonData.respawnList.Count)
                {

                    if (int.TryParse(Console.ReadLine(), out int sel))
                    {
                        if (sel == 0)
                        {
                            // 플레이어가 HP가 0일 경우
                            // 0을 누를 시 사망씬으로 넘어갑니다.
                            if (GameManager.Instance.Player.PlayerData.Vit <= 0)
                            {
                                GameManager.Instance.Scene.DieScene();


                            }
                            // 모든 적이 공격을 마칠 경우
                            if (i == dungeonData.respawnList.Count - 1)
                            {

                                // TODO 플레이어 턴으로 돌리기
                                GameManager.Instance.Scene.BattleScene();
                            }

                            break;
                        }
                        else
                        {
                            GameManager.Instance.UI.ErrorText();
                        }
                    }
                    else
                    {
                        GameManager.Instance.UI.ErrorText();
                    }
                }
            }

            // 모든 적이 죽어있는 상태일 경우
            GameManager.Instance.Scene.WinScene();

        }


        /*
         * 코드 기능
         * 
         * 플레이어가 공격할 적 대상을 고를 때
         * 던전에서 직접 Index를 표시해줍니다.
         */
        public void ShowSelectEnemies()
        {
            Console.SetCursorPosition(0, 4);

            string data = "";
            for (int i = 0; i < dungeonData.Scale; i++)
            {
                data += $"[{i + 1}]{dungeonData.respawnList[i].ShowEnemyInfo()}";
            }
            Console.WriteLine(data);
        }

        /*
         * 코드 기능
         * 
         * 약간 코드가 꼬이면서 SceneManager에서
         * 플레이어가 입력한 공격 대상 외의 값을 예외 처리할 수 있게
         * 
         * 에러를 출력 후 다시 공격 대상을 고를 수 있게 처리
         * 범위 밖의 대상을 공격하려 할 때, 
         * 다시 공격 대상을 고를 수 있게 처리합니다.
         */
        public void CheckCount(int count)
        {
            if (count > dungeonData.respawnList.Count)
            {
                GameManager.Instance.UI.ErrorText();
                GameManager.Instance.Scene.BattleAttackScene();

                return;
            }
        }

        /*
         * 코드 기능
         * 
         * 던전에 들어온 플레이어가
         * 공격할 대상을 선택하였을 때
         * 
         * 이를 던전에서 인지하고
         * 해당 몬스터가 대상을 데미지 입힙니다.
         */
        public void AttackToEnemy(int sel, float atk)
        {
            int num = sel - 1;
            if (num > dungeonData.respawnList.Count - 1)
            {
                GameManager.Instance.UI.ErrorText();
                GameManager.Instance.Scene.BattleAttackScene();
                return;
            }

            Console.SetCursorPosition(0, 4);
            Console.WriteLine($"{GameManager.Instance.Player.PlayerData.Name} 의 공격!");
            dungeonData.respawnList[num].Damage(atk);
        }
        
        public void SkillAttackEnemy(int sel)
        {
            int num = sel;


            Console.SetCursorPosition(0, 4);
            Console.WriteLine($"{GameManager.Instance.Player.PlayerData.Name} 의 스킬 공격!");

            List<IDamagable> damagables = new List<IDamagable>();
            for(int i = 0; i < dungeonData.respawnList.Count; i++)
            {
                damagables.Add(dungeonData.respawnList[i]);
            }
            GameManager.Instance.Player.UseSkill(num, damagables);

        }

        /*
         * 코드 기능
         * 
         * 던전에 들어온 플레이어가
         * 공격할 대상을 선택하고
         * 스킬을 발동하려고 할 때
         * 
         * 이를 던전에서 인지하고
         * 해당 몬스터에게 스킬 데미지를 입힙니다.
         */
        public void SkillAttackEnemy(int sel, int enemy)
        {
            int num = sel;
            int enemycount = enemy - 1;

            Console.SetCursorPosition(0, 4);
            Console.WriteLine($"{GameManager.Instance.Player.PlayerData.Name} 의 스킬 공격!");

            GameManager.Instance.Player.UseSkill(num, dungeonData.respawnList[enemycount]);
        }

        #endregion

        
    }
}

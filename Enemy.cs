namespace Sparta_Dungeon
{
    public class Enemy : IDamagable
    {
        private EnemyData? enemyData;
        private Random random;

        public bool isDead = false;

        /*
         * 코드 기능
         * 
         * 기본적으로 Enemy를 생성하는 방식입니다.
         */
        public Enemy(string name, float level, float atk, float vit, int difficulty, int rewardGold, Equipment? dropItem)
        {
            enemyData = new EnemyData(name, level, atk, vit, difficulty, rewardGold, dropItem);
            random = new Random();
        }

        /*
         * 코드 기능
         * 생성자를 통해서 Enemy를 생성할 때,
         * EnemyData를 받은 후 새로운 EnemyData 객체를 생성하여 넣어줌
         * 
         * [변경점]
         * this.enemyData = enemyData(파라미터 값)으로
         * 기존에 Enemy가 가지고 있던 값을 복사하면,
         * 결국엔 도긴개긴이기에 생상자 만들 시,
         * 스스로 생성할 수 있게 변경했습니다.
         */
        public Enemy(EnemyData enemyData)
        {
            this.enemyData = new EnemyData(enemyData.Name, enemyData.Level, enemyData.Atk, enemyData.Vit, enemyData.Difficulty, enemyData.DropGold, enemyData.DropItem);
            random = new Random();
        }

        public string GetName()
        {
            return enemyData.Name;
        }
        public void Attack()
        {
            if(!isDead)
            {
                GameManager.Instance.UI.TiteleText("!!!적의 행동!!!");
                Console.SetCursorPosition(0, 4);

                Console.WriteLine($"Lv.{enemyData.Level} {enemyData.Name} 의 공격!");
                // GameManager.Instance.UI.BattelAttackText

                GameManager.Instance.Player.Damage(enemyData.Atk);

                GameManager.Instance.UI.BattleNextText();

                GameManager.Instance.UI.InputText("대상을 선택해주세요.");
            }
        }


        public void Damage(float damage)
        {
            // 회피할 경우
            if (random.Next(0, 100) < 10)
            {
                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name}이 공격을 회피했습니다. [데미지 : 0]\n");

                Console.WriteLine($"HP {enemyData.Vit} -> {enemyData.Vit}\n");
                return;
            }

            float minDamage = damage - (damage * 0.1f);
            float maxDamage = damage + (damage * 0.1f);

            damage = minDamage + (float)random.NextDouble() * (maxDamage - minDamage);
            
            
            // 최소 데미지 - 최대 데미지 예를 들어 최소가 9 최대가 11일 경우 2가 나오고 이를 랜덤으로 생성( 0, 1, 2 중 하나) + 최소 데미지 = 데미지

            // 데미지 = 최소데미지 + 0, 최소데미지 + 1, 최소데미지 +2 > 0 일경우 9데미지, 1일경우 10 데미지, 2일 경우 11데미지 가 나옴으로서 10%의 오차를 가지게 함


            float lastdamage = (float)Math.Ceiling(damage);

            // 치명타가 걸릴 경우
            if (random.Next(0, 110) < 15)
            {
                lastdamage = lastdamage * 1.6f;
                lastdamage = (float)Math.Ceiling(lastdamage);
                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name} 을(를) 맞췄습니다. [데미지 : {lastdamage}] - 치명타 공격!!\n");
            }
            else
            {
                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name} 을(를) 맞췄습니다. [데미지 : {lastdamage}]\n");
            }

            float prevVit = enemyData.Vit;

            // 일반 공격
            enemyData.Vit -= lastdamage;

            // 사망했을 경우
            if (enemyData.Vit <= 0)
            {
                enemyData.Vit = 0;

                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name}");
                Console.WriteLine($"HP {prevVit} -> Dead");

                Dead();
            }
            else
            {
                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name}\n");
                Console.WriteLine($"HP {prevVit} -> {enemyData.Vit}");
            }

        }
        public void SkDamage(float Skdamage)
        {
            if(!isDead)
            {
                Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name} 을(를) 맞췄습니다. [데미지 : {Skdamage}]\n");

                float prevVit = enemyData.Vit;

                enemyData.Vit -= Skdamage;


                if (enemyData.Vit <= 0)
                {
                    enemyData.Vit = 0;

                    Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name}");
                    Console.WriteLine($"HP {prevVit} -> Dead\n");
                    Dead();
                }
                else
                {
                    Console.WriteLine($"Lv. {enemyData.Level} {enemyData.Name}");
                    Console.WriteLine($"HP {prevVit} -> {enemyData.Vit}\n");
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                return;
            }
        }

        private void Dead()
        {
            isDead = true;

            GameManager.Instance.Event.Dead(this);
        }

        public bool IsDead()
        {
            return isDead;
        }
        
        public string ShowEnemyInfo()
        {
            string data = "";
            if(isDead)
            {
                data = $"Lv.{enemyData.Level} {enemyData.Name} Dead\n";
            }
            else
            {
                data = $"Lv.{enemyData.Level} {enemyData.Name} HP {enemyData.Vit}\n";
            }

            return data;
        }

        public int GetDropGold()
        {
            return enemyData.DropGold;
        }

        public Equipment? GetDropItem()
        {
            if (enemyData.DropItem == null)
                return null;
            return enemyData.DropItem;
        }
    }
}
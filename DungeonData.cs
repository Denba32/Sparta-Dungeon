
namespace Sparta_Dungeon
{
    [System.Serializable]
    public class DungeonData
    {
        public Random rand = new Random();
        public int Scale { get; set; }
        public Define.Turn Turn { get; set; }
        public Define.BattleSituation BattleSituation { get; set; }

        /*
         * 코드 기능
         * 적(Enemy)의 정보만을 가지고 있는 List
         * 
         * [변경점]
         * Type : Enemy -> EnemyData
         * 
         * 기존 enemies는 Enemy를 타입으로 가지는 리스트였지만
         * Enemy가 가지고 있던 EnemyData를 추출해서
         * new 객체로 Enemy에 넣고 Add 하는 방식은 매우 불안정한 방법
         * 
         * 동시에 새로운 객체를 생성하는 것이 아닌 List가 가지고 있는 EnemyData의 주소를
         * 복사해서 넣는 형식으로 이루어져 있기에 위와 같은 변경을 판단
         */
        public List<EnemyData> enemies { get; set; } = new List<EnemyData>();
        public List<Enemy> respawnList { get; set; } = new List<Enemy>();
        public DungeonData()
        {
            Turn = Define.Turn.Player;
            BattleSituation = Define.BattleSituation.Progress;

            enemies.Add(new EnemyData("미니언", 2, 7, 15, 100, new Weapon(8, "낡은 검", 3, 0, "낡아서 사용할 수 있을지 모를 검입니다.", 500, false, false)));

            enemies.Add(new EnemyData("대포미니언", 5, 8, 17, 200, new Armor(2, "무쇠 갑옷", 0, 7, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false, false)));


            enemies.Add(new EnemyData("공허충", 3, 12, 10, 300));

            enemies.Add(new EnemyData("내셔남작", 10, 30, 40, 400));
        }

    }
}

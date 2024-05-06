
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

            // 일반 몬스터
            enemies.Add(new EnemyData("미니언", 2, 7, 15, 1, 100, new Potion()));
            enemies.Add(new EnemyData("대포미니언", 5, 8, 17, 1, 200, new Armor(2, "무쇠 갑옷", 0, 7, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false, false)));

            // 특수 몬스터
            enemies.Add(new EnemyData("공허충", 4, 13, 15, 2, 500, new Weapon(9, "카타나", 5, 0, "사무라이들이 사용하는 도입니다.", 1000, false, false)));
            enemies.Add(new EnemyData("쌍독충", 6, 16, 10, 2, 800, new Armor(3, "체인 메일", 0, 9, "체인으로 이루어져 더욱 튼튼한 갑옷입니다.", 2000, false, false)));

            // 첼린지 몬스터
            enemies.Add(new EnemyData("뱀파이어", 13, 22, 18, 3, 1200, new Armor(6, "흑기사의 갑옷", 0, 20, "전설의광물 아다만티움으로 만들어진 갑옷입니다.", 5000, false, false)));
            enemies.Add(new EnemyData("거대거미", 12, 23, 15, 3, 1100, new Weapon(11, "흑요석 검", 9, 0, "흑요석으로 만들어진 검붉은 빛을 띄는 검입니다.", 2500, false, false)));

            // 유니크 보스
            enemies.Add(new EnemyData("내셔남작", 30, 30, 40, 4, 3000, new Weapon(12, "다이아몬드 검", 13, 0, "다이아몬드로 이루어진 검입니다.", 3000, false, false)));

            // 최종 보스
            enemies.Add(new EnemyData("나 마왕", 88, 80, 80, 5, 8000, new Weapon(20, "스파르타의 창", 16, 0, "영웅 르탄이 착용했던 창입니다.", 4200, false, false)));
        }

    }
}

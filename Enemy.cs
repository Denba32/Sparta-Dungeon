using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Enemy
    {
        public static List<EnemyData> Monster()
        {
            List<EnemyData> monster = new List<EnemyData>
            {
                new EnemyData("슬라임", 1, 2, 5),
                new EnemyData("고블린", 3, 5, 10),
                new EnemyData("스켈레톤", 8, 9, 5),
                new EnemyData("게 허르 좁", 80, 1, 1),
                new EnemyData("존 나르센", 99, 80, 70),
                new EnemyData("단 데르기", 1, 1, 99)

            };

            return monster;
        }
    }
}

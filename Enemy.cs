using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_Dungeon
{
    public class Enemy
    {
        private EnemyData? enemyData;

        public Enemy(string name, int level, int atk, int hp)
        {
            enemyData = new EnemyData(name, level, atk, hp);
        }


    }
}
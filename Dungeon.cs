
namespace Sparta_Dungeon
{
    public enum Difficulty
    {
        None,
        Easy,
        Normal,
        Hard
    }

    internal class Dungeon
    {
        Difficulty difficulty = Difficulty.None;
        public int ClearProbability { get; private set; }
        public void GoToDungeon(Player player, Difficulty difficulty)
        {
            this.difficulty = difficulty;

            switch (difficulty)
            {
                case Difficulty.None:
                    break;

                case Difficulty.Easy:
                    if (player.Status.DEF >= 5)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if (IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;
                case Difficulty.Normal:
                    if (player.Status.DEF >= 11)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if (IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;
                case Difficulty.Hard:
                    if (player.Status.DEF >= 17)
                    {
                        ClearProbability = 100;
                    }
                    else
                    {
                        ClearProbability = 40;
                    }
                    if (IsClear())
                    {
                        GameManager.state = GameManager.GameState.DungeonClear;
                    }
                    else
                    {
                        GameManager.state = GameManager.GameState.DungeonFailed;
                    }
                    break;

            }
        }

        public bool IsClear()
        {
            Random random = new Random();
            int result = random.Next(0, 100);

            if (ClearProbability == 100)
            {
                return true;
            }

            if (result < ClearProbability)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public string Reward(Player player, bool result)
        {
            if (result)
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        return $"쉬운 던전을 클리어 하였습니다.\n\n" +
                            $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (5 - player.Status.DEF)), (35 + (5 - player.Status.DEF))))}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(1000 + (int)(1000 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";

                    case Difficulty.Normal:
                        return $"일반 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                        $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (11 - player.Status.DEF)), (35 + (11 - player.Status.DEF))))}\n" +
                        $"Gold {player.Status.Gold} -> {player.RewardGold(1700 + (int)(1700 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";
                    case Difficulty.Hard:
                        return $"하드 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                        $"체력 {player.Status.VIT} -> {player.Damage(new Random().Next((20 + (17 - player.Status.DEF)), (35 + (17 - player.Status.DEF))))}\n" +
                        $"Gold {player.Status.Gold} -> {player.RewardGold(2500 + (int)(2500 * (new Random().Next(player.Status.ATK, player.Status.ATK * 2) * 0.01)))} G\n\n";
                }
            }
            else
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        return $"쉬운 던전을 실패 하였습니다.\n\n" +
                            $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";

                    case Difficulty.Normal:
                        return $"일반 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";
                    case Difficulty.Hard:
                        return $"하드 던전을 클리어 하였습니다.\n\n" +
                        $"[탐험 결과]\n" +
                            $"체력 {player.Status.VIT} -> {player.Damage(player.Status.VIT / 2)}\n" +
                            $"Gold {player.Status.Gold} -> {player.RewardGold(0)} G\n\n";
                }

                return "";
            }
            return "";
        }

    }

}

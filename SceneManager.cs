
namespace Sparta_Dungeon
{
    public class SceneManager
    {
        
        #region ========== 초반 시작 화면 : 이름 설정 직업 설정 ==========
        // 시작 화면
        public void StartScene()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }

            GameManager.Instance.UI.SelectGuide(3);
            Console.WriteLine("       아무키나 눌러서 시작하세요");

            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < 40; i++)
            {
                Console.Write("=");
            }


            Console.ReadKey();
        }


        // 로그인 화면
        public void LoginScene()
        {
            GameManager.Instance.UI.TiteleText("로그인");
            GameManager.Instance.UI.LoreText("캐릭터를 생성합니다.");

            GameManager.Instance.UI.InputText("당신의 이름을 입력해주세요");
            GameManager.Instance.Player.PlayerData.Name = Console.ReadLine();

            GameManager.Instance.UI.TiteleText("로그인");
            GameManager.Instance.UI.LoreText("캐릭터를 생성합니다.");

            GameManager.Instance.UI.SelectGuide(5);
            Console.WriteLine($"   당신의 이름은 {GameManager.Instance.Player.PlayerData.Name}");
            Console.WriteLine("   이 이름으로 하시겠습니까?");

            GameManager.Instance.UI.SelectGuide(2);
            Console.Write("   0.아니오");
            Console.Write("       1.예");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        LoginScene();
                        break;
                    case 1:
                        SelectChar();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        LoginScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                LoginScene();
            }
        }

        
        
        
        // 캐릭터 선택 화면
        public void SelectChar()
        {
            GameManager.Instance.UI.TiteleText("캐릭터 선택");
            GameManager.Instance.UI.LoreText("원하는 캐릭터를 선택합니다.");

            GameManager.Instance.UI.SelectGuide(10);
            Console.WriteLine("   1.전사");
            Console.WriteLine("   2.궁수");

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 1:
                        GameManager.Instance.Player.SetChad("전사");
                        break;
                    case 2:
                        GameManager.Instance.Player.SetChad("궁수");
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        SelectChar();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                SelectChar();
            }
        }

        #endregion


        #region ========== 마을 중심 화면 ==========
        // 마을 화면
        public void TownScene()
        {
            GameManager.Instance.Event.OnSave();
            GameManager.Instance.UI.TiteleText("마을", ConsoleColor.Green);            
            GameManager.Instance.UI.LoreText("스파르타 마을에 오신 여러분 환영합니다.", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   1.상태 보기");
            Console.WriteLine("   2.인벤토리");
            Console.WriteLine("   3.상점");
            Console.WriteLine($"   4.던전 입장 : (현재 {GameManager.Instance.Player.PlayerData.DungeonFloor}층)");
            Console.WriteLine("   5.휴식하기");
            Console.WriteLine("   6.퀘스트 게시판");

            GameManager.Instance.UI.InputText(ConsoleColor.Green);

            string input = Console.ReadLine();


            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 1:
                        StatusScene();
                        break;
                    case 2:
                        InventoryScene();
                        break;
                    case 3:
                        StoreScene();
                        break;
                    case 4:
                        DungeonSelectScene();
                        break;
                    case 5:
                        HospitalScene();
                        break;
                    case 6:
                        QuestBoardScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
            }
        }

        #endregion


        #region ========== 상태 화면 ==========
        // 상태 화면
        void StatusScene()
        {
            GameManager.Instance.UI.TiteleText("상태 보기", ConsoleColor.Cyan);
            GameManager.Instance.UI.LoreText("캐릭터의 정보가 표시됩니다.");

            GameManager.Instance.Player.ShowAllStatus();

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        StatusScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                StatusScene();
            }
        }

        #endregion

        
        #region ========== 인벤토리 ==========

        // 인벤토리 화면
        void InventoryScene()
        {
            GameManager.Instance.UI.TiteleText("인벤토리", ConsoleColor.Cyan);
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("보유중인 아이템을 관리할 수 있습니다.", "[아이템 목록]", ConsoleColor.White, ConsoleColor.Cyan);

            GameManager.Instance.Event.ShowSelectorItemList(true);

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   1.장착 관리");
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        ItemManageScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        InventoryScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                InventoryScene();
            }
        }
        // 인벤토리 관리화면(기능 추가 필요)
        void ItemManageScene()
        {
            GameManager.Instance.UI.TiteleText("인벤토리", ConsoleColor.Cyan);
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력해서 장착/해제할 수 있습니다.", "[아이템 목록]", ConsoleColor.White, ConsoleColor.Cyan);

            GameManager.Instance.Event.ShowSelectorItemList(false);


            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                if(int.Parse(input) == 0)
                {
                    InventoryScene();
                }
                else if(int.Parse(input) > GameManager.Instance.Player.Inven.GetItemCount())
                {
                    GameManager.Instance.UI.ErrorText();
                    ItemManageScene();
                }
                else if(int.Parse(input) < 0)
                {
                    GameManager.Instance.UI.ErrorText();
                    ItemManageScene();
                }
                else
                {
                    GameManager.Instance.Player.Inven.SelectItem(int.Parse(input));
                    GameManager.Instance.Event.OnSave();

                    ItemManageScene();

                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                ItemManageScene();
            }
        }

        #endregion


        #region ========== 상점 씬 ==========

        // 상점 화면(기능 추가 필요)
        void StoreScene()
        {
            GameManager.Instance.UI.TiteleText("상점", ConsoleColor.Cyan);
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("필요한 아이템을 얻을 수 있는 상점입니다.", "[아이템 목록]", ConsoleColor.White, ConsoleColor.Cyan);

            GameManager.Instance.Event.ShowShopList(true);

            GameManager.Instance.UI.SelectGuide(2);
            Console.WriteLine("   1.아이템 구매");
            Console.WriteLine("   2.아이템 판매");
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        BuyItemScene();
                        break;
                    case 2:
                        SellItemScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        StoreScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                StoreScene();
            }
        }


        // 아이템 구매 화면(기능 추가 필요)
        void BuyItemScene()
        {
            GameManager.Instance.UI.TiteleText("상점 - 아이템 구매", ConsoleColor.Cyan);
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 구매합니다.", "[아이템 목록]", ConsoleColor.White, ConsoleColor.Cyan);

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            GameManager.Instance.Event.ShowShopList(false);            

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                if(int.Parse(input) == 0)
                {
                    StoreScene();
                }
                else
                {
                    GameManager.Instance.Event.Buy(int.Parse(input));
                    BuyItemScene();
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                BuyItemScene();
            }
        }
        // 아이템 판매 화면(기능 추가 필요)
        void SellItemScene()
        {
            GameManager.Instance.UI.TiteleText("상점 - 아이템 판매", ConsoleColor.Cyan);
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 판매합니다.", "[아이템 목록]", ConsoleColor.White, ConsoleColor.Cyan);

            GameManager.Instance.Event.ShowSelectorItemList(false);

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            GameManager.Instance.UI.InputText(ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                if (int.Parse(input) == 0)
                {
                    StoreScene();
                }
                else
                {
                    GameManager.Instance.Event.SellItem(int.Parse(input));
                    SellItemScene();
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                SellItemScene();
            }
        }

        #endregion


        #region ========== 던전 씬 ==========

        // 던전 선택 시 화면
        void DungeonSelectScene()
        {
            while(true)
            {
                GameManager.Instance.UI.TiteleText("던전 입구");
                GameManager.Instance.UI.LoreText("던전에 입장할 수 있습니다.");

                GameManager.Instance.UI.SelectGuide(1);
                Console.WriteLine($"   1.던전 입장 : (현재 {GameManager.Instance.Player.PlayerData.DungeonFloor}층)");
                Console.WriteLine("   0.나가기");


                GameManager.Instance.UI.InputText();
                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    if (int.TryParse(input, out int sel))
                    {
                        if(sel == 0)
                        {
                            break;
                        }
                        else if(sel == 1)
                        {
                            GameManager.Instance.Event.RespawnEnemy();
                            BattleScene();
                        }
                        else
                        {
                            GameManager.Instance.UI.ErrorText();

                        }

                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }

        }
        
        
        // 전투 시작 시 화면
        public void BattleScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("!!!전투!!!");

                GameManager.Instance.UI.EnemyText();

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattelActionText();

                GameManager.Instance.UI.NowFloorText();

                GameManager.Instance.UI.InputText();
                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    if(int.TryParse(input, out int sel))
                    {
                        if(sel == 0)
                        {
                            break;
                        }
                        else if(sel == 1)
                        {
                            BattleAttackScene();
                        }
                        else if(sel == 2)
                        {
                            BattleSkillScene();
                        }
                        else
                        {
                            GameManager.Instance.UI.ErrorText();
                        }
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }
        }

        /// <summary>
        /// 어떤 Enemy를 공격할 지 선택하는 씬
        /// </summary>
        public void BattleAttackScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("공격하기");

                GameManager.Instance.Event.SelectEnemy();

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattelAttackText();

                GameManager.Instance.UI.NowFloorText();

                GameManager.Instance.UI.InputText();
                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    if (int.Parse(input) == 0)
                    {
                        break;
                    }
                    else if (int.Parse(input) < 0)
                    {
                        GameManager.Instance.UI.ErrorText();
                    }
                    else
                    {

                        // 공격으로 넘어가기 전 에너미 번호를 넘어선 값을 넣었는지 체크
                        if(GameManager.Instance.Event.CheckAttackCount(int.Parse(input)))
                        {

                            if (GameManager.Instance.Event.EnemyDie(int.Parse(input)))
                            {
                                GameManager.Instance.UI.ErrorText();
                            }
                            else
                            {
                                // TODO : 공격 범위 내에 공격 시 처리
                                PlayerAttackResultScene(int.Parse(input));
                            }

                        }
                        else
                        {
                            GameManager.Instance.UI.ErrorText();

                        }

                    }

                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }

            }
        }

        /// <summary>
        /// 플레이어가 선택한 번호를 넘겨 받아서
        /// 데미지와 동시에 결과 출력
        /// </summary>
        /// <param name="num"></param>
        void PlayerAttackResultScene(int num)
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("플레이어 공격 결과");

                // 몬스터 결과 출력
                GameManager.Instance.Event.StartPlayerAttack(num, GameManager.Instance.Player.PlayerData.Atk);

                GameManager.Instance.UI.BattleNextText();
                GameManager.Instance.UI.InputText();


                Console.ReadKey();

                // 죽인 순간 모든 적을 처리했을 경우
                if (GameManager.Instance.Event.EnemyAllDie())
                {
                    WinScene();
                }
                BattleEnemyLogScene();

            }
        }
        
        // 소유중인 스킬을 표시하고 선택하는 메서드
        public void BattleSkillScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("스킬 사용하기");
                //적 표시 기능 추가하기

                GameManager.Instance.UI.EnemyText();

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattleSkillText();
                GameManager.Instance.UI.InputText();

                string input = Console.ReadLine();

                if (GameManager.Instance.UI.IsNumTest(input))
                {
                    if (int.TryParse(input, out int sel))
                    {
                        if (sel > 2 || sel < 0)
                        {
                            GameManager.Instance.UI.ErrorText();
                        }
                        else if (sel == 0)
                        {
                            break;
                        }
                        else
                        {
                            GameManager.Instance.Event.CheckManaCount(sel);
                            BattleSkillAttackScene(sel);
                        }
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }
        }

        // 스킬로 공격할 적 대상을 고르는 메서드
        void BattleSkillAttackScene(int skillNum)
        {
            while (true)
            {
                if (skillNum == 1) {

                    // 1인 스킬을 사용한 경우
                    GameManager.Instance.UI.TiteleText("스킬 사용하기");
                    //적 표시 기능 추가하기

                    GameManager.Instance.Event.SelectEnemy();


                    GameManager.Instance.UI.PlayerUI();
                    GameManager.Instance.UI.BattelAttackText();

                    GameManager.Instance.UI.InputText();

                    string input = Console.ReadLine();

                    if (GameManager.Instance.UI.IsNumTest(input))
                    {
                        if (int.TryParse(input, out int sel))
                        {
                            if (sel == 0)
                            {
                                break;
                            }
                            else if (sel < 0)
                            {
                                GameManager.Instance.UI.ErrorText();
                            }
                            // 공격 대상 고르는 부분
                            else
                            {
                                if(GameManager.Instance.Event.CheckAttackCount(sel))
                                {
                                    if (GameManager.Instance.Event.EnemyDie(sel))
                                    {
                                        GameManager.Instance.UI.ErrorText();
                                    }
                                    else
                                    {
                                        PlayerSkillAttackResultScene(skillNum, sel);
                                    }
                                }
                                else
                                {
                                    GameManager.Instance.UI.ErrorText();

                                }

                            }
                        }
                    }
                    else
                    {
                        GameManager.Instance.UI.ErrorText();
                    }
                }

                // 플레이어 랜덤 공격
                else if (skillNum == 2)
                {
                    PlayerSkillAttackResultScene(skillNum);
                }

            }
        }


        // 일반 스킬 공격 결과 씬
        void PlayerSkillAttackResultScene(int skillNum, int selEnemy)
        {
            // 실제 데미지를 입히는 부분
            // ↓데미지 입힌 결과의 UI 띄우는 부분
            while (true)
            {
                GameManager.Instance.UI.TiteleText("플레이어 공격 결과");

                Console.SetCursorPosition(0, 4);
                Console.WriteLine($"{GameManager.Instance.Player.PlayerData.Name} 의 스킬 공격!");

                // 몬스터 결과 출력
                GameManager.Instance.Event.StartPlayerSkillAttack(skillNum, selEnemy);

                GameManager.Instance.UI.BattleNextText();
                GameManager.Instance.UI.InputText();


                if (int.TryParse(Console.ReadLine(), out int sel))
                {
                    switch (sel)
                    {
                        // 적의 턴으로 넘아감
                        case 0:
                            BattleEnemyLogScene();
                            break;

                        default:
                            GameManager.Instance.UI.ErrorText();
                            break;
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }
        }
        // 다중 공격 씬 결과
        void PlayerSkillAttackResultScene(int skillNum)
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("플레이어 공격 결과");

                // 몬스터 결과 출력
                GameManager.Instance.Event.StartPlayerSkillAttack(skillNum);

                GameManager.Instance.UI.BattleNextText();
                GameManager.Instance.UI.InputText();


                if (int.TryParse(Console.ReadLine(), out int sel))
                {
                    switch (sel)
                    {
                        // 적의 턴으로 넘아감
                        case 0:
                            BattleEnemyLogScene();
                            break;

                        default:
                            GameManager.Instance.UI.ErrorText();
                            break;
                    }
                }
                else
                {
                    GameManager.Instance.UI.ErrorText();
                }
            }
        }


        // 적의 공격 페이즈
        public void BattleEnemyLogScene()
        {
            GameManager.Instance.Event.EnemyAttack();
        }

        // 승리 화면(기능 추가 필요)
        public void WinScene()
        {
            GameManager.Instance.UI.TiteleText("!!!승리!!!");
            GameManager.Instance.UI.LoreText("전투에서 승리하셨습니다!");


            // 보상 정보 출력
            GameManager.Instance.Event.Reward();
            GameManager.Instance.Player.PlayerData.DungeonFloor++;
            GameManager.Instance.Event.OnSave();



            //GameManager.Instance.UI.PlayerUI();
            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.마을로 돌아가기");
            Console.WriteLine("   1.다음 층으로");

            GameManager.Instance.UI.InputText();

            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        TownScene();
                        break;
                    case 1:
                        GameManager.Instance.Event.RespawnEnemy();
                        BattleScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();

                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();

                return;
            }
        }

        /*
         * 코드 기능
         * 플레이어가 사망했을 경우를 처리하는 씬
         * 
         * [변경점]
         * 마치 로그라이크 처럼
         * 플레이어가 사망할 경우,
         * 플레이어 저장 정보를 모두 삭제 후
         * 리셋시킵니다.
         */
        public void DieScene()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            GameManager.Instance.UI.TiteleText("YOU DIED...");
            GameManager.Instance.UI.DieText();
            GameManager.Instance.UI.InputText("당신을 죽었습니다.\n이로 인해 모든 데이터가 삭제되고 게임이 종료됩니다.\n" +
                "아무키나 입력해주세요.");

            Console.ReadKey();

            GameManager.Instance.Data.ClearData<PlayerData>();
            GameManager.Instance.Data.ClearData<Inventory>();


            Environment.Exit(0);
        }

        #endregion

        #region ========== 회복 씬 ==========

        // 휴식 화면(기능 추가 필요)
        void HospitalScene()
        {
            GameManager.Instance.UI.TiteleText("휴식하기");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("500 G로 체력을 화복할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.휴식하기");

            //기능 추가 필요(체력 회복)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        TownScene();
                        break;
                    case 1:
                        GameManager.Instance.UI.SystemText("   미구현!!!");
                        HospitalScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        HospitalScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                HospitalScene();
            }
        }

        #endregion


        #region ========== 퀘스트 씬 ==========
        //  퀘스트 게시판
        void QuestBoardScene()
        {
            GameManager.Instance.UI.TiteleText("퀘스트 게시판", ConsoleColor.DarkYellow);
            GameManager.Instance.UI.LoreText("[현재 모집중인 퀘스트를 확인합니다.]");

            GameManager.Instance.Quest.ShowQuestList();

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");
            GameManager.Instance.UI.InputText("원하는 행동이나 퀘스트의 번호를 입력해주세요", ConsoleColor.DarkYellow);

            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                if (int.Parse(input) == 0)
                {
                    return;
                }
                else if (int.Parse(input) > GameManager.Instance.Quest.quests.Count)
                {
                    GameManager.Instance.UI.ErrorText();
                    QuestBoardScene();
                }
                else if (GameManager.Instance.Quest.quests[int.Parse(input) - 1].State == Define.QuestState.RewardQuest)
                {
                    GameManager.Instance.UI.SystemText("   이미 완료한 퀘스트입니다!!!",ConsoleColor.Cyan, 400);
                    QuestBoardScene();
                }
                else
                {
                    QuestCheckScene(int.Parse(input));
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                QuestBoardScene();
            }

        }
        void QuestCheckScene(int num)
        {
            if (GameManager.Instance.Quest.quests[num - 1].State == Define.QuestState.AcceptQuest || GameManager.Instance.Quest.quests[num - 1].State == Define.QuestState.ClearQuest)
            {
                GameManager.Instance.Quest.quests[num - 1].Notify();
            }
            GameManager.Instance.UI.TiteleText("퀘스트 게시판",ConsoleColor.DarkYellow);
            GameManager.Instance.UI.LoreText("[퀘스트의 정보를 확인합니다.]");

            GameManager.Instance.Quest.ShowQuestInfo(num);

            GameManager.Instance.Quest.QuestSelectText(num);

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");
            GameManager.Instance.UI.InputText(ConsoleColor.DarkYellow);

            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                if (int.Parse(input) == 0)
                {
                    QuestBoardScene();
                }
                else if (GameManager.Instance.Quest.quests[num - 1].State == Define.QuestState.CanAccept)
                {
                    switch (int.Parse(input))
                    {
                        case 1:
                            GameManager.Instance.UI.SystemText("   퀘스트를 수락하셨습니다.", ConsoleColor.Green);
                            GameManager.Instance.Quest.quests[num - 1].State = Define.QuestState.AcceptQuest;
                            QuestCheckScene(num);
                            break;
                        case 2:
                            GameManager.Instance.UI.SystemText("   퀘스트를 거절하셨습니다.", ConsoleColor.Red);
                            QuestBoardScene();
                            break;
                        default:
                            GameManager.Instance.UI.ErrorText();
                            QuestCheckScene(num);
                            break;
                    }
                }
                else if (GameManager.Instance.Quest.quests[num - 1].State == Define.QuestState.AcceptQuest)
                {
                    switch (int.Parse(input))
                    {
                        case 1:
                            GameManager.Instance.UI.SystemText("   퀘스트를 포기하셨습니다.", ConsoleColor.Red);
                            GameManager.Instance.Quest.quests[num - 1].State = Define.QuestState.CanAccept;
                            QuestCheckScene(num);
                            break;
                        case 2:
                            GameManager.Instance.UI.SystemText("   아직 완료할 수 없습니다!!!", ConsoleColor.Red);
                            QuestCheckScene(num);
                            break;
                        default:
                            GameManager.Instance.UI.ErrorText();
                            QuestCheckScene(num);
                            break;
                    }
                }
                else if (GameManager.Instance.Quest.quests[num - 1].State == Define.QuestState.ClearQuest)
                {
                    switch (int.Parse(input))
                    {
                        case 1:
                            GameManager.Instance.UI.SystemText("   퀘스트를 포기하셨습니다.", ConsoleColor.Red);
                            GameManager.Instance.Quest.quests[num - 1].State = Define.QuestState.CanAccept;
                            QuestCheckScene(num);
                            break;
                        case 2:
                            GameManager.Instance.UI.RewardText(GameManager.Instance.Quest.quests[num - 1].Reward);
                            GameManager.Instance.Quest.quests[num - 1].Clear();
                            QuestBoardScene();
                            break;
                        default:
                            GameManager.Instance.UI.ErrorText();
                            QuestCheckScene(num);
                            break;
                    }
                }
                else
                {
                    QuestCheckScene(num);
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                QuestCheckScene(num);
            }
        }

        #endregion

    }
}
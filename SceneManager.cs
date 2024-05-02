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
            
            GameManager.Instance.UI.TiteleText("마을", ConsoleColor.Green);            
            GameManager.Instance.UI.LoreText("스파르타 마을에 오신 여러분 환영합니다.", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   1.상태 보기");
            Console.WriteLine("   2.인벤토리");
            Console.WriteLine("   3.상점");
            Console.WriteLine($"   4.던전 입장 : (현재 {GameManager.Instance.UI.dungeonFloor}층)");
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
            GameManager.Instance.UI.TiteleText("인벤토리");
            GameManager.Instance.UI.LoreText("보유중인 아이템을 관리할 수 있습니다.", "[아이템 목록]");

            GameManager.Instance.Event.ShowItemList();

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.장착 관리");

            GameManager.Instance.UI.InputText();
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
            GameManager.Instance.UI.TiteleText("인벤토리");
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력해서 장착/해제할 수 있습니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)
            //기능 추가 필요(아이템 장착 기능)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        InventoryScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        ItemManageScene();
                        break;
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
            GameManager.Instance.UI.TiteleText("상점");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("필요한 아이템을 얻을 수 있는 상점입니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(2);
            Console.WriteLine("   0.나가기");
            Console.WriteLine("   1.아이템 구매");
            Console.WriteLine("   2.아이템 판매");

            //기능 추가 필요(아이템 목록)

            GameManager.Instance.UI.InputText();
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
            GameManager.Instance.UI.TiteleText("상점 - 아이템 구매");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 구매합니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            // 기능 추가 필요(아이템 구매)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        BuyItemScene();
                        break;
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
            GameManager.Instance.UI.TiteleText("상점 - 아이템 판매");
            GameManager.Instance.UI.GoldText();
            GameManager.Instance.UI.LoreText("원하는 아이템의 번호를 입력하면 판매합니다.", "[아이템 목록]");

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");

            //기능 추가 필요(아이템 목록)

            //기능 추가 필요(아이템 판매)

            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        StoreScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        SellItemScene();
                        break;
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
            GameManager.Instance.UI.TiteleText("던전 입구");
            GameManager.Instance.UI.LoreText("던전에 입장할 수 있습니다.");

            GameManager.Instance.UI.SelectGuide(1);
            Console.WriteLine($"   1.던전 입장 : (현재 {GameManager.Instance.UI.dungeonFloor}층)");
            Console.WriteLine("   0.나가기");


            GameManager.Instance.UI.InputText();
            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    case 1:
                        GameManager.Instance.Event.RespawnEnemy();
                        BattleScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        DungeonSelectScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                DungeonSelectScene();
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
                    switch (int.Parse(input))
                    {
                        case 0:
                            return;
                            // 공격 실행
                        case 1:
                            BattleAttackScene();
                            break;
                        case 2:
                            BattleItemScene();
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
                    if(int.Parse(input) == 0)
                    {
                        return;
                    }
                    else if(int.Parse(input) < 0)
                    {
                        GameManager.Instance.UI.ErrorText();
                        return;
                    }
                    
                    // 공격으로 넘어가기 전 에너미 번호를 넘어선 값을 넣었는지 체크
                    GameManager.Instance.Event.CheckCount(int.Parse(input));

                    // TODO : 공격 범위 내에 공격 시 처리
                    PlayerAttackResultScene(int.Parse(input));
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


                if(int.TryParse(Console.ReadLine(), out int sel))
                {
                    switch(sel)
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
        //전투중 아이템 선택창
        void BattleItemScene()
        {
            while (true)
            {
                GameManager.Instance.UI.TiteleText("아이템 사용");
                //적 표시 기능 추가하기

                GameManager.Instance.UI.PlayerUI();

                GameManager.Instance.UI.BattleItemText();

                GameManager.Instance.UI.NowFloorText();

                while (true)
                {
                    GameManager.Instance.UI.InputText();
                    string input = Console.ReadLine();

                    if (GameManager.Instance.UI.IsNumTest(input))
                    {
                        switch (int.Parse(input))
                        {
                            case 0:
                                return;
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
                        GameManager.Instance.UI.dungeonFloor++;
                        BattleScene();
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        WinScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                WinScene();
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
            GameManager.Instance.UI.dungeonFloor = 1;
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
            GameManager.Instance.UI.TiteleText("퀘스트 게시판");
            GameManager.Instance.UI.LoreText("[현재 모집중인 퀘스트를 확인합니다.]");

            GameManager.Instance.Quest.ShowQuestList();

            GameManager.Instance.UI.SelectGuide(0);
            Console.WriteLine("   0.나가기");
            GameManager.Instance.UI.InputText("원하는 행동이나 퀘스트의 번호를 입력해주세요");

            string input = Console.ReadLine();

            if (GameManager.Instance.UI.IsNumTest(input))
            {
                switch (int.Parse(input))
                {
                    case 0:
                        break;
                    default:
                        GameManager.Instance.UI.ErrorText();
                        QuestBoardScene();
                        break;
                }
            }
            else
            {
                GameManager.Instance.UI.ErrorText();
                QuestBoardScene();
            }

        }

        #endregion

    }
}
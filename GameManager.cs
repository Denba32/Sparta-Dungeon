﻿namespace Sparta_Dungeon
{
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padLock = new object();

        private bool isLocked = false;
        GameManager()
        {

        }

        public static GameManager Instance
        {
            get
            {
                lock (padLock)
                {
                    if(instance == null)
                    {
                        instance = new GameManager();
                    }
                }
                return instance;
            }
        }

        private DataManager data = new DataManager();
        private UIManager ui = new UIManager();
        private EventManager _event = new EventManager();
        private SceneManager scene = new SceneManager();
        private QuestManager quest = new QuestManager();
        private Player player = new Player();

        public DataManager Data { get => Instance.data; }
        public UIManager UI { get => Instance.ui; }
        public EventManager Event { get => Instance._event; }
        public SceneManager Scene { get => Instance.scene; }
        public QuestManager Quest
        {
            get
            {
                if(!isLocked)
                {
                    Instance.quest.Init();
                    isLocked = true;
                }
                return Instance.quest;
            }
        }
        public Player Player { get => Instance.player; }

        public void GameStart()
        {
            Event.onSave += AllSave;
            
            Store store = new Store(true);
            Dungeon dungeon = new Dungeon();
            // Event.onSave += AllSave;
            // 플레이어의 정보가 존재하지 않을 시
            if (!Data.FileExists(typeof(PlayerData)))
            {

                Scene.StartScene();
                Scene.LoginScene();
            }
            if(Data.FileExists(typeof(Store)))
            {
                store = Data.Load<Store>();
            }
            if(Data.FileExists(typeof(QuestManager)))
            {
                isLocked = true;
                quest = Data.Load<QuestManager>();
            }
            while (true)
            {
                // 저장 시도
                // TODO 자동 저장, 플레이어 정보, 인벤 정보, 상점 정보,
                // 던전 클리어 횟수 정보
                Scene.TownScene();
            }
        }

        public void AllSave()
        {
            Data.Save<PlayerData>(Player.PlayerData); ;
            Data.Save<Inventory>(Player.Inven);
        }
    }
}// 재귀 호출 함수

// State패턴

// Fsm ( Exit-> Transition -> Enter )
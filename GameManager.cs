using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;

namespace Sparta_Dungeon
{
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padLock = new object();

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
        private EventManager _event = new EventManager();


        private SceneManager scene = new SceneManager();
        private UIManager ui = new UIManager();
        private Player player = new Player();

        public DataManager Data { get => Instance.data; }
        public UIManager UI { get => Instance.ui; }
        public EventManager Event { get => Instance._event; }
        public SceneManager Scene { get => Instance.scene; }
        public Player Player { get => Instance.player; }

        // Error 발생 시 True로 만들어서 입력란 밑에 에러를 발생시키는 코드
        public bool isError = false;

        // 이미 구매한 상품을 구매 시도 시 입력란 밑에 경고를 알려주는 코드
        public bool isBuyed = false;

        // 구매 시도 시 골드가 부족한 경우 처리
        public bool isEmpty = false;

        // 힐링 성공 시 힐링을 성공했음을 알림
        public bool isHealed = false;

        public bool isLoaded = false;


        public void GameStart()
        {
            // 플레이어의 정보가 존재하지 않을 시
            if(!Data.FileExists(typeof(PlayerData)))
            {
                Scene.StartScene();
                Scene.LoginScene();
            }

            while (true)
            {
                Data.Save<PlayerData>(Player.PlayerData);
                Data.Save<Inventory>(Player.Inven);

                // TODO 자동 저장, 플레이어 정보, 인벤 정보, 상점 정보,
                // 던전 클리어 횟수 정보
                Scene.TownScene();
            }
        }
    }
}

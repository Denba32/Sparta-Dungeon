namespace Sparta_Dungeon
{
    public class EventManager
    {
        public event Action? onSave;

        public void OnSave() 
        { 
            onSave?.Invoke(); 
        }
        // 플레이어가 아이템을 팔 때의 Event Action
        public event Action<int>? onSellItem;

        public void SellItem(int equipment)
        {
            onSellItem?.Invoke(equipment);
        }

        public event Action<int>? onBuy;

        public void Buy(int index)
        {
            onBuy?.Invoke(index);
        }
        // 플레이어가 상점에서 아이템을 팔 때 Event Action
        public event Action<Equipment>? onBuyItem;

        public void BuyItem(Equipment equipment) 
        {
            onBuyItem?.Invoke(equipment);
        }

        public event Action? onShowItems;

        public void ShowItemList()
        {
            onShowItems?.Invoke();
        }

        public event Action<bool>? onShowSelectorItemList;

        public void ShowSelectorItemList(bool isTrue)
        {
            onShowSelectorItemList?.Invoke(isTrue);
        }

        public event Action<int>? onReward;

        public void Reward(int exp)
        {
            onReward?.Invoke(exp);
        }

        public event Action? onRespawnEnemy;

        // 적을 리스폰하는 함수
        public void RespawnEnemy()
        {
            onRespawnEnemy?.Invoke();
        }
        #region 던전 내부 진입 시
        public event Action? onEnterDungeon;

        public void EnterDungeon()
        {
            onEnterDungeon?.Invoke();
        }

        #endregion

        #region 배틀 공격 대상 선택 시
        
        public event Action? onSelectEnemy;

        public void SelectEnemy()
        {
            onSelectEnemy?.Invoke();
        }

        #endregion

        #region 플레이어의 공격과 결과 출력
        public event Action<int, float>? onPlayerAttack;

        public void StartPlayerAttack(int sel, float atk)
        {
            onPlayerAttack?.Invoke(sel, atk);
        }

        #endregion


        #region 플레이어가 스킬 공격을 시도 시

        public event Action<int, float>? onPlayerSkillAttack;

        public void StartPlayerSkillAttack(int sel, float atk)
        {
            onPlayerSkillAttack?.Invoke(sel, atk);
        }

        public event Action<int>? onCheckCount;

        public void CheckCount(int num)
        {
            onCheckCount?.Invoke(num);
        }

        #endregion

        #region 에너미의 공격 시
        public event Action? onEnemyAttack;

        public void EnemyAttack()
        {
            onEnemyAttack?.Invoke();
        }

        #endregion

        //#region 에너미의 공격 결과
        //public event Action? onEnemyAttackResult;

        //public void EnemyAttackRsult()
        //{
        //    onEnemyAttackResult?.Invoke();
        //}
        //#endregion


        #region 상점

        public event Action<bool>? onShowShopList;
        public void ShowShopList(bool istrue)
        {
            onShowShopList?.Invoke(istrue);
        }

        public event Action<Equipment>? onDetach;
        public void Detached(Equipment equip)
        {
            onDetach?.Invoke(equip);
        }
        #endregion
    }
}
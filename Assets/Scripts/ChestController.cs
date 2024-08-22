using ChestSystem.Chests;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        public ChestView chestView { get; private set; }
        public ChestModel chestModel { get; private set; }
        public float UnlockTimer { get; private set; }
        public float TimeSecondsPerGem { get { return 100f; } internal set { } }
        public ChestStates ChestState { get { return currentState.GetState(); } private set { } }


        public IChestStates currentState { get; private set; }
        public LockedState chestLocked { get; private set; }
        public UnlockingState chestUnlocking { get; private set; }
        public UnlockedState chestUnlocked { get; private set; }
        public ChestController()
        {
            chestLocked = new LockedState(this);
            chestUnlocking = new UnlockingState(this);
            chestUnlocked = new UnlockedState(this);

            //state hasn't been enabled yet.
            currentState = chestLocked;
        }

        private void Initilization()
        {
            chestView.SetChestController(this);
            UnlockTimer = chestModel.UnlockDuration;
        }
        public void SetModel(ChestModel chestModel)
        {
            this.chestModel = chestModel;
        }
        public void SetInitialState()
        {
            currentState = chestLocked;
            currentState.OnStateEnable();
        }
        public void SetChestView()
        {
            this.chestView = ChestPoolService.Instance.GetFromPool(this);
            SetInitialState();
        }
        internal void UnlockNow()
        {
            PlayerServices.Instance.DecrementGems(currentState.GetRequiredGemsToUnlock());
            currentState.OnStateDisable();
            currentState = chestUnlocked;
            currentState.OnStateEnable();
        }

        internal void StartUnlocking()
        {
            currentState.OnStateDisable();
            currentState = chestUnlocking;
            currentState.OnStateEnable();
        }

        internal void RemoveView()
        {
            ChestPoolService.Instance.ReturnToPool(this.chestView);
            this.chestView = null;
        }

        internal void ChestButtonAction()
        {
            currentState.ChestButtonAction();
        }
        public ChestType GetChestType()
        {
            return chestModel.chestType;
        }
    }
}


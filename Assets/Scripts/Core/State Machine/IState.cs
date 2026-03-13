namespace ProjectZomboid.Core.StateMachine
{
    // Interface for states
    public interface IState<TOwner>
    {
        // TOwner represents Generic Owner
        public TOwner Owner { get; set; }
        public void OnEnterState();
        public void UpdateState();
        public void OnExitState();
    }
}
namespace CodeBase.Services.StateMachine.Common.Interface
{
    public interface IEnterableState : IState
    {
        void Enter();
    }
}
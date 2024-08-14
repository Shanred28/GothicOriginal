namespace CodeBase.Services.StateMachine.Common.Interface
{
    public interface IExittableState : IState
    {
        void Exit();
    }
}
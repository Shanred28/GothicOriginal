using CodeBase.Services.StateMachine.Common.Interface;

namespace CodeBase.Services.StateMachine.LevelStateMachine.Interface
{
    public interface ILevelStateSwitcher : IService
    {
        object CurrentState { get; }

        void AddState<TState>(TState state) where TState : class, IState;
        void EnterState<TState>() where TState : class, IState;
        void ExitState<TState>() where TState : class, IState;
        void RemoveState<TState>() where TState : class, IState;
        void UpdateTick();

    }
}
using CodeBase.Services.StateMachine.LevelStateMachine;
using VContainer;

namespace CodeBase.Infrastructure.Bootstraper
{
    public class LevelBootstrapper : MonoBootstrapper
    {
        private ILevelStateSwitcher _levelStateSwitcher;
        private LevelBootstrapState _levelBootstrapState;
        
        [Inject]
        public void Constructor( ILevelStateSwitcher levelStateSwitcher, LevelBootstrapState levelBootstrapState)
        {
            _levelStateSwitcher = levelStateSwitcher;
            _levelBootstrapState = levelBootstrapState;
        }

        public override void OnBindResolved()
        {
            InitLevelStateMachine();
        }

        private void InitLevelStateMachine()
        {
            _levelStateSwitcher.AddState(_levelBootstrapState);
            _levelStateSwitcher.EnterState<LevelBootstrapState>();
        }
    }
}
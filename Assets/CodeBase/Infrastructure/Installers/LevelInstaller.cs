using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInstaller : LifetimeScope
    {
        /*[SerializeField] private LevelStateMachineTicker ticker;
        public override void InstallBindings()
        {
            Debug.Log("LEVEL: Install");
            
            _dIContainer.RegisterSingle(ticker);
            
            RegisterLevelStateMachine();
        }
        
        private void OnDestroy()
        {
            _dIContainer.Unregister<LevelStateMachineTicker>();
            
            UnregisterLevelStateMachine();
        }
        
        private void RegisterLevelStateMachine()
        {
            _dIContainer.RegisterSingle<ILevelStateSwitcher, LevelStateMachine>();
            _dIContainer.RegisterSingle<LevelBootstrapState>();
            _dIContainer.RegisterSingle<LevelLostState>();
            _dIContainer.RegisterSingle<LevelResearcherState>();
            _dIContainer.RegisterSingle<LevelVictoryState>();
        }
        
        private void UnregisterLevelStateMachine()
        {
            _dIContainer.Unregister<ILevelStateSwitcher>();
            _dIContainer.Unregister<LevelBootstrapState>();
        }*/
    }
}


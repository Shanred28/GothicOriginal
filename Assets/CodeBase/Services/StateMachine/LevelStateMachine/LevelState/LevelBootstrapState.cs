using CodeBase.Configs.Interface;
using CodeBase.Services.StateMachine.Common.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine.Interface;
using CodeBase.Services.WindowsProvider;
using CodeBase.UI.MainUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.StateMachine.LevelStateMachine.LevelState
{
    public class LevelBootstrapState : IEnterableState, IService
    {
        private readonly ILevelStateSwitcher _levelStateSwitcher;
        private readonly IConfigsProvider _configsProvider;
        private readonly IWindowsProvider _windowsProvider;

        public LevelBootstrapState(ILevelStateSwitcher levelStateSwitcher, IConfigsProvider configsProvider, IWindowsProvider windowsProvider)
        {
            _levelStateSwitcher = levelStateSwitcher;
            _configsProvider = configsProvider;
            _windowsProvider = windowsProvider;
        }

        public void Enter()
        {
            var config = _configsProvider.GetSceneConfig(SceneManager.GetActiveScene().name);
            _windowsProvider.OpenWindow(WindowMainUIId.MainMenuWindow);
            
            //Todo: add state all click buttons
            
            /*string sceneName = SceneManager.GetActiveScene().name;
            LevelConfig levelConfig = _configsProvider.GetLevelConfig(sceneName);*/

            //EnemySpawner[] enemiesSpawners =GameObject.FindObjectsOfType<EnemySpawner>();

            /*for (int i = 0; i < enemiesSpawners.Length; i++)
            {
                enemiesSpawners[i].SpawnEnemy();
            }*/

            //_gameFactory.CreateHero(levelConfig.HeroSpawnPoint, Quaternion.identity);
            /*_gameFactory.CreateFollowCamera().SetTarget(_gameFactory.HeroObject.transform);
            _gameFactory.CreateVirtualJoystick();*/
            
            
            //_levelStateSwitcher.EnterState<LevelResearcherState>();
        }
    }
}
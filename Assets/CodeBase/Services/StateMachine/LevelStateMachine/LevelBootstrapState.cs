using CodeBase.Configs.Interface;
using CodeBase.Configs.Level;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.Services.StateMachine.Common.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.StateMachine.LevelStateMachine
{
    public class LevelBootstrapState : IEnterableState, IService
    {
        private readonly IGameFactory _gameFactory;
        private readonly ILevelStateSwitcher _levelStateSwitcher;
        private readonly IConfigsProvider _configsProvider;
        
        public LevelBootstrapState(IGameFactory gameFactory, ILevelStateSwitcher levelStateSwitcher, IConfigsProvider configsProvider)
        {
            _gameFactory = gameFactory;
            _levelStateSwitcher = levelStateSwitcher;
            _configsProvider = configsProvider;
        }

        public void Enter()
        {
            Debug.Log("LEVEL: Init");
            
            
            string sceneName = SceneManager.GetActiveScene().name;
            LevelConfig levelConfig = _configsProvider.GetLevelConfig(sceneName);

            //EnemySpawner[] enemiesSpawners =GameObject.FindObjectsOfType<EnemySpawner>();

            /*for (int i = 0; i < enemiesSpawners.Length; i++)
            {
                enemiesSpawners[i].SpawnEnemy();
            }*/

            _gameFactory.CreateHero(levelConfig.HeroSpawnPoint, Quaternion.identity);
            /*_gameFactory.CreateFollowCamera().SetTarget(_gameFactory.HeroObject.transform);
            _gameFactory.CreateVirtualJoystick();*/
            
            
            //_levelStateSwitcher.EnterState<LevelResearcherState>();
        }
    }
}
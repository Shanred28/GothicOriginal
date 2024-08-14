using CodeBase.Configs.Interface;
using CodeBase.Services.AssetManager;
using CodeBase.Services.Factory.EntityFactory.Interface;
using UnityEngine;

namespace CodeBase.Services.Factory.EntityFactory
{
    public class GameFactory : IGameFactory
    {
        public GameObject HeroObject { get; private set; }
        /*public VirtualJoystick VirtualJoystick { get; private set; }
        public FollowCamera FollowCamera { get; private set; }
        public HeroHealth HeroHeals { get; private set; }*/

        private readonly IAssetProvider _assetProvider;
        private readonly IConfigsProvider _configProvider;

        public GameFactory(IAssetProvider assetProvider, IConfigsProvider configProvider)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
        }

        public GameObject CreateHero(Vector3 position, Quaternion rotation)
        {
           // GameObject heroPrefab = _assetProvider.GetPrefab<GameObject>(AsseetPath.HeroPath);
            //HeroObject = _diContainer.InstantiateGameObject(heroPrefab);

            HeroObject.transform.position = position;
            HeroObject.transform.rotation = rotation;
            //HeroHeals = HeroObject.GetComponent<HeroHealth>();
            
            return HeroObject;
        }

        /*public VirtualJoystick CreateVirtualJoystick()
        {
            GameObject virtualJoystickPrefab = _assetProvider.GetPrefab<GameObject>(AsseetPath.VirtualJoystickPath);

            VirtualJoystick = _diContainer.InstantiateGameObject(virtualJoystickPrefab).GetComponent<VirtualJoystick>();

            return VirtualJoystick;
        }*/

        /*public FollowCamera CreateFollowCamera()
        {
            GameObject followCameraPrefab = _assetProvider.GetPrefab<GameObject>(AsseetPath.FollowCameraPath);

            FollowCamera = _diContainer.InstantiateGameObject(followCameraPrefab).GetComponent<FollowCamera>();

            return FollowCamera;
        }*/

        public GameObject CreateEnemy(Vector3 position, Quaternion rotation)
        {
            /*EnemyConfig config = _configProvider.GetEnemyConfig(id);
            GameObject enemyPrefab = config.Prefab;
            GameObject enemy = _diContainer.InstantiateGameObject(enemyPrefab);

            enemy.transform.position = position;
            enemy.transform.rotation = rotation;
            
            IEnemyConfigInstaller[] enemyConfigInstallers = enemy.GetComponentsInChildren<IEnemyConfigInstaller>();

            for (int i = 0; i < enemyConfigInstallers.Length; i++)
            {
                enemyConfigInstallers[i].InstallConfig(config);
            }

            return enemy;*/
            return null;
        }
    }
}
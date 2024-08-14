using UnityEngine;

namespace CodeBase.Services.Factory.EntityFactory.Interface
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 position, Quaternion rotation);
        /*VirtualJoystick CreateVirtualJoystick();
        FollowCamera CreateFollowCamera();*/
        
       // GameObject CreateEnemy(EnemyId id, Vector3 position, Quaternion rotation);

        GameObject HeroObject { get; }
        /*VirtualJoystick VirtualJoystick { get; }
        FollowCamera FollowCamera { get; }
        HeroHealth HeroHeals { get;  }*/
    }
}


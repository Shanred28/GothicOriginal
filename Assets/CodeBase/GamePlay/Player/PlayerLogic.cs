using System;
using CodeBase.GamePlay.Common;
using CodeBase.GamePlay.Player.ControllerCharacter;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private PlayerInfoHolder playerInfo;

        private CharacterInputController _characterInputController;
        private CharacterMovementHuman _characterMovementHuman;

        private void Start()
        {
            _characterMovementHuman = new CharacterMovementHuman(playerInfo);
            _characterInputController = new CharacterInputController(_characterMovementHuman, playerInfo.ThirdPersonCamera);
            
            Initialize();
        }

        private void Initialize()
        {
            _characterMovementHuman.Enter();
            _characterInputController.Enter();
        }
    }
}


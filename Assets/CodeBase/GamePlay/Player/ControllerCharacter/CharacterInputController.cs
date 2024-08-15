using CodeBase.Common.Interface;
using CodeBase.Common.Ticker;
using CodeBase.Common.Ticker.Interfaces;
using CodeBase.GamePlay.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.GamePlay.Player.ControllerCharacter
{
    public class CharacterInputController : ILogic, IUpdateable
    {
        private InputSystem_Actions _inputKeyBoard;
        private readonly CharacterMovementHuman _characterMovement;
        private readonly ThirdPersonCamera _thirdPersonCamera;


        public CharacterInputController(CharacterMovementHuman characterMovement, ThirdPersonCamera thirdPersonCamera)
        {
            _characterMovement = characterMovement;
            _thirdPersonCamera = thirdPersonCamera;
        }

        public void Enter()
        {
            _inputKeyBoard = new InputSystem_Actions();
            _inputKeyBoard.Player.Enable();
            _inputKeyBoard.Player.Jump.started += OnJump;
            _inputKeyBoard.Player.Sprint.started += OnSprint;
            
            Ticker.RegisterUpdateable(this);
        }
        
        public void OnUpdate()
        {
            _characterMovement.TargetDirectionControl = new Vector3(_inputKeyBoard.Player.Move.ReadValue<Vector2>().x, 0,  _inputKeyBoard.Player.Move.ReadValue<Vector2>().y);
            _thirdPersonCamera.RotationControl = new Vector2(_inputKeyBoard.Player.Look.ReadValue<Vector2>().x, _inputKeyBoard.Player.Look.ReadValue<Vector2>().y);
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            _characterMovement.Sprint();
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            _characterMovement.Jump();
        }

        public void Exit()
        {
            Ticker.UnregisterUpdateable(this);
            _inputKeyBoard.Player.Sprint.started -= OnSprint;
            _inputKeyBoard.Player.Jump.started -= OnJump;
        }
    }
}
using System;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class TestMoveControllerForIKLocomotion : MonoBehaviour
    {
        public Vector3 DirectionControl;
        public Vector3 _movementDirections;
        
        public CharacterController _characterController;
        public Transform _controllingModel;
        
        public float _walkSpeed;
        public float _runSpeed;
        
        private InputSystem_Actions _inputKeyBoard;

        private void Start()
        {
            _inputKeyBoard = new InputSystem_Actions();
            _inputKeyBoard.Player.Enable();
        }

        public void Update()
        {
            DirectionControl = new Vector3(_inputKeyBoard.Player.Move.ReadValue<Vector2>().x, 0,  _inputKeyBoard.Player.Move.ReadValue<Vector2>().y);
            _movementDirections = DirectionControl * GetCurrentSpeedByState();
            _movementDirections += Physics.gravity * Time.fixedDeltaTime;
            _characterController.Move(_movementDirections * Time.fixedDeltaTime);
        }
        
        public float GetCurrentSpeedByState()
        {
            /*if (IsSprint.Value)
                return _runSpeed;*/

            return _walkSpeed;
        }
        
        /*private void TargetControlMove()
        {
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * _accelerationRate);
        }*/
    }
}


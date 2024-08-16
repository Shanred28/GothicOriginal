using CodeBase.Common.Interface;
using CodeBase.Common.Ticker;
using CodeBase.Common.Ticker.Interfaces;
using CodeBase.GamePlay.Player;
using UnityEngine;

namespace CodeBase.GamePlay.Common
{
    public class CharacterMovementHuman : IUpdateable,IFixedUpdateable, ILogic
    {
        public float DistanceToGround => _distanceToGround;
        public bool IsGrounded => _characterController.isGrounded || _distanceToGround < 0.09f;
        public float CurrentSpeed => GetCurrentSpeedByState();
        public Vector3 TargetDirectionControl;
        
        public bool IsSprint;
        public bool IsCrouch;
        public bool IsFight;
        public bool IsJump;
       
        public Vector3 DirectionControl;
        private Vector3 _movementDirections;
        
        private readonly PlayerInfoHolder _playerInfoHolder;
        
        private CharacterController _characterController;
        private Transform _controllingModel;
        private float _accelerationRate;
        private float _distanceToGround;
        private float _walkSpeed;
        private float _runSpeed;
        private float _jumpSpeed;
        
        private bool _isSliding;
        private Vector3 _slopeSlideVelocity;
        private float _ySpeed;
        
        private float _distanceForRayToGround;
        private float _distanceForRaySlopeSlide;

        public CharacterMovementHuman(PlayerInfoHolder playerInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
        }
        
        public void Enter()
        {
            _characterController = _playerInfoHolder.CharacterController;
            _controllingModel = _playerInfoHolder.PlayerTransform;
            
            _walkSpeed = _playerInfoHolder.WalkSpeed;
            _runSpeed = _playerInfoHolder.RunSpeed;
            _jumpSpeed = _playerInfoHolder.JumpSpeed;
            _accelerationRate = _playerInfoHolder.AccelerationRate;
            _ySpeed = _playerInfoHolder.SpeedSlider;
            _distanceForRayToGround = _playerInfoHolder.DistanceForRayToGround;
            _distanceForRaySlopeSlide = _playerInfoHolder.DistanceForRaySlopeSlide;
            
            Ticker.RegisterUpdateable(this);
            Ticker.RegisterFixedUpdateable(this);
        }

        public void OnUpdate()
        {
            SetSlopeSlide();
            UpdateDistanceToGround();
            TargetControlMove();
        }

        public void OnFixedUpdate()
        {
            Move();
        }
        
        public void Exit()
        {
            Ticker.UnregisterUpdateable(this);
            Ticker.UnregisterFixedUpdateable(this);
        }

        private void Move()
        {
            if (IsGrounded == true && _isSliding == false)
            {
                _movementDirections = DirectionControl * GetCurrentSpeedByState();
                if (IsJump == true)
                {
                    _movementDirections.y = _jumpSpeed;
                    IsJump = false;
                }

                _movementDirections = _controllingModel.TransformDirection(_movementDirections);
                _movementDirections += Physics.gravity * Time.fixedDeltaTime;
               
                _characterController.Move(_movementDirections * Time.fixedDeltaTime);
            }
            
            _movementDirections += Physics.gravity * Time.fixedDeltaTime;
            _characterController.Move(_movementDirections * Time.fixedDeltaTime);
            
            if (_isSliding)
            {
                Vector3 velocity = _slopeSlideVelocity;
                velocity.y = _ySpeed;

                _characterController.Move(velocity * Time.deltaTime);
            }
        }
        
        private void TargetControlMove()
        {
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * _accelerationRate);

            if (_slopeSlideVelocity == Vector3.zero)
            {
                _isSliding = false;
            }
            else
                _isSliding = true;
            
        }
        public void Sprint()
        {
            if (IsGrounded == false) return;
            if (IsCrouch) return;

            if (IsSprint)
            {
                IsSprint = false;
            }
            else 
                IsSprint = true;
        }
        
        public void Jump()
        {
            if (IsGrounded == false) return;
            if (IsFight || IsCrouch) return;

            IsJump = true;
        }
        
        public float GetCurrentSpeedByState()
        {
            if (IsSprint)
                return _runSpeed;

            return _walkSpeed;
        }
        
        private void SetSlopeSlide()
        {
            if (Physics.Raycast(_controllingModel.position, Vector3.down, out RaycastHit hitInfo, _distanceForRaySlopeSlide))
            {
                float angle = Vector3.Angle(hitInfo.normal,Vector3.up);

                if (angle >= _characterController.slopeLimit)
                {
                    _slopeSlideVelocity = Vector3.ProjectOnPlane(new Vector3(0, _ySpeed, 0), hitInfo.normal);
                    return;
                }   
            }

            if (_isSliding)
            { 
                //TODO
                _slopeSlideVelocity -= _slopeSlideVelocity * Time.deltaTime * 3;

                if (_slopeSlideVelocity.magnitude > 1)
                {
                    return;
                }
            }

            _slopeSlideVelocity = Vector3.zero;
        }
        private void UpdateDistanceToGround()
        {
            if (Physics.Raycast(_controllingModel.position, Vector3.down, out RaycastHit hit, _distanceForRayToGround))
            {
                _distanceToGround = Vector3.Distance(_controllingModel.position, hit.point);
            }
        }
    }
}



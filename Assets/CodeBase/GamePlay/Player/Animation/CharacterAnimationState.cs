using CodeBase.Common.Interface;
using CodeBase.Common.Ticker;
using CodeBase.Common.Ticker.Interfaces;
using CodeBase.GamePlay.Common;
using CodeBase.GamePlay.Player;
using UnityEngine;
using UniRx;

public class CharacterAnimationState : ILogic, ILateUpdateable
{
    private const float INPUT_CONTROL_LERP = 10f;

    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    private readonly PlayerInfoHolder _playerInfoHolder;
    
    private CharacterController _targetCharacterController;
    private readonly CharacterMovementHuman _characterMovement;
    private Transform _targetTransform;
    
    private CharacterAnimatorParametersName _animatorParametersName;
    
    private AnimationCrossFadeParameters _jumpIdleFade;
    private AnimationCrossFadeParameters _jumpMoveFade;
    private AnimationCrossFadeParameters _fallFade;
    
    private Animator _targetAnimator;
    private float _minDistanceToGroundByFall;
    private Vector3 _inputControl;
    
    public CharacterAnimationState(PlayerInfoHolder playerInfoHolder, CharacterMovementHuman targetCharacterController)
    {
        _playerInfoHolder = playerInfoHolder;
        _characterMovement = targetCharacterController;
    }

    public void Enter()
    {
        _targetCharacterController = _playerInfoHolder.CharacterController;
        _targetTransform = _playerInfoHolder.PlayerTransform;
        _animatorParametersName = _playerInfoHolder.CharacterAnimatorParametersName;
        _jumpIdleFade = _playerInfoHolder.JumpIdleFade;
        _jumpMoveFade = _playerInfoHolder.JumpMoveFade;
        _fallFade = _playerInfoHolder.FallFade;
        
        _targetAnimator = _playerInfoHolder.TargetAnimator;
        _minDistanceToGroundByFall = _playerInfoHolder.MinDistanceToGroundByFall;
        
        SubscribeReactiveProperty();
        Ticker.RegisterLateUpdateable(this);
    }

    private void SubscribeReactiveProperty()
    {
        _characterMovement.IsSprint.Subscribe( v => _targetAnimator.SetBool(_animatorParametersName.Sprint, v)).AddTo(_disposables);
        _characterMovement.IsCrouch.Subscribe(v => _targetAnimator.SetBool(_animatorParametersName.Crouch, v)).AddTo(_disposables);
        _characterMovement.IsFight.Subscribe(v => _targetAnimator.SetBool(_animatorParametersName.Fight, v)).AddTo(_disposables);
        _characterMovement.IsGrounded.Subscribe(v => _targetAnimator.SetBool(_animatorParametersName.Ground, v)).AddTo(_disposables);
    }

    public void OnLateUpdate()
    {
         Vector3 movementSpeed = _targetTransform.InverseTransformDirection(_targetCharacterController.velocity);
        _inputControl = Vector3.MoveTowards(_inputControl, _characterMovement.DirectionControl, Time.deltaTime * INPUT_CONTROL_LERP);
        _targetAnimator.SetFloat(_animatorParametersName.NormolizeMovementX, _inputControl.x);
        _targetAnimator.SetFloat(_animatorParametersName.NormolizeMovementZ, _inputControl.z);

        Vector3 groundSpeed = _targetCharacterController.velocity;
        groundSpeed.y = 0;
        _targetAnimator.SetFloat(_animatorParametersName.GroundSpeed, groundSpeed.magnitude);

        if (_characterMovement.IsJump)
        {
            if (groundSpeed.magnitude <= 0.03f)
            {
                CrossFade(_jumpIdleFade);
            }

            if (groundSpeed.magnitude > 0.03f)
            {
                CrossFade(_jumpMoveFade);
            }
        }

        if (_characterMovement.IsGrounded.Value == false && _characterMovement.IsGrounded.Value == false)
        {
            _targetAnimator.SetFloat(_animatorParametersName.Jump, movementSpeed.y);

            if (movementSpeed.y < 0 && _characterMovement.DistanceToGround > _minDistanceToGroundByFall)
            {
                CrossFade(_fallFade);
            }
            _targetAnimator.SetFloat(_animatorParametersName.Jump, movementSpeed.y);
        }
        else
            _targetAnimator.SetFloat(_animatorParametersName.Jump, movementSpeed.y);

        _targetAnimator.SetFloat(_animatorParametersName.DistanceToGround, _characterMovement.DistanceToGround);
    }
    
    private void CrossFade(AnimationCrossFadeParameters parameters)
    {
        _targetAnimator.CrossFade(parameters.Name, parameters.Duration);
    }

    public void Exit()
    {
        _disposables.Clear();
        Ticker.UnregisterLateUpdateable(this);
    }
}

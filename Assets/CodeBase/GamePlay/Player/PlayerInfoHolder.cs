using System;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    [Serializable]
    public class CharacterAnimatorParametersName
    {
        public string NormolizeMovementX;
        public string NormolizeMovementZ;
        public string Sprint;
        public string Crouch;
        public string Fight;
        public string Ground;
        public string Jump;
        public string GroundSpeed;
        public string DistanceToGround;
    }

    [Serializable]
    public class AnimationCrossFadeParameters
    {
        public string Name;
        public float Duration;
    }
    
    
    public class PlayerInfoHolder : MonoBehaviour
    {
        public CharacterController CharacterController => characterController;
        public Transform PlayerTransform => playerTransform;
        public PlayerLogic PlayerLogic => playerLogic;
        public ThirdPersonCamera ThirdPersonCamera => thirdPersonCamera;
        
        //speed movement
        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float JumpSpeed => jumpSpeed;
        public float AccelerationRate => accelerationRate;
        public float SpeedSlider => speedSlider;
        
        //state 
        public float MaxHp => maxHp;
        public float MaxManna => maxManna;
        
        //Setting ray
        public float DistanceForRayToGround => distanceForRayToGround;
        public float DistanceForRaySlopeSlide => distanceForRaySlopeSlide;
        
        //Setting for animation
        
        public Animator TargetAnimator => targetAnimator;
        public CharacterAnimatorParametersName CharacterAnimatorParametersName => characterAnimatorParametersName;
        public AnimationCrossFadeParameters FallFade => fallFade;
        public float MinDistanceToGroundByFall => minDistanceToGroundByFall;
        public AnimationCrossFadeParameters JumpIdleFade => jumpIdleFade;
        
        public AnimationCrossFadeParameters JumpMoveFade => jumpMoveFade;
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerLogic playerLogic;
        [SerializeField] private ThirdPersonCamera thirdPersonCamera;
        
        [Header("Player speed")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float accelerationRate;
        [SerializeField] private float speedSlider;

        [Header("PlayerStates")] 
        [SerializeField] private float maxHp;
        [SerializeField] private float maxManna;

        [Header("Setting for ray")]
        [SerializeField] private float distanceForRayToGround;
        [SerializeField] private float distanceForRaySlopeSlide;

        [Header("Setting for animation")]
        [SerializeField] private Animator targetAnimator;
        
        [Header("Animator Parameters Name")]
        [SerializeField] private CharacterAnimatorParametersName characterAnimatorParametersName;
        
        [Header("Animation Cross Fade Parameters")]
        [SerializeField] private AnimationCrossFadeParameters fallFade;
        [SerializeField] private float minDistanceToGroundByFall;
        [SerializeField] private AnimationCrossFadeParameters jumpIdleFade;
        [SerializeField] private AnimationCrossFadeParameters jumpMoveFade;
    }
}


using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class PlayerInfoHolder : MonoBehaviour
    {
        public CharacterController CharacterController => characterController;
        public Transform PlayerTransform => playerTransform;
        public PlayerLogic PlayerLogic => playerLogic;
        public ThirdPersonCamera ThirdPersonCamera => thirdPersonCamera;
        
        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float JumpSpeed => jumpSpeed;
        public float AccelerationRate => accelerationRate;
        public float SpeedSlider => speedSlider;
        
        public float MaxHp => maxHp;
        public float MaxManna => maxManna;
        
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
    }
}


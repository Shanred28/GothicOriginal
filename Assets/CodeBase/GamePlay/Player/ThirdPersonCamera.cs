using CodeBase.Common.Ticker;
using CodeBase.Common.Ticker.Interfaces;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class ThirdPersonCamera : MonoBehaviour, IUpdateable
    {
        [HideInInspector] public Vector2 RotationControl;
        
        [SerializeField] private Transform targetCameraFollowPoint;
        [SerializeField] private Transform playerModel;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float minAngle ,maxAngle;

        private void Start()
        {
            Ticker.RegisterUpdateable(this);
        }
        
        private void OnDestroy()
        {
            Ticker.UnregisterUpdateable(this);
        }

        public void OnUpdate()
        {
            //Horizontal
            targetCameraFollowPoint.rotation *= Quaternion.AngleAxis(RotationControl.x * rotationSpeed, Vector3.up);

            //Vertical
            targetCameraFollowPoint.rotation *= Quaternion.AngleAxis(-RotationControl.y * rotationSpeed, Vector3.right);

            AngleCameraRotation();
        }
        
        private void AngleCameraRotation()
        {
            Vector3 angles = targetCameraFollowPoint.localEulerAngles;
            angles.z = 0;

            if (angles.x > 180 && angles.x < maxAngle)
            {
                angles.x = maxAngle;
            }
            else if (angles.x < 180 && angles.x > minAngle)
            {
                angles.x = minAngle;
            }

            targetCameraFollowPoint.localEulerAngles = angles;

            playerModel.rotation = Quaternion.Euler(0, targetCameraFollowPoint.eulerAngles.y, 0);
            targetCameraFollowPoint.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}


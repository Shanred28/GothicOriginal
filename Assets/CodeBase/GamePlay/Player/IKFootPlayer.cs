using System;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    public class IKFootPlayer : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform root;
        
        private Transform _leftFoot;
        private Transform _rightFoot;
        
        [Range(0f, 1f)]
        [SerializeField] private float leftFootWeight;
        [Range(0f, 1f)]
        [SerializeField] private float rightFootWeight;
        
        private Vector3 _leftFootPosition;
        private Vector3 _rightFootPosition;
        private Quaternion _leftFootRotation;
        private Quaternion _rightFootRotation;

        [SerializeField] private float offsetY;
        [SerializeField] private float offsetBodyY;

        [SerializeField] private float clampWeight;
        [SerializeField] private Transform targetPosition;
        
        [SerializeField] private Transform targetLeft;
        [SerializeField] private Transform targetRight;

        [SerializeField] private Transform targetForLeft;
        [SerializeField] private Transform targetForRight;

        [Range(-2f, 2f)] [SerializeField] private float leftPos;
        [Range(-2f, 2f)] [SerializeField] private float rightPos;

        [Range(0f, 1f)] [SerializeField] private float moveWalk;
        [SerializeField] private float acselerateRateeMoveLeg;

        private float _leftFootStart;
        
        [SerializeField] private AnimationCurve  moveLeftLeg;
        [SerializeField] private AnimationCurve  moveLegPerY;
        
        [SerializeField] private float rateForLerpMoveLeg;
        
        [SerializeField] private Transform pointForLeftLeg;
        
        private void Start()
        {
            _leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            _leftFootStart = _leftFoot.position.y;
            _leftFootRotation = _leftFoot.rotation;
            _rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
            _rightFootRotation = _rightFoot.rotation;
           // animator.SetBool("IsSprint", true);
        }

        private bool isLeftMoveEnd;
        private bool isRightMoveEnd;
        
        private float leftHight;
        private float rightHight;

        private void Update()
        {
            
            Vector3 lPos = _leftFoot.position;
            if (moveWalk > 0)
            {
                targetForLeft.position = new Vector3(targetForLeft.position.x, targetForLeft.position.y,leftPos);
                
                if (isLeftMoveEnd)
                {
                    pointForLeftLeg.position = Vector3.MoveTowards(pointForLeftLeg.position, new Vector3(pointForLeftLeg.position.x, pointForLeftLeg.position.y,1.1f), Time.deltaTime * acselerateRateeMoveLeg);
                    rightPos = Mathf.MoveTowards(rightPos, -1.1f, Time.deltaTime * acselerateRateeMoveLeg);
                    Debug.Log("Left leg +");
                    Debug.Log(pointForLeftLeg.localPosition.z);
                    if (pointForLeftLeg.localPosition.z >= 0.45f) isLeftMoveEnd = false;
                }
                else
                {
                    pointForLeftLeg.position = Vector3.MoveTowards(pointForLeftLeg.position, new Vector3(pointForLeftLeg.position.x, pointForLeftLeg.position.y,-1.1f), Time.deltaTime * acselerateRateeMoveLeg);
                    rightPos = Mathf.MoveTowards(rightPos, 1.1f, Time.deltaTime * acselerateRateeMoveLeg);
                    Debug.Log("Left leg -");
                    Debug.Log(pointForLeftLeg.localPosition.z);
                    if (pointForLeftLeg.localPosition.z <= -0.45f) isLeftMoveEnd = true;
                }
                
               
                if(Physics.Raycast(pointForLeftLeg.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit leftHit, 2))
                {
                    _leftFootPosition= Vector3.MoveTowards(lPos, leftHit.point + Vector3.up * offsetY, Time.deltaTime * rateForLerpMoveLeg);
                    _leftFootRotation= Quaternion.FromToRotation(_leftFoot.up, leftHit.normal) * _leftFoot.rotation;
                }
                Debug.DrawRay(pointForLeftLeg.position + Vector3.up * 0.5f, Vector3.down, Color.red);
            }
            else
            {
                if (Physics.Raycast(lPos + Vector3.up * 0.5f, Vector3.down, out RaycastHit leftHit, 2))
                {
                    //TODO для Idle
                    _leftFootPosition = Vector3.Lerp(lPos, leftHit.point + Vector3.up * offsetY, Time.deltaTime * 30f);
                    _leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
                }
            
                leftPos = Mathf.MoveTowards(leftPos, 0f, Time.deltaTime * acselerateRateeMoveLeg);
                rightPos = Mathf.MoveTowards(rightPos, 0f, Time.deltaTime * acselerateRateeMoveLeg);
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (moveWalk > 0)
            {
                // При движение включаем полный контрроль за ногами.
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,  1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
                
                //Устанавливаем значения для ног.
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);
            }
            else
            {
                //Это отлично рабатает для Idle.
                var leftWeight =  moveLeftLeg.Evaluate(Mathf.Abs(leftPos));
                var rightWeight =  moveLeftLeg.Evaluate(Mathf.Abs(rightPos));
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,  leftWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);
                
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootPosition);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootRotation);
            }
        }
    }
}

 /*private void Update()
        {
            if (moveWalk > 0)
            {
                //TODO При таком движется как хромой инвалид с дрыном в жопе. Движения должны мб идти через кривую. Думать.
                if (isLeftMoveEnd)
                {
                    leftPos = Mathf.MoveTowards(leftPos, 1.1f, Time.deltaTime *acselerateRateeMoveLeg);
                    rightPos = Mathf.MoveTowards(rightPos, -1.1f, Time.deltaTime * acselerateRateeMoveLeg);
                    if(leftPos >= 1f) isLeftMoveEnd = false;
                }
                else
                {
                    leftPos = Mathf.MoveTowards(leftPos, -1.1f, Time.deltaTime * acselerateRateeMoveLeg );
                    rightPos = Mathf.MoveTowards(rightPos, 1.1f, Time.deltaTime * acselerateRateeMoveLeg);
                    if(leftPos <= -1f) isLeftMoveEnd = true;
                }
            }
            else
            {
                leftPos = Mathf.MoveTowards(leftPos, 0f, Time.deltaTime * acselerateRateeMoveLeg);
                rightPos = Mathf.MoveTowards(rightPos, 0f, Time.deltaTime * acselerateRateeMoveLeg);
            }

            targetForLeft.localPosition= new Vector3(targetForLeft.localPosition.x, targetForLeft.localPosition.y, leftPos);
            targetForRight.localPosition = new Vector3(targetForRight.localPosition.x, targetForRight.localPosition.y, rightPos);
            
            
            //Vector3 lPos = _leftFoot.position;
            if(Physics.Raycast(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position + Vector3.up * 0.5f, Vector3.down, out RaycastHit leftHit, 2))
            {
                //TODO для Idle
                //_leftFootPosition= Vector3.Lerp(lPos, leftHit.point + Vector3.up * offsetY, Time.deltaTime * 30f);
               // _leftFootPosition= Vector3.Lerp(lPos, leftHit.point + Vector3.up * moveLegPerY.Evaluate(leftPos) , Time.deltaTime * rateForLerpMoveLeg);
                _leftFootPosition= leftHit.point + Vector3.down * (offsetY * moveLegPerY.Evaluate(leftPos));
                targetForLeft.position = new Vector3(_leftFoot.position.x, _leftFootPosition.y, leftPos);
                
                // _leftFootPosition= Vector3.Lerp(lPos, leftHit.point + Vector3.up * offsetY , Time.deltaTime * 30f);
                
                Debug.Log(moveLegPerY.Evaluate(leftPos));
                _leftFootRotation= Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
            }
            Debug.DrawRay(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position + Vector3.up * 0.5f, Vector3.down, Color.red);
            
            Vector3 rPos = _rightFoot.position;
            if(Physics.Raycast(rPos + Vector3.up * 0.5f, Vector3.down, out RaycastHit rightHit, 2))
            {
                _rightFootPosition= Vector3.Lerp(rPos, rightHit.point + Vector3.up * offsetY, Time.deltaTime * 30f);
                _rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
            } 
            
            /*var body = animator.GetBoneTransform(HumanBodyBones.Hips);
            if (Physics.Raycast(body.position, Vector3.down, out RaycastHit bodyHit, 2))
            {
                if (Vector3.Distance(body.position, bodyHit.point) < offsetBodyY ||Vector3.Distance(body.position, bodyHit.point) > offsetBodyY)
                {
                    root.transform.position = Vector3.Lerp(root.transform.position, new Vector3(root.transform.position.x, root.transform.position.y + offsetBodyY- Vector3.Distance(body.position, bodyHit.point),root.transform.position.z), Time.deltaTime * 30f);
                }
            }
            Debug.DrawRay(body.position, Vector3.down, Color.blue);#1#
        }*/

/*private void OnAnimatorIK(int layerIndex)
        {
            if (moveWalk > 0)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,  1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
                
                //var lPos = Vector3.Lerp(_leftFoot.position, _leftFootPosition, Time.deltaTime * 35);
               // var lPos = Vector3.Lerp(_leftFoot.position, _leftFootPosition, Time.deltaTime * rateForLerpMoveLeg);
                var lPos = Vector3.Lerp(_leftFoot.position, new Vector3(_leftFootPosition.x, _leftFootPosition.y, _leftFoot.position.z), Time.deltaTime * rateForLerpMoveLeg);
                var rPos = Vector3.Lerp(_rightFoot.position, _rightFootPosition, Time.deltaTime * rateForLerpMoveLeg);
                
                var lRotation = Quaternion.Lerp(_leftFoot.rotation, _leftFootRotation, Time.deltaTime * rateForLerpMoveLeg);
                var rRotation = Quaternion.Lerp(_rightFoot.rotation, _rightFootRotation, Time.deltaTime * rateForLerpMoveLeg);
                
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, lPos);
                //animator.SetIKRotation(AvatarIKGoal.LeftFoot, lRotation);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rPos);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rRotation);
            }
            else
            {
                var leftWeight =  moveLeftLeg.Evaluate(Mathf.Abs(leftPos));
                var rightWeight =  moveLeftLeg.Evaluate(Mathf.Abs(rightPos));
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,  leftWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);
                
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootPosition);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootRotation);
            }
        }*/

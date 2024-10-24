using System;
using UnityEngine;
using UnityEngine.Serialization;
using UniRx;

namespace CodeBase.GamePlay.Player
{
    public class IKFootPlayer : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform root;

        private Transform _leftFoot;
        private Transform _rightFoot;

        [Range(0f, 1f)] [SerializeField] private float leftFootWeight;
        [Range(0f, 1f)] [SerializeField] private float rightFootWeight;

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

        [SerializeField] private float timeForDefaultLegsPosition;

        private float _leftFootStart;

        [SerializeField] private AnimationCurve moveLeftLeg;

        [FormerlySerializedAs("moveLegPerY")] [SerializeField]
        private AnimationCurve moveLegPerYForForward;

        [SerializeField] private AnimationCurve moveLegPerYForBackward;

        [SerializeField] private float rateForLerpMoveLeg;

        [SerializeField] private Transform pointForLeftLeg;
        [SerializeField] private Transform pointForRightLeg;
        [SerializeField] private float stepWidth = 0.45f;

        private void Start()
        {
            _leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            _leftFootStart = _leftFoot.position.y;
            _leftFootRotation = _leftFoot.rotation;
            _rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
            _rightFootRotation = _rightFoot.rotation;

            _defaultPointForLeftLeg = pointForLeftLeg.localPosition;
            _defaultPointForRightLeg = pointForRightLeg.localPosition;
            animator.SetBool("IsSprint", true);
        }

        private bool _isLeftMoveForward;
        private bool _isRightMoveForward = false;
        private bool _isRightMoveEnd;

        private bool _isStartMove;
        private Vector3 _posTransform;
        private Vector3 _posTransformRight;
        private bool _isCanSteepRightFoot;
        private bool _isCanSteepLeftFoot;

        private Vector3 _defaultPointForLeftLeg;
        private Vector3 _defaultPointForRightLeg;

        private bool _isControlLegs;

        private IDisposable _timerForLegDefault;
        private IDisposable _updateForLegIdle;

        private void Update()
        {
            Vector3 lPos = _leftFoot.position;
            Vector3 rPos = _rightFoot.position;
            

            if (controller.velocity.magnitude > 0.1f)
            {
                _timerForLegDefault?.Dispose();
                _updateForLegIdle?.Dispose();
                
                if (_isStartMove)
                {
                    _isRightMoveForward = !_isLeftMoveForward;
                    _isCanSteepLeftFoot = true;
                    _isStartMove = false;
                }

                //Move left leg
                if (pointForLeftLeg.localPosition.z <= -stepWidth && _isLeftMoveForward == false)
                {
                    _isLeftMoveForward = true;
                }

                if (_isLeftMoveForward && _isCanSteepLeftFoot)
                {
                    _isCanSteepRightFoot = false;
                    pointForLeftLeg.position = Vector3.MoveTowards(pointForLeftLeg.position,
                        new Vector3(pointForLeftLeg.position.x, pointForLeftLeg.position.y,
                            pointForLeftLeg.position.z + 1.1f), Time.deltaTime * acselerateRateeMoveLeg);

                    if (pointForLeftLeg.localPosition.z >= stepWidth)
                    {
                        _isCanSteepRightFoot = true;
                        _isLeftMoveForward = false;
                        _posTransform = pointForLeftLeg.position;
                    }
                }
                else
                {
                    var changeDistance = _posTransform.z - transform.position.z;
                    pointForLeftLeg.localPosition = new Vector3(pointForLeftLeg.localPosition.x,
                        pointForLeftLeg.localPosition.y, changeDistance);
                }


                //Move right leg
                if (pointForRightLeg.localPosition.z <= -stepWidth && _isRightMoveForward == false)
                {
                    _isRightMoveForward = true;
                }

                if (_isRightMoveForward && _isCanSteepRightFoot)
                {
                    _isCanSteepLeftFoot = false;
                    pointForRightLeg.position = Vector3.MoveTowards(pointForRightLeg.position,
                        new Vector3(pointForRightLeg.position.x, pointForRightLeg.position.y,
                            pointForRightLeg.position.z + 1.1f), Time.deltaTime * acselerateRateeMoveLeg);

                    if (pointForRightLeg.localPosition.z >= stepWidth)
                    {
                        _isCanSteepLeftFoot = true;
                        _isRightMoveForward = false;
                        _posTransformRight = pointForRightLeg.position;
                    }
                }
                else
                {
                    var changeDistance = _posTransformRight.z - transform.position.z;
                    pointForRightLeg.localPosition = new Vector3(pointForRightLeg.localPosition.x,
                        pointForRightLeg.localPosition.y, changeDistance);
                }

                //Move left foot to target on ground
                if (Physics.Raycast(pointForLeftLeg.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit leftHit,
                        2))
                {
                    float directionLegLeftForOffsetY;

                    if (_isLeftMoveForward)
                    {
                        directionLegLeftForOffsetY = moveLegPerYForForward.Evaluate(pointForLeftLeg.localPosition.z);
                    }
                    else
                    {
                        directionLegLeftForOffsetY = 0;
                    }

                    _leftFootPosition = Vector3.MoveTowards(lPos,
                        leftHit.point + Vector3.up * (offsetY + directionLegLeftForOffsetY),
                        Time.deltaTime * rateForLerpMoveLeg);
                    _leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
                }

                Debug.DrawRay(pointForLeftLeg.position + Vector3.up * 0.5f, Vector3.down, Color.red);

                //Move right foot to target on ground
                if (Physics.Raycast(pointForRightLeg.position + Vector3.up * 0.5f, Vector3.down,
                        out RaycastHit rightHit, 2))
                {
                    float directionLegRightForOffsetY;

                    if (_isRightMoveForward)
                    {
                        directionLegRightForOffsetY = moveLegPerYForForward.Evaluate(pointForRightLeg.localPosition.z);
                    }
                    else
                    {
                        directionLegRightForOffsetY = 0;
                    }

                    _rightFootPosition = Vector3.MoveTowards(rPos,
                        rightHit.point + Vector3.up * (offsetY + directionLegRightForOffsetY),
                        Time.deltaTime * rateForLerpMoveLeg);
                    _rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
                }

                Debug.DrawRay(pointForRightLeg.position + Vector3.up * 0.5f, Vector3.down, Color.blue);


                /*var body = animator.GetBoneTransform(HumanBodyBones.Hips);
                if (Physics.Raycast(body.position, Vector3.down, out RaycastHit bodyHit, 2))
                {
                    if (Vector3.Distance(body.position, bodyHit.point) < offsetBodyY ||Vector3.Distance(body.position, bodyHit.point) > offsetBodyY)
                    {
                        root.transform.position = Vector3.Lerp(root.transform.position, new Vector3(root.transform.position.x, root.transform.position.y + offsetBodyY- Vector3.Distance(body.position, bodyHit.point),root.transform.position.z), Time.deltaTime * 30f);
                    }
                }*/
            }
            else  // Move legs and fix on ground
            {
                StoppedAfterMoving();
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            // При движение включаем полный контрроль за ногами.
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            //Устанавливаем значения для ног.
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);

            animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootPosition);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootRotation);
        }

        private bool _isStay;
        private void StoppedAfterMoving()
        {
            //if (_isStay == false) return;
            
            if (Physics.Raycast(_leftFoot.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit leftHit, 2))
            {
                _leftFootPosition = Vector3.MoveTowards(_leftFoot.position, leftHit.point + Vector3.up * offsetY,
                    Time.deltaTime * rateForLerpMoveLeg);
                _leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
            }

            if (Physics.Raycast(_rightFoot.position+ Vector3.up * 0.5f, Vector3.down, out RaycastHit rightHit, 2))
            {
                _rightFootPosition = Vector3.MoveTowards(_rightFoot.position, rightHit.point + Vector3.up * offsetY,
                    Time.deltaTime * rateForLerpMoveLeg);
                _rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
            }

            /*if (!_isStartMove)
            {
                _isStay = true;
                _timerForLegDefault = Observable.Timer(TimeSpan.FromSeconds(timeForDefaultLegsPosition))
                    .Subscribe(_ =>
                        {
                            ResetToDefaultPositionPointForLeg();
                        }
                    );
            }*/

            _isStartMove = true;

            /**/
        }

        private void ResetToDefaultPositionPointForLeg()
        {
            //_isStay = false;
            pointForLeftLeg.localPosition = _defaultPointForLeftLeg;
            pointForRightLeg.localPosition = _defaultPointForRightLeg;
            
            _updateForLegIdle = Observable.EveryUpdate().Subscribe(_ =>
            {
                RaycastHit leftHit;
                
                if (Physics.Raycast(pointForLeftLeg.position + Vector3.up * 0.5f, Vector3.down, out leftHit, 2))
                {
                    _leftFootPosition = Vector3.MoveTowards(_leftFoot.position, leftHit.point + Vector3.up * offsetY,
                        Time.deltaTime * rateForLerpMoveLeg);
                    _leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
                }

                RaycastHit rightHit;
                if (Physics.Raycast(pointForRightLeg.position + Vector3.up * 0.5f, Vector3.down, out rightHit, 2))
                {
                    _rightFootPosition = Vector3.MoveTowards(_rightFoot.position, rightHit.point + Vector3.up * offsetY,
                        Time.deltaTime * rateForLerpMoveLeg);
                    _rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
                }
                
                if((Vector3.Distance(_leftFootPosition, leftHit.point) < 0.1f) || (Vector3.Distance(_rightFootPosition, rightHit.point) < 0.1f))
                {
                    _isStay = true;
                    Debug.Log("Despose Stay");
                    _updateForLegIdle.Dispose();
                }
            });
        }
    }
}
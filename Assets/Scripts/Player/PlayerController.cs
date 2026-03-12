using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Controller
{
    public class PlayerController
    {
        private PlayerModelSO modelSO;
        private PlayerModel model;
        private PlayerView view;

        // player movement
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        public void Initialize(PlayerModelSO modelSO, PlayerView view)
        {
            model = new PlayerModel
            {
                currentHealth = modelSO.maxHealth
            };

            this.modelSO = modelSO;
            this.view = view;

            AssignAnimationIDs();

            _jumpTimeoutDelta = modelSO.JumpTimeout;
            _fallTimeoutDelta = modelSO.FallTimeout;
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public void TickUpdate()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = view.transform.position;
            spherePosition.y -= modelSO.GroundedOffset;

            model.IsGrounded = Physics.CheckSphere(spherePosition, modelSO.GroundedRadius, modelSO.GroundLayers, QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (view.HasAnimator)
                view.Animator.SetBool(_animIDGrounded, model.IsGrounded);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = view.Input.sprint ? modelSO.SprintSpeed : modelSO.MoveSpeed;

            // if there is no input, set the target speed to 0
            if (view.Input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(view.Controller.velocity.x, 0.0f, view.Controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = view.Input.analogMovement ? view.Input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * modelSO.SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
                _speed = targetSpeed;

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * modelSO.SpeedChangeRate);

            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(view.Input.move.x, 0.0f, view.Input.move.y).normalized;

            // if there is a move input rotate player when the player is moving
            if (view.Input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(view.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, modelSO.RotationSmoothTime);

                // rotate to face input direction relative to camera position
                view.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 moveDir = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            view.Controller.Move(moveDir.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (view.HasAnimator)
            {
                view.Animator.SetFloat(_animIDSpeed, _animationBlend);
                view.Animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (model.IsGrounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = modelSO.FallTimeout;

                // update animator if using character
                if (view.HasAnimator)
                {
                    view.Animator.SetBool(_animIDJump, false);
                    view.Animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                    _verticalVelocity = -2f;

                // Jump
                if (view.Input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(modelSO.JumpHeight * -2f * modelSO.Gravity);

                    // update animator if using character
                    if (view.HasAnimator)
                        view.Animator.SetBool(_animIDJump, true);
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                    _jumpTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = modelSO.JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                    _fallTimeoutDelta -= Time.deltaTime;
                else
                {
                    // update animator if using character
                    if (view.HasAnimator)
                        view.Animator.SetBool(_animIDFreeFall, true);
                }

                // if player is not grounded, do not jump
                view.Input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
                _verticalVelocity += modelSO.Gravity * Time.deltaTime;
        }

        public Vector3 GetPosition() => view.transform.position;
        public Transform GetTransform() => view.transform;
    }
}
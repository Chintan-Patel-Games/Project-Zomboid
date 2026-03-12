using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Controller.Movement
{
    public class PlayerMovement
    {
        private PlayerModel model;
        private PlayerModelSO config;
        private PlayerView view;

        // player movement
        private float speed;
        private float animationBlend;
        private float targetRotation = 0.0f;
        private float rotationVelocity;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        // timeout deltatime
        private float jumpTimeoutDelta;
        private float fallTimeoutDelta;

        public void Initialize(PlayerModel model, PlayerModelSO config, PlayerView view)
        {
            this.model = model;
            this.config = config;
            this.view = view;

            jumpTimeoutDelta = config.JumpTimeout;
            fallTimeoutDelta = config.FallTimeout;
        }

        public void GroundedCheck()
        {
            Vector3 spherePosition = view.transform.position; // set sphere position, with offset
            spherePosition.y -= config.GroundedOffset;
            model.IsGrounded = Physics.CheckSphere(spherePosition, config.GroundedRadius, config.GroundLayers, QueryTriggerInteraction.Ignore);
            view.SetGrounded(model.IsGrounded);
        }

        public void Move()
        {
            float targetSpeed = view.Input.sprint ? config.SprintSpeed : config.MoveSpeed; // set target speed based on move speed, sprint speed and if sprint is pressed
            if (view.Input.move == Vector2.zero) targetSpeed = 0.0f; // if there is no input, set the target speed to 0
            float currentHorizontalSpeed = new Vector3(view.Controller.velocity.x, 0.0f, view.Controller.velocity.z).magnitude; // a reference to the players current horizontal velocity
            float speedOffset = 0.1f;
            float inputMagnitude = view.Input.analogMovement ? view.Input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * config.SpeedChangeRate);
                speed = Mathf.Round(speed * 1000f) / 1000f; // round speed to 3 decimal places
            }
            else
                speed = targetSpeed;

            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * config.SpeedChangeRate);

            if (animationBlend < 0.01f)
                animationBlend = 0f;

            Vector3 inputDirection = new Vector3(view.Input.move.x, 0.0f, view.Input.move.y).normalized; // normalise input direction

            if (view.Input.move != Vector2.zero) // if there is a move input rotate player when the player is moving
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(view.transform.eulerAngles.y, targetRotation, ref rotationVelocity, config.RotationSmoothTime);
                view.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); // rotate to face input direction relative to camera position
            }

            Vector3 moveDir = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            // move the player
            view.Controller.Move(moveDir.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            view.SetSpeed(animationBlend);
            view.SetMotionSpeed(inputMagnitude);
        }

        public void JumpAndGravity()
        {
            if (model.IsGrounded)
            {
                fallTimeoutDelta = config.FallTimeout; // reset the fall timeout timer

                view.SetJump(false);
                view.SetFreeFall(false);

                if (verticalVelocity < 0.0f) // stop our velocity dropping infinitely when grounded
                    verticalVelocity = -2f;

                // Jump
                if (view.Input.jump && jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    verticalVelocity = Mathf.Sqrt(config.JumpHeight * -2f * config.Gravity);
                    view.SetJump(true); // update animator if using character
                }

                if (jumpTimeoutDelta >= 0.0f) // jump timeout
                    jumpTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                jumpTimeoutDelta = config.JumpTimeout; // reset the jump timeout timer

                if (fallTimeoutDelta >= 0.0f) // fall timeout
                    fallTimeoutDelta -= Time.deltaTime;
                else
                    view.SetFreeFall(true);  // update animator if using character

                view.Input.jump = false; // if player is not grounded, do not jump
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (verticalVelocity < terminalVelocity)
                verticalVelocity += config.Gravity * Time.deltaTime;
        }

        public Vector3 GetPosition() => view.transform.position;
        public Transform GetTransform() => view.transform;
    }
}
using ProjectZomboid.Core.InputSystem;
using ProjectZomboid.Noise;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectZomboid.Player.View
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerView : MonoBehaviour 
    {
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
#endif
        private Animator animator;
        public Animator Animator => animator;

        private CharacterController controller;
        public CharacterController Controller => controller;

        private InputService input;
        public InputService Input => input;

        private bool hasAnimator;

        // animation IDs
        private int animIDSpeed;
        private int animIDGrounded;
        private int animIDJump;
        private int animIDFreeFall;
        private int animIDMotionSpeed;

        private void Awake()
        {
            animIDSpeed = Animator.StringToHash("Speed");
            animIDGrounded = Animator.StringToHash("Grounded");
            animIDJump = Animator.StringToHash("Jump");
            animIDFreeFall = Animator.StringToHash("FreeFall");
            animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void Start()
        {
            hasAnimator = TryGetComponent(out animator);
            controller = GetComponent<CharacterController>();
            input = GetComponent<InputService>();
#if ENABLE_INPUT_SYSTEM 
            playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Player Input missing");
#endif
        }

        public void SetSpeed(float value)
        {
            if (!hasAnimator) return;
            Animator.SetFloat(animIDSpeed, value);
        }

        public void SetMotionSpeed(float value)
        {
            if (!hasAnimator) return;
            Animator.SetFloat(animIDMotionSpeed, value);
        }

        public void SetGrounded(bool grounded)
        {
            if (!hasAnimator) return;
            Animator.SetBool(animIDGrounded, grounded);
        }

        public void SetJump(bool state)
        {
            if (!hasAnimator) return;
            Animator.SetBool(animIDJump, state);
        }

        public void SetFreeFall(bool state)
        {
            if (!hasAnimator) return;
            Animator.SetBool(animIDFreeFall, state);
        }

        // Called via Animation Events in the Animator
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(controller.center), FootstepAudioVolume);
                }

                NoiseService.EmitNoise(transform.position, 10f);
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(controller.center), FootstepAudioVolume);

            NoiseService.EmitNoise(transform.position, 10f);
        }
    }
}
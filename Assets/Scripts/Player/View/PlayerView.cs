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
        [Header("Footstep")]
        [SerializeField] private AudioClip LandingAudioClip;
        [SerializeField] private AudioClip[] FootstepAudioClips;
        [Range(0, 1)][SerializeField] private float FootstepAudioVolume = 0.5f;
        [SerializeField] private ParticleSystem footstepParticle;

        [Header("Death")]
        [SerializeField] private AudioClip DeathAudioClip;
        [Range(0, 1)][SerializeField] private float DeathAudioVolume = 0.5f;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
#endif
        private Animator animator;
        private CharacterController controller;
        private InputService input;

        public CharacterController Controller => controller;
        public InputService Input => input;

        private bool hasAnimator;

        // animation IDs
        private int animIDSpeed;
        private int animIDGrounded;
        private int animIDJump;
        private int animIDFreeFall;
        private int animIDMotionSpeed;
        private int animIDDeath;

        private void Awake()
        {
            animIDSpeed = Animator.StringToHash("Speed");
            animIDGrounded = Animator.StringToHash("Grounded");
            animIDJump = Animator.StringToHash("Jump");
            animIDFreeFall = Animator.StringToHash("FreeFall");
            animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            animIDDeath = Animator.StringToHash("Death");
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
            animator.SetFloat(animIDSpeed, value);
        }

        public void SetMotionSpeed(float value)
        {
            if (!hasAnimator) return;
            animator.SetFloat(animIDMotionSpeed, value);
        }

        public void SetGrounded(bool grounded)
        {
            if (!hasAnimator) return;
            animator.SetBool(animIDGrounded, grounded);
        }

        public void SetJump(bool state)
        {
            if (!hasAnimator) return;
            animator.SetBool(animIDJump, state);
        }

        public void SetFreeFall(bool state)
        {
            if (!hasAnimator) return;
            animator.SetBool(animIDFreeFall, state);
        }

        public void SetDeath()
        {
            if (!hasAnimator) return;
            animator.SetTrigger(animIDDeath);
            AudioSource.PlayClipAtPoint(DeathAudioClip, transform.TransformPoint(controller.center), DeathAudioVolume);
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

                NoiseService.EmitNoise(transform.position, 6f);
                footstepParticle?.Emit(1);
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(controller.center), FootstepAudioVolume);

            NoiseService.EmitNoise(transform.position, 6f);
            footstepParticle?.Emit(1);
        }
    }
}
using ProjectZomboid.Core.InputSystem;
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
        private PlayerInput _playerInput;
        public PlayerInput PlayerInput => _playerInput;
#endif
        private Animator _animator;
        public Animator Animator => _animator;

        private CharacterController _controller;
        public CharacterController Controller => _controller;

        private InputService _input;
        public InputService Input => _input;

        private bool _hasAnimator;
        public bool HasAnimator => _hasAnimator;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<InputService>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Player Input missing");
#endif
        }

        private void Update() => _hasAnimator = TryGetComponent(out _animator);

        // Called via Animation Events in the Animator
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        }
    }
}
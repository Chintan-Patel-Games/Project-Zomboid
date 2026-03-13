using UnityEngine;
using UnityEngine.AI;

namespace ProjectZomboid.Enemy.Zombie.View
{
    public class ZombieView : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private AudioClip AttackAudioClip;
        [Range(0, 1)][SerializeField] private float AttackAudioVolume = 0.75f;

        [Header("Death")]
        [SerializeField] private AudioClip DeathAudioClip;
        [Range(0, 1)][SerializeField] private float DeathAudioVolume = 0.75f;

        private Animator animator;
        private NavMeshAgent agent;

        public Animator Animator => animator;
        public NavMeshAgent Agent => agent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        public void SetSpeed(float speed) => animator.SetFloat("Speed", speed);
        public void TriggerAttack()
        {
            animator.SetTrigger("Attack");
            AudioSource.PlayClipAtPoint(AttackAudioClip, transform.position, AttackAudioVolume);
        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(DeathAudioClip, transform.position, DeathAudioVolume);
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace ProjectZomboid.Enemy.Zombie.View
{
    public class ZombieView : MonoBehaviour
    {
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
        public void TriggerAttack() => animator.SetTrigger("Attack");
        public void TriggerDeath() => animator.SetTrigger("Death");
    }
}
using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.View;
using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Controller.Movement
{
    public class ZombieMovement
    {
        private ZombieView view;
        private ZombieModelSO config;

        public void Initialize(ZombieView view, ZombieModelSO config)
        {
            this.view = view;
            this.config = config;

            view.Agent.speed = config.moveSpeed;
        }

        public void MoveTo(Vector3 position) => view.Agent.SetDestination(position);

        public void ChasePlayer(Vector3 playerPosition)
        {
            view.Agent.speed = config.chaseSpeed;
            view.Agent.SetDestination(playerPosition);
        }

        public void Tick()
        {
            float normalizedSpeed = view.Agent.velocity.magnitude / view.Agent.speed;
            view.SetSpeed(normalizedSpeed);

            if (normalizedSpeed > 0.05f)
            {
                Vector3 direction = view.Agent.desiredVelocity.normalized;

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    view.transform.rotation = Quaternion.Slerp(
                        view.transform.rotation,
                        lookRotation,
                        Time.deltaTime * 10f
                    );
                }
            }
        }
    }
}
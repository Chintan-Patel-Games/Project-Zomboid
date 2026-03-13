using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.StateMachine;
using ProjectZomboid.Enemy.Zombie.View;
using System;
using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Controller.Detection
{
    public class ZombieDetection
    {
        private ZombieView view;
        private ZombieModelSO config;
        private ZombieStateMachine stateMachine;

        public Action<Vector3> OnNoiseDetected;

        public void Initialize(ZombieView view, ZombieModelSO config, ZombieStateMachine stateMachine)
        {
            this.view = view;
            this.config = config;
            this.stateMachine = stateMachine;
        }

        public void OnNoiseHeard(Vector3 noisePosition, float radius)
        {
            float distance = Vector3.Distance(view.transform.position, noisePosition);

            if (distance <= radius)
            {
                OnNoiseDetected?.Invoke(noisePosition);
                stateMachine.ChangeState(ZombieState.Search);
            }
        }

        public bool CanSeePlayer(Transform player)
        {
            Vector3 direction = (player.position - view.transform.position).normalized;

            float distance = Vector3.Distance(view.transform.position, player.position);

            if (distance > config.viewDistance)
                return false;

            float angle = Vector3.Angle(view.transform.forward, direction);

            if (angle > config.viewAngle * 0.5f)
                return false;

            // Check if something blocks view
            if (Physics.Raycast(view.transform.position, direction, distance, config.obstacleLayer))
                return false;

            return true;
        }
    }
}
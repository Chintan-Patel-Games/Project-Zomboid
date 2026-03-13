using ProjectZomboid.Enemy.Zombie.View;
using System;
using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Controller.Detection
{
    public class ZombieDetection
    {
        private ZombieView view;

        public Action<Vector3> OnNoiseDetected;

        public void Initialize(ZombieView view) => this.view = view;

        public void OnNoiseHeard(Vector3 noisePosition, float radius)
        {
            float distance = Vector3.Distance(view.transform.position, noisePosition);

            if (distance <= radius)
                OnNoiseDetected?.Invoke(noisePosition);
        }
    }
}
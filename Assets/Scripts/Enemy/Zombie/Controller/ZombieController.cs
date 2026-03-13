using ProjectZomboid.Enemy.Zombie.Controller.Attack;
using ProjectZomboid.Enemy.Zombie.Controller.Detection;
using ProjectZomboid.Enemy.Zombie.Controller.Movement;
using ProjectZomboid.Enemy.Zombie.Model;
using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.View;
using ProjectZomboid.Noise;
using ProjectZomboid.Player.Service;
using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Controller
{
    public class ZombieController
    {
        private ZombieModel model;
        private ZombieAttack attack;
        private ZombieMovement movement;
        private ZombieDetection detection;

        private PlayerService playerService;

        public void Initialize(ZombieView view, ZombieModelSO config, PlayerService playerService)
        {
            model = new ZombieModel();

            attack = new ZombieAttack();
            attack.Initialize(view, config, model);

            movement = new ZombieMovement();
            movement.Initialize(view, config);

            detection = new ZombieDetection();
            detection.Initialize(view);

            this.playerService = playerService;

            detection.OnNoiseDetected += OnNoiseDetected;

            NoiseService.OnNoiseEmitted += detection.OnNoiseHeard;
        }

        private void OnNoiseDetected(Vector3 noisePosition) => movement.MoveTo(noisePosition);

        public void OnPlayerDetected() => model.isChasing = true;

        public void Tick()
        {
            if (model.isChasing)
                movement.ChasePlayer(playerService.GetPlayerPosition());

            movement.Tick();
            attack.Tick(playerService);
        }

        public void Dispose() => NoiseService.OnNoiseEmitted -= detection.OnNoiseHeard;
    }
}
using ProjectZomboid.Player.Controller.Health;
using ProjectZomboid.Player.Controller.Movement;
using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Controller
{
    public class PlayerController
    {
        private PlayerModel model;
        private PlayerMovement movement;
        private PlayerHealth health;

        public void Initialize(PlayerModelSO modelSO, PlayerView view)
        {
            model = new PlayerModel { currentHealth = modelSO.maxHealth };

            movement = new PlayerMovement();
            movement.Initialize(model, modelSO, view);

            health = new PlayerHealth();
            health.Initialize(model, view);
        }

        public void TickUpdate()
        {
            movement.GroundedCheck();
            movement.JumpAndGravity();

            if (!model.IsDead)
                movement.Move();
        }

        public void TakeDamage(float damage) => health.TakeDamage(damage);
        public bool IsDead() => model.IsDead;

        public Vector3 GetPosition() => movement.GetPosition();
        public Transform GetTransform() => movement.GetTransform();
    }
}
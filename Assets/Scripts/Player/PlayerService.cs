using ProjectZomboid.Player.Controller;
using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Service
{
    public class PlayerService
    {
        private PlayerController controller;

        public void Initialize(PlayerView view, PlayerModelSO model)
        {
            controller = new PlayerController();
            controller.Initialize(model, view);
        }

        public void Tick() => controller.TickUpdate();

        public void TakeDamage(float damage) => controller.TakeDamage(damage);

        public Vector3 GetPlayerPosition() => controller.GetPosition();
        public Transform PlayerTransform() => controller.GetTransform();
    }
}
using ProjectZomboid.Player.Controller;
using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Service
{
    public class PlayerService
    {
        private PlayerController controller;
        private PlayerView view;
        private PlayerModelSO model;

        public void Initialize(PlayerView view, PlayerModelSO model)
        {
            this.view = view;
            this.model = model;

            controller = new PlayerController();
            controller.Initialize(model, view);
        }

        public void Tick() => controller.TickUpdate();

        public Vector3 GetPlayerPosition() => view.transform.position;

        public Transform GetPlayerTransform() => view.transform;
    }
}
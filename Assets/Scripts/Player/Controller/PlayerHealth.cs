using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.View;
using UnityEngine;

namespace ProjectZomboid.Player.Controller.Health
{
    public class PlayerHealth
    {
        private PlayerModel model;
        private PlayerView view;

        public void Initialize(PlayerModel model, PlayerView view)
        {
            this.model = model;
            this.view = view;
        }

        public void TakeDamage(float damage)
        {
            model.currentHealth -= damage;

            Debug.Log($"Player took {damage} damage. Current health: {model.currentHealth}");

            if (model.currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            if (model.IsDead) return;
            model.IsDead = true;
            view.SetDeath();
        }
    }
}
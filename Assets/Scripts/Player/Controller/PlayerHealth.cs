using ProjectZomboid.Player.Model;
using ProjectZomboid.Player.ModelSO;
using UnityEngine;

namespace ProjectZomboid.Player.Controller.Health
{
    public class PlayerHealth
    {
        private PlayerModel model;
        private PlayerModelSO config;

        public void Initialize(PlayerModel model, PlayerModelSO config)
        {
            this.model = model;
            this.config = config;
        }

        public void TakeDamage(float damage)
        {
            model.currentHealth -= damage;

            if (model.currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("Player Dead");
        }
    }
}
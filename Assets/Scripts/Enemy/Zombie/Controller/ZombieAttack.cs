using ProjectZomboid.Enemy.Zombie.Model;
using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.View;
using ProjectZomboid.Player.Service;
using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Controller.Attack
{
    public class ZombieAttack
    {
        private ZombieView view;
        private ZombieModelSO config;
        private ZombieModel model;

        public void Initialize(ZombieView view, ZombieModelSO config, ZombieModel model)
        {
            this.view = view;
            this.config = config;
            this.model = model;
        }

        public void Tick(PlayerService player)
        {
            model.attackTimer -= Time.deltaTime;

            float distance = Vector3.Distance(
                view.transform.position,
                player.GetPlayerPosition()
            );

            if (distance <= config.attackRange && model.attackTimer <= 0f)
            {
                if (!player.IsDead())
                {
                    MeeleAttack(player);
                    model.attackTimer = config.attackCooldown;
                }
            }
        }

        public void MeeleAttack(PlayerService player)
        {
            view.Agent.ResetPath();
            view.TriggerAttack();
            player.TakeDamage(config.meeleAttackDmg);
        }

        //public void RangedAttack(PlayerService player)
        //{
        //    view.TriggerAttack();

        //    GameObject projectile = Object.Instantiate(
        //        config.projectilePrefab,
        //        view.ProjectileSpawn.position,
        //        Quaternion.identity
        //    );

        //    projectile.GetComponent<ZombieProjectile>().Initialize(player.GetPlayerPosition(), config.rangedAttackDmg);
        //}
    }
}
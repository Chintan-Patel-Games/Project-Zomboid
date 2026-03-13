using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.Model
{
    public class ZombieModel
    {
        public float currentHealth;
        public float attackTimer;
        public Vector3 lastNoisePosition;
        public bool isDead;
        public bool isChasing;
    }
}
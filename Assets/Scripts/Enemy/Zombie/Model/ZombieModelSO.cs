using UnityEngine;

namespace ProjectZomboid.Enemy.Zombie.ModelSO
{
    [CreateAssetMenu(menuName = "Project Zomboid/Zombie/ZombieConfig")]
    public class ZombieModelSO : ScriptableObject
    {
        public float maxHealth = 50f;

        [Header("Movement")]
        public float moveSpeed = 1.5f;
        public float chaseSpeed = 3f;

        [Header("Detection")]
        public float visionRange = 10f;
        public float hearingRange = 8f;

        [Header("Attack")]
        public float attackRange = 1.5f;
        public float meeleAttackDmg = 10f;
        public float rangedAttackDmg = 6f;
        public float attackCooldown = 1.2f;
    }
}
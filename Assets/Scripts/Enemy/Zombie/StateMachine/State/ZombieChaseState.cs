using ProjectZomboid.Core.StateMachine;
using ProjectZomboid.Enemy.Zombie.Controller;

namespace ProjectZomboid.Enemy.Zombie.StateMachine
{
    public class ZombieChaseState : IState<ZombieController>
    {
        public ZombieController Owner { get; set; }

        public void OnEnterState() { }

        public void UpdateState() => Owner.OnPlayerDetected();

        public void OnExitState() { }
    }
}
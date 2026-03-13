using ProjectZomboid.Core.StateMachine;
using ProjectZomboid.Enemy.Zombie.Controller;

namespace ProjectZomboid.Enemy.Zombie.StateMachine
{
    public class ZombieIdleState : IState<ZombieController>
    {
        public ZombieController Owner { get; set; }

        public void OnEnterState() { }

        public void UpdateState() { }

        public void OnExitState() { }
    }
}
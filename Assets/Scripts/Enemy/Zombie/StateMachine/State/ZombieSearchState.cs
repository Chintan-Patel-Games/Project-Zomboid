using ProjectZomboid.Core.StateMachine;
using ProjectZomboid.Enemy.Zombie.Controller;

namespace ProjectZomboid.Enemy.Zombie.StateMachine
{
    public class ZombieSearchState : IState<ZombieController>
    {
        public ZombieController Owner { get; set; }

        public void OnEnterState() => Owner.InvestigateNoise(Owner.Model.lastNoisePosition);

        public void UpdateState() { }

        public void OnExitState() { }
    }
}
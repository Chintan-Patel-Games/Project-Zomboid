using ProjectZomboid.Core.StateMachine;
using ProjectZomboid.Enemy.Zombie.Controller;
using System.Collections.Generic;

namespace ProjectZomboid.Enemy.Zombie.StateMachine
{
    public class ZombieStateMachine : GenericStateMachine<ZombieController>
    {
        public ZombieStateMachine(ZombieController owner) : base(owner)
        {
            States = new Dictionary<System.Enum, IState<ZombieController>>
            {
                { ZombieState.Idle, new ZombieIdleState() },
                { ZombieState.Search, new ZombieSearchState() },
                { ZombieState.Chase, new ZombieChaseState() }
            };

            SetOwner();
            ChangeState(ZombieState.Idle);
        }
    }
}
using ProjectZomboid.Enemy.Zombie.Controller;
using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.View;
using ProjectZomboid.Player.Service;

namespace ProjectZomboid.Enemy.Zombie.Service
{
    public class ZombieService
    {
        private ZombieController zombie = new();

        public void Initialize(ZombieView view, ZombieModelSO config, PlayerService player)
        {
            zombie.Initialize(view, config, player);
        }

        public void Tick()
        {
            zombie.Tick();
        }

        public void Dispose()
        {
            zombie.Dispose();
        }
    }
}
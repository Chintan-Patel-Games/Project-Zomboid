using ProjectZomboid.Enemy.Zombie.ModelSO;
using ProjectZomboid.Enemy.Zombie.Service;
using ProjectZomboid.Enemy.Zombie.View;
using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.Service;
using ProjectZomboid.Player.View;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerModelSO playerConfig;

    [Header("Zombie")]
    [SerializeField] private ZombieView zombieView;
    [SerializeField] private ZombieModelSO zombieConfig;

    private PlayerService playerService = new PlayerService();
    private ZombieService zombieService = new ZombieService();

    private void Start()
    {
        playerService.Initialize(playerView, playerConfig);
        zombieService.Initialize(zombieView, zombieConfig, playerService);
    }

    private void Update()
    {
        playerService.Tick();
        zombieService.Tick();
    }
}
using ProjectZomboid.Player.ModelSO;
using ProjectZomboid.Player.Service;
using ProjectZomboid.Player.View;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerModelSO playerModel;

    private PlayerService playerService = new PlayerService();

    private void Start() => playerService.Initialize(playerView, playerModel);

    private void Update() => playerService.Tick();
}
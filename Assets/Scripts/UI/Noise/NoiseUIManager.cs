using ProjectZomboid.Noise;
using UnityEngine;

namespace ProjectZomboid.UI.Noise
{
    public class NoiseUIManager : MonoBehaviour
    {
        [SerializeField] private NoiseIndicator noisePrefab;

        private void OnEnable() => NoiseService.OnNoiseEmitted += ShowNoise;

        private void OnDisable() => NoiseService.OnNoiseEmitted -= ShowNoise;

        private void ShowNoise(Vector3 position, float radius)
        {
            position.y += 0.05f;
            NoiseIndicator indicator = Instantiate(noisePrefab, position, Quaternion.Euler(90f, 0f, 0f));
            indicator.Initialize(radius);
        }
    }
}
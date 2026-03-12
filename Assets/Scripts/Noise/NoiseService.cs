using UnityEngine;

namespace ProjectZomboid.Noise
{
    public static class NoiseService
    {
        public static System.Action<Vector3, float> OnNoiseEmitted;

        public static void EmitNoise(Vector3 position, float radius) => OnNoiseEmitted?.Invoke(position, radius);
    }
}
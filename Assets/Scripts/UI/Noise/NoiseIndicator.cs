using UnityEngine;

namespace ProjectZomboid.UI.Noise
{
    public class NoiseIndicator : MonoBehaviour
    {
        private float maxRadius;
        private float duration = 0.4f;

        private float timer;

        public void Initialize(float radius)
        {
            maxRadius = radius;
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            transform.localScale = Vector3.one * Mathf.Lerp(0, maxRadius * 2f, t);

            if (timer >= duration)
                Destroy(gameObject);
        }
    }
}
using UnityEngine;

namespace ReactionTapGame
{
    public class Collectible : MonoBehaviour
    {
        [Header("Score")]
        [SerializeField] private int _points = 1;

        public bool IsCollected { get; private set; }

        public void Collect()
        {
            if (IsCollected) return;

            IsCollected = true;

            if (Collector.Instance != null)
                Collector.Instance.AddScore(_points);

            Destroy(gameObject);
        }
    }
}
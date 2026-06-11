using UnityEngine;

namespace ReactionTapGame
{
    public class Lifetime : MonoBehaviour
    {
        [Header("Lifetime")]
        [SerializeField] private float _lifeTime = 1.2f;

        private float _timer;
        private Collectible _collectible;

        private void Awake()
        {
            _collectible = GetComponent<Collectible>();
            _timer = _lifeTime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                DestroyByLifetime();
            }
        }

        private void DestroyByLifetime()
        {
            if (_collectible != null && !_collectible.IsCollected)
            {
                if (Collector.Instance != null)
                    Collector.Instance.Miss();
            }

            Destroy(gameObject);
        }
    }
}
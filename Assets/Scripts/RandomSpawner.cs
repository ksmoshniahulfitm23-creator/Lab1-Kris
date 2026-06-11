using UnityEngine;

namespace ReactionTapGame
{
    public class RandomSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject _collectiblePrefab;

        [Header("Spawn Points")]
        [SerializeField] private Transform[] _spawnPoints;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnInterval = 0.8f;
        [SerializeField] private bool _spawnOnStart = true;

        private float _timer;

        private void Start()
        {
            _timer = _spawnOnStart ? 0f : _spawnInterval;
        }

        private void Update()
        {
            if (Collector.Instance != null && Collector.Instance.IsGameOver)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                SpawnCollectible();
                _timer = _spawnInterval;
            }
        }

        private void SpawnCollectible()
        {
            if (_collectiblePrefab == null)
            {
                Debug.LogError("Collectible Prefab is not assigned!");
                return;
            }

            if (_spawnPoints == null || _spawnPoints.Length == 0)
            {
                Debug.LogError("Spawn Points are not assigned!");
                return;
            }

            int index = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[index];

            Instantiate(_collectiblePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
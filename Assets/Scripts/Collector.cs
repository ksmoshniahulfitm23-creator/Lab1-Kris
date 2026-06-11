using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace ReactionTapGame
{
    public class Collector : MonoBehaviour
    {
        public static Collector Instance { get; private set; }

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _missText;
        [SerializeField] private GameObject _gameOverPanel;

        [Header("Game Settings")]
        [SerializeField] private int _maxMisses = 5;

        private Camera _camera;
        private int _score;
        private int _misses;
        private bool _isGameOver;

        public bool IsGameOver => _isGameOver;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _camera = Camera.main;
        }

        private void Start()
        {
            Time.timeScale = 1f;

            _score = 0;
            _misses = 0;
            _isGameOver = false;

            UpdateUI();

            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(false);
        }

        private void Update()
        {
            if (_isGameOver) return;

            if (Input.GetMouseButtonDown(0))
            {
                TryCollect(Input.mousePosition);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TryCollect(Input.GetTouch(0).position);
            }
        }

        private void TryCollect(Vector3 screenPosition)
        {
            if (_camera == null) return;

            Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            Vector2 point = new Vector2(worldPosition.x, worldPosition.y);

            Collider2D hit = Physics2D.OverlapPoint(point);

            if (hit != null && hit.TryGetComponent(out Collectible collectible))
            {
                collectible.Collect();
            }
        }

        public void AddScore(int amount)
        {
            if (_isGameOver) return;

            _score += amount;
            UpdateUI();
        }

        public void Miss()
        {
            if (_isGameOver) return;

            _misses++;
            UpdateUI();

            if (_misses >= _maxMisses)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            _isGameOver = true;

            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);

            Time.timeScale = 0f;
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void UpdateUI()
        {
            if (_scoreText != null)
                _scoreText.text = $"Score: {_score}";

            if (_missText != null)
                _missText.text = $"Misses: {_misses}/{_maxMisses}";
        }
    }
}
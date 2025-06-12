using UnityEngine;
using Cinemachine;

public class MainHeroRespawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _respawnPoint;
    [SerializeField]
    private float _respawnDelay = 3f;

    private GameObject _currentPlayerInstance;
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        RespawnPlayer();
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (_virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera не найдена в сцене!");
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnWithDelay(_respawnDelay));
    }

    private System.Collections.IEnumerator RespawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (_playerPrefab != null && _respawnPoint != null)
        {
            if (_currentPlayerInstance != null)
            {
                Destroy(_currentPlayerInstance);
            }

            _currentPlayerInstance = Instantiate(_playerPrefab, _respawnPoint.position, _respawnPoint.rotation);

            if (_virtualCamera != null)
            {
                _virtualCamera.m_Follow = _currentPlayerInstance.transform;
            }
            else
            {
                Debug.LogWarning("Cinemachine Virtual Camera не найдена, слежение за игроком невозможно.");
            }

            Debug.Log("Player respawned!", _currentPlayerInstance);
        }
        else
        {
            Debug.LogError("Player Prefab or Respawn Point is not assigned!");
        }
    }
}

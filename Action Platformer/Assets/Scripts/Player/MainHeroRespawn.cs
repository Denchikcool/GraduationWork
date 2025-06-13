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
        Debug.Log("MainHeroRespawn.Start() �����"); // Add this
        if(_respawnPoint.transform.position == new Vector3(11.86f, 8.47f, 0.0f))
        {
            RespawnPlayer();
        }
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (_virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera �� ������� � �����!");
        }
        Debug.Log("MainHeroRespawn.Start() ��������"); // Add this
    }

    public void RespawnPlayer()
    {
        Debug.Log("MainHeroRespawn.RespawnPlayer() �����"); // Add this
        StartCoroutine(RespawnWithDelay(_respawnDelay));
        Debug.Log("MainHeroRespawn.RespawnPlayer() ��������"); // Add this
        
    }

    private System.Collections.IEnumerator RespawnWithDelay(float delay)
    {
        Debug.Log($"MainHeroRespawn.RespawnWithDelay() �����, delay = {delay}"); // Add this
        yield return new WaitForSeconds(delay);
        Debug.Log("MainHeroRespawn.RespawnWithDelay(): �������� ���������!");  // Add this
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Debug.Log("MainHeroRespawn.SpawnPlayer() �����"); // Add this
        if (_playerPrefab != null && _respawnPoint != null)
        {
            Debug.Log("MainHeroRespawn.SpawnPlayer(): _playerPrefab � _respawnPoint ���������"); // Add this
            if (_currentPlayerInstance != null)
            {
                Debug.Log("MainHeroRespawn.SpawnPlayer(): ���������� ���������� ���������"); // Add this
                Destroy(_currentPlayerInstance);
            }
            Debug.Log($"MainHeroRespawn.SpawnPlayer(): ������� ��������� � {_respawnPoint.position}"); // Add this
            _currentPlayerInstance = Instantiate(_playerPrefab, _respawnPoint.position, _respawnPoint.rotation);
            Debug.Log("MainHeroRespawn.SpawnPlayer(): ��������� ������"); // Add this

            if (_virtualCamera != null)
            {
                Debug.Log("MainHeroRespawn.SpawnPlayer(): ����������� ������ �������"); // Add this
                _virtualCamera.m_Follow = _currentPlayerInstance.transform;
            }
            else
            {
                Debug.LogWarning("Cinemachine Virtual Camera �� �������, �������� �� ������� ����������.");
            }

            Debug.Log("MainHeroRespawn.SpawnPlayer(): Player respawned!", _currentPlayerInstance);
        }
        else
        {
            Debug.LogError("MainHeroRespawn.SpawnPlayer(): Player Prefab or Respawn Point is not assigned!");
        }
        Debug.Log("MainHeroRespawn.SpawnPlayer() ��������"); // Add this
    }

    public void SetRespawnPointPosition(Vector3 position)
    {
        Debug.Log($"MainHeroRespawn.SetRespawnPointPosition() �����, position = {position}"); // Add this
        if (_respawnPoint != null)
        {
            _respawnPoint.position = position;
            Debug.Log($"����� ������ ���������� � �������: {position}");
        }
        else
        {
            Debug.LogError("Respawn Point �� ��������!");
        }
        Debug.Log("MainHeroRespawn.SetRespawnPointPosition() ��������"); // Add this
    }
}

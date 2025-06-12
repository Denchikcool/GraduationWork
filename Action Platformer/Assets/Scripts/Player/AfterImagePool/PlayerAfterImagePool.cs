using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject _afterImagePrefab;

    private Queue<GameObject> _availableObjects = new Queue<GameObject>();

    private readonly int _countOfObjects = 10;

    public static PlayerAfterImagePool Instance { get; private set; }

    [SerializeField]
    private string _playerTag = "Player";

    [SerializeField]
    private float _checkInterval = 0.1f;

    private void Awake()
    {
        StartCoroutine(WaitForPlayerAndInitialize());
    }

    private IEnumerator WaitForPlayerAndInitialize()
    {
        while (GameObject.FindGameObjectWithTag(_playerTag) == null)
        {
            Debug.Log("Player not found, waiting...");
            yield return new WaitForSeconds(_checkInterval);
        }

        Debug.Log("Player found, initializing PlayerAfterImagePool.");
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < _countOfObjects; i++)
        {
            var instanceToAdd = Instantiate(_afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        _availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (_availableObjects.Count == 0)
        {
            GrowPool();
        }

        var instance = _availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}

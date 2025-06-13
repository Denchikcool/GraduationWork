using System.Collections;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    private SaveController _saveController;
    private MainHeroRespawn _mainHeroRespawn;
    //private GameObject _player; // Больше не нужен

    private void Start() // Изменил на Start, чтобы быть уверенным, что SaveController уже загрузился
    {
        // Получаем ссылку на SaveController
        _saveController = FindObjectOfType<SaveController>();
        if (_saveController == null)
        {
            Debug.LogError("SaveController не найден на сцене!");
            return;
        }

        _mainHeroRespawn = FindObjectOfType<MainHeroRespawn>();
        if (_mainHeroRespawn == null)
        {
            Debug.LogError("MainHeroRespawn не найден на сцене!");
            return;
        }
        if(_saveController.PlayerPos == Vector3.zero)
        {
            return;
        }
        ChangePositionAfterDelay();
    }

    private void ChangePositionAfterDelay()
    {
        // Применяем сохранённую позицию к точке спавна
        _mainHeroRespawn.SetRespawnPointPosition(_saveController.PlayerPos); // Используем PlayerPos из SaveController
        _mainHeroRespawn.RespawnPlayer();
    }
}

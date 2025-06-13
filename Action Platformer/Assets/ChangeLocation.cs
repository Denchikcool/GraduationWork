using System.Collections;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    private SaveController _saveController;
    private MainHeroRespawn _mainHeroRespawn;
    //private GameObject _player; // ������ �� �����

    private void Start() // ������� �� Start, ����� ���� ���������, ��� SaveController ��� ����������
    {
        // �������� ������ �� SaveController
        _saveController = FindObjectOfType<SaveController>();
        if (_saveController == null)
        {
            Debug.LogError("SaveController �� ������ �� �����!");
            return;
        }

        _mainHeroRespawn = FindObjectOfType<MainHeroRespawn>();
        if (_mainHeroRespawn == null)
        {
            Debug.LogError("MainHeroRespawn �� ������ �� �����!");
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
        // ��������� ���������� ������� � ����� ������
        _mainHeroRespawn.SetRespawnPointPosition(_saveController.PlayerPos); // ���������� PlayerPos �� SaveController
        _mainHeroRespawn.RespawnPlayer();
    }
}

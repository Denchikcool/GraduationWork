using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    private string _saveLocation;

    public Vector3 PlayerPos { get; private set; }

    private void Start()
    {
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        // ���������� ������� ������
        saveData.PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // ���������� ������� ������ � �� ID
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //saveData.AliveEnemiesPosition = new List<Vector3>();
        //saveData.EnemyIDs = new List<string>(); // ������ ID ������
        //foreach (GameObject enemy in enemies)
        //{
        //    saveData.AliveEnemiesPosition.Add(enemy.transform.position);
            //  ��������� ID, ���� ����� �� �����
        //    saveData.EnemyIDs.Add(enemy.GetInstanceID().ToString()); // ���������� GetInstanceID() ��� �������� ����������� ID
       // }

        // ���������� ������ ������
        saveData.LevelNumber = SceneManager.GetActiveScene().buildIndex;

        // ���������� �����
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_saveLocation, json);
    }

    public void LoadGame()
    {
        if (File.Exists(_saveLocation))
        {
            string json = File.ReadAllText(_saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if(saveData.PlayerPosition == Vector3.zero)
            {
                return;
            }

            PlayerPos = saveData.PlayerPosition; // ������������� PlayerPos ����� ����� ��������
            // �������� ������� ������
            Vector3 playerPosition = saveData.PlayerPosition;
            
            Debug.Log("PlayerPos = " + PlayerPos);
            // �������� �� null � ����� ���������
            if (playerPosition != Vector3.zero)
            {
                Debug.LogWarning("����� ������ � ����� 'Player' ��� ��������.");
            }
            else
            {
                Debug.LogWarning("����� �� ������ � ����� 'Player' ��� ��������.");
            }

            // �������� ������� ������ (��������� ������� ������������ ������)
            //GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            //if (saveData.EnemyIDs != null && saveData.AliveEnemiesPosition != null && saveData.EnemyIDs.Count == existingEnemies.Length)
            //{
             //   for (int i = 0; i < existingEnemies.Length; i++)
             //   {
                    //  ��������� ������� ��� ������� �����.
            //        existingEnemies[i].transform.position = saveData.AliveEnemiesPosition[i];
            //    }
            //}
           // else
           // {
           //     Debug.LogWarning("���������� ������ � ���������� �� ��������� � ����������� ������ �� �����, ���� ������ �� ����������������.");
           // }

            // �������� �����
            SceneManager.LoadScene(saveData.LevelNumber);
        }
        else
        {
            Debug.LogWarning("���� ���������� �� ������. ������ �����.");
            SaveGame();
        }
    }
}


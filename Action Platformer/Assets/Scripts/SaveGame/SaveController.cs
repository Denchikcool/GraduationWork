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

        // Сохранение позиции игрока
        saveData.PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Сохранение позиций врагов и их ID
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //saveData.AliveEnemiesPosition = new List<Vector3>();
        //saveData.EnemyIDs = new List<string>(); // Список ID врагов
        //foreach (GameObject enemy in enemies)
        //{
        //    saveData.AliveEnemiesPosition.Add(enemy.transform.position);
            //  Сохраняем ID, чтоб потом их найти
        //    saveData.EnemyIDs.Add(enemy.GetInstanceID().ToString()); // Используем GetInstanceID() для создания уникального ID
       // }

        // Сохранение номера уровня
        saveData.LevelNumber = SceneManager.GetActiveScene().buildIndex;

        // Сохранение файла
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

            PlayerPos = saveData.PlayerPosition; // Устанавливаем PlayerPos сразу после загрузки
            // Загрузка позиции игрока
            Vector3 playerPosition = saveData.PlayerPosition;
            
            Debug.Log("PlayerPos = " + PlayerPos);
            // Проверка на null и вывод сообщения
            if (playerPosition != Vector3.zero)
            {
                Debug.LogWarning("Игрок найден с тегом 'Player' при загрузке.");
            }
            else
            {
                Debug.LogWarning("Игрок не найден с тегом 'Player' при загрузке.");
            }

            // Загрузка позиции врагов (изменение позиции существующих врагов)
            //GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            //if (saveData.EnemyIDs != null && saveData.AliveEnemiesPosition != null && saveData.EnemyIDs.Count == existingEnemies.Length)
            //{
             //   for (int i = 0; i < existingEnemies.Length; i++)
             //   {
                    //  Применяем позицию для каждого врага.
            //        existingEnemies[i].transform.position = saveData.AliveEnemiesPosition[i];
            //    }
            //}
           // else
           // {
           //     Debug.LogWarning("Количество врагов в сохранении не совпадает с количеством врагов на сцене, либо данные не инициализированы.");
           // }

            // Загрузка сцены
            SceneManager.LoadScene(saveData.LevelNumber);
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден. Создаю новый.");
            SaveGame();
        }
    }
}


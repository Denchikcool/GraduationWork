using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    private AsyncOperation _asyncOperation;

    [SerializeField]
    private Image LoadBar;
    [SerializeField]
    private TMP_Text Text;
    [SerializeField]
    private int SceneId;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1f);

        _asyncOperation = SceneManager.LoadSceneAsync(SceneId);

        while (!_asyncOperation.isDone)
        {
            float progress = _asyncOperation.progress / 0.9f;
            LoadBar.fillAmount = progress;
            Text.text = "«¿√–”« ¿  " + string.Format("{0:0}%", progress * 100f);
            yield return null;
        }
    }
}

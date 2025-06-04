using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterControl : MonoBehaviour
{
    public void ExitCharacterControl()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


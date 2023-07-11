using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor; // There is no UnityEditor when running standalone
#endif

public class MenuHandler : MonoBehaviour
{

    public void StartGame()
    {
        DataCenter.Instance.currentScore = 0;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        if (DataCenter.Instance != null)
            DataCenter.Instance.SaveGame();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

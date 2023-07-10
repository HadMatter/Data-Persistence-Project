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



    public void Awake()
    {
        string savePath = "D:\\Documents\\Unity\\Unity Projects\\Learning\\Junior Programmer Path\\Data-Persistence-Project\\SaveData\\savedata.json";
        if (File.Exists(savePath))
            MainManager.Instance.LoadGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        MainManager.Instance.SaveGame();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

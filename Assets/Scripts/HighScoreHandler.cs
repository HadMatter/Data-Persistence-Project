using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    // need to look up how to work with UI Input element
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = MainManager.Instance.highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

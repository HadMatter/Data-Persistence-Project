using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // for TextMeshPro text box
using UnityEngine.UI; // for InputField box

public class HighScoreHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public Text highScoreHandlerInput;
    public int score;
    public string highScoreName;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = DataCenter.Instance.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        DataCenter.Instance.highScoreHolder = highScoreHandlerInput.text;
    }

    public void ReturnToMenu()
    {
        DataCenter.Instance.SaveGame();
        SceneManager.LoadScene(0);
    }
}

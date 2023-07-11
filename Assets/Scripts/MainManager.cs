using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int score;
    
    private bool m_GameOver = false;

    private int brickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetTheBoard();
        DataCenter.Instance.LoadGame();
        highScoreText.text = "Best Score : " + DataCenter.Instance.highScoreHolder + " : " + DataCenter.Instance.highScore;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        
        if (m_GameOver)
        {
            if (score > DataCenter.Instance.highScore)
            {
                DataCenter.Instance.highScore = score;
                DataCenter.Instance.SaveGame();
                SceneManager.LoadScene(2);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        // Keep the game going if you clear all the bricks
        if (brickCount <= 0 )
            SetTheBoard();
    }

    private void SetTheBoard()
    {
        // If I understand this correctly...
        // the lateral (x) scale of each brick is 0.55
        // step, at 0.6, sets the spacing with 0.05 b/w each brick
        const float step = 0.6f; 
        int perLine = Mathf.FloorToInt(4.0f / step);
        Debug.Log("perLine = " + perLine);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint); // Ah, HERE'S where this listener is set
            }
        }
        brickCount = LineCount * perLine;
    }

    void AddPoint(int point)
    {
        score += point;
        ScoreText.text = $"Score : {score}"; //???????????????
        --brickCount;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }


}

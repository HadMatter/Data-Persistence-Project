using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public static MainManager Instance;
    public int highScore;
    public string highScoreHolder;


    private void Awake()
    {
        if(MainManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        LoadGame();
        highScoreText.text = "Best Score : " + highScoreHolder + " : " + highScore;
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
        else if (m_GameOver)
        {
            if ( score > MainManager.Instance.highScore)
            {
                MainManager.Instance.highScore = score;
                MainManager.Instance.SaveGame();
                SceneManager.LoadScene(2);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //m_GameOver = false;
                //m_Started = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        score += point;
        ScoreText.text = $"Score : {score}"; //???????????????
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // ============= SAVE DATA ================

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreHolder;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreHolder = highScoreHolder;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText("D:\\Documents\\Unity\\Unity Projects\\Learning\\Junior Programmer Path\\Data-Persistence-Project\\SaveData\\savedata.json", json);
    }

    public void LoadGame()
    {
        string path = "D:\\Documents\\Unity\\Unity Projects\\Learning\\Junior Programmer Path\\Data-Persistence-Project\\SaveData\\savedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
            highScoreHolder = data.highScoreHolder;
        }
    }
}

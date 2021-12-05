using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public PlayerControl player { get; private set; }
    public Brick[] bricks;
    public int level = 1;

    public int score = 0;
    public int lives = 3;

    // UI Elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        this.scoreText.text = this.score.ToString();
        this.livesText.text = "x" + this.lives;
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level " + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.player = FindObjectOfType<PlayerControl>();
        this.bricks = FindObjectsOfType<Brick>();
    }
    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.player.ResetPlayer();
    }

    private void GameOver()
    {
        NewGame();
    }
    public void Hit(Brick brick)
    {
        this.score += brick.points;
        this.scoreText.text = this.score.ToString();
        Debug.Log(Cleared());
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for(int i = 0; i < this.bricks.Length; i++)
        {
            if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }
    public void Death()
    {
        this.lives--;
        this.livesText.text = "x" + this.lives;
        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
}

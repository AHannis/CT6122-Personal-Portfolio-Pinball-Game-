using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] private GameObject pinBallPrefab;
    [SerializeField] private Transform ballStartPoint;
    [SerializeField] private TextMeshProUGUI textScore;

    private int score;
    private int currentScore;

    private bool resetInProgress = false;

    public GameObject PinBall1
    {
        get => pinBallPrefab;
        set => pinBallPrefab = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (pinBallPrefab == null)
        {
            Debug.LogError("PinBall prefab not assigned in Game.", this);
        }

        if (ballStartPoint == null)
        {
            Debug.LogError("Ball start point not assigned in Game.", this);
        }

        if (textScore == null)
        {
            Debug.LogError("Score text (TMP) not assigned in Game.", this);
        }
    }

    private void Start()
    {
        Physics.gravity = new Vector3(0, -10, 0);

        if (pinBallPrefab != null && ballStartPoint != null)
        {
            SpawnBall();
        }
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (textScore == null)
        {
            return;
        }

        if (currentScore < score)
        {
            currentScore += (int)(1000 * Time.deltaTime);

            if (currentScore > score)
            {
                currentScore = score;
            }

            textScore.text = currentScore.ToString("00000000");
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void SpawnBall()
    {
        if (pinBallPrefab == null || ballStartPoint == null)
        {
            return;
        }

        Instantiate(pinBallPrefab, ballStartPoint.position, Quaternion.identity);
    }

    
    // BALL RESET 
   
    public void RequestBallReset()
    {
        if (resetInProgress)
        {
            return;
        }

        resetInProgress = true;
        Invoke(nameof(ResetBall), 0.5f);
    }

    private void ResetBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }

        SpawnBall();
        resetInProgress = false;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

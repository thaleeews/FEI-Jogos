using UnityEngine;
using UnityEngine.UI;

public enum GameState { Stop, Play, Win, GameOver, Pause };
public class GameController : MonoBehaviour
{
    public Text txtScore, txtStart, txtLose;
    public static GameController instance;
    public GameState gameState;
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private GameObject Player;
    private float score;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            if (instance != this) {
                Destroy(this);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.Stop;
        txtLose.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        txtScore.text = "Score: " + this.score;
        if (gameState != GameState.Play) {
            if (Input.GetKey(KeyCode.Space)) {
                StartGame();
                txtStart.gameObject.SetActive(false);
            }
        }
        if (gameState == GameState.GameOver) {
            txtLose.gameObject.SetActive(true);
        }
    }

    public void AddPoints(float valor) {
        this.score += valor;
    }

    public void StartGame() {
        if (gameState == GameState.Stop) {
            gameState = GameState.Play;
            score = 0;
            Ball.GetComponent<Ball>().StartBall();
        }
        else if (gameState == GameState.GameOver) {
            txtLose.gameObject.SetActive(false);
            gameState = GameState.Play;
            score = 0;
            BlockController.instance.DestroyAllByTag();
            BlockController.instance.CreateBlock();
            Player.GetComponent<Raquete>().StartRaquete();
            Ball.GetComponent<Ball>().StartBall();
        }
    }
}

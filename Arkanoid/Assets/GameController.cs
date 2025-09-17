using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    
    [Header("Level System")]
    public string[] levelScenes = {"Level1", "Level2", "Level3", "VictoryScene"};
    
    private int score;

    private void Awake() {
        // Verificar se já existe uma instância
        if (instance != null && instance != this) {
            Destroy(gameObject); // Destruir este GameObject duplicado
            return;
        }
        instance = this;
    }

    private void OnDestroy() {
        if (instance == this) {
            instance = null; // Limpar a instância quando destruído
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
        
        if (gameState == GameState.Play) {
            CheckWinCondition();
        }

        if (gameState != GameState.Play) {
            if (Input.GetKey(KeyCode.Space)) {
                StartGame();
                txtStart.gameObject.SetActive(false);
            }
            // Restart completo com R
            if (Input.GetKey(KeyCode.R)) {
                RestartGame();
            }
        }
        if (gameState == GameState.GameOver) {
            txtLose.gameObject.SetActive(true);
            txtLose.text = "GAME OVER";
            txtStart.gameObject.SetActive(true);
            txtStart.text = "Pressione Space para reiniciar";
        }
        else if (gameState == GameState.Win) {
            Ball.GetComponent<Ball>().StopBall();
            txtLose.gameObject.SetActive(true);
            txtLose.text = "Parabéns! Você venceu!";
            txtStart.gameObject.SetActive(true);
            txtStart.text = "Pressione Space para reiniciar";
        }
    }

    public void AddPoints(int valor) {
        this.score += valor;
    }

    public void CheckWinCondition() {
        GameObject[] remainingBlocks = GameObject.FindGameObjectsWithTag("Bloco");
        
        if (remainingBlocks.Length == 0) {
            LoadNextLevel();
        }
    }

    void LoadNextLevel() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Determinar próxima fase baseada na cena atual
        switch(currentSceneName) {
            case "Level1":
                try {
                    SceneManager.LoadScene("Level2");
                } catch (System.Exception e) {
                    ShowCompleteMessage();
                }
                break;
            case "Level2":
                gameState = GameState.Win;
                break;
            default:
                RestartGame(); // Se não reconhecer a cena, reiniciar
                break;
        }
    }

    public void RestartGame() {
        // Sistema simples: apenas resetar score e voltar para Level1
        score = 0;
        SceneManager.LoadScene("Level1");
    }


    public void StartGame() {
        if (gameState == GameState.Stop) {
            gameState = GameState.Play;
            score = 0;
            // Encontrar Ball pela tag se a referência estiver nula
            if (Ball == null) {
                GameObject ballObj = GameObject.FindWithTag("Ball");
                if (ballObj != null) Ball = ballObj;
            }
            
            if (Ball != null) {
                Ball.GetComponent<Ball>().StartBall();
            }
        }
        else if (gameState == GameState.GameOver) {
            txtLose.gameObject.SetActive(false);
            gameState = GameState.Play;
            score = 0;
            
            // Limpar e recriar blocos
            if (BlockController.instance != null) {
                BlockController.instance.DestroyAllByTag();
                BlockController.instance.CreateLevelBlocks();
            }
            
            // Resetar posições
            if (Player != null) {
                Player.GetComponent<Raquete>().StartRaquete();
            }
            
            if (Ball != null) {
                Ball.GetComponent<Ball>().StartBall();
            }
        }
        else if (gameState == GameState.Win) {
            if (Input.GetKey(KeyCode.Space)) {
                RestartGame();
            }
            if (Input.GetKey(KeyCode.R)) {
                RestartGame();
            }
        }
        else if (gameState == GameState.Win) {
            txtLose.gameObject.SetActive(false);
            SceneManager.LoadScene("Level1");
        }
    }
    
    void ShowCompleteMessage() {
        gameState = GameState.Win;
        txtStart.text = "🏆 PARABÉNS!\nVocê completou a fase!\n\n📋 Para continuar:\n1. Crie Level2\n2. Adicione às Build Settings\n\nESPAÇO - Reiniciar\nR - Novo jogo";
        txtStart.gameObject.SetActive(true);
    }
}

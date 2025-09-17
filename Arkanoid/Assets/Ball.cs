using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 10.0f;
    public static Ball instance;
    //string currentLevel = SceneManager.GetActiveScene().name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() {
        // Verificar se já existe uma instância
        if (instance != null && instance != this) {
            Destroy(gameObject); // Destruir este GameObject duplicado
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Mantém o GameController entre cenas
    }

    public void StartBall() {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 posInicial = new Vector2(0, -4);
        rb2d.position = posInicial;
        rb2d.linearVelocity = Vector2.up * speed;
    }

    public void StopBall() {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 pararBolinha = new Vector2(0,0);
        rb2d.linearVelocity = pararBolinha;
    }

    float HitFactor(Vector2 ball, Vector2 player, float playerWidth) {
        return (ball.x - player.x) / playerWidth;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
                float x = HitFactor (transform.position, col.transform.position, col.collider.bounds.size.x);
                Vector2 dir = new Vector2(x, 1).normalized;
                rb2d.linearVelocity = Vector2.ClampMagnitude(dir * speed, speed);
            }
            if(col.gameObject.name == "botWall") {
                StopBall();
                int vidas = GameController.instance.RemoveLife();
                if (vidas <= 0) {
                    GameController.instance.gameState = GameState.GameOver;
                }
                else {
                    GameController.instance.gameState = GameState.LessLife;
                }
            }
        
        /*if (currentLevel == "Level1") {
            if(col.gameObject.tag == "Player") {
                float x = HitFactor (transform.position, col.transform.position, col.collider.bounds.size.x);
                Vector2 dir = new Vector2(x, 1).normalized;
                rb2d.linearVelocity = Vector2.ClampMagnitude(dir * speed, speed);
            }
            if(col.gameObject.name == "botWall") {
                StopBall();
                int vidas = GameController.instance.RemoveLife();
                if (vidas <= 0) {
                    GameController.instance.gameState = GameState.GameOver;
                }
                else {
                    GameController.instance.gameState = GameState.LessLife;
                }
            }
        }
        else {
            if(col.gameObject.tag == "Player") {
                float x = HitFactor (transform.position, col.transform.position, col.collider.bounds.size.x);
                Vector2 dir = new Vector2(x, 1).normalized;
                rb2d.linearVelocity = Vector2.ClampMagnitude(dir * speed, speed);
            }
            if(col.gameObject.name == "botWall") {
                StopBall();
                int vidas = GameController.instance.RemoveLife();
                if (vidas <= 0) {
                    GameController.instance.gameState = GameState.GameOver;
                }
                else {
                    GameController.instance.gameState = GameState.LessLife;
                }
            }
        }*/
    }
}

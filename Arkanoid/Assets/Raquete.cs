using UnityEngine;

public class Raquete : MonoBehaviour
{
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;
    public float speed = 10.0f;
    public float boundX = 2.25f;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameState == GameState.Play) {
            var vel = rb2d.linearVelocity;                // Acessa a velocidade da raquete
            if (Input.GetKey(moveRight)) {             // Velocidade da Raquete para ir para cima
                vel.x = speed;
            }
            else if (Input.GetKey(moveLeft)) {      // Velocidade da Raquete para ir para baixo
                vel.x = -speed;                    
            }
            else {
                vel.x = 0;                          // Velociade para manter a raquete parada
            }
            rb2d.linearVelocity = vel;                    // Atualizada a velocidade da raquete

            var pos = transform.position;           // Acessa a Posição da raquete
            if (pos.x > boundX) {                  
                pos.x = boundX;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
            }
            else if (pos.x < -boundX) {
                pos.x = -boundX;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
            }
            transform.position = pos;               // Atualiza a posição da raquete
        }
        else if (GameController.instance.gameState == GameState.GameOver || GameController.instance.gameState == GameState.LessLife) {
            Vector2 pararRaquete = new Vector2(0,0);
            rb2d.linearVelocity = pararRaquete;
        }
    }

    public void StartRaquete() {
        Vector2 posInicial = new Vector2(0, -4.5f);
        rb2d.position = posInicial;
    }
}

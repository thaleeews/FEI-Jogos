using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public GameManager GameManagerInstance;
    public static bool WasGoal { get; private set;}
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        WasGoal = false;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if(!WasGoal) {
            if(hitInfo.tag == "GolAi") {
                GameManagerInstance.Increment(GameManager.Score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck());
            }
            else if(hitInfo.tag == "GolPlayer") {
                GameManagerInstance.Increment(GameManager.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck());
            }
        }
    }

    private IEnumerator ResetPuck() {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb2d.linearVelocity = rb2d.position = new Vector2(0,0);
    }
    /*public float speed = 10.0f;

    // inicializa a bola randomicamente para esquerda ou direita
    void GoBall(){                      
        float rand = Random.Range(0, 2);
        if(rand < 1){
            rb2d.AddForce(new Vector2(600, -600));
        } else {
            rb2d.AddForce(new Vector2(-600, 600));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        Invoke("GoBall", 2);    // Chama a função GoBall após 2 segundos
    }

    // Reinicializa a posição e velocidade da bola
    void ResetBall(){
        rb2d.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}

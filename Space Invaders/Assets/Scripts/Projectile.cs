using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projetil : MonoBehaviour
{
    private BoxCollider2D colisorCaixa;
    public Vector3 direcao = Vector3.up;
    public float velocidade = 20f;

    private void Awake()
    {
        colisorCaixa = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position += velocidade * Time.deltaTime * direcao;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        VerificarColisao(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        VerificarColisao(other);
    }

    private void VerificarColisao(Collider2D other)
    {
        Destroy(gameObject);
    }

}

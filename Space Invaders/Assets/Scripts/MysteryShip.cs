using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NaveMisteriosa : MonoBehaviour
{
    public float velocidade = 5f;
    public float tempoCiclo = 30f;
    public int pontuacao = 300;

    private Vector2 destinoEsquerda;
    private Vector2 destinoDireita;
    private int direcao = -1;
    private bool apareceu;

    private void Start()
    {
        Vector3 bordaEsquerda = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 bordaDireita = Camera.main.ViewportToWorldPoint(Vector3.right);

        destinoEsquerda = new Vector2(bordaEsquerda.x - 1f, transform.position.y);
        destinoDireita = new Vector2(bordaDireita.x + 1f, transform.position.y);

        Desaparecer();
    }

    private void Update()
    {
        if (!apareceu) return;

        if (direcao == 1) {
            MoverDireita();
        } else {
            MoverEsquerda();
        }
    }

    private void MoverDireita()
    {
        transform.position += velocidade * Time.deltaTime * Vector3.right;

        if (transform.position.x >= destinoDireita.x) {
            Desaparecer();
        }
    }

    private void MoverEsquerda()
    {
        transform.position += velocidade * Time.deltaTime * Vector3.left;

        if (transform.position.x <= destinoEsquerda.x) {
            Desaparecer();
        }
    }

    private void Aparecer()
    {
        direcao *= -1;

        if (direcao == 1) {
            transform.position = destinoEsquerda;
        } else {
            transform.position = destinoDireita;
        }

        apareceu = true;
    }

    private void Desaparecer()
    {
        apareceu = false;

        if (direcao == 1) {
            transform.position = destinoDireita;
        } else {
            transform.position = destinoEsquerda;
        }

        Invoke(nameof(Aparecer), tempoCiclo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Desaparecer();
            GerenciadorJogo.Instancia.OnNaveMisteriosaMorto(this);
        }
    }

}

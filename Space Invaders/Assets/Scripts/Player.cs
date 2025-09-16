using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Jogador : MonoBehaviour
{
    public float velocidade = 5f;
    public Projetil prefabLaser;
    private Projetil laser;

    private void Update()
    {
        Vector3 posicao = transform.position;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            posicao.x -= velocidade * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            posicao.x += velocidade * Time.deltaTime;
        }

        Vector3 bordaEsquerda = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 bordaDireita = Camera.main.ViewportToWorldPoint(Vector3.right);
        posicao.x = Mathf.Clamp(posicao.x, bordaEsquerda.x, bordaDireita.x);

        transform.position = posicao;

        if (laser == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
            laser = Instantiate(prefabLaser, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            GerenciadorJogo.Instancia.OnJogadorMorto(this);
        }
    }

}

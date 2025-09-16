using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Invasor : MonoBehaviour
{
    public Sprite[] spritesAnimacao = new Sprite[0];
    public float tempoAnimacao = 1f;
    public int pontuacao = 10;

    private SpriteRenderer renderizadorSprite;
    private int quadroAnimacao;

    private void Awake()
    {
        renderizadorSprite = GetComponent<SpriteRenderer>();
        renderizadorSprite.sprite = spritesAnimacao[0];
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimarSprite), tempoAnimacao, tempoAnimacao);
    }

    private void AnimarSprite()
    {
        quadroAnimacao++;

        if (quadroAnimacao >= spritesAnimacao.Length) {
            quadroAnimacao = 0;
        }

        renderizadorSprite.sprite = spritesAnimacao[quadroAnimacao];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            GerenciadorJogo.Instancia.OnInvasorMorto(this);
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary")) {
            GerenciadorJogo.Instancia.OnLimiteAlcancado();
        }
    }

}

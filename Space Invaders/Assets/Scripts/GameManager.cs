using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GerenciadorJogo : MonoBehaviour
{
    public static GerenciadorJogo Instancia { get; private set; }

    [SerializeField] private GameObject interfaceGameOver;
    [SerializeField] private Text textoPontuacao;
    [SerializeField] private Text textoVidas;

    private Jogador jogador;
    private Invasores invasores;
    private NaveMisteriosa naveMisteriosa;

    public int pontuacao { get; private set; } = 0;
    public int vidas { get; private set; } = 3;

    private void Awake()
    {
        if (Instancia != null) {
            DestroyImmediate(gameObject);
        } else {
            Instancia = this;
        }
    }

    private void OnDestroy()
    {
        if (Instancia == this) {
            Instancia = null;
        }
    }

    private void Start()
    {
        jogador = FindObjectOfType<Jogador>();
        invasores = FindObjectOfType<Invasores>();
        naveMisteriosa = FindObjectOfType<NaveMisteriosa>();

        NovoJogo();
    }

    private void Update()
    {
        if (vidas <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NovoJogo();
        }
    }

    private void NovoJogo()
    {
        interfaceGameOver.SetActive(false);

        DefinirPontuacao(0);
        DefinirVidas(3);
        NovaRodada();
    }

    private void NovaRodada()
    {
        invasores.ReiniciarInvasores();
        invasores.gameObject.SetActive(true);

        Ressuscitar();
    }

    private void Ressuscitar()
    {
        Vector3 posicao = jogador.transform.position;
        posicao.x = 0f;
        jogador.transform.position = posicao;
        jogador.gameObject.SetActive(true);
    }

    private void FimDeJogo()
    {
        interfaceGameOver.SetActive(true);
        invasores.gameObject.SetActive(false);
    }

    private void DefinirPontuacao(int pontuacao)
    {
        this.pontuacao = pontuacao;
        textoPontuacao.text = pontuacao.ToString().PadLeft(4, '0');
    }

    private void DefinirVidas(int vidas)
    {
        this.vidas = Mathf.Max(vidas, 0);
        textoVidas.text = this.vidas.ToString();
    }

    public void OnJogadorMorto(Jogador jogador)
    {
        DefinirVidas(vidas - 1);

        jogador.gameObject.SetActive(false);

        if (vidas > 0) {
            Invoke(nameof(NovaRodada), 1f);
        } else {
            FimDeJogo();
        }
    }

    public void OnInvasorMorto(Invasor invasor)
    {
        invasor.gameObject.SetActive(false);

        DefinirPontuacao(pontuacao + invasor.pontuacao);

        if (invasores.ObterContagemVivos() == 0) {
            NovaRodada();
        }
    }

    public void OnNaveMisteriosaMorto(NaveMisteriosa naveMisteriosa)
    {
        DefinirPontuacao(pontuacao + naveMisteriosa.pontuacao);
    }

    public void OnLimiteAlcancado()
    {
        if (invasores.gameObject.activeSelf)
        {
            invasores.gameObject.SetActive(false);
            OnJogadorMorto(jogador);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GerenciadorJogo : MonoBehaviour
{
    public static GerenciadorJogo Instancia { get; private set; }

    [SerializeField] private GameObject interfaceGameOver;
    [SerializeField] private GameObject interfaceVitoria;
    [SerializeField] private Text textoPontuacao;
    [SerializeField] private Text textoVidas;
    [SerializeField] private Text textoBoost;

    private Jogador jogador;
    private Invasores invasores;
    private NaveMisteriosa naveMisteriosa;

    public int pontuacao { get; private set; } = 0;
    public int vidas { get; private set; } = 3;

    private bool boostAtivado = false;
    private bool vitoria = false;

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
        if (!boostAtivado && pontuacao > 40 && Input.GetKeyDown(KeyCode.S)) {
            textoBoost.gameObject.SetActive(false);
            jogador.velocidade += 3f;
            boostAtivado = true;
        }
        if (vitoria && vidas > 0 && Input.GetKeyDown(KeyCode.Return)) {
            NovoJogo();
        }
    }

    private void NovoJogo()
    {
        interfaceGameOver.SetActive(false);
        interfaceVitoria.SetActive(false);
        textoBoost.gameObject.SetActive(false);

        boostAtivado = false;
        vitoria = false;
        jogador.velocidade = 5f;
        DefinirPontuacao(0);
        DefinirVidas(3);
        NovaRodada();
    }

    private void CenaVitoria(){
        jogador.gameObject.SetActive(false);
        invasores.gameObject.SetActive(false);
        interfaceVitoria.gameObject.SetActive(true);
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
        if (this.pontuacao > 40 && !boostAtivado) {
            textoBoost.gameObject.SetActive(true);
        }
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
            vitoria = true;
            CenaVitoria();
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

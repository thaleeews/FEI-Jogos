using UnityEngine;

public class Invasores : MonoBehaviour
{
    [Header("Invasores")]
    public Invasor[] prefabs = new Invasor[5];
    public AnimationCurve velocidade = new AnimationCurve();
    private Vector3 direcao = Vector3.right;
    private Vector3 posicaoInicial;

    [Header("Grade")]
    public int linhas = 5;
    public int colunas = 11;

    [Header("Mísseis")]
    public Projetil prefabMissil;
    public float taxaSpawnMissil = 1f;

    private void Awake()
    {
        posicaoInicial = transform.position;

        CriarGradeInvasores();
    }

    private void CriarGradeInvasores()
    {
        for (int i = 0; i < linhas; i++)
        {
            float largura = 2f * (colunas - 1);
            float altura = 2f * (linhas - 1);

            Vector2 deslocamentoCentro = new Vector2(-largura * 0.5f, -altura * 0.5f);
            Vector3 posicaoLinha = new Vector3(deslocamentoCentro.x, (2f * i) + deslocamentoCentro.y, 0f);

            for (int j = 0; j < colunas; j++)
            {
                Invasor invasor = Instantiate(prefabs[i], transform);

                Vector3 posicao = posicaoLinha;
                posicao.x += 2f * j;
                invasor.transform.localPosition = posicao;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(AtaqueMissil), taxaSpawnMissil, taxaSpawnMissil);
    }

    private void AtaqueMissil()
    {
        int quantidadeVivos = ObterContagemVivos();

        if (quantidadeVivos == 0) {
            return;
        }

        foreach (Transform invasor in transform)
        {
            if (!invasor.gameObject.activeInHierarchy) {
                continue;
            }

            if (Random.value < (1f / quantidadeVivos))
            {
                Instantiate(prefabMissil, invasor.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Update()
    {
        int contagemTotal = linhas * colunas;
        int quantidadeVivos = ObterContagemVivos();
        int quantidadeMortos = contagemTotal - quantidadeVivos;
        float percentualMortos = quantidadeMortos / (float)contagemTotal;

        float velocidadeAtual = this.velocidade.Evaluate(percentualMortos);
        transform.position += velocidadeAtual * Time.deltaTime * direcao;

        Vector3 bordaEsquerda = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 bordaDireita = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invasor in transform)
        {
            if (!invasor.gameObject.activeInHierarchy) {
                continue;
            }

            if (direcao == Vector3.right && invasor.position.x >= (bordaDireita.x - 1f))
            {
                AvancarLinha();
                break;
            }
            else if (direcao == Vector3.left && invasor.position.x <= (bordaEsquerda.x + 1f))
            {
                AvancarLinha();
                break;
            }
        }
    }

    private void AvancarLinha()
    {
        direcao = new Vector3(-direcao.x, 0f, 0f);

        Vector3 posicao = transform.position;
        posicao.y -= 1f;
        transform.position = posicao;
    }

    public void ReiniciarInvasores()
    {
        direcao = Vector3.right;
        transform.position = posicaoInicial;

        foreach (Transform invasor in transform) {
            invasor.gameObject.SetActive(true);
        }
    }

    public int ObterContagemVivos()
    {
        int contagem = 0;

        foreach (Transform invasor in transform)
        {
            if (invasor.gameObject.activeSelf) {
                contagem++;
            }
        }

        return contagem;
    }

}

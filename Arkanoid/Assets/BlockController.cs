using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
    public GameObject[] blocks;

    public static BlockController instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); // Destruir o GameObject inteiro, não só o script
            return;
        }
        instance = this;
    }
    
    private void OnDestroy() {
        if (instance == this) {
            instance = null; // Limpar a instância quando destruído
        }
    }

    [Header("Debug")]
    public bool forceTestMode = false; // Desabilitado - agora usa layout baseado na cena
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (forceTestMode) {
            CreateLevel1Blocks();
        } else {
            CreateLevelBlocks();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevelBlocks() {
        string currentScene = SceneManager.GetActiveScene().name;
        
        switch(currentScene) {
            case "Level1":
                CreateLevel1Blocks();
                break;
            case "Level2":
                CreateLevel2Blocks();
                break;
            case "Level3":
                CreateLevel3Blocks();
                break;
            case "SampleScene":
                CreateLevel1Blocks();
                break;
            default:
                CreateBlock(); // Layout padrão para cenas não reconhecidas
                break;
        }
    }
    
    void CreateLevel1Blocks() {
        // Level 1: Mesmo formato do Level3, mas apenas 2 linhas
        CreateBlockGrid(2, 12, -7.15f, 0.3f); // 2 linhas, 12 colunas
    }
    
    
    void CreateLevel2Blocks() {
        // Level 2: Layout médio
        CreateBlockGrid(4, 8, -5.2f, 0.3f); // 4 linhas, 8 colunas
    }
    
    void CreateLevel3Blocks() {
        // Level 3: Layout completo original
        CreateBlock(); // Layout completo original
    }
    
    void CreateBlockGrid(int rows, int cols, float startX, float startY) {
        float px = startX;
        float py = startY;
        
        for (int i = 0; i < rows; i++) {
            px = startX;
            for (int j = 0; j < cols; j++) {
                Vector3 pos = new Vector3(px, py, 0);
                Instantiate(blocks[UnityEngine.Random.Range(0, 6)], pos, Quaternion.identity);
                px = px + 1.3f;
            }
            py = py + 0.7f;
        }
    }

    public void CreateBlock() {
        // Layout original completo (7x12)
        float px = -7.15f;
        float py = 0.3f;
        for (int i = 0; i < 7; i++) {
            px = -7.15f;
            for (int j = 0; j < 12; j++) {
                Vector3 pos = new Vector3(px, py, 0);
                Instantiate(blocks[UnityEngine.Random.Range(0, 6)], pos, Quaternion.identity);
                px = px + 1.3f;
            }
            py = py + 0.7f;
        }
    }

    public void DestroyAllByTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Bloco");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}

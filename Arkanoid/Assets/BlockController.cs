using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] blocks;

    public static BlockController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            if (instance != this) {
                Destroy(this);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.CreateBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlock() {
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

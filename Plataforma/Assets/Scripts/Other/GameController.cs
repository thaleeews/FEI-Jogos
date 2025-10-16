using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    public static Action<GameObject> OnPlayerSpawned;


    private void Awake()
    {
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
    
    void Start()
    {
        OnPlayerSpawned?.Invoke(player);
    }

    private void ResetScene()
    {
        Invoke("ResetSceneDelay", 2f);
    }

    private void ResetSceneDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDie += ResetScene;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDie -= ResetScene;
    }
}

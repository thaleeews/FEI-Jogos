using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private TMP_Text winText;
    private GameObject player;
    public static Action<GameObject> OnPlayerSpawned;
    public enum GameState
    {
        Loading,
        Active,
        Paused,
        Complete,
        Win
    }
    public GameState CurrentGameState { get; private set; }
    public static Action<GameState> OnGameStateChanged;


    private void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        if (winText != null)
            winText.gameObject.SetActive(false);
    }
    
    void Start()
    {
        SetGameState(GameState.Active);
        OnPlayerSpawned?.Invoke(player);
    }

    public void SetGameState(GameState newState)
    {
        if (CurrentGameState == newState) return;
        switch (newState)
        {
            case GameState.Loading:
                Time.timeScale = 1f;
                break;
            case GameState.Active:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.Complete:
                HandleComplete();
                break;
            case GameState.Win:
                HandleWin();
                break;
        }

        CurrentGameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleComplete()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = index + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogError($"Erro: Tentando carregar cena que não existe. Índice: {nextIndex}");
        }
    }

    private void HandleWin()
    {
        winText.gameObject.SetActive(true);
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

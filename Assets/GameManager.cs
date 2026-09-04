using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameEndState { None, Victory, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 3;
    public int pontuacao;
    public int pontosPorBloco = 10;
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public GameEndState EndState { get; private set; } = GameEndState.None;
    public int ActiveBallCount => activeBallIds.Count;
    private readonly HashSet<int> activeBallIds = new HashSet<int>();
    private bool phaseTransitioning;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    void Start()
    {
        if (IsGameplayScene(SceneManager.GetActiveScene()) && ActiveBallCount == 0) CriarBola();
    }

    void Update() => VerificarFimDaFase();
    void OnEnable() { if (instance == this) SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        activeBallIds.Clear();
        phaseTransitioning = false;
        if (IsGameplayScene(scene)) CriarBola();
    }

    public void PerderVida()
    {
        if (phaseTransitioning) return;
        vidas--;
        if (vidas > 0) CriarBola();
        else EncerrarJogo(GameEndState.GameOver);
    }

    public void AdicionarPontos() => pontuacao += pontosPorBloco;
    public void RegistrarBola(BallController ball) { if (ball != null) activeBallIds.Add(ball.GetInstanceID()); }
    public void RemoverBola(BallController ball) { if (ball != null) activeBallIds.Remove(ball.GetInstanceID()); }

    public void PerderBola(BallController ball)
    {
        if (ball == null || phaseTransitioning) return;
        RemoverBola(ball);
        Destroy(ball.gameObject);
        if (ActiveBallCount == 0) PerderVida();
    }

    public void AplicarPowerUp(PowerUpType type)
    {
        if (type == PowerUpType.ExpandPaddle)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null) player.ApplyPaddleExpansion(1.5f, 30f);
        }
        else if (type == PowerUpType.MultiBall) CriarBolasExtras(2);
    }

    public void CriarBolasExtras(int requestedAmount)
    {
        int amount = Mathf.Min(requestedAmount, Mathf.Max(0, 3 - ActiveBallCount));
        for (int i = 0; i < amount; i++) CriarBola();
    }

    void CriarBola()
    {
        if (!IsGameplayScene(SceneManager.GetActiveScene()) || ballPrefab == null) return;
        GameObject spawn = GameObject.Find("BallSpawnPoint");
        if (spawn != null) ballSpawnPoint = spawn.transform;
        if (ballSpawnPoint != null)
        {
            GameObject ballObject = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            RegistrarBola(ballObject.GetComponent<BallController>());
        }
    }

    void VerificarFimDaFase()
    {
        if (phaseTransitioning || !IsGameplayScene(SceneManager.GetActiveScene())) return;
        BrickLayout layout = FindFirstObjectByType<BrickLayout>();
        if (layout == null || !layout.Generated) return;
        if (GameObject.FindGameObjectsWithTag("Brick").Length != 0) return;
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene1") CarregarFase("Scene2");
        else if (sceneName == "Scene2") CarregarFase("Scene3");
        else EncerrarJogo(GameEndState.Victory);
    }

    public void ReiniciarPartida()
    {
        vidas = 3; pontuacao = 0; EndState = GameEndState.None;
        activeBallIds.Clear(); phaseTransitioning = true;
        SceneManager.LoadScene("Scene1");
    }

    public void RestartGame() => ReiniciarPartida();
    void CarregarFase(string sceneName) { phaseTransitioning = true; SceneManager.LoadScene(sceneName); }
    void EncerrarJogo(GameEndState result) { phaseTransitioning = true; EndState = result; SceneManager.LoadScene("FinalScene"); }
    static bool IsGameplayScene(Scene scene) => scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3";
}

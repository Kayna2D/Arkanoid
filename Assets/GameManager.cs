using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Vidas")]
    public int vidas = 3;

    [Header("Pontuação")]
    public int pontuacao = 0;
    public int pontosPorBloco = 10;

    [Header("Bola")]
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        VerificarFimDaFase();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EncontrarSpawnPoint();
        CriarBola();
    }

    public void PerderVida()
    {
        vidas--;

        Debug.Log("Vida perdida! Vidas restantes: " + vidas);

        if (vidas > 0)
        {
            CriarBola();
        }
        else
        {
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void AdicionarPontos()
    {
        pontuacao += pontosPorBloco;

        Debug.Log("Pontuação: " + pontuacao);
    }

    void CriarBola()
    {
        EncontrarSpawnPoint();

        if (ballSpawnPoint != null)
        {
            Instantiate(
                ballPrefab,
                ballSpawnPoint.position,
                Quaternion.identity
            );
        }
        else
        {
            Debug.LogError(
                "BallSpawnPoint não encontrado!"
            );
        }
    }

    void VerificarFimDaFase()
    {
        GameObject[] blocos =
            GameObject.FindGameObjectsWithTag("Brick");

        if (blocos.Length == 0)
        {
            Scene cenaAtual =
                SceneManager.GetActiveScene();

            if (cenaAtual.name == "Scene1")
            {
                SceneManager.LoadScene("Scene2");
            }
            else if (cenaAtual.name == "Scene2")
            {
                SceneManager.LoadScene("Scene3");
            }
            else if (cenaAtual.name == "Scene3")
            {
                SceneManager.LoadScene("FinalScene");
            }
        }
    }

    void EncontrarSpawnPoint()
    {
        GameObject spawn =
            GameObject.Find("BallSpawnPoint");

        if (spawn != null)
        {
            ballSpawnPoint = spawn.transform;
        }
    }
}
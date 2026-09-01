using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Vidas")]
    public int vidas = 3;

    [Header("Pontuação")]
    public int pontuacao = 0;
    public int pontosPorBloco = 10;

    [Header("Bola")]
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    void Start()
    {
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
            SceneManager.LoadScene("Cena_Final");
        }
    }

    public void AdicionarPontos()
    {
        pontuacao += pontosPorBloco;

        Debug.Log("Pontuação: " + pontuacao);
    }

    void CriarBola()
    {
        Instantiate(
            ballPrefab,
            ballSpawnPoint.position,
            Quaternion.identity
        );
    }
}
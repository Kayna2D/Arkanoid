using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        AtualizarHUD();
    }

    void Update()
    {
        AtualizarHUD();
    }

    void AtualizarHUD()
    {
        scoreText.text = "PONTOS: " + gameManager.pontuacao;

        livesText.text = "VIDAS: ";

        for (int i = 0; i < gameManager.vidas; i++)
        {
            livesText.text += "♥ ";
        }
    }
}
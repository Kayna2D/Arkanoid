using UnityEngine;

public class FinalScreenController : MonoBehaviour
{
    [Tooltip("Arte horizontal sem texto para o fundo.")]
    public Sprite backgroundImage;

    void Start()
    {
        ArcadeMenuUI.EnsureEventSystem();
        Canvas canvas = ArcadeMenuUI.CreateCanvas("FinalCanvas");
        ArcadeMenuUI.Background(canvas.transform, backgroundImage);
        bool victory = GameManager.instance != null && GameManager.instance.EndState == GameEndState.Victory;
        string result = victory ? "VOC\u00CA VENCEU!" : "GAME OVER";
        Color resultColor = victory ? new Color(0.25f, 1f, 0.55f) : new Color(1f, 0.28f, 0.45f);
        ArcadeMenuUI.Label(canvas.transform, result, new Vector2(0.5f, 0.62f), 72, resultColor);
        ArcadeMenuUI.Label(canvas.transform, "PRONTO PARA MAIS UMA PARTIDA?", new Vector2(0.5f, 0.51f), 25, Color.white);
        ArcadeMenuUI.Button(canvas.transform, "REINICIAR", new Vector2(0.5f, 0.35f), RestartGame);
    }

    public void RestartGame()
    {
        if (GameManager.instance != null) GameManager.instance.ReiniciarPartida();
    }
}

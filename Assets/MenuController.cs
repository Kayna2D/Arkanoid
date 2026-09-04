using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Tooltip("Arte horizontal sem texto para o fundo.")]
    public Sprite backgroundImage;

    void Start()
    {
        ArcadeMenuUI.EnsureEventSystem();
        Canvas canvas = ArcadeMenuUI.CreateCanvas("IntroCanvas");
        ArcadeMenuUI.Background(canvas.transform, backgroundImage);
        ArcadeMenuUI.Label(canvas.transform, "ARKANOID", new Vector2(0.5f, 0.66f), 88, new Color(0.2f, 0.95f, 1f));
        ArcadeMenuUI.Label(canvas.transform, "QUEBRE TODOS OS BLOCOS", new Vector2(0.5f, 0.55f), 26, Color.white);
        ArcadeMenuUI.Button(canvas.transform, "INICIAR", new Vector2(0.5f, 0.38f), StartGame);
    }

    public void StartGame()
    {
        if (GameManager.instance != null) GameManager.instance.ReiniciarPartida();
        else SceneManager.LoadScene("Scene1");
    }
}

internal static class ArcadeMenuUI
{
    public static void EnsureEventSystem()
    {
        if (Object.FindFirstObjectByType<EventSystem>() == null)
            new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
    }

    public static Canvas CreateCanvas(string name)
    {
        GameObject root = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = root.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        return canvas;
    }

    public static void Background(Transform parent, Sprite sprite)
    {
        Image image = Image("Background", parent, sprite == null ? new Color(0.025f, 0.02f, 0.09f) : Color.white);
        image.sprite = sprite;
        image.preserveAspect = true;
        Stretch(image.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
    }

    public static void Label(Transform parent, string value, Vector2 anchor, float size, Color color)
    {
        GameObject root = new GameObject(value, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        root.transform.SetParent(parent, false);
        TextMeshProUGUI text = root.GetComponent<TextMeshProUGUI>();
        text.text = value; text.font = TMP_Settings.defaultFontAsset; text.fontSize = size;
        text.fontStyle = FontStyles.Bold; text.alignment = TextAlignmentOptions.Center; text.color = color;
        Stretch(text.rectTransform, anchor, anchor, new Vector2(-600, -65), new Vector2(600, 65));
    }

    public static void Button(Transform parent, string value, Vector2 anchor, UnityEngine.Events.UnityAction action)
    {
        Image image = Image(value + "Button", parent, new Color(0.08f, 0.55f, 0.9f, 0.95f));
        Stretch(image.rectTransform, anchor, anchor, new Vector2(-170, -45), new Vector2(170, 45));
        UnityEngine.UI.Button button = image.gameObject.AddComponent<UnityEngine.UI.Button>();
        button.targetGraphic = image; button.onClick.AddListener(action);
        Label(image.transform, value, new Vector2(0.5f, 0.5f), 32, Color.white);
    }

    static Image Image(string name, Transform parent, Color color)
    {
        GameObject root = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        root.transform.SetParent(parent, false);
        Image image = root.GetComponent<Image>(); image.color = color;
        return image;
    }

    static void Stretch(RectTransform rect, Vector2 min, Vector2 max, Vector2 offsetMin, Vector2 offsetMax)
    {
        rect.anchorMin = min; rect.anchorMax = max; rect.offsetMin = offsetMin; rect.offsetMax = offsetMax;
    }
}

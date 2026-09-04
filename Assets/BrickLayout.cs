using System.Collections.Generic;
using UnityEngine;

public class BrickLayout : MonoBehaviour
{
    public GameObject[] brickPrefabs;
    public int columns = 10;
    public int rows = 5;
    public float brickWidth = 0.64f;
    public float brickHeight = 0.32f;
    public float spacingX;
    public float spacingY;
    public int pattern;
    public float startY = 4f;
    public int hitsToDestroy = 1;
    [Header("Sprites opcionais dos power-ups")] public Sprite expandPaddleSprite;
    public Sprite multiBallSprite;
    public bool Generated { get; private set; }

    void Start() => GenerateBricks();

    void GenerateBricks()
    {
        Generated = false;
        if (brickPrefabs.Length == 0) return;
        float totalWidth = columns * brickWidth + (columns - 1) * spacingX;
        float startX = -totalWidth / 2f + brickWidth / 2f;
        List<Vector2Int> validPositions = new List<Vector2Int>();

        for (int row = 0; row < rows; row++)
            for (int column = 0; column < columns; column++)
                if (DeveCriarBloco(row, column)) validPositions.Add(new Vector2Int(row, column));

        HashSet<Vector2Int> positionsWithBrick = new HashSet<Vector2Int>(validPositions);
        Vector2Int expandPosition = new Vector2Int(-1, -1);
        Vector2Int multiBallPosition = new Vector2Int(-1, -1);
        if (validPositions.Count >= 2)
        {
            int index = Random.Range(0, validPositions.Count);
            expandPosition = validPositions[index];
            validPositions.RemoveAt(index);
            multiBallPosition = validPositions[Random.Range(0, validPositions.Count)];
        }

        for (int row = 0; row < rows; row++)
        for (int column = 0; column < columns; column++)
        {
            if (!positionsWithBrick.Contains(new Vector2Int(row, column))) continue;
            Vector3 position = new Vector3(startX + column * (brickWidth + spacingX), startY - row * (brickHeight + spacingY), 0);
            GameObject brick = Instantiate(brickPrefabs[(row + column) % brickPrefabs.Length], position, Quaternion.identity, transform);
            Brick brickScript = brick.GetComponent<Brick>();
            if (brickScript != null)
            {
                brickScript.hitsRemaining = hitsToDestroy;
                Vector2Int current = new Vector2Int(row, column);
                if (current == expandPosition) brickScript.ConfigurePowerUp(PowerUpType.ExpandPaddle, expandPaddleSprite);
                else if (current == multiBallPosition) brickScript.ConfigurePowerUp(PowerUpType.MultiBall, multiBallSprite);
            }
            brick.name = "Brick_" + row + "_" + column;
        }
        Generated = true;
    }

    bool DeveCriarBloco(int row, int column)
    {
        if (pattern == 0) return true;
        if (pattern == 1) return Mathf.Abs(column - columns / 2) <= row + 1;
        if (pattern == 2) return Mathf.Abs(column - columns / 2) + Mathf.Abs(row - rows / 2) <= 3;
        // Padrão 3: piramide curta para uma fase inicial facilzinha
        if (pattern == 3) return row < 3 && Mathf.Abs(column - columns / 2) <= row + 1;
        return false;
    }
}

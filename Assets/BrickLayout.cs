using UnityEngine;

public class BrickLayout : MonoBehaviour
{
    [Header("Prefabs dos blocos")]
    public GameObject[] brickPrefabs;

    [Header("Configuração da formação")]
    public int columns = 10;
    public int rows = 5;

    [Header("Tamanho do bloco")]
    public float brickWidth = 0.64f;
    public float brickHeight = 0.32f;

    [Header("Espaçamento")]
    public float spacingX = 0.05f;
    public float spacingY = 0.05f;

    [Header("Posição inicial")]
    public float startY = 6f;

    void Start()
    {
        GenerateBricks();
    }

    void GenerateBricks()
    {
        if (brickPrefabs.Length == 0)
        {
            Debug.LogError("Nenhum prefab de bloco foi configurado!");
            return;
        }

        float totalWidth =
            columns * brickWidth +
            (columns - 1) * spacingX;

        float startX =
            -totalWidth / 2f +
            brickWidth / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float x =
                    startX +
                    column * (brickWidth + spacingX);

                float y =
                    startY - 
                    row * (brickHeight + spacingY);

                Vector3 position =
                    new Vector3(x, y, 0);

                // Escolhe o prefab de acordo com a linha
                int prefabIndex = row % brickPrefabs.Length;

                GameObject brick = Instantiate(
                    brickPrefabs[prefabIndex],
                    position,
                    Quaternion.identity,
                    transform
                );

                brick.name =
                    "Brick_" + row + "_" + column;
            }
        }
    }
}
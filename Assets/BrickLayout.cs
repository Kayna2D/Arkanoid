using UnityEngine;

public class BrickLayout : MonoBehaviour
{
    [Header("Prefabs dos blocos")]
    public GameObject[] brickPrefabs;

    [Header("Configuração")]
    public int columns = 10;
    public int rows = 5;

    [Header("Tamanho do bloco")]
    public float brickWidth = 0.64f;
    public float brickHeight = 0.32f;

    [Header("Espaçamento")]
    public float spacingX = 0.0f;
    public float spacingY = 0.0f;

    [Header("Padrão do nível")]
    public int pattern = 0;
    public float startY = 4.0f;

    [Header("Resistência dos blocos")]
    public int hitsToDestroy = 1;



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
                if (!DeveCriarBloco(row, column))
                    continue;

                float x =
                    startX +
                    column * (brickWidth + spacingX);

                float y =
                    startY - 
                    row * (brickHeight + spacingY);

                Vector3 position =
                    new Vector3(x, y, 0);

                int prefabIndex =
                    (row + column) % brickPrefabs.Length;

                GameObject brick = Instantiate(
                    brickPrefabs[prefabIndex],
                    position,
                    Quaternion.identity,
                    transform
                );

                Brick brickScript = brick.GetComponent<Brick>();

                if (brickScript != null)
                {
                    brickScript.hitsRemaining = hitsToDestroy;
                }

                brick.name =
                    "Brick_" + row + "_" + column;
            }
        }
    }

    bool DeveCriarBloco(int row, int column)
    {
        // Padrão 0 = formação completa
        if (pattern == 0)
        {
            return true;
        }

        // Padrão 1 = pirâmide
        if (pattern == 1)
        {
            int distanciaCentro = Mathf.Abs(column - columns / 2);

            return distanciaCentro <= row + 1;
        }

        // Padrão 2 = diamante
        if (pattern == 2)
        {
            int centroColuna = columns / 2;
            int centroLinha = rows / 2;

            int distanciaX = Mathf.Abs(column - centroColuna);
            int distanciaY = Mathf.Abs(row - centroLinha);

            return distanciaX + distanciaY <= 3;
        }

        return true;
    }
}
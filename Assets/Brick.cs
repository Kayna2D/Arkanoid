using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsRemaining = 1;

    public void TakeHit()
    {
        hitsRemaining--;

        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        }
    }
}
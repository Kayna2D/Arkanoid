using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 normalScale;
    private int expansionVersion;

    void Awake() => normalScale = transform.localScale;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -2.6f, 2.6f);
        transform.position = position;
    }

    public void ApplyPaddleExpansion(float multiplier, float duration)
    {
        expansionVersion++;
        transform.localScale = new Vector3(normalScale.x * multiplier, normalScale.y, normalScale.z);
        StartCoroutine(RestorePaddleAfterDelay(expansionVersion, duration));
    }

    IEnumerator RestorePaddleAfterDelay(int version, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (version == expansionVersion) transform.localScale = normalScale;
    }
}

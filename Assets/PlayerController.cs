using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(
            Vector2.right * horizontal * speed * Time.deltaTime
        );

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, -2.6f, 2.6f);

        transform.position = position;
    }
}
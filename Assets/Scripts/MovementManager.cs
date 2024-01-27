using UnityEngine;

public class MovementManager : MonoBehaviour
{
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        var newPos = body.transform.position + new Vector3(-hor, 0 , -vert);

        body.MovePosition(newPos);
    }
}

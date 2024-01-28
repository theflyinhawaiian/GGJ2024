using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
            return;

        collider.gameObject.transform.position = new Vector3(-1, 4, 1);
    }
}

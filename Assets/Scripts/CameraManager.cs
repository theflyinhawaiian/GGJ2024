using UnityEngine;

namespace Assets.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        public float mouseSensitivity;
        public Transform playerBody;
        public Vector3 offset; // Offset position from the player
        public float followSpeed = 10f; // Speed at which the camera follows the player

        private float xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Mouse input for camera rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Calculating camera rotation
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply rotation to camera and player
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        void LateUpdate()
        {
            // Update camera position to follow the player with offset
            Vector3 targetPosition = playerBody.position + playerBody.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}

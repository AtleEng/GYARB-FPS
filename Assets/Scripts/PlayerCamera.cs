using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject camHolder;
    [SerializeField] GameObject oriention;
    float pitch;
    float yaw;

    [Header("Stats")]
    [SerializeField] float sensitivity;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        pitch -= mouseDelta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, -50, 20);

        yaw += mouseDelta.x * sensitivity;
        yaw = Mathf.Clamp(yaw, -140, -40);

        camHolder.transform.localEulerAngles = Vector3.right * pitch;

        oriention.transform.localEulerAngles = Vector3.up * yaw;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(camHolder.transform.position, camHolder.transform.position + camHolder.transform.forward);
    }
}

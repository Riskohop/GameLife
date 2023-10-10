using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 move;
    Camera camera;
    [SerializeField] float speed;
    [SerializeField] float speedZoom;
    private void OnValidate() {
        camera ??= GetComponent<Camera>();
    }
    private void Update() {
        move.x = Input.GetAxis("Horizontal") * speed;
        move.y = Input.GetAxis("Vertical") * speed;
        float zoom = Input.GetAxis("Debug Vertical") * speedZoom;
        transform.Translate(move);
        camera.orthographicSize += zoom;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 13);
        camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -23, 63), 8,
                                                Mathf.Clamp(camera.transform.position.z, -14, 33));
    }
}

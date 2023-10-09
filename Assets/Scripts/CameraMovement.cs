using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 move;
    Camera camera;
    float speed;
    private void OnValidate() {
        camera ??= GetComponent<Camera>();
    }
    private void Update() {
        move.x = Input.GetAxis("Horizontal") * speed;
        move.y = Input.GetAxis("Vertical") * speed;
        float scroll = Input.mouseScrollDelta.y;
        transform.Translate(move);
        camera.orthographicSize += scroll;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 10);
    }
}

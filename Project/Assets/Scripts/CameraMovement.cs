using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 move;
    Camera camera;
    [SerializeField] float speed;
    private void OnValidate() {
        camera ??= GetComponent<Camera>();
    }
    private void Update() {
        move.x = Input.GetAxis("Horizontal") * speed;
        move.y = Input.GetAxis("Vertical") * speed;
        transform.Translate(move);
        if(Input.GetKey(KeyCode.E)) {
            camera.orthographicSize += 0.05f;
        }
        if(Input.GetKey(KeyCode.Q)) {
            camera.orthographicSize -= 0.05f;
        }
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 10);
        camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -20, 20), 8,
                                                Mathf.Clamp(camera.transform.position.z, -17, 17));
    }
}

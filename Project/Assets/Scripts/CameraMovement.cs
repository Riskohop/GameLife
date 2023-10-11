using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 move;
    [SerializeField] Camera camera;
    [SerializeField] float speed;
    private void OnValidate() {
        camera ??= GetComponent<Camera>();
    }
    private void Start() {
        camera = GetComponent<Camera>();
    }
    private void Update() {
        move.x = Input.GetAxis("Horizontal") * speed;
        move.y = Input.GetAxis("Vertical") * speed;
        transform.Translate(move);
        
        //camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 13);
        camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -23, 63), 8,
                                                Mathf.Clamp(camera.transform.position.z, -14, 33));
    }
    public void ZoomIn() {
        camera.orthographicSize += 0.4f;
        if(camera.orthographicSize > 13) {
            camera.orthographicSize = 13;
        }

    }
    public void ZoomOut() {
        camera.orthographicSize -= 0.4f;
        if(camera.orthographicSize < 1) {
            camera.orthographicSize = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] MeshRenderer boardMeshRenderer;
    Texture2D tex;
    int x = 0;
    int y = 0;
    void Start()
    {
        tex = new Texture2D(200,200, TextureFormat.ARGB32, false);
        tex.Apply();
        tex.filterMode = FilterMode.Point;
        boardMeshRenderer.material.mainTexture = tex;
        StartCoroutine(paint());
    }
    IEnumerator paint() {
        while(true) {
            if(x < 200) {
                while(y < 200) {
                    tex.SetPixel(x, y, Random.ColorHSV(0, 1, 0, 1, 0, 1));
                    tex.Apply();
                    boardMeshRenderer.material.mainTexture = tex;
                    y++;
                    yield return new WaitForSeconds(0.00001f);
                }
                y = 0;
                x++;
            }
            else {
                StopCoroutine(paint());
            }
            tex.Apply();
            boardMeshRenderer.material.mainTexture = tex;
            yield return new WaitForSeconds(0.00001f);

        }
    }
}

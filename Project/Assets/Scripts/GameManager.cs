using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Game game;
    [SerializeField] Display display;
    private void Start() {
        game.CreateGame(400, 200);
        //game.StartGame();
    }
    private void Update() {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(diference.x * 10);
        int y = (game.boardCells.GetLength(1) - (int)(diference.z * 10)) - 1;
        if(Input.GetKey(KeyCode.Mouse0) && (x < 400 && y < 200 && y > 0 && x > 0) ) {
            game.Draw(x, y);
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            game.StartGame();
        }
    }
}

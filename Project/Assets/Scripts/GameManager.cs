using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Game game;
    [SerializeField] Display display;
    [SerializeField] public UI ui;
    [SerializeField] CameraMovement cameraMovement;
    bool _isPencil = true;
    private void Start()
    {
        game.CreateGame(400, 200);
        //game.StartGame();
    }
    private void Update() {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(diference.x * 10);
        int y = (game.BoardCells.GetLength(1) - (int)(diference.z * 10)) - 1;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, Mathf.Infinity)) {
            if(Input.GetKey(KeyCode.Mouse0) && (x < 400 && y < 200 && y > 0 && x > 0)) {
                game.Draw(x, y, _isPencil);
            }
        }
        
    }
    public void StartPauseGame() {
        if(game.isGaming) {
            game.PauseGame();
            ui.playImage.SetActive(true);
            ui.pauseImage.SetActive(false);
        }
        else {
            game.StartGame();
            ui.playImage.SetActive(false);
            ui.pauseImage.SetActive(true);
        }
    }
    public void RestartGame() {
        game.ClearGame();
        ui.playImage.SetActive(true);
        ui.pauseImage.SetActive(false);
    }
    public void ChangeSpeedGame() {
        game.speedGame = ui.speedSlider.value;
    }
    public void ChangeTypeDrawing() {
        if(_isPencil) {
            _isPencil = false;
            ui.isPencilImage.SetActive(true);
            ui.isEraseImage.SetActive(false);
        } else {
            _isPencil = true;
            ui.isPencilImage.SetActive(false);
            ui.isEraseImage.SetActive(true);
        }
    }
    public void CameraZoomIn() {
        cameraMovement.ZoomIn();
    }
    public void CameraZoomOut() {
        cameraMovement.ZoomOut();
    }
}

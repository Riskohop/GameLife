using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Game game;
    [SerializeField] Display _display;
    [SerializeField] public UI ui;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] Brush pixelBrush;
    bool _isPencil = true; 
    Queue<int> _coordinateX;
    Queue<int> _coordinateY;
    private void Start()
    {
        game.CreateGame(200, 400);
        _coordinateX = new Queue<int>();
        _coordinateY = new Queue<int>();
        _coordinateX.Enqueue(1);
        _coordinateY.Enqueue(1);
        //game.StartGame();
    }

    private void Update() {
        if (_coordinateX == null)
        {
            _coordinateX.Enqueue(60);
        }
        if (_coordinateY == null)
        {
            _coordinateY.Enqueue(60);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var x = (int)(diference.z * 10);
        var y = (game.BoardCells.GetLength(1) - (int)(diference.x * 10)) - 1;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Board"))
            {
                Cursor.visible = false;
                if ((x < (200 - (pixelBrush.brushMatrix.GetCells().GetLength(0) - 1)) && y < (400 - (pixelBrush.brushMatrix.GetCells().GetLength(1) - 1)) && y > -1 && x > -1))
                {
                    if (x != _coordinateX.Peek() && !game.isGaming)
                    {
                        _coordinateX.Enqueue(x);
                    }
                    if (y != _coordinateX.Peek() && !game.isGaming)
                    {
                        _coordinateY.Enqueue(y);
                    }
                    game.EaseCursor(_coordinateX.Peek(),_coordinateY.Peek(), pixelBrush);
                    if (x != _coordinateX.Peek() && !game.isGaming)
                    {
                        _coordinateX.Dequeue();
                    }
                    if (y != _coordinateX.Peek() && !game.isGaming)
                    {
                        _coordinateY.Dequeue();
                    }
                    if(Input.GetKey(KeyCode.Mouse0)) {
                        game.Draw(_coordinateX.Peek(), _coordinateY.Peek(), _isPencil, pixelBrush);
                    }
                    game.DrawCursor(_coordinateX.Peek(),_coordinateY.Peek(), pixelBrush);
                }
                
            }
            else
            {
                Cursor.visible = true;
                game.EaseCursor(_coordinateX.Peek(),_coordinateY.Peek(), pixelBrush);

            }
        }
        else
        {
            Cursor.visible = true;
            game.EaseCursor(_coordinateX.Peek(),_coordinateY.Peek(), pixelBrush);
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

    public void ChangeBrush(Brush brush)
    {
        pixelBrush = brush;
        _coordinateX.Peek();
        _coordinateY.Peek();
        game.EaseCursor(_coordinateX.Peek(),_coordinateY.Peek(), pixelBrush);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRendererBorder;
    [SerializeField] private GameManager _gameManager;
    Texture2D _textureGame;
    public Cell[,] BoardCells { get; private set; }
    public float speedGame;
    public bool isGaming;
    float _timer;
    private int[] _ruleB;
    private int[] _ruleS;

    #region GameAlgoritm

    public bool CreateGame(int x, int y) {
        if(x > 0 && y > 0) {
            BoardCells = new Cell[x, y];
            _textureGame = new Texture2D(BoardCells.GetLength(0), BoardCells.GetLength(1), TextureFormat.ARGB32, false);
            for(int xx = 0; xx < BoardCells.GetLength(0); xx++) {
                for(int yy = 0; yy < BoardCells.GetLength(1); yy++) {
                    BoardCells[xx, yy] = new Cell();
                    _textureGame.SetPixel(xx, yy, Color.white);
                    _textureGame.Apply();
                }
            }
            _textureGame.filterMode = FilterMode.Point;
            meshRendererBorder.material.mainTexture = _textureGame;
            
            return true;
        }
        else {
            return false;
        }
    }
    public bool ClearGame() {
        if(BoardCells != null) {
            isGaming = false;
            CreateGame(BoardCells.GetLength(0), BoardCells.GetLength(1));
            return true;
        } else {
            return false;
        }
    }
    public bool PauseGame() {
        if(BoardCells != null) {
            isGaming = false;
            return true;
        } else {
            return false;
        }
    }
    public bool StartGame()
    {
        _ruleB = string.IsNullOrEmpty(_gameManager.ui.ruleB.text) ? new int[] { 3 } : EncodeRule(_gameManager.ui.ruleB.text);
        _ruleS = string.IsNullOrEmpty(_gameManager.ui.ruleS.text) ? new int[] { 2, 3 } : EncodeRule(_gameManager.ui.ruleS.text);
        if(BoardCells != null && !isGaming) {
            isGaming = true;
            return true;
        }
        else {
            return false;
        }
    }

    #endregion
    public void Draw(int x, int y, bool isPencil, Brush brush) {
        var cellsBrush = brush.brushMatrix.GetCells();
        if(isPencil) {
            for (int xCell = 0; xCell < cellsBrush.GetLength(0); xCell++)
            {
                for (int yCell = 0; yCell < cellsBrush.GetLength(1); yCell++)
                {
                    if (cellsBrush[xCell, yCell])
                    {
                        _textureGame.SetPixel(x + xCell, y + yCell, Color.black);
                        BoardCells[x + xCell, y + yCell].IsLive = true;
                    }
                }
            }
        } else {
            for (int xCell = 0; xCell < cellsBrush.GetLength(0); xCell++)
            {
                for (int yCell = 0; yCell < cellsBrush.GetLength(1); yCell++)
                {
                    if (cellsBrush[xCell, yCell])
                    {
                        _textureGame.SetPixel(x + xCell, y + yCell, Color.white);
                        BoardCells[x + xCell, y + yCell].IsLive = false;
                    }
                }
            }
            
        }
        _textureGame.Apply();
        meshRendererBorder.material.mainTexture = _textureGame;
        
    }

    public void DrawCursor(int x, int y, Brush brush)
    {
        var cellsBrush = brush.brushMatrix.GetCells();
        for (int xCell = 0 ; xCell < cellsBrush.GetLength(0); xCell++)
        {
            for (int yCell = 0 ; yCell < cellsBrush.GetLength(1); yCell++)
            {
                if (cellsBrush[xCell, yCell])
                {
                    _textureGame.SetPixel(x + xCell, y + yCell, Color.grey);
                }
            }
        }
        

        _textureGame.Apply();
        meshRendererBorder.material.mainTexture = _textureGame;
    }
    public void EaseCursor(int x, int y, Brush brush)
    {
        var cellsBrush = brush.brushMatrix.GetCells();
        for (int xCell = 0; xCell < cellsBrush.GetLength(0); xCell++)
        {
            for (int yCell = 0; yCell < cellsBrush.GetLength(1); yCell++)
            {
                _textureGame.SetPixel(x + xCell, y + yCell, BoardCells[x + xCell, y + yCell].IsLive ? Color.black : Color.white);
            }
        }
      
        _textureGame.Apply();
        meshRendererBorder.material.mainTexture = _textureGame;
    }
    private void Update() {
        if(isGaming && speedGame < _timer) {
            //CalculateNeighboors
            for(int x = 0; x < BoardCells.GetLength(0); x++) {
                for(int y = 0; y < BoardCells.GetLength(1); y++) {
                    BoardCells[x, y].NeighborsCells = CalculateNeighborsAtCell(x, y);
                }
            }
            //Turn
            for(int x = 0; x < BoardCells.GetLength(0); x++) {
                for(int y = 0; y < BoardCells.GetLength(1); y++) {
                    MakeTurn(x, y);
                    Color color = BoardCells[x,y].IsLive ? Color.black : Color.white;
                    _textureGame.SetPixel(x, y, color);
                }
            }
            _timer = 0;
            _textureGame.Apply();
            meshRendererBorder.material.mainTexture = _textureGame;
        }
        _timer += Time.deltaTime;
    }
    private bool MakeTurn(int x, int y) {
        if(BoardCells != null) {
            if(BoardCells[x,y].IsLive) {
                for (var i = 0; i < _ruleS.Length; i++)
                {
                    if (BoardCells[x, y].NeighborsCells == _ruleS[i])
                    {
                        BoardCells[x, y].IsLive = true;
                        return true;
                    }
                    else
                    {
                        BoardCells[x, y].IsLive = false;
                    }
                }
            } else {
                for (var i = 0; i < _ruleB.Length; i++)
                {
                    if (BoardCells[x, y].NeighborsCells == _ruleB[i])
                    {
                        BoardCells[x, y].IsLive = true;
                        return true;
                    }
                    else
                    {
                        BoardCells[x, y].IsLive = false;
                    }
                }
            }
            return true;
        } else {
            return false;
        }
    }
    private int CalculateNeighborsAtCell(int x, int y) {
        var xCoordinate = x; var yCoordinate = y;
        var neighbors = 0;
        //x++
        if(xCoordinate == BoardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //y++
        if(yCoordinate == BoardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }
        
        xCoordinate = x; yCoordinate = y;
        //x--
        if(xCoordinate == 0) { xCoordinate = BoardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //y--
        if(yCoordinate == 0) { yCoordinate = BoardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //x++ y++
        if(xCoordinate == BoardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(yCoordinate == BoardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //x-- y--
        if(yCoordinate == 0) { yCoordinate = BoardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(xCoordinate == 0) { xCoordinate = BoardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //x++ y--
        if(xCoordinate == BoardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(yCoordinate == 0) { yCoordinate = BoardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        xCoordinate = x; yCoordinate = y;
        //x-- y++
        if(yCoordinate == BoardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(xCoordinate == 0) { xCoordinate = BoardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(BoardCells[xCoordinate, yCoordinate].IsLive) { neighbors++; }

        return neighbors;
    }
    private int[] EncodeRule(string rule)
    {
        var ruleArray = new int[rule.Length];
        var ruleChars = rule.ToCharArray();
        for (var i = 0; i < ruleArray.Length; i++)
        {
            ruleArray[i] = char.IsDigit(ruleChars[i]) ? (int)char.GetNumericValue(ruleChars[i]) : 0;
            Debug.Log(ruleArray[i]);
        }
        return ruleArray;
    }
}
public class Cell {
    public bool IsLive;
    public int NeighborsCells = 0;
}
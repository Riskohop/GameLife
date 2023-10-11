using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRendererBorder;
    Texture2D textureGame;
    public Cell[,] boardCells { get; private set; }
    public float speedGame;
    public bool isGaming;
    float timer;
    public bool CreateGame(int x, int y) {
        if(x > 0 && y > 0) {
            boardCells = new Cell[x, y];
            textureGame = new Texture2D(boardCells.GetLength(0), boardCells.GetLength(1), TextureFormat.ARGB32, false);
            for(int xx = 0; xx < boardCells.GetLength(0); xx++) {
                for(int yy = 0; yy < boardCells.GetLength(1); yy++) {
                    boardCells[xx, yy] = new Cell();
                    textureGame.SetPixel(xx, yy, Color.white);
                    textureGame.Apply();
                }
            }

           

            textureGame.filterMode = FilterMode.Point;
            meshRendererBorder.material.mainTexture = textureGame;
            
            return true;
        }
        else {
            return false;
        }
    }
    public bool ClearGame() {
        if(boardCells != null) {
            isGaming = false;
            CreateGame(boardCells.GetLength(0), boardCells.GetLength(1));
            return true;
        } else {
            return false;
        }
    }
    public bool PauseGame() {
        if(boardCells != null) {
            isGaming = false;
            return true;
        } else {
            return false;
        }
    }
    public bool StartGame() {
        if(boardCells != null && !isGaming) {
            isGaming = true;
            return true;
        }
        else {
            return false;
        }
    }
    public void Draw(int x, int y, bool isPencil) {
        if(isPencil) {
            textureGame.SetPixel(x, y, Color.black);
            boardCells[x, y].isLive = true;
        } else {
            textureGame.SetPixel(x, y, Color.white);
            boardCells[x, y].isLive = false;
        }
        textureGame.Apply();
        meshRendererBorder.material.mainTexture = textureGame;
        
    }

    private void Update() {
        if(isGaming && speedGame < timer) {
            //CalculateNeighboors
            for(int x = 0; x < boardCells.GetLength(0); x++) {
                for(int y = 0; y < boardCells.GetLength(1); y++) {
                    boardCells[x, y].neighboorsCells = CalculateNeighboorsAtCell(x, y);
                }
            }
            //Turn
            for(int x = 0; x < boardCells.GetLength(0); x++) {
                for(int y = 0; y < boardCells.GetLength(1); y++) {
                    MakeTurn(x, y);
                    Color color = boardCells[x,y].isLive ? Color.black : Color.white;
                    textureGame.SetPixel(x, y, color);
                }
            }
            timer = 0;
            textureGame.Apply();
            meshRendererBorder.material.mainTexture = textureGame;
        }
        timer += Time.deltaTime;
    }
    private bool MakeTurn(int x, int y) {
        if(boardCells != null) {
            if(boardCells[x,y].isLive) {
                if(boardCells[x, y].neighboorsCells == 3 || boardCells[x, y].neighboorsCells == 2) { } else {
                    boardCells[x, y].isLive = false;
                }
            } else {
                if(boardCells[x, y].neighboorsCells == 3) {
                    boardCells[x, y].isLive = true;
                }
            }
            return true;
        } else {
            return false;
        }
    }
    private int CalculateNeighboorsAtCell(int x, int y) {
        int xCoordinate = x; int yCoordinate = y;
        int neighboors = 0;
        //x++
        if(xCoordinate == boardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //y++
        if(yCoordinate == boardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }
        
        xCoordinate = x; yCoordinate = y;
        //x--
        if(xCoordinate == 0) { xCoordinate = boardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //y--
        if(yCoordinate == 0) { yCoordinate = boardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //x++ y++
        if(xCoordinate == boardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(yCoordinate == boardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //x-- y--
        if(yCoordinate == 0) { yCoordinate = boardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(xCoordinate == 0) { xCoordinate = boardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //x++ y--
        if(xCoordinate == boardCells.GetLength(0) - 1) { xCoordinate = 0; } else { xCoordinate++; }
        if(yCoordinate == 0) { yCoordinate = boardCells.GetLength(1) - 1; } else { yCoordinate--; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        xCoordinate = x; yCoordinate = y;
        //x-- y++
        if(yCoordinate == boardCells.GetLength(1) - 1) { yCoordinate = 0; } else { yCoordinate++; }
        if(xCoordinate == 0) { xCoordinate = boardCells.GetLength(0) - 1; } else { xCoordinate--; }
        if(boardCells[xCoordinate, yCoordinate].isLive) { neighboors++; }

        return neighboors;
    }
}
public class Cell {
    public bool isLive;
    public int neighboorsCells = 0;
}
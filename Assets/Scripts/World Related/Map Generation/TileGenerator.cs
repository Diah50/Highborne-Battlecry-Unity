/* TileGenerator.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: DaynerKurdi, C137
 * Original: DaynerKurdi
 * 
 * Edited By: C137
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (DaynerKurdi)
 *      [01/08/2023] - Fixed spelling mistakes + Made "Grid" serializable (C137)
 *      [02/08/2023] - Use of new singleton system (C137)
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Grid
{
    /// <summary>
    /// The total number of cells on the positive X axis
    /// </summary>
    private int width;

    /// <summary>
    /// The total number of cells on the positive Y axis
    /// </summary>
    private int height;

    /// <summary>
    /// The size of each cell on the grid
    /// </summary>
    private float cellSize;

    /// <summary>
    /// Total cell count in the grid
    /// </summary>
    private int cellCount;

    /// <summary>
    /// For storing the cell index
    /// </summary>
    private Tile[,] cellArray;

    /// <summary>
    /// Used for moving the grid  
    /// </summary>
    private Vector3 gridOffset;

    /// <summary>
    /// Getter for Width
    /// </summary>
    public int Width { get { return width; } }

    /// <summary>
    /// Getter for Height
    /// </summary>
    public int Height { get { return height; } }

    /// <summary>
    /// Getter for total number of cells
    /// </summary>
    public int TotalCellCount { get { return cellCount; } }

    public Grid(int width, int height, float cellSize, Vector3 offset)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.gridOffset = offset;

        this.cellArray = new Tile[width, height];

        string output = "";

        int cellCount = 0;

        // y first
        for (int y = 0; y < this.cellArray.GetLength(1); y++) 
        {
            // x second
            for (int x = 0; x < this.cellArray.GetLength(0); x++)
            {
                if (x == 0)
                {
                    output = y.ToString();
                }

                output = output + "  " + x.ToString();

                //Debug.DrawLine(this.GetCellBorders(x, y), this.GetCellBorders(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(this.GetCellBorders(x, y), this.GetCellBorders(x + 1, y), Color.white, 100f);

                //For testing
                GameObject cell = new GameObject(cellCount.ToString());
                cell.transform.position = GetCellCenter(x,y);
                //Scaling the object so the sprite match the cell size. roughly 
                cell.transform.localScale = new Vector3(4, 4, 1);
            
                Tile tile = cell.AddComponent<Tile>();
                tile.SetupTile(BiomeType.Grass, 0, new Vector2Int(x, y));

                cellArray[x, y] = tile;
                //Assigning the sprite to the current tile
                tile.AssignSprite(SpriteLoader.singleton.tileGrassSpriteArray[0]);

                cellCount++;
            }

           // Debug.Log(output);
        }

        //Debug.DrawLine(this.GetCellBorders(0, this.width), this.GetCellBorders(this.width, this.height), Color.white, 100f);
        //Debug.DrawLine(this.GetCellBorders(this.width, 0), this.GetCellBorders(this.width, this.height), Color.white, 100f);

        this.cellCount = cellCount;
    }

    private Vector3 GetCellBorders(int x, int y)
    {
        Vector3 worldPos = new Vector3(x, y, 0) * cellSize;

        return worldPos + gridOffset;
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        return GetCellBorders(x,y) + new Vector3(cellSize, cellSize,0) * 0.5f;
    }

    public Tile[,] GetCellArray()
    {
        return cellArray;
    }
}

public class TileGenerator : MonoBehaviour
{
    /// <summary>
    /// The initial gird size
    /// </summary>
    [SerializeField]
    private Vector2Int gridSize = new Vector2Int();

    /// <summary>
    /// The size of each cell
    /// </summary>
    [SerializeField]
    private float cellSize = 5f;

    /// <summary>
    /// Used to move the grid
    /// </summary>
    [SerializeField]
    private Vector3 gridOffSet = Vector3.zero;

    /// <summary>
    /// The grid object
    /// </summary>
    public Grid grid;

    public Tile[,] tileArray;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(gridSize.x, gridSize.y, cellSize, gridOffSet );
        tileArray = new Tile[grid.Width,grid.Height];

        tileArray = grid.GetCellArray();
    }
}

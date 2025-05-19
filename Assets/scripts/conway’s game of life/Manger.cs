using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manger : MonoBehaviour
{
    private Image[,] cells;
    [SerializeField] private TableGenrator tableGenrator;

    private List<Cell> AliveCells;
    private List<Cell> DestroyCells;
    private List<Cell> BornCells;

    private bool play = false;

    private void Start()
    {
        AliveCells = new List<Cell>();
        DestroyCells = new List<Cell>();
        BornCells = new List<Cell>();

        cells = tableGenrator.GetTileData();
    }
    private void Update()
    {
        if (play)
        {
            NextIteration();
        }
    }

    public void LetsPlay() => play = !play;

    private void NextIteration()
    {
        for (int i = 0; i < AliveCells.Count; i++)
        {
            ChackStatues(AliveCells[i].location);
        }
        CleanCells();
        BornNewCells();
    }

    private void ChackStatues(int[] location)
    {
        int cellNear = 0;

        List<Cell> notYetBorn = new List<Cell>();

        for(int x = location[0] - 1; x <= location[0] + 1; x++)
        {
            if (x < 0 || x >= cells.GetLength(0))
                continue;

            for(int y = location[1] - 1; y <= location[1] + 1; y++)
            {
                if (y < 0 || y >= cells.GetLength(1))
                {
                    continue;
                }
                 

                if (x == location[0] && y == location[1]) { continue; }
               

                if (cells[x, y].GetComponent<Cell>().GetState())
                {
                    cellNear++;
                }
                else 
                {
                    if(!notYetBorn.Contains(cells[x, y].GetComponent<Cell>()))
                    {
                        notYetBorn.Add(cells[x, y].GetComponent<Cell>());
                    }
                }
            }
        }

        if (cellNear < 2 || cellNear > 3)
        {
            DestroyCells.Add(cells[location[0], location[1]].GetComponent<Cell>());
        }

        cellNear = 0;

        for(int j = 0; j < notYetBorn.Count; j++)
        {
            for(int x = notYetBorn[j].location[0] - 1; x <= notYetBorn[j].location[0] + 1; x++)
            {
                if (x < 0 || x >= cells.GetLength(0))
                    continue;

                for (int y = notYetBorn[j].location[1] - 1; y <= notYetBorn[j].location[1] + 1; y++)
                {
                    if (y < 0 || y >= cells.GetLength(1))
                        continue;

                    if (cells[x, y].GetComponent<Cell>().GetState())
                    {
                        if (notYetBorn[j].location[0] == x && notYetBorn[j].location[0] == y)
                            continue;
               
                        cellNear++; 
                    }
                }
            }

            if (cellNear == 3)
            {
                BornCells.Add(notYetBorn[j]);
            }
            cellNear = 0;
        }
        notYetBorn.Clear();
        

    }
    private void CleanCells()
    {
        foreach(Cell cell in DestroyCells)
        {
            cell.ChangeState();
        }
        DestroyCells.Clear();
    }

    private void BornNewCells()
    {
        foreach (Cell cell in BornCells)
        {
            cell.ChangeState();
        }
        BornCells.Clear();
    }

    public void AddCell(Cell cell)
    {
        AliveCells.Add(cell);
    }

    public void RemoveCell(Cell cell)
    {
        AliveCells.Remove(cell);
    }

}

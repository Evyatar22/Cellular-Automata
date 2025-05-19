using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrianManger : MonoBehaviour
{
    private Image[,] cells;
    [SerializeField] private TableGenrator tableGenrator;

    private List<Neuron> AliveCells;
    private List<Neuron> DestroyCells;
    private List<Neuron> NewNeurons;

    private bool play = false;

    private void Start()
    {
        AliveCells = new List<Neuron>();
        NewNeurons = new List<Neuron>();

        cells = tableGenrator.GetTileData();
    }
    private void FixedUpdate()
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

        for (int i = 0; i < AliveCells.Count; i++)
        {
            AliveCells[i].ChangeState();
        }
        BornNewCells();

    }

    private void ChackStatues(int[] location)
    {
        int cellNear = 0;

        List<Neuron> deadNeurons = new List<Neuron>();

        for (int x = location[0] - 1; x <= location[0] + 1; x++)
        {
            if (x < 0 || x >= cells.GetLength(0))
                continue;

            for (int y = location[1] - 1; y <= location[1] + 1; y++)
            {
                if (y < 0 || y >= cells.GetLength(1)) { continue; }

                Neuron tmp = cells[x, y].GetComponent<Neuron>();
                if (tmp.GetState() == 0)
                {
                    if (!deadNeurons.Contains(tmp))
                    {
                        deadNeurons.Add(tmp);
                    }
                }
            }
        }

        for (int j = 0; j < deadNeurons.Count; j++)
        {
            for (int x = deadNeurons[j].location[0] - 1; x <= deadNeurons[j].location[0] + 1; x++)
            {
                if (x < 0 || x >= cells.GetLength(0))
                    continue;

                for (int y = deadNeurons[j].location[1] - 1; y <= deadNeurons[j].location[1] + 1; y++)
                {
                    if (y < 0 || y >= cells.GetLength(1))
                        continue;

                    if (cells[x, y].GetComponent<Neuron>().GetState() == 1)
                    {
                        cellNear++;
                    }
                }
            }

            if (cellNear == 2 && !NewNeurons.Contains(deadNeurons[j]))
            {
                NewNeurons.Add(deadNeurons[j]);
            }
            cellNear = 0;
        }
        deadNeurons.Clear();

    }

    private void BornNewCells()
    {
        foreach (Neuron cell in NewNeurons)
        {
            cell.ChangeState();
        }
        NewNeurons.Clear();
    }

    public void AddCell(Neuron cell)
    {
        AliveCells.Add(cell);
    }

    public void RemoveCell(Neuron cell)
    {
        AliveCells.Remove(cell);
    }
}

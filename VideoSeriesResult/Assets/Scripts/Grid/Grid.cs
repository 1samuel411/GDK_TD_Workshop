using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{

    public static Grid instance;

    private Cell[] cells;
    private Cell[] lastSelectedCells;

    public Cell[] selectedCells;
    public int radius;

    private void Awake()
    {
        instance = this;
        cells = GetComponentsInChildren<Cell>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(BuildManager.instance.editMode == false)
        {
            // editmode
            for(int i = 0; i < cells.Length; i++)
            {
                cells[i].gameObject.SetActive(false);
            }
            return;
        }

        selectedCells = GetClosestCells(Input.mousePosition, radius);

        if(CompareCells(selectedCells, lastSelectedCells) == false)
        {
            lastSelectedCells = selectedCells;
            AudioManager.instance.PlayClip(AudioManager.Effects.quiet, "CellChange");
        }

        for(int i = 0; i < cells.Length; i++)
        {
            cells[i].gameObject.SetActive(true);

            if (cells[i].status == Cell.Status.used)
                continue;

            if(selectedCells.Contains(cells[i]) == true)
            {
                cells[i].status = Cell.Status.selected;
            }
            else
            {
                cells[i].status = Cell.Status.unselected;
            }
        }
    }

    Cell[] GetClosestCells(Vector3 cellPosition, int radius)
    {
        Cell curCell = GetClosestCell(Input.mousePosition);
        List<Cell> closestCellsFound = new List<Cell>();

        for(int x = 0; x < radius; x++)
        {
            for(int y = 0; y < radius; y++)
            {
                Cell newCell = (GetClosestCell(Camera.main.WorldToScreenPoint(curCell.transform.position + (Vector3.right * x) + (Vector3.forward * y))));
                if (closestCellsFound.Contains(newCell) == false && newCell.transform.parent == curCell.transform.parent)
                    closestCellsFound.Add(newCell);
            }
        }

        return closestCellsFound.ToArray();
    }

    Cell GetClosestCell(Vector3 screenPosition)
    {
        float closestMagnitude = float.MaxValue;
        Cell closestCellFound = null;

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].status == Cell.Status.used)
                continue;

            float cellMagnitude = (screenPosition - Camera.main.WorldToScreenPoint(cells[i].transform.position)).magnitude;

            if (cellMagnitude < closestMagnitude)
            {
                // found a new closest cell
                closestMagnitude = cellMagnitude;
                closestCellFound = cells[i];
            }
        }

        return closestCellFound;
    }

    public void MarkUsed(Cell[] cell)
    {
        for(int i = 0; i < cell.Length; i++)
            cell[i].status = Cell.Status.used;
    }

    public void UnmarkUsed(Cell[] cell)
    {
        for(int i = 0; i < cell.Length; i++)
            cell[i].status = Cell.Status.unselected;
    }

    public bool CompareCells(Cell[] a, Cell[] b)
    {
        if (a == null || b == null)
            return false;

        Vector3 positionsAddedA = Vector3.zero;
        for (int i = 0; i < a.Length; i++)
        {
            positionsAddedA += a[i].transform.position;
        }

        Vector3 positionsAddedB = Vector3.zero;
        for (int i = 0; i < b.Length; i++)
        {
            positionsAddedB += b[i].transform.position;
        }

        return positionsAddedA.magnitude == positionsAddedB.magnitude;
    }
}

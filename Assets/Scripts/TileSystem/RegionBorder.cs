using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class RegionBorder : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Color defaultColor;
    private Shader defaultShader;
    private bool isHilited;
    public void AssignVertices(List<TerrainCell> _regionCells, CellType _cellType)
    {
        List<Vector3> vertex = new BorderMetrics().GetRegionBorder(_regionCells);
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = vertex.Count;
        _lineRenderer.SetPositions(vertex.ToArray());

        RegionPalette palette = Resources.Load<RegionPalette>("Palettes/RegionPalette");
        defaultColor = palette.GetColor(_cellType.climate);
        defaultShader = palette.GetShader(_cellType.move);
        SetDefaulColor();
    }


    private void SetDefaulColor()
    {
        _lineRenderer.startColor = defaultColor;
        _lineRenderer.endColor = defaultColor;
        _lineRenderer.material.shader = defaultShader;
    }
    public void ShowBorders()
    {
        if (!isHilited)
        {
            _lineRenderer.enabled = true;
        }
    }

    public void HideBorders()
    {
        if (!isHilited)
        {
            _lineRenderer.enabled = false;
        }
    }

    public void HiliteBorders(Color col)
    {
        isHilited = true;
        _lineRenderer.enabled = true;
        _lineRenderer.startColor = col;
        _lineRenderer.endColor = col;
    }

    public void EndHiliteBorders()
    {
        SetDefaulColor();
        isHilited = false;
        _lineRenderer.enabled = false;
    }
}

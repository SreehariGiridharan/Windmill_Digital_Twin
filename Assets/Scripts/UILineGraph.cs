using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;




public class UILineGraph : MaskableGraphic
{
    public List<float> xPositions = new List<float>();
    public List<float> timeStamps = new List<float>();

    public List<float> data = new List<float>();
    public float maxValue = 1f;
    public int maxPoints = 100;
    public float lineThickness = 2f;
    public Color axisColor = Color.gray;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        if (data.Count < 2) return;

        float xStep = width / (maxPoints - 1);

        xPositions.Clear();
        timeStamps.Clear();


        // Draw line graph
        for (int i = 0; i < data.Count - 1; i++)
        {
            float y1 = Mathf.Clamp01(data[i] / maxValue) * height;
            float y2 = Mathf.Clamp01(data[i + 1] / maxValue) * height;

            float x1 = i * xStep;
            xPositions.Add(x1);
            timeStamps.Add(i * 0.5f); // or use a real clock
            Vector2 p1 = new Vector2(x1, y1);
            Vector2 p2 = new Vector2((i + 1) * xStep, y2);

            DrawLine(vh, p1, p2, lineThickness, color);
        }

        // Draw X-axis (horizontal line at y=0)
        DrawLine(vh, new Vector2(0, 0), new Vector2(width, 0), 1f, axisColor);

        // Draw Y-axis (vertical line at x=0)
        DrawLine(vh, new Vector2(0, 0), new Vector2(0, height), 1f, axisColor);
    }

    void DrawLine(VertexHelper vh, Vector2 start, Vector2 end, float thickness, Color col)
    {
        Vector2 dir = (end - start).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * thickness * 0.5f;

        UIVertex[] verts = new UIVertex[4];
        verts[0] = GetVertex(start - normal, col);
        verts[1] = GetVertex(start + normal, col);
        verts[2] = GetVertex(end + normal, col);
        verts[3] = GetVertex(end - normal, col);

        vh.AddUIVertexQuad(verts);
    }

    UIVertex GetVertex(Vector2 point, Color col)
    {
        UIVertex v = UIVertex.simpleVert;
        v.color = col;
        v.position = point;
        return v;
    }

    public void AddPoint(float value)
    {
        data.Add(value);
        if (data.Count > maxPoints)
            data.RemoveAt(0);

        SetVerticesDirty();
    }
}

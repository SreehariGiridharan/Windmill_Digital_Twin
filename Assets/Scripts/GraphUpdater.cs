using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphUpdater : MonoBehaviour
{
    [Header("References")]
    public UILineGraph graph;
    public RectTransform xAxisLabel;
    public RectTransform yAxisLabel;
    public RectTransform graphRect;

    [Header("Prefabs")]
    public GameObject xLabelPrefab;
    public GameObject yLabelPrefab;
    public GameObject verticalLinePrefab;

    [Header("Graph Settings")]
    public float interval = 0.5f;
    public float minTemp = 25f;
    public float maxTemp = 45f;
    public int yTicks = 5;
    public int maxPoints = 100;

    private float currentTemperature;
    private float elapsedTime = 0f;

    private List<GameObject> xLabels = new List<GameObject>();
    private List<GameObject> gridLines = new List<GameObject>();

    void Start()
    {
        currentTemperature = Random.Range(25f, 30f);
        graph.maxValue = maxTemp;
        graph.maxPoints = maxPoints;

        GenerateYTicks();
        InvokeRepeating(nameof(AddTemperaturePoint), 0f, interval);
    }

    void AddTemperaturePoint()
    {
        float delta = Random.Range(-2f, 2f);
        currentTemperature = Mathf.Clamp(currentTemperature + delta, minTemp, maxTemp);
        graph.AddPoint(currentTemperature);

        float xStep = graphRect.rect.width / (maxPoints - 1);
        float x = (graph.data.Count - 1) * xStep;

        // Time label
        GameObject label = Instantiate(xLabelPrefab, xAxisLabel);
        label.GetComponent<Text>().text = System.TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss");
        RectTransform rt = label.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, -10f);
        xLabels.Add(label);

        // Grid line
        GameObject grid = Instantiate(verticalLinePrefab, graphRect);
        RectTransform gridRt = grid.GetComponent<RectTransform>();
        gridRt.anchoredPosition = new Vector2(x, 0);
        gridRt.sizeDelta = new Vector2(1f, graphRect.rect.height);
        gridLines.Add(grid);

        // Cleanup
        if (xLabels.Count > maxPoints)
        {
            Destroy(xLabels[0]);
            xLabels.RemoveAt(0);
        }
        if (gridLines.Count > maxPoints)
        {
            Destroy(gridLines[0]);
            gridLines.RemoveAt(0);
        }

        elapsedTime += interval;
    }

    void GenerateYTicks()
    {
        float height = graphRect.rect.height;

        for (int i = 0; i <= yTicks; i++)
        {
            float t = Mathf.Lerp(minTemp, maxTemp, i / (float)yTicks);
            float y = height * (i / (float)yTicks);

            GameObject label = Instantiate(yLabelPrefab, yAxisLabel);
            RectTransform rt = label.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, y);
            label.GetComponent<Text>().text = $"{t:F1}°C";
        }
    }
}

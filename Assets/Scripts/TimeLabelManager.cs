using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeLabelManager : MonoBehaviour
{
    public UILineGraph graph;
    public GameObject labelPrefab;
    public RectTransform labelParent;
    public float yOffset = -15f; // how far below the graph to place the labels

    private List<GameObject> labels = new List<GameObject>();

    void Update()
    {
        SyncLabels();
    }

    void SyncLabels()
    {
        int count = graph.xPositions.Count;

        // Create new labels if needed
        while (labels.Count < count)
        {
            GameObject newLabel = Instantiate(labelPrefab, labelParent);
            labels.Add(newLabel);
        }

        // Update label text and positions
        for (int i = 0; i < labels.Count; i++)
        {
            if (i >= graph.xPositions.Count) break;

            float x = graph.xPositions[i];
            float t = graph.timeStamps[i];

            string timestamp = System.TimeSpan.FromSeconds(t).ToString(@"mm\:ss");

            RectTransform labelRect = labels[i].GetComponent<RectTransform>();
            labelRect.anchoredPosition = new Vector2(x, yOffset);

            Text txt = labels[i].GetComponent<Text>();
            txt.text = timestamp;
        }

        // Remove extra labels if graph has fewer points
        while (labels.Count > graph.xPositions.Count)
        {
            Destroy(labels[^1]);
            labels.RemoveAt(labels.Count - 1);
        }
    }
}

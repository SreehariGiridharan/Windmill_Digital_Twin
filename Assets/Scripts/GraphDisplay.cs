using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphDisplay : MonoBehaviour
{
    public RectTransform graphContainer;
    public GameObject dotPrefab;
    public Sprite dotSprite;

    private void Start()
    {
        List<float> values = new List<float>() { 5, 10, 7, 15, 20, 12, 18 };
        ShowGraph(values);
    }

    private void ShowGraph(List<float> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 20f; // Max value in graph
        float xSize = 50f;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPos = i * xSize;
            float yPos = (valueList[i] / yMax) * graphHeight;
            CreateCircle(new Vector2(xPos, yPos));
        }
    }

    private void CreateCircle(Vector2 anchoredPos)
    {
        GameObject go = new GameObject("dot", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().sprite = dotSprite;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = new Vector2(11, 11);
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
    }
}

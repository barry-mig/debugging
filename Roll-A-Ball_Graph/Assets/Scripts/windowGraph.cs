using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class windowGraph : MonoBehaviour
{
    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;// takes in the circle sprite
    private void Awake()// awake intializes variables before the application starts
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        List<int> valueList = new List<int>() { 5, 500, 240, 75, 30, 80, 120, 172, 100, 200, 250, 400, 350, 300, 250 };
        showGraph(valueList);
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    //Takes in a list of values and creates the dots on the graph
    private void showGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;// grabs the size of the graphContainer
        float yMaximum = 100f;
        float xSize = 50f;
        GameObject lastCircleGameObject = null;
        //for every value in the list, it creates a circle at that value and creates a connection between the values
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            //prevents a connection from being create for the first and the last point
            if (lastCircleGameObject != null)
            {
                createDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }

    }
    //creates the connections between the points on the graph, basically creates a rectangle between 2 points
    private void createDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //some math that I dont really understand to get the proper rotation and length for the connections on the graph
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));


    }
    
}

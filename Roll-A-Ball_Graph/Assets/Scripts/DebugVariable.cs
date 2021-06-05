using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class DebugVariable : MonoBehaviour
{
    public GameObject window;
    Rect windowRect = new Rect(20, 20, 120, 50);
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private float lastCircleX = 10f;
    private float xSize = 0.2f; //10f
    private float yMaximum = 10f;
    private float yMinimum = -10f;

    private GameObject lastCircleGameObject = null;

    [SerializeField] private Sprite circleSprite;// takes in the circle sprite

    private void Awake()// awake intializes variables before the application starts
    {
        graphContainer = window.transform.Find("graphContainer").GetComponent<RectTransform>();
        //labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        List<int> valueList = new List<int>() { 5, 500, 240, 75, 30, 80, 120, 172, 100, 200, 250, 400, 350, 300, 250 };
        showGraph(valueList);
    }

    // Start is called before the first frame update
    // Update is called once per frame
    public void printVariable(float num)
    {
        //Debug.Log(num);
    }

    public void AddPoint(float num)
    {
        float graphHeight = graphContainer.sizeDelta.y;// grabs the size of the graphContainer
        Debug.Log(graphHeight);
        //float xSize = 10f; //size distance between each point on the x axis
        //float yMaximum = 20f;
        float xPosition = lastCircleX + xSize;
        float yPosition = ((num - yMinimum) / (yMaximum - yMinimum)) * graphHeight; //if you receive yMaximum value it will be located exactly at the graphHeight

        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
        if (lastCircleGameObject != null)
        {
            createDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
        }
        lastCircleGameObject = circleGameObject;
        lastCircleX = xPosition;
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0); //anchor to the lower left corner
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    //Takes in a list of values and creates the dots on the graph
    private void showGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;// grabs the size of the graphContainer
        //GameObject lastCircleGameObject = null;
        //for every value in the list, it creates a circle at that value and creates a connection between the values
        /*
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight; //if you receive yMaximum value it will be located exactly at the graphHeight
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            //prevents a connection from being create for the first and the last point (will be null when i is 0)
            if (lastCircleGameObject != null)
            {
                createDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX.GetComponent<Text>().text = i.ToString();
        }
        */
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt((yMinimum + normalizedValue * (yMaximum - yMinimum))).ToString();
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
        rectTransform.anchorMin = new Vector2(0, 0); //set up the anchor on the lower left corner
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir)); //rotation: converts vector to angle between 0 and 360

    }
}

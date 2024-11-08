using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BGPointsRandomControl : MonoBehaviour
{
    [SerializeField]
    private float boundMinX = -9;
    [SerializeField]
    private float boundMinY = -5;
    [SerializeField]
    private float boundMaxX = 9;
    [SerializeField]
    private float boundMaxY = 5;
    [SerializeField]
    private float moveStep = 0.05f;
    [SerializeField]
    private float randomFactor = 0.5f; // to increase random range
    [SerializeField]
    private float diffDistance = 0.9f;

    private Transform[] childs;
    private Vector3[] targetPositions;

    void Start()
    {
        childs = new Transform[transform.childCount];
        targetPositions = new Vector3[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            targetPositions[i] = GetRandomPosition();
        }
    }

    

    private void Update()
    {
        for(int i = 0; i < childs.Length; i ++)
        {
            Transform t = childs[i];

            //t.position += Vector3.one * Random.Range(-moveStep, moveStep);

            
            t.position = Vector2.Lerp(t.position, targetPositions[i], moveStep * Time.deltaTime);
           
            if (Vector2.Distance(t.position, targetPositions[i]) < diffDistance)
            {
                targetPositions[i] = GetRandomPosition();
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(boundMinX, boundMaxX);
        float randomY = Random.Range(boundMinY, boundMaxY);
        
        float sineOffsetX = Mathf.Sin(Time.time) * randomFactor;
        float sineOffsetY = Mathf.Cos(Time.time) * randomFactor;
        return new Vector2(randomX + sineOffsetX, randomY + sineOffsetY);
    }
}

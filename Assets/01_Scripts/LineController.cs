using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    private List<Vector2> points;

    private Vector3 initPosition;

    private int pointCount
    {
        get
        {
            return lineRenderer.positionCount;
        }
        set
        {
            lineRenderer.positionCount = value;
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     AddPoint();
        // }
    }

    public void Reset()
    {
        points.Clear();
        
        pointCount = 1;
        lineRenderer.SetPosition(pointCount - 1, Vector3.zero);
        for (int i = 0; i < 100; i++)
        {
            AddPoint();
        }
        lineRenderer.enabled = true;
        
        edgeCollider.points = new Vector2[0]; 
    }
    
    public void AddPoint()
    {
        Vector3 newPoint = GetNewPoint();
        
        pointCount++;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        // 콜라이더 적용
        points.Add(newPoint);
        edgeCollider.SetPoints(points);
    }

    private Vector3 GetNewPoint()
    {
        Vector3 lastPoint = lineRenderer.GetPosition(pointCount - 1);

        float pointPositionX = Random.Range(2f, 10f) / 4;

        float pointPositionY = 0f;
        if (lastPoint.y < -3f)
        {
            pointPositionY = Random.Range(2f, 8f) / 4;
        }
        else if (lastPoint.y > 3f)
        {
            pointPositionY = Random.Range(-2f, -8f) / 4;
        }
        else
        {
            pointPositionY = Random.Range(-1f, 1f);
        }

        Vector3 newPoint = lastPoint + new Vector3(pointPositionX, pointPositionY, 0);
        return newPoint;
    }

}

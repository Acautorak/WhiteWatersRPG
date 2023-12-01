using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapper : MonoBehaviour
{
    public float cellSize;
    void Start()
    {
        Vector3 currentPosition = transform.position;
        cellSize = 5.5f;
        float snappedX = Mathf.Round(currentPosition.x / cellSize) * cellSize;
        float snappedY = Mathf.Round(currentPosition.y / cellSize) * cellSize;
        Vector3 snappedPosition = new Vector3(snappedX, snappedY, currentPosition.z);

        transform.position = snappedPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

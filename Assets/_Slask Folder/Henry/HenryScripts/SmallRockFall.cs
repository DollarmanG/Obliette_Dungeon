//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class SmallRockFall : MonoBehaviour
{
    // Reference to rock prefab
    [SerializeField]
    private GameObject rock;

    // Reference to rockfall empty parent transform
    [SerializeField]
    private Transform rockfallTransform;

    // Reference to rockfall body parent transform

    [SerializeField]
    private Transform rockfallBodyTransform;

    Vector3 rockfallPosition;

    private float randomXPosition;
    private float randomZPosition;

    float minXPosition;
    float maxXPosition;

    float maxYPosition;
    float minYPosition;

    [SerializeField]
    private float delay;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        rockfallPosition = rockfallTransform.position;

        minXPosition = -(rockfallBodyTransform.localScale.x / 2);
        maxXPosition = rockfallBodyTransform.localScale.x / 2;

        maxYPosition = rockfallBodyTransform.localScale.y / 2;
        minYPosition = -(rockfallBodyTransform.localScale.y / 2);

        randomXPosition = Random.Range(minXPosition, maxXPosition);
        randomZPosition = Random.Range(minYPosition, maxYPosition);

        Debug.Log($"MinXPosition = {minXPosition}");
        Debug.Log($"MaxXPosition = {maxXPosition}");

        Debug.Log($"MinYPosition = {minYPosition}");
        Debug.Log($"MaxYPosition = {maxYPosition}");

        Debug.Log($"Random X Position = {randomXPosition}");
        Debug.Log($"Random Z Position = {randomZPosition}");

        instantiateSmallRockFall();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void instantiateSmallRockFall()
    {
        GameObject rockClone = Instantiate(rock, rockfallPosition + new Vector3(randomXPosition, -0.5f, randomZPosition), Quaternion.Euler(0.0f, 0.0f, 90.0f), rockfallTransform);
        rockClone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}

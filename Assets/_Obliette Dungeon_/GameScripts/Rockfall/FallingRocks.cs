//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
using System.Collections;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    // Reference to rock prefab
    [SerializeField]
    private GameObject rock;

    // Reference to rockfall empty parent transform. Setting this to parent
    // prevents the scale of the instantiated rocks from being skewed.
    [SerializeField]
    private Transform rockfallTransform;

    // Variable to refer to the position of the rockfall empty parent transform.
    Vector3 rockfallPosition;

    // Reference to rockfall body parent transform
    [SerializeField]
    private Transform rockfallBodyTransform;

    // Variables used to set bounds for instantiated prefabs based on scaling of rockfall body.
    float minXPosition, maxXPosition;

    float maxYPosition, minYPosition;

    // Variables to store random x and y axis positions for instantiated prefabs.
    float randomXPosition, randomZPosition;

    // Variables used to set scale for instantiated first set of instantiated prefabs.
    [SerializeField]
    float smallRocksMinXScale, smallRocksMaxXScale, smallRocksMinYScale, smallRocksMaxYScale, smallRocksMinZScale, smallRocksMaxZScale;

    // Variables used to set scale for instantiated second set of instantiated prefabs.
    [SerializeField]
    float largeRocksMinXScale, largeRocksMaxXScale, largeRocksMinYScale, largeRocksMaxYScale, largeRocksMinZScale, largeRocksMaxZScale;

    // Variables to store random x, y and z axis scale for first set of instantiated prefabs.
    float smallRandomXScale, smallRandomYScale, smallRandomZScale;
    
    // Variables to store random x, y and z axis scale for second set of instantiated prefabs.
    float largeRandomXScale, largeRandomYScale, largeRandomZScale;

    // Coroutine variable that starts rockfall
    private IEnumerator coroutine;

    // Max number of small rocks
    [SerializeField]
    private int maxNumberOfSmallRocks = 5;

    // Max number of large rocks
    [SerializeField]
    private int maxNumberOfLargeRocks = 5;

    // Time in between generating small rocks.
    [SerializeField]
    private float timeBetweenInstantiatingSmallRocks = 0.2f;
    
    // Time in between generating large rocks.
    [SerializeField]
    private float timeBetweenInstantiatingLargeRocks = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Abbreviated reference to the position of the empty rockfall parent object.
        rockfallPosition = rockfallTransform.position;
    }

    public void StartSmallRockFall()
    {
        // Set coroutine variable equal to desired version of rockfall.
        coroutine = InstantiateSmallRockFall(timeBetweenInstantiatingSmallRocks);

        StartCoroutine(coroutine);
    }
    
    public void StartLargeRockFall()
    {
        // Set coroutine variable equal to desired version of rockfall.
        coroutine = InstantiateLargeRockFall(timeBetweenInstantiatingLargeRocks);

        StartCoroutine(coroutine);
    }

    private IEnumerator InstantiateSmallRockFall(float waitTime)
    {
        for (int i = 0; i < maxNumberOfSmallRocks; i++)
        {
            // Set min/max ranges for instantiated rock position
            minXPosition = -(rockfallBodyTransform.localScale.x / 2);
            maxXPosition = rockfallBodyTransform.localScale.x / 2;

            minYPosition = -(rockfallBodyTransform.localScale.y / 2);
            maxYPosition = rockfallBodyTransform.localScale.y / 2;

            // Set instantiated rock to random position
            randomXPosition = Random.Range(minXPosition, maxXPosition);
            randomZPosition = Random.Range(minYPosition, maxYPosition);

            // Set instantiated rock to random scale based on user defined bounds.
            smallRandomXScale = Random.Range(smallRocksMinXScale, smallRocksMaxXScale);
            smallRandomYScale = Random.Range(smallRocksMinYScale, smallRocksMaxYScale);
            smallRandomZScale = Random.Range(smallRocksMinZScale, smallRocksMaxZScale);


            GameObject rockClone = Instantiate(rock, rockfallPosition + new Vector3(randomXPosition, -0.5f, randomZPosition), Quaternion.identity, rockfallTransform);
            rockClone.transform.localScale = new Vector3(smallRandomXScale, smallRandomYScale, smallRandomZScale);

            

            yield return new WaitForSeconds(waitTime);
        }

        StopCoroutine(coroutine);
    }
    
    private IEnumerator InstantiateLargeRockFall(float waitTime)
    {
        for (int i = 0; i < maxNumberOfLargeRocks; i++)
        {
            // Set min/max ranges for instantiated rock position
            minXPosition = -(rockfallBodyTransform.localScale.x / 2);
            maxXPosition = rockfallBodyTransform.localScale.x / 2;

            minYPosition = -(rockfallBodyTransform.localScale.y / 2);
            maxYPosition = rockfallBodyTransform.localScale.y / 2;

            // Set instantiated rock to random position
            randomXPosition = Random.Range(minXPosition, maxXPosition);
            randomZPosition = Random.Range(minYPosition, maxYPosition);

            // Set instantiated rock to random scale based on user defined bounds.
            largeRandomXScale = Random.Range(largeRocksMinXScale, largeRocksMaxXScale);
            largeRandomYScale = Random.Range(largeRocksMinYScale, largeRocksMaxYScale);
            largeRandomZScale = Random.Range(largeRocksMinZScale, largeRocksMaxZScale);


            GameObject rockClone = Instantiate(rock, rockfallPosition + new Vector3(randomXPosition, -0.5f, randomZPosition), Quaternion.identity, rockfallTransform);
            rockClone.transform.localScale = new Vector3(largeRandomXScale, largeRandomYScale, largeRandomZScale);

            yield return new WaitForSeconds(waitTime);
        }

        StopCoroutine(coroutine);
    }
}

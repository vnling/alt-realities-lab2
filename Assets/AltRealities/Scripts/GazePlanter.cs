using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePlanter : MonoBehaviour
{
    public int amountToPool = 30;
    public Transform poolObjectsHolder;
    public GameObject prefab;

    private List<GameObject> poolObjects;
    private const float _maxDistance = 10;
    private Vector3 lastPlantPosition;
    private int currentPlantIndex = 0;

    void Start()
    {
        // 1. create a pool of mushroom to use
        poolObjects = new List<GameObject>();
        GameObject tmpObject;

        for (int i = 0; i < amountToPool; i++)
        {
            tmpObject = Instantiate(prefab, poolObjectsHolder);
            tmpObject.SetActive(false);
            tmpObject.transform.Translate(0, -100f, 0);
            // Save to the list
            poolObjects.Add(tmpObject);
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            Vector3 offset = lastPlantPosition - hit.point;
            // sqrMagnitude is faster than using Vector3.magnitude
            float sqrLen = offset.sqrMagnitude;
            // When distance is big enough, show the worm!
            if (sqrLen > 1)
            {
                GameObject currentOne = poolObjects[currentPlantIndex % poolObjects.Count];
                currentOne.transform.position = hit.point;
                currentOne.SetActive(true);
                currentPlantIndex++;

                lastPlantPosition = hit.point;
            }
        }
    }
}

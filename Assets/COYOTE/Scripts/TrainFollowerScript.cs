using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrainFollowerScript : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}

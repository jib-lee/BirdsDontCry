using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resist_SetUp : MonoBehaviour
{
    public GameObject enemy;
    public GameObject character;
    public float distToEnemy;

    void Start()
    {

    }

    void Update()
    {
        distToEnemy = Vector3.Distance(character.transform.position, enemy.transform.position);
        //Debug.Log(distToEnemy);
    }

    //defining a function with a return of a value
    float map(float dist, float a1, float a2, float b1, float b2)
    {
        return b1 + (dist - a1) * (b2 - b1) / (a2 - a1);
    }
}

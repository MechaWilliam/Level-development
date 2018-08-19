using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enenmycontroller : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }
    //Variables managing enemy waypoint system
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public float targetThreshold = 1.5f;
    private int currentWaypointIndex = 0;
    private Transform target;

    //Variables managing enemy chase
    private GameObject player;
    public float searchRadius = 2;
    public float searchAngle = 60;

    private EnemyState state = EnemyState.Patrol;

    void Start()
    {
        //Locate the player inside the game scene
        player = GameObject.Find("Player");
        //Setup initial target based on the first waypoint
        target = waypoints[0];
    }

    void Update()
    {
        //If the player is within the search radius and search angle, chase them
        if ((transform.position - player.transform.position).magnitude <= searchRadius && WithinViewRange())
            state = EnemyState.Chase;
        //If not, patrol around the set waypoints
        else
            state = EnemyState.Patrol;

        //Run state functions
        if (state == EnemyState.Chase)
            RunChase();
        else if (state == EnemyState.Patrol)
            RunPatrol();
    }

    void RunChase()
    {
        //Assign player position as enemy destination if chasing
        agent.SetDestination(player.transform.position);
    }

    void RunPatrol()
    {
        //Do not patrol if there are less than 2 waypoints
        if (waypoints.Length < 2)
            return;

        agent.SetDestination(target.position);
        //If the enemy has reached the current waypoint, iterate to the next waypoint
        if ((transform.position - target.position).magnitude <= targetThreshold)
        {
            UpdateWaypoint();
        }
    }

    void UpdateWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        target = waypoints[currentWaypointIndex];
    }

    bool WithinViewRange()
    {
        //Use dot product to determine if the player is currently within the enemys vision, specified by searchAngle

        //First normalise the enemys heading vector, and the vector pointing from the enemy to the player
        Vector3 normalisedHeading = transform.forward.normalized;
        Vector3 normalisedPlayerVector = (player.transform.position - transform.position).normalized;
        //Determine the dotproduct of the two normalised vectors
        float dotProduct = Vector3.Dot(normalisedHeading, normalisedPlayerVector);
        //Convert the dot product into an angle in degrees
        float angleToPlayer = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        if (angleToPlayer > searchAngle)
            return false;
        else
            return true;
    }
}

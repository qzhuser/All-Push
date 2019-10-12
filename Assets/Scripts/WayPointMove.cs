using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointMove : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    public Transform[] waypoints;
    int m_wayPointsIndex;
    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = transform.GetComponent<NavMeshAgent>();
        navmeshagent.SetDestination(waypoints[0].position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (navmeshagent.remainingDistance < navmeshagent.stoppingDistance) {
            //下标到达最大值自动返回0，然后继续循环，不用if循环
            m_wayPointsIndex = (m_wayPointsIndex + 1) % waypoints.Length;
            navmeshagent.SetDestination(waypoints[m_wayPointsIndex].position);
        }
    }
}

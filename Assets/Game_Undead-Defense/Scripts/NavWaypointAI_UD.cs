using UnityEngine;
using System.Collections;
using UnityEngine.AI;
 
[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class NavWaypointAI_UD : MonoBehaviour {

    public float m_speed = 2.0f;
    public Vector2 m_wpOffset = Vector2.zero;
    public float m_minWaypointDistance = 0.5f;
    public Transform m_wpContainer;
    public Vector3 Velocity { get {return nav.velocity; }}
    public bool IsChasing { get {return m_chasingPlayer; }}
 
    private Transform[] m_waypoints;
    private NavMeshAgent nav;
    private int m_curWaypoint = 0;
    private int m_lastWP;
    private bool m_chasingPlayer = false;
    private bool m_wpReached = false;
    private Vector3 m_targetPos;
 
    private void Awake () {
        nav = GetComponent<NavMeshAgent>();
        nav.autoTraverseOffMeshLink = true;
    }

    private void Start() {
        nav.speed = m_speed;
    }

    public void Move() {
        if (m_wpReached) {
           MoveToWP();
        }
        CheckDistanceToWP();
    }
 
    public void MoveToWP() {
        nav.isStopped = false;
        Vector3 tempWaypointPosition;
        tempWaypointPosition = m_waypoints[m_curWaypoint].position;
        tempWaypointPosition.x += Random.Range(-m_wpOffset.x,m_wpOffset.x);
        tempWaypointPosition.z += Random.Range(-m_wpOffset.y,m_wpOffset.y);
        m_targetPos = tempWaypointPosition;
        nav.SetDestination(m_targetPos);
        m_wpReached = false;
    }

    public void ChaseTarget(Transform m_target) {
        nav.isStopped = false;
        nav.SetDestination(m_target.position);
    }

    private void CheckDistanceToWP() {
        if (Vector3.Distance(transform.position, m_targetPos) <= m_minWaypointDistance) {
            if (m_curWaypoint == m_lastWP){
//                Debug.Log("at Player's base");
//              m_lastWP = 0;
            } else {
                m_curWaypoint++;
                m_wpReached = true;
            }
        }
    }

    public void StopMovement() {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }

    public void Slow (float prcnt) {
		m_speed = nav.speed * (1f - prcnt);
	}

    public void SetWaypoints(Transform[] waypoints) {
        m_wpReached = false;
        m_waypoints = waypoints;
        m_curWaypoint = 0;
        m_lastWP = waypoints.Length - 1;
        m_targetPos = m_waypoints[m_curWaypoint].position;  
        nav.SetDestination(m_targetPos);
//        StartCoroutine(StartMovement());   
    }

    public void Reset() {
        m_curWaypoint = 0;
        transform.position = Vector3.zero;
        nav.Warp(Vector3.zero);
        nav.ResetPath();
    }
}
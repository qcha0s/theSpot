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
    }

    private void Start() {
        nav.speed = m_speed;
    }

    public void Move() {
        if (m_chasingPlayer) {
            ChasePlayer();
        } else if (m_wpReached) {
           MoveToWP();
        }
        CheckDistanceToWP();
    }
 
    private void MoveToWP() {
        Vector3 tempWaypointPosition;
        tempWaypointPosition = m_waypoints[m_curWaypoint].position;
        tempWaypointPosition.x += Random.Range(-m_wpOffset.x,m_wpOffset.x);
        tempWaypointPosition.z += Random.Range(-m_wpOffset.y,m_wpOffset.y);
        m_targetPos = tempWaypointPosition;
        nav.SetDestination(m_targetPos);
        m_wpReached = false;
    }

    public void GetWayPoints() {
        Transform[] potentialWaypoints = m_wpContainer.GetComponentsInChildren<Transform>();
        Transform[] waypoints = new Transform[ (potentialWaypoints.Length - 1) ];
        for (int i = 1; i < potentialWaypoints.Length; i++ ) {
            waypoints[i - 1] = potentialWaypoints[i];
        }
        SetWaypoints(waypoints);
    }

    private void ChasePlayer() {
      
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            m_targetPos = other.transform.position;
            m_chasingPlayer = true;
            nav.SetDestination(m_targetPos);  
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            m_chasingPlayer = false;
            m_wpReached = true;
        }
    }

    private void CheckDistanceToWP() {
        if (Vector3.Distance(transform.position, m_targetPos) <= m_minWaypointDistance) {
            if (m_curWaypoint == m_lastWP){
                Debug.Log("at Player's base");
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
        m_waypoints = waypoints;
        m_curWaypoint = 0;
        m_lastWP = waypoints.Length - 1;
        m_targetPos = m_waypoints[m_curWaypoint].position;  
        nav.SetDestination(m_targetPos);
//        StartCoroutine(StartMovement());   
    }

    IEnumerator StartMovement() {
        yield return new WaitForSeconds(1);
        nav.SetDestination(m_targetPos);
    }
}
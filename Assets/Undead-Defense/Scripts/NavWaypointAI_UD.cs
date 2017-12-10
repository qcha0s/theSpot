using UnityEngine;
using System.Collections;
using UnityEngine.AI;
 
[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class NavWaypointAI_UD : MonoBehaviour {

    public float m_speed = 2.0f;
    public float m_minWaypointDistance = 0.1f;
    public Transform m_wpContainer;
    public Vector3 Velocity { get {return nav.velocity; }}
 
    private Transform[] m_waypoints;
    private NavMeshAgent nav;
    private int m_curWaypoint = 0;
    private int m_lastWP;
    private bool m_chasingPlayer = false;
    private Transform m_target = null;
 
    private void Awake () {
        nav = GetComponent<NavMeshAgent> ();

    }

    private void Start() {
        GetWaypoints();
        m_lastWP = m_waypoints.Length - 1;
        nav.speed = m_speed;
        m_target = m_waypoints[m_curWaypoint];        
    }

    private void Update() {
        if (m_chasingPlayer) {
            ChasePlayer();
        } else {
           MoveToWP();
        }
        nav.SetDestination(m_target.position);
    }
 
    private void MoveToWP() {
        Vector3 tempLocalPosition;
        Vector3 tempWaypointPosition;
        tempLocalPosition = transform.position;
        tempLocalPosition.y = 0f;
        tempWaypointPosition = m_waypoints[m_curWaypoint].position;
        tempWaypointPosition.y = 0f;
        if (Vector3.Distance (tempLocalPosition, tempWaypointPosition) <= m_minWaypointDistance) {
            if (m_curWaypoint == m_lastWP){
                Debug.Log("at Player's base");
//              m_lastWP = 0;
            } else {
                m_curWaypoint++;
                m_target = m_waypoints[m_curWaypoint];
            }
        }
    }

    private void ChasePlayer() {

    }

	private void GetWaypoints() {
		Transform[] potentialWaypoints = m_wpContainer.GetComponentsInChildren<Transform>();
		m_waypoints = new Transform[ (potentialWaypoints.Length - 1) ];
		for (int i = 1; i < potentialWaypoints.Length; i++ ) {
 			m_waypoints[i - 1] = potentialWaypoints[i];
		}
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            m_target = other.transform;
            m_chasingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            m_target = m_waypoints[m_curWaypoint];
            m_chasingPlayer = false;
        }
    }
}
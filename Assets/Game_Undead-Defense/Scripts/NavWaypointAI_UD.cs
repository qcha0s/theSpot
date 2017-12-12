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
 
    private Transform[] m_waypoints;
    private NavMeshAgent nav;
    private int m_curWaypoint = 0;
    private int m_lastWP;
    private bool m_chasingPlayer = false;
    private bool m_wpReached = false;
    private Vector3 m_targetPos;
 
    private void Awake () {
        nav = GetComponent<NavMeshAgent> ();

    }

    private void Start() {
        GetWaypoints();
        m_lastWP = m_waypoints.Length - 1;
        nav.speed = m_speed;
        Vector3 firstTarget = m_waypoints[m_curWaypoint].position;
        firstTarget.y = 0;
        m_targetPos = firstTarget;   
        nav.SetDestination(m_targetPos);     
    }

    private void Update() {
        // Can Turn all of the below into a single function and call it from outside this script if needed
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
        tempWaypointPosition.y = 0f;
        m_targetPos = tempWaypointPosition;
        nav.SetDestination(m_targetPos);
        m_wpReached = false;
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
        Vector3 tempPosition = new Vector3(transform.position.x,0,transform.position.z);
        if (Vector3.Distance(tempPosition, m_targetPos) <= m_minWaypointDistance) {
            if (m_curWaypoint == m_lastWP){
                Debug.Log("at Player's base");
//              m_lastWP = 0;
            } else {
                m_curWaypoint++;
                m_wpReached = true;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;


	[RequireComponent(typeof(BaseKartMovement))]
	public class AiScript : MonoBehaviour {

		#region Public Variables

		public Transform m_path;
		public float m_maxSteerAngle = 40f;
		

		#endregion


		private List<Transform> m_nodes;
		private int m_currentNode;
		private BaseKartMovement m_baseKartMovement;
		private CharacterController m_characterController;


		private void Awake() {
			Assert.IsNotNull(m_path);
		}

		#region private methods

		// Use this for initialization
		void Start() {

			m_baseKartMovement = GetComponent<BaseKartMovement>();
			m_characterController = GetComponent<CharacterController>();
			Transform[] m_pathTransforms = m_path.GetComponentsInChildren<Transform>();
			m_nodes = new List<Transform>();
			for (int i = 0; i < m_pathTransforms.Length; i++) {
				if (m_pathTransforms[i] != transform) {
					m_nodes.Add(m_pathTransforms[i]);
				}
			}
		}

		private void FixedUpdate() {
			ApplySteering();
			Drive();
		}

		private void ApplySteering() {
			Vector3 m_relativeVector = m_nodes[m_currentNode].position - transform.position;
			float m_newSteeringAngle = 0;
			if (Vector3.Dot(transform.right, m_relativeVector) > 0) {
				m_newSteeringAngle =
					(m_relativeVector.magnitude / 2) / Mathf.Cos(Vector3.Angle(Vector3.right, m_relativeVector) * Mathf.Deg2Rad);
				Debug.Log(Vector3.Angle(transform.right, m_relativeVector));
				Debug.Log(Mathf.Cos(Vector3.Angle(transform.right, m_relativeVector) * Mathf.Deg2Rad));
			}
			else {
				m_newSteeringAngle =
					(-m_relativeVector.magnitude / 2) / Mathf.Cos(Vector3.Angle(-Vector3.right, m_relativeVector) * Mathf.Deg2Rad);
				Debug.Log(Vector3.Angle(-transform.right, m_relativeVector));
				Debug.Log(Mathf.Cos(Vector3.Angle(-transform.right, m_relativeVector) * Mathf.Deg2Rad));

			}
			m_baseKartMovement.Turn(m_baseKartMovement.GetTurnAmountForTurnRadius(m_newSteeringAngle));
			Debug.Log(m_baseKartMovement.GetTurnAmountForTurnRadius(m_newSteeringAngle));
		}

		private void Drive() {
			m_baseKartMovement.Gas(1);
		}

		private void CheckWaypointPosition() {
			if (Vector3.Distance(transform.position, m_nodes[m_currentNode].position) < 0.5f) {
				if (m_currentNode == m_nodes.Count - 1) {
					m_currentNode = 0;
				}
				else {
					m_currentNode++;
				}
			}
		}



		#endregion
	}



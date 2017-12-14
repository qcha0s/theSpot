using System.Collections;
using System.Collections.Generic;
using UnityEngine;




	public class Path : MonoBehaviour {


		#region public variables

		public Color m_lineColor;

		#endregion

		#region private variables

		[SerializeField]private List<Transform> m_nodes = new List<Transform>();

		#endregion

		#region private methods

		private void OnDrawGizmos() {
			Gizmos.color = m_lineColor;
			Transform[] m_pathTransforms = GetComponentsInChildren<Transform>();
			m_nodes = new List<Transform>();

			for (int i = 0; i < m_pathTransforms.Length; i++) {
				if (m_pathTransforms[i] != transform) {
					m_nodes.Add(m_pathTransforms[i]);
				}
			}
			for (int i = 0; i < m_nodes.Count; i++) {
				Vector3 m_currentNode = m_nodes[i].position;
				Vector3 m_previousNode = Vector3.zero;
				if (i > 0) {
					m_previousNode = m_nodes[i - 1].position;
				} else if (i == 0 && m_nodes.Count > 1) {
					m_previousNode = m_nodes[m_nodes.Count - 1].position;
				}
				Gizmos.DrawLine(m_previousNode,m_currentNode);
			}
		}

		#endregion
		
		
	}

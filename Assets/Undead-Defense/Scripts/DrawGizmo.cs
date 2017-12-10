using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour {

    public bool m_drawLines = false;
    public bool m_connectFirstToLast = false;
    public float m_size = 1;
    public Color m_color = Color.white;
    private Transform[] points;
    public int m_type;

    private void OnDrawGizmos() {
        Vector3 last = Vector3.zero;
        points = gameObject.GetComponentsInChildren<Transform>();
        if (m_drawLines && m_connectFirstToLast) {
           last = points[points.Length-1].position;
        } else {
            last = Vector3.zero;
        }
        for (int i = 1; i < points.Length; i++) {
            Gizmos.color = m_color;
            Gizmos.DrawSphere(points[i].position, m_size);
            if (m_drawLines) {
                if (i != 1 || m_connectFirstToLast) {
                    Gizmos.DrawLine(last, points[i].position); 
                }    
                last = points[i].position;
            }
        }   
    }
}

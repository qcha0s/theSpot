using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent( typeof( Camera ) )]
public class CRTEffect : MonoBehaviour {
	public Material m_material;
	public float m_tuningStrength = 0.5f;
	public float m_gradingResolution = 16f;

	private void OnRenderImage(RenderTexture src, RenderTexture dest) {

		m_material.SetFloat( "Tuning_Strength", m_tuningStrength);
		m_material.SetFloat( "GradingRes", m_gradingResolution );

		Graphics.Blit( src, dest, m_material );
	}
}

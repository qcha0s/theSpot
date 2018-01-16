using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings (channel = 0, sendInterval = 0.1f)]
public class TS_Network_TransformSync : NetworkBehaviour {

	[SyncVar(hook = "SyncPosValues")]
	private Vector3 syncPos;
	[SyncVar(hook = "SyncRotValues")]
	private Vector3 syncRot;
	private float m_distBeforeClientMove = 0.5f;
	private float m_angleBeforeClientRotate = 4f;
	private float m_distanceOffset = 0.1f;
	private float m_rotationOffset = 1f;
	private List<Vector3> syncPosList = new List<Vector3>();
	private List<Vector3> syncRotList = new List<Vector3>();
	enum PosLerps{Low = 16, Medium = 21, High = 25}
	enum RotLerps{Low = 10, Medium = 25, High = 40}
	private int posLerpRate = (int)PosLerps.Low;
	private int rotLerpRate = (int)RotLerps.Low;
	private float rotNormLerpRate = 10f;
	private float rotFastLerpRate = 20f;
	public bool m_useHistoricalLerping = false;

	private void Start() {
	}

	void FixedUpdate() {
		TransmitTransform();
	}

	void Update() {
		LerpTransform();
	}

	private void LerpTransform() {
		if (!isLocalPlayer) {
			if (m_useHistoricalLerping) {
				UseHistoricalLerping();
			} else {
				NormalLerping();
			}
		}
	}

	private void NormalLerping() {
		transform.position = Vector3.Lerp(transform.position,syncPos,Time.deltaTime*posLerpRate);
		transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(syncRot),Time.deltaTime*rotLerpRate);
	}

	private void UseHistoricalLerping() {
		HistoricalPosLerp();
		HistoricalRotLerp();
	}

	private void HistoricalPosLerp() {
		if (syncPosList.Count > 0) {
			transform.position = Vector3.Lerp(transform.position,syncPosList[0],Time.deltaTime*posLerpRate);
			if (Vector3.Distance(transform.position,syncPosList[0]) < m_distanceOffset) {
				syncPosList.RemoveAt(0);
			}
			if (syncPosList.Count > 7) {
				posLerpRate = (int)PosLerps.High;
			} else if (syncPosList.Count > 4) {
				posLerpRate = (int)PosLerps.Medium;
			} else {
				posLerpRate = (int)PosLerps.Low;
			}
		}
	}

	private void HistoricalRotLerp() {
		if (syncRotList.Count > 0) {
			transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(syncRotList[0]),Time.deltaTime*rotLerpRate);
			if ((transform.rotation.eulerAngles - syncRotList[0]).magnitude < m_rotationOffset) {
				syncRotList.RemoveAt(0);
			}
			if (syncRotList.Count > 6) {
				rotLerpRate = (int)RotLerps.High;
			} else if (syncRotList.Count > 3) {
				rotLerpRate = (int)RotLerps.Medium;
			} else {
				rotLerpRate = (int)RotLerps.Low;
			}
		}
	}

	[Command]
	void CmdTellServerMyPos(Vector3 pos) {
		syncPos = pos;
	}

	[Command]
	void CmdTellServerMyRot(Vector3 rot) {
		syncRot = rot;
	}

	[ClientCallback]
	void TransmitTransform() {
		if (isLocalPlayer) {
			if (Vector3.Distance(transform.position, syncPos) > m_distBeforeClientMove) {
				CmdTellServerMyPos(transform.position);
			} 
			if ((transform.rotation.eulerAngles - syncRot).magnitude > m_angleBeforeClientRotate) {
				CmdTellServerMyRot(transform.rotation.eulerAngles);
			}
		}
	}

	[Client]
	void SyncPosValues(Vector3 newPos) {
		syncPos = newPos;
		syncPosList.Add(newPos);
	}

	[Client]
	void SyncRotValues(Vector3 newRot) {
		syncRot = newRot;
		syncRotList.Add(newRot);
	}
}

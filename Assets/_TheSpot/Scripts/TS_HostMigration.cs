using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;

public class TS_HostMigration : MonoBehaviour {

	[SerializeField]
	private bool m_HostMigration = true;
	private int m_OldServerConnectionID = -1;
	private PeerInfoMessage m_NewHostInfo = new PeerInfoMessage();
	private PeerListMessage m_PeerListMessage = new PeerListMessage();
	private Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> m_PendingPlayers = new Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers>();
	private NetworkClient m_client;
	private bool m_WaitingToBecomeNewHost;
	private bool m_WaitingReconnectToNewHost;
	private bool m_DisconnectedFromHost;
	private bool m_HostWasShutDown;
	private MatchInfo m_MatchInfo;
	private string m_NewHostAdress;
	private PeerInfoMessage[] m_Peers;




}


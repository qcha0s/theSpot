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

	// controls wether Host Migration is active or not
	public bool HostMigration {
		get {
			return this.m_HostMigration;
		} set {
			this.m_HostMigration = value;
		}
	}
	// the client that is being used to connect to the host
	public NetworkClient client {
		get {
			return this.m_client;
		} 
	}
	// true if client was disconnected from host and is waiting to becvome new host
	public bool waitingToBecomeNewHost {
		get {
			return this.m_WaitingReconnectToNewHost;
		} set {
			this.m_WaitingToBecomeNewHost = value;
		}
	}
	// true if this client was disconnected from host and is waiting reconnect to new host
	public bool WaitingReconnectToNewHost {
		get {
			return this.m_WaitingReconnectToNewHost;
		} set {
			this.m_WaitingReconnectToNewHost = value;
		}
	}
	// true if this client was disconnected from the host
	public bool DisconnectedFromHost {
		get {
			return this.m_DisconnectedFromHost;
		}
	}
	// true if this was the host and was now shut down
	public bool HostWasShutDown {
		get {
			return this.m_HostWasShutDown;
		}
	}
	// information about the match, can be null if there is no current match
	public MatchInfo  matchinfo{
		get {
			return this.m_MatchInfo;
		}
	}
	// the connection ID that this client was assigned on the old host
	public int OldServerConnectionID {
		get {
			return this.m_OldServerConnectionID;
		}
	}
	// the IP address of the new host to connect to
	public string newHostAdress {
		get {
			return this.m_NewHostAdress;
		} set {
			this.m_NewHostAdress = value;
		}
	}
	// the set peers involved in the game. this includes the host and this client
	public PeerInfoMessage[] peers {
		get {
			return this.m_Peers;
		}
	}
	// the player objects that have been disabled, and are waiting for their clients to reconnect
	public Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> pendingPlayers {
		get {
			return this.m_PendingPlayers;
		}
	}
	// reconnecting the pending client
	private void AddPendingPlayer(GameObject obj, int connectionId, NetworkInstanceId netId, short playerControllerId) {
		if (!this.m_PendingPlayers.ContainsKey(connectionId))
		this.m_PendingPlayers[connectionId] = new NetworkMigrationManager.ConnectionPendingPlayers(){
			players = new List<NetworkMigrationManager.PendingPlayerInfo>()
		};
		this.m_PendingPlayers[connectionId].players.Add(new NetworkMigrationManager.PendingPlayerInfo() {
			netId = netId,
			playerControllerId = playerControllerId, 
			obj = obj
		});
	}




}


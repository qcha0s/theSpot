/*using System.Collections.Generic;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
  /// <summary>
  ///         <para>A component that manages the process of a new host taking over a game when the old host is lost. This is referred to as "host migration". The migration manager sends information about each peer in the game to all the clients, and when the host is lost because of a crash or network outage, the clients are able to choose a new host, and continue the game.
  /// 
  /// The old host is able to rejoin the new game on the new host.
  /// 
  /// The state of SyncVars and SyncLists on all objects with NetworkIdentities in the scene is maintained during a host migration. This also applies to custom serialized data for objects.
  /// 
  /// All of the player objects in the game are disabled when the host is lost. Then, when the other clients rejoin the new game on the new host, the corresponding players for those clients are re-enabled on the host, and respawned on the other clients. No player state data is lost during a host migration.</para>
  ///       </summary>
  [AddComponentMenu("Network/NetworkMigrationManager")]
  public class NetworkMigrationManager : MonoBehaviour
  {
    [SerializeField]
    private bool m_HostMigration = true;
    [SerializeField]
    private bool m_ShowGUI = true;
    [SerializeField]
    private int m_OffsetX = 10;
    [SerializeField]
    private int m_OffsetY = 300;
    private int m_OldServerConnectionId = -1;
    private PeerInfoMessage m_NewHostInfo = new PeerInfoMessage();
    private PeerListMessage m_PeerListMessage = new PeerListMessage();
    private Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> m_PendingPlayers = new Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers>();
    private NetworkClient m_Client;
    private bool m_WaitingToBecomeNewHost;
    private bool m_WaitingReconnectToNewHost;
    private bool m_DisconnectedFromHost;
    private bool m_HostWasShutdown;
    private MatchInfo m_MatchInfo;
    private string m_NewHostAddress;
    private PeerInfoMessage[] m_Peers;

    /// <summary>
    ///   <para>Controls whether host migration is active.</para>
    /// </summary>
    public bool hostMigration
    {
      get
      {
        return this.m_HostMigration;
      }
      set
      {
        this.m_HostMigration = value;
      }
    }

    /// <summary>
    ///   <para>Flag to toggle display of the default UI.</para>
    /// </summary>
    public bool showGUI
    {
      get
      {
        return this.m_ShowGUI;
      }
      set
      {
        this.m_ShowGUI = value;
      }
    }

    /// <summary>
    ///   <para>The X offset in pixels of the migration manager default GUI.</para>
    /// </summary>
    public int offsetX
    {
      get
      {
        return this.m_OffsetX;
      }
      set
      {
        this.m_OffsetX = value;
      }
    }

    /// <summary>
    ///   <para>The Y offset in pixels of the migration manager default GUI.</para>
    /// </summary>
    public int offsetY
    {
      get
      {
        return this.m_OffsetY;
      }
      set
      {
        this.m_OffsetY = value;
      }
    }

    /// <summary>
    ///   <para>The client instance that is being used to connect to the host.</para>
    /// </summary>
    public NetworkClient client
    {
      get
      {
        return this.m_Client;
      }
    }

    /// <summary>
    ///   <para>True if this is a client that was disconnected from the host, and was chosen as the new host.</para>
    /// </summary>
    public bool waitingToBecomeNewHost
    {
      get
      {
        return this.m_WaitingToBecomeNewHost;
      }
      set
      {
        this.m_WaitingToBecomeNewHost = value;
      }
    }

    /// <summary>
    ///   <para>True if this is a client that was disconnected from the host and is now waiting to reconnect to the new host.</para>
    /// </summary>
    public bool waitingReconnectToNewHost
    {
      get
      {
        return this.m_WaitingReconnectToNewHost;
      }
      set
      {
        this.m_WaitingReconnectToNewHost = value;
      }
    }

    /// <summary>
    ///   <para>True is this is a client that has been disconnected from a host.</para>
    /// </summary>
    public bool disconnectedFromHost
    {
      get
      {
        return this.m_DisconnectedFromHost;
      }
    }

    /// <summary>
    ///   <para>True if this was the host and the host has been shut down.</para>
    /// </summary>
    public bool hostWasShutdown
    {
      get
      {
        return this.m_HostWasShutdown;
      }
    }

    /// <summary>
    ///   <para>Information about the match. This may be null if there is no match.</para>
    /// </summary>
    public MatchInfo matchInfo
    {
      get
      {
        return this.m_MatchInfo;
      }
    }

    /// <summary>
    ///   <para>The connectionId that this client was assign on the old host.</para>
    /// </summary>
    public int oldServerConnectionId
    {
      get
      {
        return this.m_OldServerConnectionId;
      }
    }

    /// <summary>
    ///   <para>The IP address of the new host to connect to.</para>
    /// </summary>
    public string newHostAddress
    {
      get
      {
        return this.m_NewHostAddress;
      }
      set
      {
        this.m_NewHostAddress = value;
      }
    }

    /// <summary>
    ///   <para>The set of peers involved in the game. This includes the host and this client.</para>
    /// </summary>
    public PeerInfoMessage[] peers
    {
      get
      {
        return this.m_Peers;
      }
    }

    /// <summary>
    ///   <para>The player objects that have been disabled, and are waiting for their corresponding clients to reconnect.</para>
    /// </summary>
    public Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> pendingPlayers
    {
      get
      {
        return this.m_PendingPlayers;
      }
    }

    private void AddPendingPlayer(GameObject obj, int connectionId, NetworkInstanceId netId, short playerControllerId)
    {
      if (!this.m_PendingPlayers.ContainsKey(connectionId))
        this.m_PendingPlayers[connectionId] = new NetworkMigrationManager.ConnectionPendingPlayers()
        {
          players = new List<NetworkMigrationManager.PendingPlayerInfo>()
        };
      this.m_PendingPlayers[connectionId].players.Add(new NetworkMigrationManager.PendingPlayerInfo()
      {
        netId = netId,
        playerControllerId = playerControllerId,
        obj = obj
      });
    }

    private GameObject FindPendingPlayer(int connectionId, NetworkInstanceId netId, short playerControllerId)
    {
      if (this.m_PendingPlayers.ContainsKey(connectionId))
      {
        using (List<NetworkMigrationManager.PendingPlayerInfo>.Enumerator enumerator = this.m_PendingPlayers[connectionId].players.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NetworkMigrationManager.PendingPlayerInfo current = enumerator.Current;
            if (current.netId == netId && (int) current.playerControllerId == (int) playerControllerId)
              return current.obj;
          }
        }
      }
      return (GameObject) null;
    }

    private void RemovePendingPlayer(int connectionId)
    {
      this.m_PendingPlayers.Remove(connectionId);
    }

    private void Start()
    {
      this.Reset(-1);
    }

    /// <summary>
    ///   <para>Resets the migration manager, and sets the ClientScene's ReconnectId.</para>
    /// </summary>
    /// <param name="reconnectId">The connectionId for the ClientScene to use when reconnecting.</param>
    public void Reset(int reconnectId)
    {
      this.m_OldServerConnectionId = -1;
      this.m_WaitingToBecomeNewHost = false;
      this.m_WaitingReconnectToNewHost = false;
      this.m_DisconnectedFromHost = false;
      this.m_HostWasShutdown = false;
      ClientScene.SetReconnectId(reconnectId, this.m_Peers);
      if (!((Object) NetworkManager.singleton != (Object) null))
        return;
      NetworkManager.singleton.SetupMigrationManager(this);
    }

    internal void AssignAuthorityCallback(NetworkConnection conn, NetworkIdentity uv, bool authorityState)
    {
      PeerAuthorityMessage authorityMessage = new PeerAuthorityMessage();
      authorityMessage.connectionId = conn.connectionId;
      authorityMessage.netId = uv.netId;
      authorityMessage.authorityState = authorityState;
      if (LogFilter.logDebug)
        Debug.Log((object) ("AssignAuthorityCallback send for netId" + (object) uv.netId));
      for (int index = 0; index < NetworkServer.connections.Count; ++index)
      {
        NetworkConnection connection = NetworkServer.connections[index];
        if (connection != null)
          connection.Send((short) 17, (MessageBase) authorityMessage);
      }
    }

    /// <summary>
    ///   <para>Used to initialize the migration manager with client and match information.</para>
    /// </summary>
    /// <param name="newClient">The NetworkClient being used to connect to the host.</param>
    /// <param name="newMatchInfo">Information about the match being used. This may be null if there is no match.</param>
    public void Initialize(NetworkClient newClient, MatchInfo newMatchInfo)
    {
      if (LogFilter.logDev)
        Debug.Log((object) "NetworkMigrationManager initialize");
      this.m_Client = newClient;
      this.m_MatchInfo = newMatchInfo;
      newClient.RegisterHandlerSafe((short) 11, new NetworkMessageDelegate(this.OnPeerInfo));
      newClient.RegisterHandlerSafe((short) 17, new NetworkMessageDelegate(this.OnPeerClientAuthority));
      NetworkIdentity.clientAuthorityCallback = new NetworkIdentity.ClientAuthorityCallback(this.AssignAuthorityCallback);
    }

    /// <summary>
    ///   <para>This causes objects for known players to be disabled.</para>
    /// </summary>
    public void DisablePlayerObjects()
    {
      if (LogFilter.logDev)
        Debug.Log((object) "NetworkMigrationManager DisablePlayerObjects");
      if (this.m_Peers == null)
        return;
      foreach (PeerInfoMessage peer in this.m_Peers)
      {
        if (peer.playerIds != null)
        {
          foreach (PeerInfoPlayer playerId in peer.playerIds)
          {
            if (LogFilter.logDev)
              Debug.Log((object) ("DisablePlayerObjects disable player for " + peer.address + " netId:" + (object) playerId.netId + " control:" + (object) playerId.playerControllerId));
            GameObject localObject = ClientScene.FindLocalObject(playerId.netId);
            if ((Object) localObject != (Object) null)
            {
              localObject.SetActive(false);
              this.AddPendingPlayer(localObject, peer.connectionId, playerId.netId, playerId.playerControllerId);
            }
            else if (LogFilter.logWarn)
              Debug.LogWarning((object) ("DisablePlayerObjects didnt find player Conn:" + (object) peer.connectionId + " NetId:" + (object) playerId.netId));
          }
        }
      }
    }

    /// <summary>
    ///   <para>This sends the set of peers in the game to all the peers in the game.</para>
    /// </summary>
    public void SendPeerInfo()
    {
      if (!this.m_HostMigration)
        return;
      PeerListMessage peerListMessage = new PeerListMessage();
      List<PeerInfoMessage> peerInfoMessageList = new List<PeerInfoMessage>();
      for (int index = 0; index < NetworkServer.connections.Count; ++index)
      {
        NetworkConnection connection = NetworkServer.connections[index];
        if (connection != null)
        {
          PeerInfoMessage peerInfoMessage = new PeerInfoMessage();
          string address;
          int port;
          NetworkID network;
          NodeID dstNode;
          byte error;
          NetworkTransport.GetConnectionInfo(NetworkServer.serverHostId, connection.connectionId, out address, out port, out network, out dstNode, out error);
          peerInfoMessage.connectionId = connection.connectionId;
          peerInfoMessage.port = port;
          if (index == 0)
          {
            peerInfoMessage.port = NetworkServer.listenPort;
            peerInfoMessage.isHost = true;
            peerInfoMessage.address = "<host>";
          }
          else
          {
            peerInfoMessage.address = address;
            peerInfoMessage.isHost = false;
          }
          List<PeerInfoPlayer> peerInfoPlayerList = new List<PeerInfoPlayer>();
          using (List<PlayerController>.Enumerator enumerator = connection.playerControllers.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PlayerController current = enumerator.Current;
              if (current != null && (Object) current.unetView != (Object) null)
              {
                PeerInfoPlayer peerInfoPlayer;
                peerInfoPlayer.netId = current.unetView.netId;
                peerInfoPlayer.playerControllerId = current.unetView.playerControllerId;
                peerInfoPlayerList.Add(peerInfoPlayer);
              }
            }
          }
          if (connection.clientOwnedObjects != null)
          {
            using (HashSet<NetworkInstanceId>.Enumerator enumerator = connection.clientOwnedObjects.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                NetworkInstanceId current = enumerator.Current;
                GameObject localObject = NetworkServer.FindLocalObject(current);
                if (!((Object) localObject == (Object) null) && (int) localObject.GetComponent<NetworkIdentity>().playerControllerId == -1)
                {
                  PeerInfoPlayer peerInfoPlayer;
                  peerInfoPlayer.netId = current;
                  peerInfoPlayer.playerControllerId = (short) -1;
                  peerInfoPlayerList.Add(peerInfoPlayer);
                }
              }
            }
          }
          if (peerInfoPlayerList.Count > 0)
            peerInfoMessage.playerIds = peerInfoPlayerList.ToArray();
          peerInfoMessageList.Add(peerInfoMessage);
        }
      }
      peerListMessage.peers = peerInfoMessageList.ToArray();
      for (int index = 0; index < NetworkServer.connections.Count; ++index)
      {
        NetworkConnection connection = NetworkServer.connections[index];
        if (connection != null)
        {
          peerListMessage.oldServerConnectionId = connection.connectionId;
          connection.Send((short) 11, (MessageBase) peerListMessage);
        }
      }
    }

    private void OnPeerClientAuthority(NetworkMessage netMsg)
    {
      PeerAuthorityMessage authorityMessage = netMsg.ReadMessage<PeerAuthorityMessage>();
      if (LogFilter.logDebug)
        Debug.Log((object) ("OnPeerClientAuthority for netId:" + (object) authorityMessage.netId));
      if (this.m_Peers == null)
        return;
      foreach (PeerInfoMessage peer in this.m_Peers)
      {
        if (peer.connectionId == authorityMessage.connectionId)
        {
          if (peer.playerIds == null)
            peer.playerIds = new PeerInfoPlayer[0];
          if (authorityMessage.authorityState)
          {
            foreach (PeerInfoPlayer playerId in peer.playerIds)
            {
              if (playerId.netId == authorityMessage.netId)
                return;
            }
            peer.playerIds = new List<PeerInfoPlayer>((IEnumerable<PeerInfoPlayer>) peer.playerIds)
            {
              new PeerInfoPlayer()
              {
                netId = authorityMessage.netId,
                playerControllerId = (short) -1
              }
            }.ToArray();
          }
          else
          {
            for (int index = 0; index < peer.playerIds.Length; ++index)
            {
              if (peer.playerIds[index].netId == authorityMessage.netId)
              {
                List<PeerInfoPlayer> peerInfoPlayerList = new List<PeerInfoPlayer>((IEnumerable<PeerInfoPlayer>) peer.playerIds);
                peerInfoPlayerList.RemoveAt(index);
                peer.playerIds = peerInfoPlayerList.ToArray();
                break;
              }
            }
          }
        }
      }
      this.OnAuthorityUpdated(ClientScene.FindLocalObject(authorityMessage.netId), authorityMessage.connectionId, authorityMessage.authorityState);
    }

    private void OnPeerInfo(NetworkMessage netMsg)
    {
      if (LogFilter.logDebug)
        Debug.Log((object) "OnPeerInfo");
      netMsg.ReadMessage<PeerListMessage>(this.m_PeerListMessage);
      this.m_Peers = this.m_PeerListMessage.peers;
      this.m_OldServerConnectionId = this.m_PeerListMessage.oldServerConnectionId;
      for (int index = 0; index < this.m_Peers.Length; ++index)
      {
        if (LogFilter.logDebug)
          Debug.Log((object) ("peer conn " + (object) this.m_Peers[index].connectionId + " your conn " + (object) this.m_PeerListMessage.oldServerConnectionId));
        if (this.m_Peers[index].connectionId == this.m_PeerListMessage.oldServerConnectionId)
        {
          this.m_Peers[index].isYou = true;
          break;
        }
      }
      this.OnPeersUpdated(this.m_PeerListMessage);
    }

    private void OnServerReconnectPlayerMessage(NetworkMessage netMsg)
    {
      ReconnectMessage reconnectMessage = netMsg.ReadMessage<ReconnectMessage>();
      if (LogFilter.logDev)
        Debug.Log((object) ("OnReconnectMessage: connId=" + (object) reconnectMessage.oldConnectionId + " playerControllerId:" + (object) reconnectMessage.playerControllerId + " netId:" + (object) reconnectMessage.netId));
      GameObject pendingPlayer = this.FindPendingPlayer(reconnectMessage.oldConnectionId, reconnectMessage.netId, reconnectMessage.playerControllerId);
      if ((Object) pendingPlayer == (Object) null)
      {
        if (!LogFilter.logError)
          return;
        Debug.LogError((object) ("OnReconnectMessage connId=" + (object) reconnectMessage.oldConnectionId + " player null for netId:" + (object) reconnectMessage.netId + " msg.playerControllerId:" + (object) reconnectMessage.playerControllerId));
      }
      else if (pendingPlayer.activeSelf)
      {
        if (!LogFilter.logError)
          return;
        Debug.LogError((object) ("OnReconnectMessage connId=" + (object) reconnectMessage.oldConnectionId + " player already active?"));
      }
      else
      {
        if (LogFilter.logDebug)
          Debug.Log((object) ("OnReconnectMessage: player=" + (object) pendingPlayer));
        NetworkReader extraMessageReader = (NetworkReader) null;
        if (reconnectMessage.msgSize != 0)
          extraMessageReader = new NetworkReader(reconnectMessage.msgData);
        if ((int) reconnectMessage.playerControllerId != -1)
        {
          if (extraMessageReader == null)
            this.OnServerReconnectPlayer(netMsg.conn, pendingPlayer, reconnectMessage.oldConnectionId, reconnectMessage.playerControllerId);
          else
            this.OnServerReconnectPlayer(netMsg.conn, pendingPlayer, reconnectMessage.oldConnectionId, reconnectMessage.playerControllerId, extraMessageReader);
        }
        else
          this.OnServerReconnectObject(netMsg.conn, pendingPlayer, reconnectMessage.oldConnectionId);
      }
    }

    /// <summary>
    ///   <para>This re-establishes a non-player object with client authority with a client that is reconnected.  It is similar to NetworkServer.SpawnWithClientAuthority().</para>
    /// </summary>
    /// <param name="newConnection">The connection of the new client.</param>
    /// <param name="oldObject">The object with client authority that is being reconnected.</param>
    /// <param name="oldConnectionId">This client's connectionId on the old host.</param>
    /// <returns>
    ///   <para>True if the object was reconnected.</para>
    /// </returns>
    public bool ReconnectObjectForConnection(NetworkConnection newConnection, GameObject oldObject, int oldConnectionId)
    {
      if (!NetworkServer.active)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "ReconnectObjectForConnection must have active server");
        return false;
      }
      if (LogFilter.logDebug)
        Debug.Log((object) ("ReconnectObjectForConnection: oldConnId=" + (object) oldConnectionId + " obj=" + (object) oldObject + " conn:" + (object) newConnection));
      if (!this.m_PendingPlayers.ContainsKey(oldConnectionId))
      {
        if (LogFilter.logError)
          Debug.LogError((object) ("ReconnectObjectForConnection oldConnId=" + (object) oldConnectionId + " not found."));
        return false;
      }
      oldObject.SetActive(true);
      oldObject.GetComponent<NetworkIdentity>().SetNetworkInstanceId(new NetworkInstanceId(0U));
      if (NetworkServer.SpawnWithClientAuthority(oldObject, newConnection))
        return true;
      if (LogFilter.logError)
        Debug.LogError((object) ("ReconnectObjectForConnection oldConnId=" + (object) oldConnectionId + " SpawnWithClientAuthority failed."));
      return false;
    }

    /// <summary>
    ///   <para>This re-establishes a player object with a client that is reconnected.  It is similar to NetworkServer.AddPlayerForConnection(). The player game object will become the player object for the new connection.</para>
    /// </summary>
    /// <param name="newConnection">The connection of the new client.</param>
    /// <param name="oldPlayer">The player object.</param>
    /// <param name="oldConnectionId">This client's connectionId on the old host.</param>
    /// <param name="playerControllerId">The playerControllerId of the player that is rejoining.</param>
    /// <returns>
    ///   <para>True if able to re-add this player.</para>
    /// </returns>
    public bool ReconnectPlayerForConnection(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
    {
      if (!NetworkServer.active)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "ReconnectPlayerForConnection must have active server");
        return false;
      }
      if (LogFilter.logDebug)
        Debug.Log((object) ("ReconnectPlayerForConnection: oldConnId=" + (object) oldConnectionId + " player=" + (object) oldPlayer + " conn:" + (object) newConnection));
      if (!this.m_PendingPlayers.ContainsKey(oldConnectionId))
      {
        if (LogFilter.logError)
          Debug.LogError((object) ("ReconnectPlayerForConnection oldConnId=" + (object) oldConnectionId + " not found."));
        return false;
      }
      oldPlayer.SetActive(true);
      NetworkServer.Spawn(oldPlayer);
      if (!NetworkServer.AddPlayerForConnection(newConnection, oldPlayer, playerControllerId))
      {
        if (LogFilter.logError)
          Debug.LogError((object) ("ReconnectPlayerForConnection oldConnId=" + (object) oldConnectionId + " AddPlayerForConnection failed."));
        return false;
      }
      if (NetworkServer.localClientActive)
        this.SendPeerInfo();
      return true;
    }

    /// <summary>
    ///   <para>This should be called on a client when it has lost its connection to the host.</para>
    /// </summary>
    /// <param name="conn">The connection of the client that was connected to the host.</param>
    /// <returns>
    ///   <para>True if the client should stay in the on-line scene.</para>
    /// </returns>
    public bool LostHostOnClient(NetworkConnection conn)
    {
      if (LogFilter.logDebug)
        Debug.Log((object) "NetworkMigrationManager client OnDisconnectedFromHost");
      if (Application.platform == RuntimePlatform.WebGLPlayer)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "LostHostOnClient: Host migration not supported on WebGL");
        return false;
      }
      if (this.m_Client == null)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "NetworkMigrationManager LostHostOnHost client was never initialized.");
        return false;
      }
      if (!this.m_HostMigration)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "NetworkMigrationManager LostHostOnHost migration not enabled.");
        return false;
      }
      this.m_DisconnectedFromHost = true;
      this.DisablePlayerObjects();
      byte error;
      NetworkTransport.Disconnect(this.m_Client.hostId, this.m_Client.connection.connectionId, out error);
      if (this.m_OldServerConnectionId == -1)
        return false;
      NetworkMigrationManager.SceneChangeOption sceneChange;
      this.OnClientDisconnectedFromHost(conn, out sceneChange);
      return sceneChange == NetworkMigrationManager.SceneChangeOption.StayInOnlineScene;
    }

    /// <summary>
    ///   <para>This should be called on a host when it has has been shutdown.</para>
    /// </summary>
    public void LostHostOnHost()
    {
      if (LogFilter.logDebug)
        Debug.Log((object) "NetworkMigrationManager LostHostOnHost");
      if (Application.platform == RuntimePlatform.WebGLPlayer)
      {
        if (!LogFilter.logError)
          return;
        Debug.LogError((object) "LostHostOnHost: Host migration not supported on WebGL");
      }
      else
      {
        this.OnServerHostShutdown();
        if (this.m_Peers == null)
        {
          if (!LogFilter.logError)
            return;
          Debug.LogError((object) "NetworkMigrationManager LostHostOnHost no peers");
        }
        else
        {
          if (this.m_Peers.Length == 1)
            return;
          this.m_HostWasShutdown = true;
        }
      }
    }

    /// <summary>
    ///   <para>This causes a client that has been disconnected from the host to become the new host of the game.</para>
    /// </summary>
    /// <param name="port">The network port to listen on.</param>
    /// <returns>
    ///   <para>True if able to become the new host.</para>
    /// </returns>
    public bool BecomeNewHost(int port)
    {
      if (LogFilter.logDebug)
        Debug.Log((object) ("NetworkMigrationManager BecomeNewHost " + (object) this.m_MatchInfo));
      NetworkServer.RegisterHandler((short) 47, new NetworkMessageDelegate(this.OnServerReconnectPlayerMessage));
      NetworkClient externalClient = NetworkServer.BecomeHost(this.m_Client, port, this.m_MatchInfo, this.oldServerConnectionId, this.peers);
      if (externalClient != null)
      {
        if ((Object) NetworkManager.singleton != (Object) null)
        {
          NetworkManager.singleton.RegisterServerMessages();
          NetworkManager.singleton.UseExternalClient(externalClient);
        }
        else
          Debug.LogWarning((object) "MigrationManager BecomeNewHost - No NetworkManager.");
        externalClient.RegisterHandlerSafe((short) 11, new NetworkMessageDelegate(this.OnPeerInfo));
        this.RemovePendingPlayer(this.m_OldServerConnectionId);
        this.Reset(-1);
        this.SendPeerInfo();
        return true;
      }
      if (LogFilter.logError)
        Debug.LogError((object) "NetworkServer.BecomeHost failed");
      return false;
    }

    protected virtual void OnClientDisconnectedFromHost(NetworkConnection conn, out NetworkMigrationManager.SceneChangeOption sceneChange)
    {
      sceneChange = NetworkMigrationManager.SceneChangeOption.StayInOnlineScene;
    }

    /// <summary>
    ///   <para>A virtual function that is called when the host is shutdown.</para>
    /// </summary>
    protected virtual void OnServerHostShutdown()
    {
    }

    /// <summary>
    ///   <para>A virtual function that is called on the new host when a client from the old host reconnects to the new host.</para>
    /// </summary>
    /// <param name="newConnection">The connection of the new client.</param>
    /// <param name="oldPlayer">The player object associated with this client.</param>
    /// <param name="oldConnectionId">The connectionId of this client on the old host.</param>
    /// <param name="playerControllerId">The playerControllerId of the player that is re-joining.</param>
    /// <param name="extraMessageReader">Additional message data (optional).</param>
    protected virtual void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
    {
      this.ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
    }

    /// <summary>
    ///   <para>A virtual function that is called on the new host when a client from the old host reconnects to the new host.</para>
    /// </summary>
    /// <param name="newConnection">The connection of the new client.</param>
    /// <param name="oldPlayer">The player object associated with this client.</param>
    /// <param name="oldConnectionId">The connectionId of this client on the old host.</param>
    /// <param name="playerControllerId">The playerControllerId of the player that is re-joining.</param>
    /// <param name="extraMessageReader">Additional message data (optional).</param>
    protected virtual void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId, NetworkReader extraMessageReader)
    {
      this.ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
    }

    /// <summary>
    ///   <para>A virtual function that is called for non-player objects with client authority on the new host when a client from the old host reconnects to the new host.</para>
    /// </summary>
    /// <param name="newConnection">The connection of the new client.</param>
    /// <param name="oldObject">The object with authority that is being reconnected.</param>
    /// <param name="oldConnectionId">The connectionId of this client on the old host.</param>
    protected virtual void OnServerReconnectObject(NetworkConnection newConnection, GameObject oldObject, int oldConnectionId)
    {
      this.ReconnectObjectForConnection(newConnection, oldObject, oldConnectionId);
    }

    /// <summary>
    ///   <para>A virtual function that is called when the set of peers in the game changes.</para>
    /// </summary>
    /// <param name="peers">The set of peers in the game.</param>
    protected virtual void OnPeersUpdated(PeerListMessage peers)
    {
      if (!LogFilter.logDev)
        return;
      Debug.Log((object) ("NetworkMigrationManager NumPeers " + (object) peers.peers.Length));
    }

    /// <summary>
    ///   <para>A virtual function that is called when the authority of a non-player object changes.</para>
    /// </summary>
    /// <param name="go">The game object whose authority has changed.</param>
    /// <param name="connectionId">The id of the connection whose authority changed for this object.</param>
    /// <param name="authorityState">The new authority state for the object.</param>
    protected virtual void OnAuthorityUpdated(GameObject go, int connectionId, bool authorityState)
    {
      if (!LogFilter.logDev)
        return;
      Debug.Log((object) ("NetworkMigrationManager OnAuthorityUpdated for " + (object) go + " conn:" + (object) connectionId + " state:" + (object) authorityState));
    }

    public virtual bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
    {
      if (this.m_Peers == null)
      {
        if (LogFilter.logError)
          Debug.LogError((object) "NetworkMigrationManager FindLowestHost no peers");
        newHostInfo = (PeerInfoMessage) null;
        youAreNewHost = false;
        return false;
      }
      if (LogFilter.logDev)
        Debug.Log((object) "NetworkMigrationManager FindLowestHost");
      newHostInfo = new PeerInfoMessage();
      newHostInfo.connectionId = 50000;
      newHostInfo.address = string.Empty;
      newHostInfo.port = 0;
      int num = -1;
      youAreNewHost = false;
      if (this.m_Peers == null)
        return false;
      foreach (PeerInfoMessage peer in this.m_Peers)
      {
        if (peer.connectionId != 0 && !peer.isHost)
        {
          if (peer.isYou)
            num = peer.connectionId;
          if (peer.connectionId < newHostInfo.connectionId)
            newHostInfo = peer;
        }
      }
      if (newHostInfo.connectionId == 50000)
        return false;
      if (newHostInfo.connectionId == num)
        youAreNewHost = true;
      if (LogFilter.logDev)
        Debug.Log((object) ("FindNewHost new host is " + newHostInfo.address));
      return true;
    }

    private void OnGUIHost()
    {
      int offsetY = this.m_OffsetY;
      GUI.Label(new Rect((float) this.m_OffsetX, (float) offsetY, 200f, 40f), "Host Was Shutdown ID(" + (object) this.m_OldServerConnectionId + ")");
      int num1 = offsetY + 25;
      if (Application.platform == RuntimePlatform.WebGLPlayer)
      {
        GUI.Label(new Rect((float) this.m_OffsetX, (float) num1, 200f, 40f), "Host Migration not supported for WebGL");
      }
      else
      {
        int num2;
        if (this.m_WaitingReconnectToNewHost)
        {
          if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num1, 200f, 20f), "Reconnect as Client"))
          {
            this.Reset(0);
            if ((Object) NetworkManager.singleton != (Object) null)
            {
              NetworkManager.singleton.networkAddress = GUI.TextField(new Rect((float) (this.m_OffsetX + 100), (float) num1, 95f, 20f), NetworkManager.singleton.networkAddress);
              NetworkManager.singleton.StartClient();
            }
            else
              Debug.LogWarning((object) "MigrationManager Old Host Reconnect - No NetworkManager.");
          }
          num2 = num1 + 25;
        }
        else
        {
          bool youAreNewHost;
          if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num1, 200f, 20f), "Pick New Host") && this.FindNewHost(out this.m_NewHostInfo, out youAreNewHost))
          {
            this.m_NewHostAddress = this.m_NewHostInfo.address;
            if (youAreNewHost)
              Debug.LogWarning((object) "MigrationManager FindNewHost - new host is self?");
            else
              this.m_WaitingReconnectToNewHost = true;
          }
          num2 = num1 + 25;
        }
        if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num2, 200f, 20f), "Leave Game"))
        {
          if ((Object) NetworkManager.singleton != (Object) null)
          {
            NetworkManager.singleton.SetupMigrationManager((NetworkMigrationManager) null);
            NetworkManager.singleton.StopHost();
          }
          else
            Debug.LogWarning((object) "MigrationManager Old Host LeaveGame - No NetworkManager.");
          this.Reset(-1);
        }
        int num3 = num2 + 25;
      }
    }

    private void OnGUIClient()
    {
      int offsetY = this.m_OffsetY;
      GUI.Label(new Rect((float) this.m_OffsetX, (float) offsetY, 200f, 40f), "Lost Connection To Host ID(" + (object) this.m_OldServerConnectionId + ")");
      int num1 = offsetY + 25;
      if (Application.platform == RuntimePlatform.WebGLPlayer)
      {
        GUI.Label(new Rect((float) this.m_OffsetX, (float) num1, 200f, 40f), "Host Migration not supported for WebGL");
      }
      else
      {
        int num2;
        if (this.m_WaitingToBecomeNewHost)
        {
          GUI.Label(new Rect((float) this.m_OffsetX, (float) num1, 200f, 40f), "You are the new host");
          int num3 = num1 + 25;
          if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num3, 200f, 20f), "Start As Host"))
          {
            if ((Object) NetworkManager.singleton != (Object) null)
              this.BecomeNewHost(NetworkManager.singleton.networkPort);
            else
              Debug.LogWarning((object) "MigrationManager Client BecomeNewHost - No NetworkManager.");
          }
          num2 = num3 + 25;
        }
        else if (this.m_WaitingReconnectToNewHost)
        {
          GUI.Label(new Rect((float) this.m_OffsetX, (float) num1, 200f, 40f), "New host is " + this.m_NewHostAddress);
          int num3 = num1 + 25;
          if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num3, 200f, 20f), "Reconnect To New Host"))
          {
            this.Reset(this.m_OldServerConnectionId);
            if ((Object) NetworkManager.singleton != (Object) null)
            {
              NetworkManager.singleton.networkAddress = this.m_NewHostAddress;
              NetworkManager.singleton.client.ReconnectToNewHost(this.m_NewHostAddress, NetworkManager.singleton.networkPort);
            }
            else
              Debug.LogWarning((object) "MigrationManager Client reconnect - No NetworkManager.");
          }
          num2 = num3 + 25;
        }
        else
        {
          bool youAreNewHost;
          if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num1, 200f, 20f), "Pick New Host") && this.FindNewHost(out this.m_NewHostInfo, out youAreNewHost))
          {
            this.m_NewHostAddress = this.m_NewHostInfo.address;
            if (youAreNewHost)
              this.m_WaitingToBecomeNewHost = true;
            else
              this.m_WaitingReconnectToNewHost = true;
          }
          num2 = num1 + 25;
        }
        if (GUI.Button(new Rect((float) this.m_OffsetX, (float) num2, 200f, 20f), "Leave Game"))
        {
          if ((Object) NetworkManager.singleton != (Object) null)
          {
            NetworkManager.singleton.SetupMigrationManager((NetworkMigrationManager) null);
            NetworkManager.singleton.StopHost();
          }
          else
            Debug.LogWarning((object) "MigrationManager Client LeaveGame - No NetworkManager.");
          this.Reset(-1);
        }
        int num4 = num2 + 25;
      }
    }

    private void OnGUI()
    {
      if (!this.m_ShowGUI)
        return;
      if (this.m_HostWasShutdown)
      {
        this.OnGUIHost();
      }
      else
      {
        if (!this.m_DisconnectedFromHost)
          return;
        this.OnGUIClient();
      }
    }

    /// <summary>
    ///   <para>An enumeration of how to handle scene changes when the connection to the host is lost.</para>
    /// </summary>
    public enum SceneChangeOption
    {
      StayInOnlineScene,
      SwitchToOfflineScene,
    }

    /// <summary>
    ///   <para>Information about a player object from another peer.</para>
    /// </summary>
    public struct PendingPlayerInfo
    {
      /// <summary>
      ///   <para>The networkId of the player object.</para>
      /// </summary>
      public NetworkInstanceId netId;
      /// <summary>
      ///   <para>The playerControllerId of the player object.</para>
      /// </summary>
      public short playerControllerId;
      /// <summary>
      ///   <para>The gameObject for the player.</para>
      /// </summary>
      public GameObject obj;
    }

    /// <summary>
    ///   <para>The player objects for connections to the old host.</para>
    /// </summary>
    public struct ConnectionPendingPlayers
    {
      /// <summary>
      ///   <para>The list of players for a connection.</para>
      /// </summary>
      public List<NetworkMigrationManager.PendingPlayerInfo> players;
    }
  }
}
*/

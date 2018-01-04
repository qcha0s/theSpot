using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TS_Network_Chat : TS_ChatInterface{

	private const short m_chatMessage = 131; //identifier, can seperate these to do PMs and group messages and stuff
	private string m_playerName;

	private void Start() {
		if (isClient) {
			NetworkManager.singleton.client.RegisterHandler(m_chatMessage, ReceiveMessage); //if network receives message with code (m_chatmessage) will do method 'receivemessage'
		}
		InitComponents();	
		if (NetworkServer.active) {
			NetworkServer.RegisterHandler(m_chatMessage, ServerReceiveMessage);
		}
	}

	private void ReceiveMessage(NetworkMessage message) {
		string newMessage = message.ReadMessage<StringMessage>().value;
		AddMessage(newMessage);
	}

	public override void SendMessage(string input) { //this is called through the input UI
		if (input.Length > 0) {
			Debug.Log("message sent");
			string newMessage = m_playerName + ": " + input;
			StringMessage myMessage = new StringMessage();
			myMessage.value = newMessage;
			NetworkManager.singleton.client.Send(m_chatMessage, myMessage);
		}
	}

	private void ServerReceiveMessage(NetworkMessage message) {
		StringMessage myMessage = new StringMessage();
		myMessage.value = message.ReadMessage<StringMessage>().value;
		NetworkServer.SendToAll(m_chatMessage, myMessage);
	}

	public void SetPlayerName(string name) {
		m_playerName = name;
	}
}

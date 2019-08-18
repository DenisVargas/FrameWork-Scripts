using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//forma 2 - AVANZADA - con NetworkServer y NetworkClient

public class MultiplayerManager2 : MonoBehaviour {

    public const int OnConnectedPlayer = 900;
    public const int StartMatch = 901;
    public const int ClientInfo = 902;
    public const int ClientInfoResponse = 903;
    public const int MaxPlayers = 904;

    public static NetworkClient myClient;


    private void OnGUI()
    {
        if(!NetworkServer.active && !NetworkClient.active)
        {
            if (GUILayout.Button("Host Game"))
                SetupServer();

            if (GUILayout.Button("Start Client"))
                SetupClient();

            if (GUILayout.Button("Host & Launch Client"))
            {
                SetupServer();
                SetupLocalClient();
            }
        }

        if (NetworkClient.active)
            if (GUILayout.Button("Disconnect")) 
                myClient.Disconnect();

    }

    //Creamos un servidor y escuchamos las conexiones de un puerto
    public void SetupServer()
    {
        RegisterServerMsgs();
        NetworkServer.Listen(8080);
    }

    //Creamos un cliente y nos conectamos al ip y puerto del sv
    public void SetupClient()
    {
        myClient = new NetworkClient();
        RegisterClientMsgs();
        myClient.Connect("127.0.0.1", 8080);
    }

    //Creamos un server local y una instancia de cliente local
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        RegisterClientMsgs();
    }

    public void RegisterClientMsgs()
    {
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        myClient.RegisterHandler(ClientInfoResponse, OnClientInfoResponse);
        myClient.RegisterHandler(MaxPlayers, OnMaxPlayers);
    }

    public void RegisterServerMsgs()
    {
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnUserDisconnected);
        NetworkServer.RegisterHandler(OnConnectedPlayer, OnUserConnected);
        NetworkServer.RegisterHandler(ClientInfo, OnClientInfo);
    }

    //evento - me conecté satisfactoriamente a la partida
    public void OnConnected(NetworkMessage msg)
    {
        print("me conecte");

        var ms = new LobbyMessage();
        myClient.Send(OnConnectedPlayer, ms);

        ms.level = 200;
        ms.heroID = 8;
        myClient.Send(ClientInfo, ms);
    }

    //evento - me desconecté de la partida
    public void OnDisconnected(NetworkMessage msg)
    {
        print("me desconecte");
    }

    //evento  - se desconectó un usuario
    private void OnUserDisconnected(NetworkMessage netMsg)
    {
        print("user disconnected ");
    }

    //evento propio S - se conectó un usuario ID 900  
    void OnUserConnected(NetworkMessage netMsg)
    {
        print("user connected");
        var msg = new ConnectionMessage();
        msg.connections = NetworkServer.connections.Count;
        NetworkServer.SendToClient(netMsg.conn.connectionId, MaxPlayers, msg);
    }

    //evento propio C - recibo cantidad de jugadores
    private void OnMaxPlayers(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<ConnectionMessage>();
        print(msg.connections);
    }

    //evento propio S - se recive char info ID 908
    void OnClientInfo(NetworkMessage netMsg)
    {
        LobbyMessage msg = netMsg.ReadMessage<LobbyMessage>();

        print("servidor recibio info de cliente: heroID=" + msg.heroID + " / level=" + msg.level + ".... reenviando a clientes");
        var ms = new LobbyMessage();
        ms.level = msg.level;
        ms.heroID = msg.heroID;
        NetworkServer.SendToAll(ClientInfoResponse, ms);
    }

    //evento propio C - se recive respuesta de info ID 909
    void OnClientInfoResponse(NetworkMessage netMsg)
    {
        LobbyMessage msg = netMsg.ReadMessage<LobbyMessage>();

        print("cliente recibio info de cliente: heroID=" + msg.heroID + " / level=" + msg.level);
    }

    //evento propio C - comienza la partida ID 910
    void OnGameStart(NetworkMessage netMsg)
    {
        print("comenzando juego...");
    }
}

public class SerializedMessage : MessageBase
{
    public string serializedString;
    public byte[] serializedByteArray;
}

public class LobbyMessage : MessageBase
{
    public int heroID = -1;
    public int level = -1;
}

public class ConnectionMessage : MessageBase
{
    public int connections = -1;
}

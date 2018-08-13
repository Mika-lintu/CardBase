using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class NetworkClientUI : MonoBehaviour
{

    public string serverIP;
    public string portNumber;
    static string ipaddress;
    
    static NetworkClient client;

    //static public CardController controller;
    public PlayerDeck playerDeck;

    string serverMessage;

    static short messageNumber = 999;
    static int clientID = 0;
    

    private void OnGUI()
    {
        ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

        GUI.Box(new Rect(150, Screen.height - 50, 100, 50), "Server message");
        GUI.Label(new Rect(150, Screen.height - 30, 300, 20), serverMessage);
        
        if (!client.isConnected)
        {
            serverIP = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20),serverIP,25);
            portNumber = GUI.TextField(new Rect(Screen.width - 110, 50, 100, 20), portNumber, 25);
            if (GUI.Button(new Rect(10, 10, 60, 50), "Connect"))
            {
                Connect();                
            }
        }
    }

    void Start ()
    {
        playerDeck = GetComponent<PlayerDeck>();
        client = new NetworkClient();
        client.RegisterHandler(999, GetMessageFromServer);
    }

   
    void Connect()
    {
        int port;
        if (int.TryParse(portNumber, out port))
        client.Connect(serverIP, port);
    }

    void SetClientID(string number)
    {
        if (int.TryParse(number, out clientID)) { }
    }

    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 1 + "|" + clientID + "|" + hDelta + "|" + vDelta;
            client.Send(messageNumber, msg);
        }
    }

    static public void SendButtonInfo(int pressed, int buttonID)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 2 + "|" + clientID + "|" + pressed + "|" + buttonID;
            client.Send(messageNumber, msg);
        }
    }

    static public void SendCardInfo(int cardID)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 4 + "|" + clientID + "|" +  cardID;
            client.Send(messageNumber, msg);
        }
    }

    public void GetMessageFromServer(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        if (deltas[0] == "0")
        {
            SetClientID(deltas[1]);
        }
        else if (deltas[0] == "1")
        {
            List<int> cardHand = new List<int>();
            for (int i = 1; i < deltas.Length; i++)
            {
                int temp;
                if (int.TryParse(deltas[i], out temp))
                {
                    cardHand.Add(temp);
                }

            }
            serverMessage = "cards: "+deltas[1]+" "+ deltas[2] + " " + deltas[3] + " " + deltas[4];
            playerDeck.SetDeck(cardHand);
        }
        else if(deltas[0]== "2")
        {
            int temp;
            if (int.TryParse(deltas[1], out temp))
            {
                playerDeck.DestroyCard(temp);
            }
        }
    }

}

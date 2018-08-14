using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;



public class NetworkServerUI : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    int playercount = 0;

    public string newPort;
    DeckManager deckManager;

    string playerDeckMsg;

    private void OnGUI()
    {
        string ipaddress = Network.player.ipAddress;

        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);

        newPort = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20), newPort, 25);

        if (GUI.Button(new Rect(Screen.width - 110, 30, 100, 60), "Update Port"))
        {
            int portNumber;
            if (int.TryParse(newPort, out portNumber))
                NetworkServer.Listen(portNumber);
        }
    }

    void Start ()
    {
        deckManager = GetComponent<DeckManager>();
        NetworkServer.Listen(2310);
        NetworkServer.RegisterHandler(999, RecieveMessage);
    }

    private void Update()
    {
        if (playercount < (NetworkServer.connections.Count - 1))
        {
            playercount = (NetworkServer.connections.Count - 1);
            switch (playercount)
            {
                case 1:
                    SendMessage(1, 0, 1);
                    deckManager.players[0].SetActive(true);
                    deckManager.players[0].GetComponent<PlayerStats>().playerID = 1;
                    player1 = deckManager.players[0];
                    break;
                case 2:
                    SendMessage(2, 0, 2);
                    deckManager.players[1].SetActive(true);
                    deckManager.players[1].GetComponent<PlayerStats>().playerID = 2;
                    player2 = deckManager.players[1];
                    break;
                case 3:
                    SendMessage(3, 0, 3);
                    deckManager.players[2].SetActive(true);
                    deckManager.players[2].GetComponent<PlayerStats>().playerID = 2;
                    player3 = deckManager.players[2];
                    break;
                case 4:
                    SendMessage(4, 0, 4);
                    deckManager.players[3].SetActive(true);
                    deckManager.players[3].GetComponent<PlayerStats>().playerID = 2;
                    player4 = deckManager.players[3];
                    break;
                default:
                    break;
            }
            
        }
    }

    // GET MESSAGE FROM CLIENT WITH FIRST ID
    // DELTAS[0] = Message ID ----------   DELTAS[1] = Player ID
    private void RecieveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        int messageID;
        int playerIDNum;

        if (int.TryParse(deltas[0], out messageID)) { }
        if (int.TryParse(deltas[1], out playerIDNum)) { }

        if (messageID == 1) //Joystick Input message
        {
            JoystickInput(playerIDNum, deltas[2], deltas[3]);
        }
        else if (messageID == 2) // Button input message
        {
            ButtonInput(playerIDNum, deltas[2], deltas[3]);
        }
        else if (messageID == 4)
        {
           
        }
    }

    void JoystickInput(int playerID, string horizontal, string vertical)
    {
        PlayerInput player = GetPlayerScript(playerID);
       //layer.axisH = (Convert.ToSingle(horizontal));
       //layer.axisV = (Convert.ToSingle(vertical));
        print("Axis information H: " + horizontal + " and V: " + vertical);
    }

    void ButtonInput(int playerID, string pressed, string buttonPressed)
    {
        PlayerInput player = GetPlayerScript(playerID);
        int theBool;
        int buttonID;
        if (int.TryParse(pressed, out theBool))
            if (int.TryParse(buttonPressed, out buttonID))

                switch (buttonID)
                {
                    case 1:
                        if (theBool == 1)
                        {
                            print("Button 1 pressed");
                            player.Button1(true);
                        }
                        else
                        {
                            print("Button 1 released");
                            player.Button1(false);
                        }

                        break;
                    case 2:
                        if (theBool == 1)
                        {
                            print("Button 2 pressed");
                            player.Button2(true);
                        }
                        else
                        {
                            print("Button 2 released");
                            player.Button2(false);
                        }

                        break;
                    case 3:
                        if (theBool == 1)
                        {
                            print("Button 3 pressed");
                            player.Button3(true);
                        }
                        else
                        {
                            print("Button 3 released");
                            player.Button3(false);
                        }

                        break;

                    default:
                        break;
                }
    }

    PlayerInput GetPlayerScript(int id)
    {
        switch (id)
        {
            case 1:
                return player1.GetComponent<PlayerInput>();
            case 2:
                return player2.GetComponent<PlayerInput>();
            case 3:
                return player3.GetComponent<PlayerInput>();
            case 4:
                return player4.GetComponent<PlayerInput>();
            default:
                break;
        }
        return null;
    }

    // SEND MESSAGE TO SPESIFIC CLIENT
    public void SendMessage(int playerID, int msgID, int msgInfo)
    {
        StringMessage msg = new StringMessage();
        msg.value = msgID + "|" + msgInfo;
        print(msgID + " " + msgInfo);
        NetworkServer.SendToClient(NetworkServer.connections[playerID].connectionId, 999, msg);
    }

    // SEND TO ALL CLIENTS
    void SendToAllClients()
    {
        StringMessage msg = new StringMessage();
        msg.value = 1 + "";
        NetworkServer.SendToAll(999, msg);
    }

    public void SendPlayerDeck(int playerID, int msgID, int[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            print("Cards to send: " + cards[i]);
        }
        GoThroughDeck(cards);
        StringMessage msg = new StringMessage();
        msg.value = msgID + "|" + playerDeckMsg;
        print(msgID + " " + playerDeckMsg);
        NetworkServer.SendToClient(NetworkServer.connections[playerID].connectionId, 999, msg);
        playerDeckMsg = "";
    }

    void GoThroughDeck(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            MakeMessage(array[i]);
        }
    }

    void MakeMessage(int info)
    {
        playerDeckMsg = playerDeckMsg + "|" + info;
    }
}

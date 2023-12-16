using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public Button roomBtn;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        JoinLobby();
    }

    void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        roomBtn.interactable = true;
    }

    public void CreateRoom()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        else
        {
            Debug.LogError("Room name cannot be empty!");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        LoadGameplayScene();
    }

    void LoadGameplayScene()
    {
        PhotonNetwork.LoadLevel("GameplayScene"); // Replace "GameplayScene" with the name of your gameplay scene
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }
}

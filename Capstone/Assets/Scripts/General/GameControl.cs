using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameControl : MonoBehaviourPunCallbacks
{
    public GameObject lessons;
    public GameObject gameMode;
    public GameObject blocker;
    public void SingleModeSelected()
    {
        gameMode.SetActive(false);
        lessons.SetActive(true);
    }    
    public void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
        blocker.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        blocker.SetActive(false);
        foreach (var playersName in PhotonNetwork.PlayerList)
        {
            Debug.Log(playersName + " is in the room");
        }
        SceneManager.LoadScene("MultiplayerGame");
    }
}

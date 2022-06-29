using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // Start is called before the first frame update
    LobbyManager manager;
    public Text roomName;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}

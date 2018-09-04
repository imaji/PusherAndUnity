using PusherClient;
using UnityEngine;

public class PusherManager : MonoBehaviour
{
    // A mutation of https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager

    public static PusherManager PusherManagerInstance = null;

    private Pusher _pusher;
    private Channel _channel;

    // Use this for initialization
    void Awake ()
    {
        if (PusherManagerInstance == null)
        {
            PusherManagerInstance = this;
            InitialisePusher();
        }
        else if (PusherManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void InitialisePusher()
    {
        if (_pusher == null)
        {
            _pusher = new Pusher("", new PusherOptions
            {
                // We'll use the autorised from the Client solution
                Authorizer = new HttpAuthorizer("http://localhost:8888/auth/Unity")
            });

            _pusher.Error += (s, e) =>
            {
                var i = 1;
            };
            _pusher.Connect();
            _channel = _pusher.Subscribe("TestChannel");
            _channel.Subscribed += (s) =>
            {
                var j = 2;
            };
        }
    }

    public void Message(string message)
    {
        var k = 1;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
        {
            //if (OnClicked != null)
            //    OnClicked();
        }
    }

    void OnApplicationQuit()
    {
        if (_pusher != null)
        {
            _pusher.Disconnect();
        }
    }
}

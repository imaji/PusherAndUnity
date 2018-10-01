using System;
using PusherClient;
using UnityEngine;

public class PusherManager : MonoBehaviour
{
    // A mutation of https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static PusherManager instance = null;

    private Pusher _pusher;
    private Channel _channel;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitialisePusher();
        Console.WriteLine("Starting");
    }

    private void InitialisePusher()
    {
        if (_pusher == null)
        {
            _pusher = new Pusher("<Your app key goes here>");

            _pusher.Error += OnPusherOnError;
            _pusher.ConnectionStateChanged += PusherOnConnectionStateChanged;
            _pusher.Connected += PusherOnConnected;

            _channel = _pusher.Subscribe("test-channel");
            _channel.Subscribed += OnChannelOnSubscribed;

            _pusher.Connect();
        }
    }

    private void PusherOnConnected(object sender)
    {
        // todo handle pusher connected
    }

    private void PusherOnConnectionStateChanged(object sender, ConnectionState state)
    {
        // todo handle connection state change
    }

    private void OnPusherOnError(object s, PusherException e)
    {
        // Todo handle an error
    }

    private void OnChannelOnSubscribed(object s)
    {
        // Todo handle channel subscribed too
    }

    public void Message(string message)
    {
        _channel.Trigger("time has occured", message);
    }

    void OnApplicationQuit()
    {
        if (_pusher != null)
        {
            _pusher.Disconnect();
        }
    }
}

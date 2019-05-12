using System;
using System.Threading.Tasks;
using PusherClient;
using UnityEngine;

public class PusherManager : MonoBehaviour
{
    // A mutation of https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static PusherManager instance = null;

    private Pusher _pusher;
    private Channel _channel;

    async Task Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        await InitialisePusher();
        Console.WriteLine("Starting");
    }

    private async Task InitialisePusher()
    {
        if (_pusher == null)
        {
            //Environment.SetEnvironmentVariable("PREFER_DNS_IN_ADVANCE", "true");

            _pusher = new Pusher("<Your app key goes here>");

            _pusher.Error += OnPusherOnError;
            _pusher.ConnectionStateChanged += PusherOnConnectionStateChanged;
            _pusher.Connected += PusherOnConnected;

            _channel = await _pusher.SubscribeAsync("test-channel");
            _channel.Subscribed += OnChannelOnSubscribed;

            await _pusher.ConnectAsync();
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
        _channel?.Trigger("time has occured", message);
    }

    async Task OnApplicationQuit()
    {
        if (_pusher != null)
        {
            await _pusher.DisconnectAsync();
        }
    }
}

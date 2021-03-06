using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVSBluetooth;
using UnityEngine.SceneManagement;

public class NetworkCoordinator : MonoBehaviour
{
    public static NetworkCoordinator instance;

    public List<int> activeBlockIds = new List<int>();

    private void OnEnable()
    {
        BluetoothForAndroid.ReceivedByteMessage += GetMessage;
        BluetoothForAndroid.DeviceDisconnected += EndGame;
    }

    private void OnDisable()
    {
        BluetoothForAndroid.ReceivedByteMessage -= GetMessage;
        BluetoothForAndroid.DeviceDisconnected -= EndGame;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < 90; i++)
        {
            activeBlockIds.Add(i);
        }
    }

    void GetMessage(byte[] message)
    {
        switch ((int)message[0])
        {
            // Start of game data.
            case 0:
                GameCoordinator.instance.UpdateStartData(message);
                break;
            // Block data.
            case 1:
                GameCoordinator.instance.UpdateBlockData(message);
                break;
            // Ball data.
            case 2:
                GameCoordinator.instance.UpdateBallData(message);
                break;
            // Paddle data.
            case 3:
                GameCoordinator.instance.UpdatePaddleData(message);
                break;
            default:
                break;
        }
    }

    public void WriteMessage(byte[] message)
    {
        BluetoothForAndroid.WriteMessage(message);
    }

    public void Disconnect()
    {
        BluetoothForAndroid.Disconnect();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Win");
    }
}

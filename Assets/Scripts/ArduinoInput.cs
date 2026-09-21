using System.IO.Ports;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    public static Vector2 Move;
    public static bool Click;
    public string portName = "COM3";

    SerialPort port;
    bool held;

    void Start()
    {
        port = new SerialPort(portName, 9600) { ReadTimeout = 10 };
        port.Open();
    }

    void Update()
    {
        Click = false;
        try
        {
            string[] p = port.ReadLine().Trim().Split(',');
            Move = new Vector2(-int.Parse(p[0]), int.Parse(p[1])); bool now = p[2] == "1";
            Click = now && !held;
            held = now;
        }
        catch { }
    }
}
using System.IO.Ports;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    public static Vector2 Move;
    public static bool Click;
    public static bool Left;
    public static bool Right;
    public string portName = "COM3";

    SerialPort port;
    bool lastClick;
    bool lastLeft;
    bool lastRight;

    void Start()
    {
        port = new SerialPort(portName, 9600);
        port.ReadTimeout = 10;
        port.Open();
    }

    void Update()
    {
        Click = false;
        Left = false;
        Right = false;

        try
        {
            string[] p = port.ReadLine().Trim().Split(',');

            Move = new Vector2(-int.Parse(p[0]), int.Parse(p[1]));

            bool clickNow = p[2] == "1";
            bool leftNow = p[3] == "1";
            bool rightNow = p[4] == "1";

            Click = clickNow && !lastClick;
            Left = leftNow && !lastLeft;
            Right = rightNow && !lastRight;

            lastClick = clickNow;
            lastLeft = leftNow;
            lastRight = rightNow;
        }
        catch { }
    }
}
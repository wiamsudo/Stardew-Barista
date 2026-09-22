using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    Rigidbody2D rb;
    Animator anim;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("MoveY", -1);
    }

    void Update()
    {
        input = ArduinoInput.Move;

        if (input == Vector2.zero)
            input = KeyboardInput();

        input = input.normalized;

        if (input != Vector2.zero)
        {
            anim.SetFloat("MoveX", input.x);
            anim.SetFloat("MoveY", input.y);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    Vector2 KeyboardInput()
    {
        Key[] keys = { Key.W, Key.S, Key.A, Key.D };
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        Vector2 direction = Vector2.zero;

        for (int i = 0; i < keys.Length; i++)
            if (Keyboard.current[keys[i]].isPressed)
                direction += directions[i];

        return direction;
    }
}
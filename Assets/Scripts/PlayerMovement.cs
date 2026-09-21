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
        var k = Keyboard.current;
        input = ArduinoInput.Move;

        if (input == Vector2.zero && k != null)
            input = new Vector2(k.dKey.isPressed ? 1 : k.aKey.isPressed ? -1 : 0,
                                k.wKey.isPressed ? 1 : k.sKey.isPressed ? -1 : 0);

        input = input.normalized;
        if (input == Vector2.zero) return;

        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
    }

    void FixedUpdate() => rb.linearVelocity = input * moveSpeed;
}
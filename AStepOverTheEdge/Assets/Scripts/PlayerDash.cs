using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashCooldown = 1f;

    private Rigidbody rb;
    private float nextDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && Time.time > nextDashTime)
        {
            Dash();
        }
    }

    void Dash()
    {
        nextDashTime = Time.time + dashCooldown;

        Vector3 dashDirection = transform.forward;
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }
}
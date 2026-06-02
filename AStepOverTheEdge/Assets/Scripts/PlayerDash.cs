using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody rb;
    private PlayerMovement movement;

    private bool isDashing;
    private float nextDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && Time.time > nextDashTime)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;

        movement.enabled = false; // stop movement overriding velocity

        Vector3 dashDir = transform.forward;

        rb.linearVelocity = Vector3.zero; // clean current movement
        rb.AddForce(dashDir * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashTime);

        movement.enabled = true;
        isDashing = false;
    }
}
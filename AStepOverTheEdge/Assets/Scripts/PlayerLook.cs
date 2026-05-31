using UnityEngine;
using UnityEngine.InputSystem;

public class  PlayerLook : MonoBehaviour  
{

	public static PlayerLook Instance;

	public float mouseSensitivity = 200f; 
	public Transform cam;

	private float xRotation = 0f;
	private Vector2 lookInput;

	private float shakeDuration = 0f;
	private float shakeMagnitude = 0.1f;
	private float shakeFadeSpeed = 1.5f;
	private Vector3 initalCamPos;

	private void Awake()
	{
		Instance = this;

        initalCamPos = cam.localPosition;

    }
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

	}

	void Update()
	{
		HandleMouseLook();
		HandleShake();
	}

	public void OnLook(InputValue value)
	{
		lookInput = value.Get<Vector2>();
	}

	void HandleMouseLook()
	{ 
		float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime; 
		float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 90);

		cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		transform.Rotate(Vector3.up * mouseX);
	}

	void HandleShake()
	{
		if (shakeDuration > 0f)
		{
			cam.localPosition = initalCamPos + Random.insideUnitSphere;
			shakeDuration -= Time.deltaTime * shakeFadeSpeed;
		}
		else
		{
			cam.localPosition = initalCamPos;
		}
	}

	public void AddShake(float duration, float magnitude)
	{
		shakeDuration = duration;
		shakeMagnitude = magnitude;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public event System.Action OnReachedEndOfLevel;

	public float speed = 7;
	public float smoothSpeed = .1f;
	public float turnSpeed = 8;

	float smoothedMagnitude;
	float smoothVelocity;
	float angle;
	Vector3 velocity;

	Rigidbody myRigidbody;
	bool disabled;

	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		Guard.OnGuardHasSpottedPlayer += Disable;
	}

	private void Update()
	{
		Vector3 inputDirection = Vector3.zero;
		if (!disabled)
		{ inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; }
		float inputMagnitude = inputDirection.magnitude;
		smoothedMagnitude = Mathf.SmoothDamp(inputMagnitude, smoothedMagnitude, ref smoothVelocity, smoothSpeed);
		float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
		angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
		velocity = transform.forward * speed * smoothedMagnitude;
		
	}

	private void FixedUpdate()
	{
		myRigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
	}

	private void Disable()
	{
		disabled = true;
	}

	private void OnDestroy()
	{
		Guard.OnGuardHasSpottedPlayer -= Disable;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Finish")
		{
			disabled = true;
			if(OnReachedEndOfLevel != null)
			{
				OnReachedEndOfLevel();
			}
		}
	}
}



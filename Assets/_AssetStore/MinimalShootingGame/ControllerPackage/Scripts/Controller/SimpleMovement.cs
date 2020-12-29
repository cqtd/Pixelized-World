using UnityEngine;

namespace MinimalShooting.ControllerPackage
{
	public class SimpleMovement : MonoBehaviour
	{
		// Player movement speed by joystick.
		[SerializeField] private float joystickMovementSpeed;

		// Player movement speed by keyboard.
		[SerializeField] private float keyboardMovementSpeed;


		// Private variables.
		private Vector3 firstTouchDistance;


		private void Update()
		{
			Movement();
		}


		private void JoystickMovement()
		{
			Vector3 velocity = GameControllerSetting.Instance.joystick.InputVector * joystickMovementSpeed;
			transform.position = transform.position + Time.deltaTime * velocity;

			if (GameControllerSetting.Instance.lookAtByDirection)
			{
				if (velocity.magnitude > 0)
				{
					Quaternion to = Quaternion.LookRotation(velocity.normalized);
					transform.localRotation = Quaternion.Slerp(transform.localRotation, to,
						Time.deltaTime * GameControllerSetting.Instance.turnSpeed);
				}
			}
		}


		private void HorizontalJoystickMovement()
		{
			Vector3 targetPosition = GameControllerSetting.Instance.horizonJoystick.InputVector;
			Vector3 myPosition = transform.position;

			targetPosition.y = myPosition.y;
			targetPosition.z = myPosition.z;

			if (GameControllerSetting.Instance.allowTeleport)
			{
				myPosition.x = targetPosition.x;
				transform.position = myPosition;
			}
			else
			{
				Vector3 velocity = Vector3.zero;

				if ((targetPosition - myPosition).magnitude < 0.1f)
				{
					velocity = Vector3.zero;
				}
				else
				{
					velocity = (targetPosition - myPosition).normalized * joystickMovementSpeed;
				}

				if (GameControllerSetting.Instance.horizonJoystick.isStop)
				{
					velocity = Vector3.zero;
				}

				transform.position = transform.position + Time.deltaTime * velocity;
			}
		}


		private void TouchAndDragMovement()
		{
			if (GameControllerSetting.Instance.touchDragController.touchState ==
			    TouchDragController.TouchState.PointerDown)
			{
				Vector3 touchPosition = GameControllerSetting.Instance.touchDragController.touchPosition;
				Vector3 myPosition = transform.position;
				myPosition.y = 0;

				firstTouchDistance = myPosition - touchPosition;
			}
			else if (GameControllerSetting.Instance.touchDragController.touchState ==
			         TouchDragController.TouchState.PointerDrag)
			{
				Vector3 input = GameControllerSetting.Instance.touchDragController.InputVector;
				Vector3 myPosition = transform.position;
				Vector3 newPosition = input + firstTouchDistance;

				if (GameControllerSetting.Instance.onlyXMovement)
				{
					newPosition.z = myPosition.z;
				}

				transform.position = newPosition;
			}
		}


		private void KeyboardMovement()
		{
			// Make the speed same for each direction straight and diagonal.
			Vector3 input = Vector3.ClampMagnitude(GameControllerSetting.Instance.keyboardController.InputVector, 1.0f);
			Vector3 velocity = input * keyboardMovementSpeed;
			transform.position += Time.deltaTime * velocity;
		}


		private void Movement()
		{
			switch (GameControllerSetting.Instance.controllerType)
			{
				case ControllerType.Keyboard:
					KeyboardMovement();
					break;

				case ControllerType.VirtualJoystick:
					JoystickMovement();
					break;

				case ControllerType.VirtualJoystickHorizontal:
					HorizontalJoystickMovement();
					break;

				case ControllerType.TouchAndDrag:
					TouchAndDragMovement();
					break;
			}

			ClampBoundary();
		}


		private void ClampBoundary()
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -5.0f, 5.0f);
			pos.z = Mathf.Clamp(pos.z, -9.5f, 9.5f);
			transform.position = pos;
		}
	}
}
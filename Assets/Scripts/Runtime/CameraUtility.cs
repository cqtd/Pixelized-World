using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pixelo
{
	public class CameraUtility : MonoBehaviour
	{
		[SerializeField] private Transform m_target = default;
		[SerializeField] private float m_lerp = default;
		
		private void LateUpdate()
		{
			// var pos = m_target.transform.position;
			// pos.z = -10;
			// transform.position = pos;
		}
	}
}
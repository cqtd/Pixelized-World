using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelo.UI
{
	[DefaultExecutionOrder(Constant.Order.UI)]
	public class DashUI : MonoBehaviour
	{
		public Image cooldown;
		public TextMeshProUGUI text;


		private void Awake()
		{
			Game.instance.onDashCooldown += OnDashCooldown;
		}

		private void OnDashCooldown()
		{
			StartCoroutine(CooldownCoroutine());
		}

		IEnumerator CooldownCoroutine()
		{
			text.alpha = 0.2f;
			cooldown.fillAmount = 0f;
			
			float value = 0f;
			while (value < 2.0f)
			{
				cooldown.fillAmount = value / 2.0f;
				
				value += Time.deltaTime;
				yield return null;
			}

			cooldown.fillAmount = 1.0f;
			text.alpha = 1.0f;
		}
	}
}
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pixelo.UI
{
	[DefaultExecutionOrder(Constant.Order.UI)]
	public class GameOverUI : MonoBehaviour
	{
		public CanvasGroup group;
		public float fadeSpeed = 5f;
		public Button button;
		public TextMeshProUGUI scoreValueText = default;
		public Image bonobono;
		public Image image;

		void Awake()
		{
			@group.alpha = 0;
			@group.interactable = false;
			@group.blocksRaycasts = false;
			
			bonobono.gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			scoreValueText.gameObject.SetActive(false);
			button.gameObject.SetActive(false);
		}
		
		private void Start()
		{
			Game.instance.onGameOver += OnGameOver;
		}

		private void OnGameOver()
		{
			scoreValueText.SetText($"에에엥? {Game.instance.score.Total}점???");
			StartCoroutine(FadeIn());
		}

		private IEnumerator FadeIn()
		{
			@group.alpha = 0;
			float value = 0f;
			while (value < 0.99f)
			{
				@group.alpha = value;
				
				value += Time.deltaTime * fadeSpeed;
				yield return null;
			}

			@group.alpha = 1;

			@group.interactable = true;
			@group.blocksRaycasts = true;
			
			yield return new WaitForSeconds(1.0f);
			
			scoreValueText.gameObject.SetActive(true);
			
			yield return new WaitForSeconds(1.0f);
			
			bonobono.gameObject.SetActive(true);
			yield return new WaitForSeconds(1.0f);
			
			image.gameObject.SetActive(true);
			yield return new WaitForSeconds(1.0f);
			
			button.gameObject.SetActive(true);
			
			button.onClick.AddListener(() =>
			{
				SceneManager.LoadScene(0);
			});
		}
	}
}
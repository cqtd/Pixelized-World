using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Pixelo.UI
{
	[DefaultExecutionOrder(Constant.Order.UI)]
	public class LevelUI : MonoBehaviour
	{
		public BlockDefinition[] definitions;
		
		[Serializable]
		public class Context
		{
			public Button button;
			public Image thumbnail;
			public TextMeshProUGUI name;
			public TextMeshProUGUI desc;
		}
		
		public CanvasGroup cg;
		public float fadeSpeed = 1f;
		
		public Context item1;
		public Context item2;
		public Context item3;

		private void Awake()
		{
			cg.alpha = 0;
		}

		private void Start()
		{
			Game.instance.experience.onLevelUp += OnLevelUp;
		}

		private void OnLevelUp()
		{
			Game.instance.Pause(true);

			var def1 = definitions[Random.Range(0, definitions.Length-1)];
			var def2 = definitions[Random.Range(0, definitions.Length-1)];
			var def3 = definitions[Random.Range(0, definitions.Length-1)];
			
			item1.name.SetText(def1.blockName);
			item1.desc.SetText(def1.blockDesc);
			item1.thumbnail.color = (def1.blockColor);
			item1.button.onClick.AddListener(() =>
			{
				Game.instance.ship.Attach(def1);
				Resume();
			});
			
			item2.name.SetText(def2.blockName);
			item2.desc.SetText(def2.blockDesc);
			item2.thumbnail.color = (def2.blockColor);
			item2.button.onClick.AddListener(() =>
			{
				Game.instance.ship.Attach(def2);
				Resume();
			});
			
			item3.name.SetText(def3.blockName);
			item3.desc.SetText(def3.blockDesc);
			item3.thumbnail.color = (def3.blockColor);
			item3.button.onClick.AddListener(() =>
			{
				Game.instance.ship.Attach(def3);
				Resume();
			});
			
			StartCoroutine(FadeIn());
		}

		IEnumerator FadeIn()
		{
			cg.alpha = 0;
			float value = 0f;
			while (value < 0.99f)
			{
				cg.alpha = value;
				
				value += Time.deltaTime * fadeSpeed;
				yield return null;
			}

			cg.alpha = 1;

			cg.interactable = true;
			cg.blocksRaycasts = true;
		}

		private void Resume()
		{
			cg.alpha = 0f;
			Game.instance.Pause(false);
			Game.instance.ship
				.SetInvincible(true);

			StartCoroutine(DisInvincible());
		}

		IEnumerator DisInvincible()
		{
			yield return new WaitForSeconds(1.0f);
			Game.instance.ship
				.SetInvincible(false);
		}
	}
}
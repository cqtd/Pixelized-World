using TMPro;
using UniRx;
using UnityEngine;

namespace Pixelo.UI
{
	[DefaultExecutionOrder(Constant.Order.UI)]
	public class ScoreUI : MonoBehaviour
	{
		public TextMeshProUGUI scoreText = default;
		
		private const string format = "SCORE\n{0:N0}";

		public void Start()
		{
			Game.instance.score
				.lifeTimeScore
				.AsObservable()
				.DistinctUntilChanged()
				.Subscribe(e =>
				{
					scoreText.SetText(string.Format(format, Game.instance.score.Total));
				})
				.AddTo(this.gameObject);
		}
	}
}
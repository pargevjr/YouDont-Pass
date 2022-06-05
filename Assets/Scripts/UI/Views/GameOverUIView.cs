using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views {
	public class GameOverUIView : MonoBehaviour {
		[SerializeField] private Button _homeButton;
		[SerializeField] private Button _restartButton;
		[SerializeField] private TextMeshProUGUI _currentScore;
		[SerializeField] private TextMeshProUGUI _highScore;

		public void Init(Action onGoHome, Action onRestart, string currentScore, string highScore) {
			_homeButton.onClick.RemoveAllListeners();
			_homeButton.onClick.AddListener(()=>onGoHome?.Invoke());
			_restartButton.onClick.RemoveAllListeners();
			_restartButton.onClick.AddListener(()=>onRestart?.Invoke());
			_currentScore.text = currentScore;
			_highScore.text = highScore;
		}

		public void Show(bool value) {
			gameObject.SetActive(value);
		}
	}
}
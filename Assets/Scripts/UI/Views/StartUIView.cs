using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views {
	public class StartUIView : MonoBehaviour {
		[SerializeField] private Button _startButton;
		[SerializeField] private TextMeshProUGUI _highScore;

		public void Init(Action onStartGame, string highScore) {
			_startButton.onClick.RemoveAllListeners();
			_startButton.onClick.AddListener(() => onStartGame?.Invoke());
			_highScore.text = highScore;
		}

		public void Show(bool value) {
			gameObject.SetActive(value);
		}
	}
}
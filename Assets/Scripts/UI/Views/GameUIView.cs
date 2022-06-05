using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Views {
    public class GameUIView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _healthLabel;
        [SerializeField] private TextMeshProUGUI _currentScoreLabel;
        [SerializeField] private GunButton[] _gunButtons;

        private Dictionary<GunType, Button> _buttonsDict = new();
        private bool _isInitialize;

        private void OnDisable() {
            foreach (var gunButton in _gunButtons) {
                gunButton.Button.interactable = false;
            }
        }

        public void Init(Action<GunType> onChangeGun) {
            if(_isInitialize) return;
            foreach (var gunButton in _gunButtons) {
                _buttonsDict.Add(gunButton.Type,gunButton.Button);
                gunButton.Button.onClick.RemoveAllListeners();
                gunButton.Button.onClick.AddListener(()=>onChangeGun(gunButton.Type));
            }
            _isInitialize = true;
        }
        public void SetButtonInteractable(GunType type) {
            _buttonsDict[type].interactable = true;
        }
        public void Show(bool value) {
            gameObject.SetActive(value);
        }
        public void SetHealth(int health) {
            _healthLabel.text = health.ToString();
        }
        public void SetCurrentScore(int score) {
            _currentScoreLabel.text = score.ToString();
        }
    }
    [Serializable]
    public struct GunButton {
        [SerializeField] private Button _button;
        [SerializeField] private GunType _type;

        public Button Button => _button;
        public GunType Type => _type;
    }

}

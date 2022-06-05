using System;

namespace Utils {
	public class Events {
		public static event Action<int> OnPlayerHealthChanged;
		public static event Action OnEnemyDown;
		public static event Action<GunType> OnSetGunType;
		public static event Action OnStartGame;
		public static event Action OnGameOver;
		public static event Action OnGoHome;

		public static void SetPlayerHealth(int health) {
			OnPlayerHealthChanged?.Invoke(health);
		}

		public static void SetGunType(GunType type) {
			OnSetGunType?.Invoke(type);
		}

		public static void StartGame() {
			OnStartGame?.Invoke();
		}

		public static void GameOver() {
			OnGameOver?.Invoke();
		}
		public static void EnemyDown() {
			OnEnemyDown?.Invoke();
		}

		public static void GoHome() {
			OnGoHome?.Invoke();
		}
	}
}

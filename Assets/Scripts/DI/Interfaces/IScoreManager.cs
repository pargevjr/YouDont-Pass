using UniRx;

namespace DI.Interfaces {
	public interface IScoreManager {
		public int HighScore { get; }
		public ReactiveProperty<int> CurrentScore { get; }
	}
}
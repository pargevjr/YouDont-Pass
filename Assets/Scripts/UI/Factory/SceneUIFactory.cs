using DI.Interfaces;
using UI.Views;

namespace UI.Factory {
	public class SceneUIFactory : ISceneUIFactory {
		private readonly StartUIView _startUIView;
		private readonly GameUIView _gameUIView;
		private readonly GameOverUIView _gameOverUIView;
		private readonly IPrefabFactory _prefabFactory;

		public SceneUIFactory(IPrefabFactory prefabFactory,
			StartUIView startUIView,
			GameUIView gameUIView,
			GameOverUIView gameOverUIView
		) {
			_startUIView = startUIView;
			_gameUIView = gameUIView;
			_gameOverUIView = gameOverUIView;
			_prefabFactory = prefabFactory;
		}

		public StartUIView CreateStartUIView() {
			return _prefabFactory.Create(_startUIView);
		}

		public GameUIView CreateGameUIView() {
			return _prefabFactory.Create(_gameUIView);
		}

		public GameOverUIView CreateGameOverUIView() {
			return _prefabFactory.Create(_gameOverUIView);
		}
	}
}
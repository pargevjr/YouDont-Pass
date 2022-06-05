using UI.Views;

namespace UI.Factory {
	public interface ISceneUIFactory {
		public StartUIView CreateStartUIView();
		public GameUIView CreateGameUIView();
		public GameOverUIView CreateGameOverUIView();
	}
}
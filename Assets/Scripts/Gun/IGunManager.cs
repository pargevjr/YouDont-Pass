using UniRx;

namespace Gun {
	public interface IGunManager {
		ReactiveProperty<int> Health { get; }
	}
}
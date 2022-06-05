using DI.Interfaces;
using Enemy.VIew;

namespace Enemy.Factory {
	public class EnemyFactory : IEnemyFactory {
		private readonly EnemyView _enemyView;
		private readonly IPrefabFactory _prefabFactory;

		public EnemyFactory(EnemyView enemyView, IPrefabFactory prefabFactory) {
			_enemyView = enemyView;
			_prefabFactory = prefabFactory;
		}

		public EnemyView CreateEnemy() {
			var enemy = _prefabFactory.Create(_enemyView);
			return enemy;
		}
	}
}
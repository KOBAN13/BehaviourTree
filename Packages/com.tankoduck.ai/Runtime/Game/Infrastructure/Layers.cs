using UnityEngine;

namespace Game.Infrastructure
{
    public class Layers
	{
		public const string Default = "Default";
		public const string Player = "Player";
		public const string Enemy = "Enemy";
		public const string Projectile = "Projectile";
		public const string LootBreakable = "LootBreakable";
		public const string Loot = "Loot";
		public const string Environment = "Environment";
		public const string Ground = "Ground";
		
		private static readonly Layer _playerLayer = new Layer(Player);
		private static readonly Layer _defaultLayer = new Layer(Default);
		private static readonly Layer _enemyLayer = new Layer(Enemy);
		private static readonly Layer _projectileLayer = new Layer(Projectile);
		private static readonly Layer _lootBreakableLayer = new Layer(LootBreakable);
		private static readonly Layer _lootLayer = new Layer(Loot);
		private static readonly Layer _environmentLayer = new Layer(Environment);
		private static readonly Layer _groundLayer = new Layer(Ground);
		
		
		public static int PlayerLayer => _playerLayer.Id;
		public static int DefaultLayer => _defaultLayer.Id;
		public static int EnemyLayer => _enemyLayer.Id;
		public static int ProjectileLayer => _projectileLayer.Id;
		public static int LootBreakableLayer => _lootBreakableLayer.Id;
		public static int LootLayer => _lootLayer.Id;
		public static int EnvironmentLayer => _environmentLayer.Id;
		public static int GroundLayer => _groundLayer.Id;

		private class Layer
		{
			private readonly string _name;

			private int? _id;

			public int Id
			{
				get
				{
					_id ??= LayerMask.NameToLayer(_name);
					return _id.Value;
				}
			}

			public Layer(string name)
			{
				_name = name;
			}
		}
	}
}
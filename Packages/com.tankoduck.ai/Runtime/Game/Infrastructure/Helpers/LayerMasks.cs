using UnityEngine;

namespace Game.Infrastructure.Helpers
{ 
	public class LayerMasks
	{
		private static readonly Mask _playerMask = new Mask(Layers.Player);
		private static readonly Mask _defaultMask = new Mask(Layers.Default);
		private static readonly Mask _enemyMask = new Mask(Layers.Enemy);
		private static readonly Mask _projectileMask = new Mask(Layers.Projectile);
		private static readonly Mask _lootBreakableMask = new Mask(Layers.LootBreakable);
		private static readonly Mask _lootMask = new Mask(Layers.Loot);
		private static readonly Mask _environmentMask = new Mask(Layers.Environment);
		private static readonly Mask _groundMask = new Mask(Layers.Ground);
		

		public static int Player => _playerMask.Value;
		public static int Default => _defaultMask.Value;
		public static int Enemy => _enemyMask.Value;
		public static int Projectile => _projectileMask.Value;
		public static int LootBreakable => _lootBreakableMask.Value;
		public static int Loot => _lootMask.Value;
		public static int Environment => _environmentMask.Value;
		public static int Ground => _groundMask.Value;

		private class Mask
		{
			private readonly string[] _layerNames;

			private int? _value;

			public Mask(params string[] layerNames)
			{
				_layerNames = layerNames;
			}

			public int Value
			{
				get
				{
					if (!_value.HasValue)
						_value = LayerMask.GetMask(_layerNames);
					return _value.Value;
				}
			}
		}
	}
}
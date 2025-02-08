using ME.BECS;
using ME.BECS.Bullets;
using ME.BECS.Players;
using ME.BECS.Views;
using SampleShooter.Views.Muzzles;
using UnityEngine;

namespace SampleShooter.Views.Player
{
    public class PlayerView : EntityView
    {
        [SerializeField] private PlayerMuzzlePointView _playerMuzzlePointView;
        public ViewSource ViewSource;

        protected override void OnInitialize(in EntRO ent)
        {
            Ent playerEntity = ent.GetEntity();
            Debug.Log($"{nameof(PlayerView) } initialized with root entity: {playerEntity}");

            if (playerEntity.Has<PlayerComponent>())
            {
                Debug.Log($"{nameof(PlayerView) } It's a player entity");
            }
        }
    }
}
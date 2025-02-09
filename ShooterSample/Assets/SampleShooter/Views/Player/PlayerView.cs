using ME.BECS;
using ME.BECS.Players;
using ME.BECS.Views;
using SampleShooter.Components.Player;
using SampleShooter.Views.Muzzles;
using UnityEngine;

namespace SampleShooter.Views.Player
{
    public class PlayerView : EntityView
    {
        [SerializeField] private PlayerMuzzlePointView _playerMuzzlePointView;

        protected override void OnInitialize(in EntRO ent)
        {
            Ent playerEntity = ent.GetEntity();
            Debug.Log($"{nameof(PlayerView) } initialized with root entity: {playerEntity}");

            if (!playerEntity.Has<PlayerComponent>())
            {
                Debug.LogError($"{nameof(PlayerView) } It's NOT a player entity");
                return;
            }
        }
    }
}
using ME.BECS.Views;
using UnityEngine;

namespace SampleShooter.Views.Muzzles
{
    public class PlayerMuzzlePointView : EntityView
    {
        [SerializeField] private Transform _muzzlePoint;

        public Transform MuzzlePoint => _muzzlePoint;
    }
}

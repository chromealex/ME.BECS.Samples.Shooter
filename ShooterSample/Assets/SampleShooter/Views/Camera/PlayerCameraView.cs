using ME.BECS;
using ME.BECS.Views;
using SampleShooter.Enums;
using UnityEngine;

namespace SampleShooter.Views.Camera
{
    public class PlayerCameraView : EntityView
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private GameCameraType _cameraType;

        protected override void OnInitialize(in EntRO ent)
        {
            Debug.Log($"On initialize {nameof(PlayerCameraView)}!");
        }
    }
}

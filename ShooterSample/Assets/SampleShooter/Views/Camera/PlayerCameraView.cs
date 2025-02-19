using ME.BECS;
using ME.BECS.Views;
using SampleShooter.Components.Camera;
using SampleShooter.Enums;
using SampleShooter.Systems.Input;
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
            ent.World.GetSystem<ReadInputSystem>().SetCamera(_camera);
        }

        protected override void OnUpdate(in EntRO entRO, float dt)
        {
            Ent playerCameraEntity = entRO.GetEntity();
            playerCameraEntity.Get<CameraRayComponent>().CameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
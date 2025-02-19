using ME.BECS;
using ME.BECS.Bullets;
using ME.BECS.Views;
using UnityEngine;

namespace SampleShooter.Views.Bullets
{
    public class BulletView : EntityView
    {
        [Header("Bullet Raycast")]
        [SerializeField] private Transform _bulletTransform;
        [SerializeField] private float _sphereRadius = 0.5f;
        [SerializeField] private float _maxDistance = 100.0f;
        
        private bool _hasGizmo;

        protected override void OnUpdate(in EntRO entity, float dt)
        {
            if (gameObject.activeSelf)
            {
                Vector3 direction = _bulletTransform.forward;
                Vector3 origin = _bulletTransform.position;
                
                
                if (Physics.SphereCast(origin, _sphereRadius, direction, out RaycastHit hit, _maxDistance))
                {
                    _hasGizmo = true;
                }
                else
                {
                    _hasGizmo = false;
                }
            }
        }

        //вызывается при любом изменении данных на компонненте
        protected override void ApplyState(in EntRO ent)
        {
            //animator.SetTrigger("Shoot");
            base.ApplyState(in ent);
        }

        void OnDrawGizmos()
        {
            if (!_hasGizmo)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_bulletTransform.position, _sphereRadius);
        }
    }
}
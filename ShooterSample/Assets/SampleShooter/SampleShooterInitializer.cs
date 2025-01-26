using ME.BECS;
using ME.BECS.Network;
using UnityEngine;

namespace SampleShooter
{
    public struct NetworkModuleComponent : IComponent
    {
        // public NetworkModule NetworkModule;
    }

    public class SampleShooterInitializer : WorldInitializer
    {
        protected override void Awake()
        {
            Debug.Log("Awake in SampleShooterInitializer");
            base.Awake();
            var networkModule = modules.Get<NetworkModule>();
            var networkModuleEntity = Ent.New(this.world);
            networkModuleEntity.Set(new NetworkModuleComponent(){
            });
        }
    }
}
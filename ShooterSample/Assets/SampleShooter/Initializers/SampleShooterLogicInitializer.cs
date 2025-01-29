using ME.BECS.Network;
using UnityEngine;

namespace SampleShooter.Initializers
{
    [DefaultExecutionOrder(-100)]
    public class SampleShooterLogicInitializer : NetworkWorldInitializer
    {
        public static SampleShooterLogicInitializer Instance;
        
        public NetworkModule GetNetworkModule() => Instance.networkModule;

        protected override void Awake() 
        {
            Instance = this;
            base.Awake();
        }
            
        protected override void DoWorldAwake() 
        {
            world.parent = Instance.world;
            base.DoWorldAwake();
        }
    }
}
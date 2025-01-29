using ME.BECS;
using UnityEngine;

namespace SampleShooter.Initializers
{
    [DefaultExecutionOrder(-80)]
    public class SampleShooterVisualInitializer : WorldInitializer
    {
        public static SampleShooterVisualInitializer Instance;
        
        protected override void Awake() 
        {
            Instance = this;
            base.Awake();
        }
            
        protected override void DoWorldAwake() 
        {
            world.parent = SampleShooterLogicInitializer.Instance.world;
            base.DoWorldAwake();
        }
    }
}
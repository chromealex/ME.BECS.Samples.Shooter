using ME.BECS;
using UnityEngine;

namespace SampleShooter.Systems.Input
{
    public struct InitInputSystem : IAwake
    {
        public Config InputConfig;
        public void OnAwake(ref SystemContext context)
        {
            Debug.Log($"{nameof(InitInputSystem)} started to work");
            
            // var inputEntity = Ent.New(in context);
            // InputConfig.Apply(in inputEntity);
            //todo add input settings
            
        }
    }
}
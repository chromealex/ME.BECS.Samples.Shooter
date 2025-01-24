using ME.BECS;
using UnityEngine;

namespace SampleShooter.Systems.Input
{
    public struct ReadInputSystem : IUpdate
    {
        public void OnUpdate(ref SystemContext context)
        {
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                Debug.Log("Keycode W is pressed");
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                Debug.Log("Keycode A is pressed");
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                Debug.Log("Keycode S is pressed");
            }
            
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                Debug.Log("Keycode D is pressed");
            }
        }
    }
}
using ME.BECS;
using ME.BECS.Views;
using UnityEngine;

namespace SampleShooter.Views.Level
{
    public class LevelView : EntityView
    {
        protected override void OnInitialize(in EntRO ent)
        {
            Debug.Log("On initialize level view!");
        }
    }
}
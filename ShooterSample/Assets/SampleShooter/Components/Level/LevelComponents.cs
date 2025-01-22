using ME.BECS;
using UnityEngine;

namespace SampleShooter.Components.Level
{
    public struct LevelComponent : IConfigComponent
    {
    }
    public struct LevelIdComponent : IConfigComponent
    {
        public int LevelId;
    }
    public struct LevelBugsAmountComponent : IConfigComponent
    {
        public int LevelBugsAmount;
    }
    public struct LevelStartPositionComponent : IConfigComponent
    {
        public Vector3 StartPosition;
    }
}
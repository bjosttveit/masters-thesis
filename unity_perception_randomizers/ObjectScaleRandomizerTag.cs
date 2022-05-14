using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[AddComponentMenu("Perception/RandomizerTags/Object Scale Randomizer Tag")]
[RequireComponent(typeof(Transform))]
public class ObjectScaleRandomizerTag : RandomizerTag {
    public FloatParameter ScalingFactor;
    public FloatParameter xFactor;
    public FloatParameter xzFactor;
}
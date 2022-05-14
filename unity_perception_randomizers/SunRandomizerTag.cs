using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;

[AddComponentMenu("Perception/RandomizerTags/Sun Randomizer Tag")]
[RequireComponent(typeof(Light))]
public class SunRandomizerTag : RandomizerTag {
    public FloatParameter Intensity;
    public ColorHsvaParameter Color;
}
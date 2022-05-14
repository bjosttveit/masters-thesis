using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[AddComponentMenu("Perception/RandomizerTags/Object Rotation Randomizer Tag")]
[RequireComponent(typeof(Transform))]
public class ObjectRotationRandomizerTag : RandomizerTag {
    public Vector3Parameter Rotation;
}
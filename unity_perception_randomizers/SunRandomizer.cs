using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;

[Serializable]
[AddRandomizerMenu("Perception/Sun Randomizer")]
public class SunRandomizer : Randomizer
{

    protected override void OnIterationStart()
    {
        var taggedObjects = tagManager.Query<SunRandomizerTag>();
        foreach (var taggedObject in taggedObjects)
        {
            var light = taggedObject.GetComponent<Light>();
            light.intensity = taggedObject.Intensity.Sample();
            light.color = taggedObject.Color.Sample();
        }
    }
}

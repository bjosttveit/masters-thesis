using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;

[Serializable]
[AddRandomizerMenu("Perception/Camera Randomizer")]
public class CameraRandomizer : Randomizer
{
    public FloatParameter Altitude;
    public FloatParameter Pitch;

    protected override void OnIterationStart()
    {
        var taggedObjects = tagManager.Query<CameraRandomizerTag>();
        foreach (var taggedObject in taggedObjects)
        {
            var transform = taggedObject.GetComponent<Transform>();

            transform.position = new Vector3(
                transform.position.x,
                Altitude.Sample(),
                transform.position.z
            );
            
            transform.rotation = Quaternion.Euler(new Vector3(
                Pitch.Sample(),
                transform.rotation.y,
                transform.rotation.z
            ));

        }
    }
}

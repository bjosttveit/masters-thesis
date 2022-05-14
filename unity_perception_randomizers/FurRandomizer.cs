using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;

[Serializable]
[AddRandomizerMenu("Perception/Fur Randomizer")]
public class FurRandomizer : Randomizer
{

    protected override void OnIterationStart()
    {
        var taggedObjects = tagManager.Query<FurRandomizerTag>();
        foreach (var taggedObject in taggedObjects)
        {
            var mesh = taggedObject.transform.Find("SK_Sheep_LOD0").GetComponent<SkinnedMeshRenderer>();
            var furTexture = taggedObject.Textures.Sample();
            mesh.materials[1].mainTexture = furTexture;
        }
    }
}

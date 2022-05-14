using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Samplers;

[Serializable]
[AddRandomizerMenu("Perception/Surface Placement Randomizer")]
public class SurfacePlacementRandomizer : Randomizer
{
    public Vector2Parameter SurfaceBounds;

    [Serializable]
    public struct ObjectSet
    {
        public IntegerParameter NumberOfObjects;
        public GameObjectParameter GameObjects;
    }

    [Serializable]
    public struct TreeSet
    {
        public IntegerParameter NumberOfClusters;
        public IntegerParameter ClusterSize;
        public FloatParameter ClusterSparsity;
        public GameObjectParameter PineTrees;
        public GameObjectParameter OakTrees;
    }

    public ObjectSet Rocks;
    public ObjectSet Sheep;
    public ObjectSet Animals;
    public TreeSet Trees;
    private List<GameObject> instances;
    private System.Random random;

    protected override void OnScenarioStart()
    {
        random = new System.Random(69);
        base.OnScenarioStart();
    }

    protected override void OnIterationStart()
    {
        instances = new List<GameObject>();

        var objectSets = new ObjectSet[] { Rocks, Sheep, Animals };

        foreach (var objectSet in objectSets)
        {

            var n = objectSet.NumberOfObjects.Sample();

            for (var i = 0; i < n; i++)
            {
                var o = objectSet.GameObjects.Sample();
                var p = getRandomPosition();
                instantiateObject(o, p);
            }
        }

        var nClusters = Trees.NumberOfClusters.Sample();
        var treeSets = new GameObjectParameter[] { Trees.PineTrees, Trees.OakTrees};

        var xMin = (SurfaceBounds.x as UniformSampler).range.minimum;
        var xMax = (SurfaceBounds.x as UniformSampler).range.maximum;
        var yMin = (SurfaceBounds.y as UniformSampler).range.minimum;
        var yMax = (SurfaceBounds.y as UniformSampler).range.maximum;

        for (var i = 0; i < nClusters; i++)
        {

            var t = random.Next(treeSets.Length);
            var nTrees = Trees.ClusterSize.Sample();
            var pCluster = getRandomPosition();

            for (var j = 0; j < nTrees; j++)
            {

                var r = nTrees * Math.Sqrt(random.NextDouble()) * Trees.ClusterSparsity.Sample();
                var theta = random.NextDouble() * 2 * Math.PI;
                var x = pCluster.x + r * Math.Cos(theta);
                var y = pCluster.z + r * Math.Sin(theta);

                if (x < xMin || x > xMax || y < yMin || y > yMax)
                {
                    continue;
                }

                var p = getSurfacePoint((float)x, (float)y);
                var o = treeSets[t].Sample();
                instantiateObject(o, p);
            }
        }

    }

    private Vector3 getSurfacePoint(float x, float y)
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(x, 100, y), Vector3.down, out hit);
        return hit.point;
    }

    private Vector3 getRandomPosition()
    {
        // Randomize position on surface
        var p = SurfaceBounds.Sample();
        RaycastHit hit;
        Physics.Raycast(new Vector3(p.x, 100, p.y), Vector3.down, out hit);
        return hit.point;
    }

    private void instantiateObject(GameObject o, Vector3 p)
    {
        // Randomize rotation
        var rotation = Vector3.zero;
        var rotationRandomizer = o.GetComponent<ObjectRotationRandomizerTag>();
        if (rotationRandomizer != null)
        {
            rotation = rotationRandomizer.Rotation.Sample();
        }

        // Create instance
        var instance = GameObject.Instantiate(o, p, Quaternion.Euler(rotation));

        // Randomize scale
        var scale = Vector3.one;
        var scaleRandomizer = o.GetComponent<ObjectScaleRandomizerTag>();
        if (scaleRandomizer != null)
        {
            scale *= scaleRandomizer.ScalingFactor.Sample();

            scale.x *= scaleRandomizer.xFactor.Sample();

            var xzFactor = scaleRandomizer.xzFactor.Sample();
            scale.x *= xzFactor;
            scale.z *= xzFactor;
        }
        instance.transform.localScale = scale;

        // Keep track of instances for removal
        instances.Add(instance);
    }

    protected override void OnIterationEnd()
    {
        foreach (var i in instances)
        {
            GameObject.Destroy(i);
        }
        instances = null;
        base.OnIterationEnd();
    }

    protected override void OnScenarioComplete()
    {
        random = null;
        base.OnScenarioComplete();
    }
}

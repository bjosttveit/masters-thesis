using System;
using System.Threading;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine;
using MapMagic.Terrains;

[Serializable]
[AddRandomizerMenu("Perception/Terrain Randomizer")]
public class TerrainRandomizer : Randomizer
{
    public IntegerParameter Seed;

    protected override void OnIterationStart()
    {
        var taggedObjects = tagManager.Query<TerrainRandomizerTag>();
        foreach (var taggedObject in taggedObjects)
        {
            var mapMagic = taggedObject.GetComponent<MapMagic.Core.MapMagicObject>();
            var graph = mapMagic.graph;
            var tile = mapMagic.tiles[0, 0];

            graph.defaults["Seed"] = Seed.Sample();
            tile.GenerateSync(graph, tile.main);

        }
        base.OnIterationStart();
    }

    /*
    public void GenerateSync(Graph graph, DetailLevel det) {
        det.stop = new StopToken();
        StopToken stop = det.stop;

        if (det.data == null) det.data = new TileData();
        det.data.area = new Area(coord, (int)mapMagic.tileResolution, mapMagic.tileMargins, mapMagic.tileSize);
        det.data.globals = mapMagic.globals;
        det.data.random = graph.random;
        det.data.isPreview = mapMagic.PreviewData==det.data;
        det.data.isDraft = false;

        det.generateStarted = true;
        det.applyReady = false;
        det.generateReady = false;

        var tile = this;
        
        OnBeforeTileGenerate?.Invoke(tile, det.data, stop);

        //do not return (for draft) until the end (apply)
        //				if (!stop.stop) graph.CheckClear(det.data, stop);
        if (!stop.stop) graph.Generate(det.data, stop);
        if (!stop.stop) graph.Finalize(det.data, stop);

        //finalize event
        OnTileFinalized?.Invoke(tile, det.data, stop);
            
        //flushing products for playmode (all except apply)
        if (MapMagicObject.isPlaying)
            det.data.Clear(clearApply:false, inSubs:true);

        //welding (before apply since apply will flush 2d array)
        if (!stop.stop) Weld.ReadEdges(det.data, det.edges);
        if (!stop.stop) Weld.WeldEdgesInThread(det.edges, tile.mapMagic.tiles, tile.coord, det.data.isDraft);
        if (!stop.stop) Weld.WriteEdges(det.data, det.edges);

        ApplyNow(det,stop);
        
        det.generateReady = true;
    }
    */
}

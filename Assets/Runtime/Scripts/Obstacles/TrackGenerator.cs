using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] TrackSegment[] segmentsPrefab;
    List<TrackSegment> currentSegments = new List<TrackSegment>();

    void Start()
    {
        TrackSegment initialTrack = Instantiate(segmentsPrefab[0], transform);
        currentSegments.Add(initialTrack);

        TrackSegment previousTrack = initialTrack;
        foreach (var trackPrefab in segmentsPrefab)
        {
            TrackSegment trackInstance = Instantiate(trackPrefab, transform);
            trackInstance.transform.position = previousTrack.End.position + (trackInstance.transform.position - trackInstance.Start.position);


            currentSegments.Add(initialTrack);
            previousTrack = trackInstance;
        }
    }
}

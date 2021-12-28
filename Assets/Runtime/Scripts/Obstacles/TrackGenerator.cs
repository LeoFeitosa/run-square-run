using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TrackSegment segmentPrefabEasy;
    [SerializeField] TrackSegment segmentPrefabMedium;
    [SerializeField] TrackSegment segmentPrefabHard;
    [Range(0, 1)]
    [SerializeField] float hardTackChance = 0.1f;
    [SerializeField] TrackSegment segmentPrefabInitial;
    [SerializeField] int initialTrackCount = 5;
    [SerializeField] int minTracksInFrontPlayer = 2;
    [SerializeField] float distanceToCosiderTack = 3;
    List<TrackSegment> currentSegments = new List<TrackSegment>();

    void Start()
    {
        SpawnTrackSegment(segmentPrefabInitial, null);
        SpawnTracks(initialTrackCount);
    }

    void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0 ? currentSegments[currentSegments.Count - 1] : null;

        for (int i = 0; i < trackCount; i++)
        {
            TrackSegment track = GetRandomSegment();
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }

    TrackSegment GetRandomSegment()
    {
        TrackSegment track = Random.value < hardTackChance
        ? segmentPrefabHard
        : Random.value < 0.5f
        ? segmentPrefabMedium
        : segmentPrefabEasy;

        return track;
    }

    TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = Instantiate(track, transform);

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.End.position + (trackInstance.transform.position - trackInstance.Start.position);
        }
        else
        {
            trackInstance.transform.localPosition = Vector2.zero;
        }

        currentSegments.Add(trackInstance);

        return trackInstance;
    }

    void Update()
    {
        int playerTrackIndex = GetTrackWithPlayer();
        CreateTrackInFront(playerTrackIndex);
        DestroyTrackInBack(playerTrackIndex);
    }

    int GetTrackWithPlayer()
    {
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];

            if (player.transform.position.x >= (track.Start.position.x + distanceToCosiderTack) &&
                player.transform.position.x <= track.End.position.x)
            {
                return i;
            }
        }
        return -1;
    }

    void CreateTrackInFront(int playerTrackIndex)
    {
        int tracksInFrontPlayer = currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontPlayer < minTracksInFrontPlayer)
        {
            SpawnTracks(minTracksInFrontPlayer - tracksInFrontPlayer);
        }
    }

    void DestroyTrackInBack(int playerTrackIndex)
    {
        if (playerTrackIndex >= 0)
        {
            for (int i = 0; i < playerTrackIndex; i++)
            {
                TrackSegment track = currentSegments[i];
                Destroy(track.gameObject);
            }

            currentSegments.RemoveRange(0, playerTrackIndex);
        }
    }
}

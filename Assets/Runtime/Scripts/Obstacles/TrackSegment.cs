using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    public Transform Start => start;
    public Transform End => end;
}

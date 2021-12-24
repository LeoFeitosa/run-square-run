using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] GameObject[] tracks;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    public Transform Start => start;
    public Transform End => end;
}

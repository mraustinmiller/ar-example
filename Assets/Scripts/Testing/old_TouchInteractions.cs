using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class old_TouchInteractions : MonoBehaviour {

    public GameObject childObject = null;
    public Transform hitTransform = null;

    [SerializeField] ARRaycastManager raycastManager = null;

    static List<ARRaycastHit> hits = new List<ARRaycastHit> ();

    void Start () {
        if (childObject != null) {
            childObject.SetActive (false);
        }
    }

    void Update () {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                OnScreenTouch (touch);
            }
        }
    }

    void OnScreenTouch (Touch touch) {
        if (Input.touchCount == 1 && hitTransform != null) {
            Touch thisTouch = Input.GetTouch (0);

            if (thisTouch.phase == TouchPhase.Began || thisTouch.phase == TouchPhase.Moved) {
                if (raycastManager.Raycast (touch.position, hits, TrackableType.PlaneWithinPolygon)) {
                    if (childObject != null && !childObject.activeInHierarchy) {
                        // new view activate
                        childObject.SetActive (true);
                    }

                    // position object accordingly
                    Pose hitPose = hits[0].pose;
                    hitTransform.position = hitPose.position;
                }
            }
        }
    }
}

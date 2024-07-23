using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using TMPro;
public class PlaceManager : MonoBehaviour
{
    
    private ARRaycastManager raycastManager;
    public GameObject indicator;
    public GameObject objectToPlace;
    public GameObject placedObject;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        if (raycastManager == null)
        { Debug.Log("AR raycast manager not found"); return; }
        indicator.transform.GetChild(0);
        //indicator.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            indicator.transform.position = hitPose.position;
            indicator.transform.rotation = hitPose.rotation;

            

        }

    }

    public void placeButton()               //when the user presses the   button this  function gets called
    {
        if (placedObject == null)
        { placedObject = Instantiate(objectToPlace, indicator.transform.position, indicator.transform.rotation); }

        else
        {
            placedObject.transform.position = indicator.transform.position;
            placedObject.transform.rotation = indicator.transform.rotation;
        }

    }

    public TMP_Text statusText;

    public void LoadSaved()     //rerouting function for the coroutine
    {
        StartCoroutine(FindAndPlaceAnchor());
    }
    GameObject anchorObject;
    public IEnumerator FindAndPlaceAnchor()
    {
        while (true)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                var hitPose = hits[0].pose;
                if (anchorObject == null)           //no double instantiations
                     { anchorObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation); }
                statusText.text = "Anchor instantiated";
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void Load_intro()
    {
        if (placedObject != null)
        { Destroy(placedObject); }
    }

    public void Check_intro()
    {
        if (anchorObject != null)
        { Destroy(anchorObject); }
    }

}

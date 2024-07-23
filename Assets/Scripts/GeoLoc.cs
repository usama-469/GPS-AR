using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Firebase.Database;

public class GeoLoc : MonoBehaviour
{
    [Serializable]
    public class datatToSave
    {
        public float Lati;
        public float Longi;

        public datatToSave(float lati, float longi)
        {
            Lati = lati;
            Longi = longi;
        }
    }


    public float Latitude, Longitude, Altitude;         //current values
    public double distance;
    private Vector3 newPosition, OriginalPosition;
    public float Radius = 5.0f;   // range of tracking start
    public float time_update = 3.0f;    //update time between the gps signal
    public string newLat, newLong, newAlt;      //string representation of these values 
    public TMP_Text T_lat, T_long, T_alt, T_dist;       //to display on the screen
    public TMP_Text TrackingMode; //tracking indicator

    //COMPARISON ONES
    public float F_lat, F_lon, F_alt;           //to be saved on firebase
    public bool loading = false;

    //DB stuff
    public datatToSave dts;
    public DatabaseReference dbRef;

    void Start()
    {

        
        //call and try to connect to satellite
        Input.location.Start(1, 1);

        StartCoroutine(GPSprocess());
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public IEnumerator GPSprocess()
    {
        while (true)    //infinite loop---till break
        {
            yield return new WaitForSeconds(time_update);

            if (Input.location.isEnabledByUser == true)
            {
                Input.location.Start(1, 1);

                Latitude = Input.location.lastData.latitude;
                newLat = Latitude.ToString();
                T_lat.text = "Latitude:     " + newLat;

                Longitude = Input.location.lastData.longitude;
                newLong = Longitude.ToString();
                T_long.text = "Longitude:     " + newLong;

                Altitude = Input.location.lastData.altitude;
                newAlt = Altitude.ToString();
                T_alt.text = "Altitude:     " + newAlt;

                if (loading == true) { Calc(loadedDat.Lati, loadedDat.Longi, Latitude, Longitude); }       //replace the F values with the cloud ones and wait till they appear
            }

            if (Input.location.isEnabledByUser == false)
            {
                Input.location.Start();

                T_lat.text = "open the location services";

                T_long.text = "open the location services";

                T_alt.text = "open the location services";
            }
        }

    }

    public void Calc(float lat1, float lon1, float lat2, float lon2)            //TARGET vs current geopositions
    {
        var R = 6378.137; // Radius of earth in KM
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
            Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
            Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        distance = R * c;
        distance = distance * 1000f;
        float distanceFloat = (float)distance;
        newPosition = OriginalPosition - new Vector3(0, 0, distanceFloat * 12);

        T_dist.text = "Distance:    " + distance.ToString();

        if (distance < Radius)      //inside bounds
        {
            //do something
            TrackingMode.text = "Tracking";

            placemanager.LoadSaved();           //trigger the function from other script
        }

        if (distance > Radius)          //outside bounds
        {
            //do something
            TrackingMode.text = "Not Tracking";
        }
    }

    public PlaceManager placemanager;
    public Vector3 F_pos;
    public Quaternion F_rot;
    public void SaveLoc()            //save these values   and broadcast them to the firebase DB
    {
        loading = false;

        F_lat = Latitude;
        F_lon = Longitude;
        F_alt = Altitude;

        if (placemanager.placedObject != null)
        {
            F_pos = placemanager.placedObject.transform.position;      //get the pos and rot of the indicator at the time of save
            F_rot = placemanager.placedObject.transform.rotation;
        }

        //function to send to a DB      F_ values
        datatToSave data = new datatToSave(F_lat, F_lon);

        string json = JsonUtility.ToJson(data);

        
        dbRef.Child("geo Data").SetRawJsonValueAsync(json);
    }

    public void LoadLoc()
    {
        //load the data from the cloud
        //once loaded, trigger the calculations 
        //loading = true;

        StartCoroutine(LoadData());
        //placemanager.LoadSaved();           //trigger the function from other script
    }



    public void introScreen()
    { loading = false ; 
      
    }
    public datatToSave loadedDat;
    IEnumerator LoadData()
    {
        var serverData = dbRef.Child("geo Data").GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);
        Debug.Log("Data loading completed successfully");

        DataSnapshot snapshot = serverData.Result;
        String jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            Debug.Log("-------------------------Sever data found");
            loadedDat = JsonUtility.FromJson<datatToSave>(jsonData);
            loading = true;  //success triggers this

            loadedDataShow(loadedDat.Lati.ToString(), loadedDat.Longi.ToString());
        }
        else
        { Debug.Log("---------------------------Server data not found"); }
        
    }
    public TMP_Text loadLA, loadLo;
    public void loadedDataShow(string la, string lo)
    {
        loadLA.text = "Loaded Latitude:  " + la;
        loadLo.text = "Loaded LOngitude:  " + lo;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;

[System.Serializable]
public class pnMessage
{
    //public int atStation;
    public bool atStationStatus;
    public int status;
}



public class collisionDetec : MonoBehaviour
{

    public static PubNub dataServer;
    public string pubKey = "pub-c-8efd8f87-9fe2-45a5-81f6-7b60513f5ddc";
    public string subKey = "sub-c-43574d8c-135b-11e9-abd1-2a488504b737";
    public string channelName = "train";


    //public int atStationStatus;
    //public bool station;
    public bool atStation;

    void Start()
    {
        PNConfiguration connectionSettings = new PNConfiguration();
        connectionSettings.PublishKey = pubKey;
        connectionSettings.SubscribeKey = subKey;
        connectionSettings.LogVerbosity = PNLogVerbosity.BODY;
        connectionSettings.Secure = true;

        dataServer = new PubNub(connectionSettings);


        dataServer.Subscribe()
          .Channels(new List<string>() { channelName })
          .Execute();


    }



    void OnTriggerEnter(Collider colSend)
    {
        //atStationStatus++;
        //station = true;
        atStation = true;

        pnMessage newMessage = new pnMessage();
        //newMessage.atStation = atStationStatus;
        newMessage.atStationStatus = atStation;
        newMessage.status = 1;
      

        /*
        dataServer.Publish()
        .Channel(channelName)
        .Message(newMessage)
        .Async((result, status) =>
        {
            if (!status.Error)
            {
                Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                Debug.Log(atStation);
            }
            else
            {
                Debug.Log(status.Error);
                Debug.Log(status.ErrorData.Info);
            }
        });
        */

    }

    void OnTriggerExit(Collider colSend)
    {
        //atStationStatus--;
        //station = false;
        atStation = false;

        pnMessage newMessage = new pnMessage();
        //newMessage.atStation = atStationStatus;
        newMessage.atStationStatus = atStation;
        newMessage.status = 0;
        /*
        dataServer.Publish()
        .Channel(channelName)
        .Message(newMessage)
        .Async((result, status) =>
        {
            if (!status.Error)
            {
                Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                Debug.Log(atStation);

            }
            else
            {
                Debug.Log(status.Error);
                Debug.Log(status.ErrorData.Info);
            }
        });
        */
    }

}

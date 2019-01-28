using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using Pixelplacement;


public class pnAnimControlTween : MonoBehaviour
{
    public Spline mySpline;
    public Transform myObject;

    ///these variable are used to set up Pubnub
    public static PubNub dataServer;
    public string pubKey = "pub-c-959add75-e9d1-4aa4-bfbd-62a5cb8d1ca3";
    public string subKey = "sub-c-fd6890b4-1e6e-11e9-a469-92940241a6b5";
    public string channelName = "train";
    ////

    public int trainSpeed;                  //this holds the value coming in from PN
                                            //this holds the link to the path to the Animator object
    public float changeSpeed;

    void Start()
    {
        Tween.Spline(mySpline, myObject, 0, 1, true, trainSpeed, 0, Tween.EaseInOut, Tween.LoopType.PingPong);
        //Tween.Spline(mySpline, myObject, 0, 1, true, changeSpeed, 0, Tween.EaseInOut, Tween.LoopType.PingPong);


        //This section establishes the parameters for connecting to Pubnub
        PNConfiguration connectionSettings = new PNConfiguration();
        connectionSettings.PublishKey = pubKey;
        connectionSettings.SubscribeKey = subKey;
        connectionSettings.LogVerbosity = PNLogVerbosity.BODY;
        connectionSettings.Secure = true;
        ////////

        dataServer = new PubNub(connectionSettings);  //make the connection to the server

        Debug.Log("Connected to Pubnub");

        //Subscribe to the channel specified above
        dataServer.Subscribe()
          .Channels(new List<string>() { channelName })
          .Execute();

        //define the function that is called when a new message arrives
        //unlike javascript it is named and defined all at once rather than linking to another function
        dataServer.SusbcribeCallback += (sender, evt) =>
        {

            SusbcribeEventEventArgs inMessage = evt as SusbcribeEventEventArgs;

            if (inMessage.MessageResult != null)    //error check to insure the message has contents
            {

                //convert the object that holds the message contents into a Dictionary
                Dictionary<string, object> msg = inMessage.MessageResult.Payload as Dictionary<string, object>;

                //Debug.Log(inMessage.MessageResult.Payload);
                trainSpeed = (int)msg["trainS"];  //force convert the "slide" parameter of the dictionary to be an integer and assign it to the currentSlide variable.

            }

        };



    }

    void Update()
    {
        changeSpeed = trainSpeed;
    }
    
}

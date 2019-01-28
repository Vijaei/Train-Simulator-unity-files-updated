using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BansheeGz.BGSpline.Components;
using PubNubAPI;

[RequireComponent(typeof(BGCcMath))]
public class followAndSpeed : MonoBehaviour
{
    public Transform ObjectToMove;
    public float distance;

    ///these variable are used to set up Pubnub
    public static PubNub dataServer;
    public string pubKey = "pub-c-8efd8f87-9fe2-45a5-81f6-7b60513f5ddc";
    public string subKey = "sub-c-43574d8c-135b-11e9-abd1-2a488504b737";
    public string channelName = "train";
    ////

    //public float trainSpeed;                  //this holds the value coming in from PN
                                              //this holds the link to the path to the Animator object
    public int trainSpeed;

    void Start()
    {


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

                Debug.Log(inMessage.MessageResult.Payload);

                Debug.Log("GOOOO!!!");

               
               
                //


                //trainSpeed = (float)msg["trainS"];  //force convert the "slide" parameter of the dictionary to be an integer and assign it to the currentSlide variable.
               trainSpeed = (int)msg["train"];  //force convert the "slide" parameter of the dictionary to be an integer and assign it to the currentSlide variable.


            }

        };

    }

    void Update()
    {
  

        //increase distance
        //distance += trainSpeed * Time.deltaTime;
        if (distance > 1500)
        {
            trainSpeed = 0;
            distance = 0;

        }
        else if (distance < 0)
        {

            trainSpeed = 0;
            distance = 0;

        }
        else
        {
            distance += trainSpeed * Time.deltaTime;
        }

        //calculate position and tangent
        Vector3 tangent;
        ObjectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
        ObjectToMove.rotation = Quaternion.LookRotation(tangent);
    }
}

using System;
using System.Net.Sockets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class videocapture : MonoBehaviour
{
    private int updateCount = 0, imageCount = 0;
    WebCamTexture webCam;
    TcpClient client;
    NetworkStream stream;
    string[] recognizeResult={"Unknown","Closed_Fist","Open_Palm","Pointing_Up","Thumb_Down","Thumb_Up","Victory","ILoveYou"};
    GameObject[] all;
    void Start()
    {
        client = new TcpClient("127.0.0.1", 8080);
        stream = client.GetStream();
        StartCoroutine("OpenCamera");
        all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
    }
    void FixedUpdate()
    {
        if (webCam != null && webCam.isPlaying)
        {
            if (updateCount == 200)
            {
                updateCount = 0;
                Texture2D texture = new Texture2D(webCam.width, webCam.height);
                texture.SetPixels(webCam.GetPixels());
                texture.Apply();
                StartCoroutine("sendImage", texture);
            }
            else
            {
                updateCount++;
            }
        }
    }
    public async void sendImage(Texture2D texture)
    {
        Debug.Log("start send: " + imageCount);
        byte[] imageBytes = ImageToByteArray(texture);
        //Debug.Log("imageBytes.Length: " + imageBytes.Length);
        byte[] imageSizeBytes = BitConverter.GetBytes(imageBytes.Length);
        await stream.WriteAsync(imageSizeBytes, 0, imageSizeBytes.Length);
        int bytesSent = 0;
        while (bytesSent < imageBytes.Length)
        {
            int bytesToSend = Math.Min(1024, imageBytes.Length - bytesSent);
            await stream.WriteAsync(imageBytes, bytesSent, bytesToSend);
            bytesSent += bytesToSend;
        }
        //Debug.Log("finish send:" + imageCount++);
        byte[] resultCode = new byte[4];
        await stream.ReadAsync(resultCode, 0, resultCode.Length);
        int result = BitConverter.ToInt32(resultCode, 0);
        Debug.Log("result: " + recognizeResult[result]);
        foreach(var go in all ){
            ExecuteEvents.Execute<EventInterface>(go, null, (x, y) => x.RecognizeEvent(result));
        }
    }
    byte[] ImageToByteArray(Texture2D texture)
    {
        return texture.EncodeToPNG();
    }
    void OnDestroy()
    {
        if (webCam != null)
        {
            webCam.Stop();
        }
        stream.Close();
        client.Close();
    }
    public IEnumerator OpenCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            webCam = new WebCamTexture(devices[0].name, 1280, 720, 30);
            webCam.Play();
        }
    }
}
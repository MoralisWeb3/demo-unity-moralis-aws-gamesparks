using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon.GameSparks.Unity.DotNet;
using Amazon.GameSparks.Unity.Editor.Assets;
using Amazon.GameSparks.Unity.Generated;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ConnectionScriptableObject connectionScriptableObject;
    
    private MyWeb3GameBackendOperations.GetNativeBalanceRequest getNativeBalanceRequest;

    private void Start()
    {
        getNativeBalanceRequest = new MyWeb3GameBackendOperations.GetNativeBalanceRequest();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            try
            {
                Debug.Log("Sending GetNativeBalance request");
                connectionScriptableObject.Connection.EnqueueGetNativeBalanceRequest(
                    getNativeBalanceRequest,
                    HandleGetNativeBalanceResponse,
                    error => { Debug.Log("Request failed: " + error); },
                    () => { Debug.Log("Request timed out."); },
                    TimeSpan.FromMinutes(2));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    private void HandleGetNativeBalanceResponse(Message<MyWeb3GameBackendOperations.GetNativeBalanceResponse> response)
    {
        Debug.Log(response.Payload.result);
    }
}

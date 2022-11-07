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
    
    // GameSparks requests
    private MyWeb3GameBackendOperations.GetNativeBalanceRequest getNativeBalanceRequest;
    private MyWeb3GameBackendOperations.GetWalletNftsRequest getWalletNftsRequest;
    
    #region UNITY_LIFECYCLE

    private void Start()
    {
        // Initializing requests
        getNativeBalanceRequest = new MyWeb3GameBackendOperations.GetNativeBalanceRequest();
        getWalletNftsRequest = new MyWeb3GameBackendOperations.GetWalletNftsRequest();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetNativeBalance();
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetWalletNfts();
        }
    }

    #endregion

    
    #region PUBLIC_METHODS
    
    public void GetNativeBalance()
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
    
    public void GetWalletNfts()
    {
        try
        {
            Debug.Log("Sending GetNativeBalance request");
            connectionScriptableObject.Connection.EnqueueGetWalletNftsRequest(
                getWalletNftsRequest,
                HandleGetWalletNftsRequest,
                error => { Debug.Log("Request failed: " + error); },
                () => { Debug.Log("Request timed out."); },
                TimeSpan.FromMinutes(2));
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    #endregion
    
    
    #region RESPONSE_HANDLERS

    private void HandleGetNativeBalanceResponse(Message<MyWeb3GameBackendOperations.GetNativeBalanceResponse> response)
    {
        Debug.Log(response.Payload.result);
    }
    
    private void HandleGetWalletNftsRequest(Message<MyWeb3GameBackendOperations.GetWalletNftsResponse> response)
    {
        Debug.Log(response.Payload.result);
    }

    #endregion
}

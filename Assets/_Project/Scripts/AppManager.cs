using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon.GameSparks.Unity.DotNet;
using Amazon.GameSparks.Unity.Editor.Assets;
using Amazon.GameSparks.Unity.Generated;

public class AppManager : MonoBehaviour
{
    [Header("ChainSafe Auth")]
    [SerializeField] private SignVerifyWebWallet signVerifyWebWallet;
    
    [Header("GameSparks")]
    [SerializeField] private ConnectionScriptableObject connectionScriptableObject;
    
    // GameSparks requests
    private MyWeb3GameBackendOperations.GetNativeBalanceRequest getNativeBalanceRequest;
    private MyWeb3GameBackendOperations.GetWalletNftsRequest getWalletNftsRequest;

    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    
    #region UNITY_LIFECYCLE

    private void Start()
    {
        string address = "";
        string chain = "";
        
        // Initializing requests
        getNativeBalanceRequest = new MyWeb3GameBackendOperations.GetNativeBalanceRequest(address, chain);
        getWalletNftsRequest = new MyWeb3GameBackendOperations.GetWalletNftsRequest(address, chain);
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
    
    public async void Connect()
    {
        var message = "Plase sign this message";
        
        var signature = await signVerifyWebWallet.SignMessage(message);

        if (string.IsNullOrEmpty(signature))
        {
            Debug.Log("Signature failed");
            return;
        }

        var walletAddress = signVerifyWebWallet.VerifySignature(signature, message);

        if (string.IsNullOrEmpty(walletAddress))
        {
            Debug.Log("Verification failed");
            return;
        }
        
        //TODO
        Debug.Log("Connected :)");
    }
    
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

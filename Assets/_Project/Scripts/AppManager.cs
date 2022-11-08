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
    
    private string _walletAddress;
    private string _chainId = "80001"; //Mumbai
        
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    
    
    #region UNITY_LIFECYCLE

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

        var publicAddress = signVerifyWebWallet.VerifySignature(signature, message);

        if (string.IsNullOrEmpty(publicAddress))
        {
            Debug.Log("Verification failed");
            return;
        }

        _walletAddress = publicAddress;
    }
    
    public void GetNativeBalance()
    {
        var getNativeBalanceRequest = new MyWeb3GameBackendOperations.GetNativeBalanceRequest(_walletAddress, _chainId);
        
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
        var getWalletNftsRequest = new MyWeb3GameBackendOperations.GetWalletNftsRequest(_walletAddress, _chainId);
        
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

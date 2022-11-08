using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon.GameSparks.Unity.DotNet;
using Amazon.GameSparks.Unity.Editor.Assets;
using Amazon.GameSparks.Unity.Generated;

public class AppManager : MonoBehaviour
{
    [Header("GameSparks")]
    [SerializeField] private ConnectionScriptableObject connectionScriptableObject;

    [Header("UI Elements")]
    [SerializeField] private GameObject authPanel;
    [SerializeField] private GameObject gameSparksPanel;
    
    private string _walletAddress;
    private string _chainId = "80001"; // Mumbai. Check if we can also retrieve the chain id automatically using ChainSafe!
    
    
    #region PUBLIC_METHODS
    
    public async void Authenticate()
    {
        _walletAddress = await CustomAuthService.Authenticate();

        if (string.IsNullOrEmpty(_walletAddress))
        {
            Debug.Log("We could not retrieve the wallet address");
            return;
        }
        
        authPanel.SetActive(false);
        gameSparksPanel.SetActive(true);
    }
    
    public void GetNativeBalance()
    {
        if (string.IsNullOrEmpty(_walletAddress) || string.IsNullOrEmpty(_chainId))
        {
            Debug.Log("You need the wallet address and the chain id to make this request!");
            return;
        }
        
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
        if (string.IsNullOrEmpty(_walletAddress) || string.IsNullOrEmpty(_chainId))
        {
            Debug.Log("You need the wallet address and the chain id to make this request!");
            return;
        }
        
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

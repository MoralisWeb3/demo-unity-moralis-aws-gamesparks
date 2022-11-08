using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Nethereum.Signer;
using Nethereum.Util;
using UnityEngine;

public static class CustomAuthService
{
    #region PUBLIC_METHODS

    public static async UniTask<string> Authenticate()
    {
        var message = "Plase sign this message";
        
        var signature = await SignMessage(message);

        if (string.IsNullOrEmpty(signature))
        {
            Debug.Log("Signature failed");
            return null;
        }

        var publicAddress = VerifySignature(signature, message);

        if (string.IsNullOrEmpty(publicAddress))
        {
            Debug.Log("Verification failed");
            return null;
        }

        return publicAddress;
    }

    #endregion
    

    // This methods use ChainSafe SDK functionalities
    #region PRIVATE_METHODS

    private static async UniTask<string> SignMessage(string message)
    {
        string signature = await Web3Wallet.Sign(message);
        
        Debug.Log(signature);
        return signature;
    }

    private static string VerifySignature(string signature, string originalMessage)
    {
        string msg = "\x19" + "Ethereum Signed Message:\n" + originalMessage.Length + originalMessage;
        byte[] msgHash = new Sha3Keccack().CalculateHash(Encoding.UTF8.GetBytes(msg));
        EthECDSASignature ecdsaSignature = MessageSigner.ExtractEcdsaSignature(signature);
        EthECKey key = EthECKey.RecoverFromSignature(ecdsaSignature, msgHash);

        bool isValid = key.Verify(msgHash, ecdsaSignature);

        if (!isValid) return null;
        
        string publicAddress = key.GetPublicAddress();
        Debug.Log("Address Returned: " + publicAddress);

        return publicAddress;
    }

    #endregion
}

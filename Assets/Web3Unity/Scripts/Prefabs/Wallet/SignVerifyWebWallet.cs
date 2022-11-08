using System.Text;
using Cysharp.Threading.Tasks;
using Nethereum.Signer;
using Nethereum.Util;
using UnityEngine;

public class SignVerifyWebWallet : MonoBehaviour
{
    public async UniTask<string> SignMessage(string message)
    {
        string signature = await Web3Wallet.Sign(message);
        
        Debug.Log(signature);
        return signature;
    }

    public string VerifySignature(string signature, string message)
    {
        string msg = "\x19" + "Ethereum Signed Message:\n" + message.Length + message;
        byte[] msgHash = new Sha3Keccack().CalculateHash(Encoding.UTF8.GetBytes(msg));
        EthECDSASignature ecdsaSignature = MessageSigner.ExtractEcdsaSignature(signature);
        EthECKey key = EthECKey.RecoverFromSignature(ecdsaSignature, msgHash);

        bool isValid = key.Verify(msgHash, ecdsaSignature);

        if (!isValid) return null;
        
        string publicAddress = key.GetPublicAddress();
        Debug.Log("Address Returned: " + publicAddress);

        return publicAddress;
    }
}

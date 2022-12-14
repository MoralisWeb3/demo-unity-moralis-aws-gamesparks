using System;
using Amazon.GameSparks.Unity.DotNet;
using Amazon.GameSparks.Unity.EngineIntegration;
using Newtonsoft.Json;

namespace Amazon.GameSparks.Unity.Generated
{
    public static class MyWeb3GameBackendOperations
    {

        public sealed class GetWalletNftsRequest
        {
            [JsonProperty]
            public string address { get; }
            [JsonProperty]
            public string chain { get; }

            public GetWalletNftsRequest(
                string address,
                string chain)
            {
                this.address = address;
                this.chain = chain;
            }

            public override string ToString()
            {
                return string.Concat(
                   $"{nameof(address)}: { address }", Environment.NewLine,
                   $"{nameof(chain)}: { chain }");
            }
        }

        public sealed class GetWalletNftsResponse
        {
            [JsonProperty]
            public string result { get; }

            public GetWalletNftsResponse(
                string result)
            {
                this.result = result;
            }

            public override string ToString()
            {
                return string.Concat(
                   $"{nameof(result)}: { result }");
            }
        }

        public static void EnqueueGetWalletNftsRequest(
            this Connection connection,
            GetWalletNftsRequest payload,
            Action<Message<GetWalletNftsResponse>> handleResponse,
            Action<Message<DefaultError>> handleError,
            Action handleTimeout,
            TimeSpan timeout)
        {
            connection.SendRequest(
                new Message<GetWalletNftsRequest>("Custom.Game.GetWalletNfts", payload),
                timeout,
                handleResponse,
                handleError,
                handleTimeout);
        }
        public sealed class GetNativeBalanceRequest
        {
            [JsonProperty]
            public string address { get; }
            [JsonProperty]
            public string chain { get; }

            public GetNativeBalanceRequest(
                string address,
                string chain)
            {
                this.address = address;
                this.chain = chain;
            }

            public override string ToString()
            {
                return string.Concat(
                   $"{nameof(address)}: { address }", Environment.NewLine,
                   $"{nameof(chain)}: { chain }");
            }
        }

        public sealed class GetNativeBalanceResponse
        {
            [JsonProperty]
            public Decimal result { get; }

            public GetNativeBalanceResponse(
                Decimal result)
            {
                this.result = result;
            }

            public override string ToString()
            {
                return string.Concat(
                   $"{nameof(result)}: { result }");
            }
        }

        public static void EnqueueGetNativeBalanceRequest(
            this Connection connection,
            GetNativeBalanceRequest payload,
            Action<Message<GetNativeBalanceResponse>> handleResponse,
            Action<Message<DefaultError>> handleError,
            Action handleTimeout,
            TimeSpan timeout)
        {
            connection.SendRequest(
                new Message<GetNativeBalanceRequest>("Custom.Game.GetNativeBalance", payload),
                timeout,
                handleResponse,
                handleError,
                handleTimeout);
        }


    }
}

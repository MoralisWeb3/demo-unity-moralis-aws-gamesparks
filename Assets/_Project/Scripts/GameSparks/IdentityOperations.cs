using System;
using Amazon.GameSparks.Unity.DotNet;
using Amazon.GameSparks.Unity.EngineIntegration;
using Newtonsoft.Json;

namespace Amazon.GameSparks.Unity.Generated
{
    public static class IdentityOperations
    {

        public sealed class GetPlayerTokenRequest
        {

            public GetPlayerTokenRequest(
)
            {
            }

            public override string ToString()
            {
                return string.Concat(
                   "");
            }
        }

        public sealed class GetPlayerTokenResponse
        {
            [JsonProperty]
            public string AuthenticationToken { get; }

            public GetPlayerTokenResponse(
                string AuthenticationToken)
            {
                this.AuthenticationToken = AuthenticationToken;
            }

            public override string ToString()
            {
                return string.Concat(
                   $"{nameof(AuthenticationToken)}: { AuthenticationToken }");
            }
        }

        public static void EnqueueGetPlayerTokenRequest(
            this Connection connection,
            GetPlayerTokenRequest payload,
            Action<Message<GetPlayerTokenResponse>> handleResponse,
            Action<Message<DefaultError>> handleError,
            Action handleTimeout,
            TimeSpan timeout)
        {
            connection.SendRequest(
                new Message<GetPlayerTokenRequest>("GameSparks.Identity.GetPlayerToken", payload),
                timeout,
                handleResponse,
                handleError,
                handleTimeout);
        }


    }
}

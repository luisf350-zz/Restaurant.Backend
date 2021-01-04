using Nexmo.Api;
using Nexmo.Api.Request;

namespace Restaurant.Backend.Common.Notifications
{
    public static class NexmoNotifications
    {
        public static NumberVerify.VerifyResponse SendNotification(string apiKey, string apiSecret, string phoneNumber)
        {
            var client = new Client(creds: new Credentials(apiKey, apiSecret));

            return client.NumberVerify.Verify(new NumberVerify.VerifyRequest
            {
                number = phoneNumber,
                brand = "Vonage",
                code_length = "6"
            });
        }

        public static NumberVerify.ControlResponse CancelNotification(string apiKey, string apiSecret, string requestId)
        {
            var client = new Client(creds: new Credentials(apiKey, apiSecret));

            return client.NumberVerify.Control(new NumberVerify.ControlRequest
            {
                request_id = requestId,
                cmd = "cancel"
            });
        }

        public static NumberVerify.CheckResponse VerifyNotification(string apiKey, string apiSecret, string requestId, int code)
        {
            var client = new Client(creds: new Credentials(apiKey, apiSecret));
            return client.NumberVerify.Check(new NumberVerify.CheckRequest
            {
                request_id = requestId,
                code = code.ToString()
            });
        }
        
        public static NumberVerify.VerifyResponse CheckStatus(string apiKey, string apiSecret, string requestId)
        {
            var credentials = new Credentials(apiKey, apiSecret);
            var client = new Client(credentials);
            var request = new NumberVerify.VerifyRequest { Brand = "Vonage", Number = requestId };

            return client.NumberVerify.Verify(request, credentials);
        }
    }
}

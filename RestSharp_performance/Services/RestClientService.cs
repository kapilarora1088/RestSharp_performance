using RestSharp;

namespace ProjectName.Services
{
    public class RestClientService
    {
        private readonly RestClient _client;

        public RestClientService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public RestResponse RegisterClient(string fullName, string userName, string email, string password, string phone)
        {
            var request = new RestRequest("/client_registeration", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("fullName", fullName);
            request.AddParameter("userName", userName);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            request.AddParameter("phone", phone);
            return _client.Execute(request);
        }

        public RestResponse LoginClient(string userName, string email, string password)
        {
            var request = new RestRequest("/client_login", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("userName", userName);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            return _client.Execute(request);
        }
    }
}

using RestSharp;

public class ClientRegisterPage
{
    private readonly RestClient _client;

    public ClientRegisterPage(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    public RestResponse RegisterClient(string fullName, string userName, string email, string password, string phone)
    {
        var request = new RestRequest("/client_registeration", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); // Default for form data
        request.AddParameter("fullName", fullName);
        request.AddParameter("userName", userName);
        request.AddParameter("email", email);
        request.AddParameter("password", password);
        request.AddParameter("phone", phone);
        return _client.Execute(request);
    }
}
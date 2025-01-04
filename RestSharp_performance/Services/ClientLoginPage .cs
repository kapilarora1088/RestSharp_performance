using RestSharp;

public class ClientLoginPage
{
    private readonly RestClient _client;
    private readonly RestRequest _request;

    public ClientLoginPage(string baseUrl)
    {
        _client = new RestClient(baseUrl);
        _request = new RestRequest("/client_login", Method.Post);
    }

    public RestResponse LoginClient(string username, string email ,string password)
    {
        var request = new RestRequest("/client_login", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); // Default for form data
        request.AddParameter("userName", username); // Optional: userName or email can be used
        request.AddParameter("email", email);       // Optional
        request.AddParameter("password", password);
        return _client.ExecuteAsync(request).Result;
    }
}

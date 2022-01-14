namespace Demo_Polly.Services
{
    public class ApiConsumerService
    {
        public string ConsumeAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"https://v2.jokeapi.dev/joke/Any?lang=es");
            var response = client.Send(new HttpRequestMessage() { Method = new HttpMethod("GET")});
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}

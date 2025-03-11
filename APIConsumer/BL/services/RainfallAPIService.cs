namespace APIConsumer.BL.services
{
    public class RainfallAPIService
    {
        public Uri GetUri()
        {
           return new Uri("http://localhost:5209/");
        }
    }
}

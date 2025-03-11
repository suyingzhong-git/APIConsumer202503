namespace APIConsumer.BL.Interfaces
{
    public interface IHttpClientExternalAPIRainfallService
    {
        public Task<HttpResponseMessage> GetRainfallMessageAsync();
    }
}

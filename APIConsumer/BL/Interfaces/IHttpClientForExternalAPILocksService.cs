using APIConsumer.Models;

namespace APIConsumer.BL.Interfaces
{
    public interface IHttpClientForExternalAPILocksService
    {
        public Task<HttpResponseMessage> GetLocksMessagesAsync();
        public Task<HttpResponseMessage> CreateLockMessageAsync(Lock aLock);
        public Task<HttpResponseMessage> UpdateLockMessageAsync(Lock aLock);
        public Task<HttpResponseMessage> DeleteLockMessageAsync(int id);
        public Task<HttpResponseMessage> GetLocksMessagesRangeAsync(string queryString);
    }
}

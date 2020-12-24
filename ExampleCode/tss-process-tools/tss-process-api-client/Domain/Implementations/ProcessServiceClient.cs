using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using log4net;

namespace Tss.Process.Api.Client.Domain.Implementations {

    public class ProcessServiceClient : IProcessServiceClient {

       #region Fields

        private readonly ILog       _log;
        private readonly HttpClient _httpClient;

       #endregion 

       #region ctor 
        public ProcessServiceClient(
            string baseUrl,
            string version,
            ILog   log
        ) {
            _log = log;

            // The ProcessClient doe not use a HttpClientFactory (does it exist in .NET 5?)
            // Do not create many of these classes.
            _httpClient             = new HttpClient();
            _httpClient.BaseAddress = new System.Uri($"{baseUrl}//Process/v{version.TrimStart('v')}");
        }

        public ProcessServiceClient(
            string baseUrl,
            string version
        ) : this(baseUrl, version, LogManager.GetLogger(typeof(ProcessServiceClient))) {}


       #endregion ctor

       #region IProcessClient

        public async Task<string> PublishStepServerPackage(StepServicePackageDto stepServicePackageDto) {
            var result = await _httpClient.PostAsJsonAsync("/StepServicePackage", stepServicePackageDto);

            return await HandleHttpResponse<string>(result);
        }

       #endregion IProcessClient

       #region Private
    
        private async Task<T> HandleHttpResponse<T>(HttpResponseMessage message){
            if(!message.IsSuccessStatusCode)
                throw new HttpRequestException($"{message.StatusCode}: {message.ReasonPhrase}");
            
            return await message.Content.ReadAsAsync<T>();
        }

       #endregion 

    }
}
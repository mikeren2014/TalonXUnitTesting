using RestEase;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Talon.Web.Models.Create;
using Talon.Web.Models.Get;
using Taylor.UFP.XCore.Common.Models.Pagination;

namespace Talon.Api.Testing.Interfaces
{
    [AllowAnyStatusCode]
    public interface IFinishingOptions
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Token { get; set; }

        [Get("FinishingOptions/{id}")]
        Task<Response<PagedData<GetFinishingOptionPayload>>> GetById([Path]int id);

        [Post("FinishingOptions")]
        Task<Response<PagedData<GetFinishingOptionPayload>>> Create([Body]CreateFinishingOptionPayload payload);

    }

}

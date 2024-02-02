using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public class SearchFormRequest() : IRequest
    {
        public string? Label { get; set; } = null;
        public string? CurrentUserId { get; set; } = null;
        public string[] IsStatusEnumPicked { get; set; } = [];
        public string[] IsFormTypePicked { get; set; } = [];
        public int? Order { get; set; } = null;
    }
}
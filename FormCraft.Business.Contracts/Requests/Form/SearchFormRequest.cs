using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public class SearchFormRequest() : IRequest
    {
        public string? Label = null;
        public string? CurrentUserId = null;
        public bool[] IsStatusEnumPicked = [];
        public bool[] IsFormTypePicked = [];
        public int? Order = null;
    }
}
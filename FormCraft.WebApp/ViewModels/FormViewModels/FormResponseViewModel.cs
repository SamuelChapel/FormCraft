using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.WebApp.ViewModels.FormViewModels
{
    public class FormResponseViewModel : IRequest
    {
        public string Id { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public FormTypeEnum FormTypeId { get; set; }
        public StatusEnum StatusId { get; set; }
        public string Label { get; set; } = null!;
    }
}

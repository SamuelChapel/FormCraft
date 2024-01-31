using FormCraft.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormCraft.WebApp.ViewModels.FormViewModels
{
    public class FormCreateViewModel
    {
        public string CreatorName { get; set; } = null!;

        [Required(ErrorMessage = "Please select a form type.")]
        public FormTypeEnum FormTypeId { get; set; }

        public StatusEnum StatusId { get; set; }

        [Required(ErrorMessage = "Please enter the title.")]
        public string Label { get; set; } = null!;
    }
}

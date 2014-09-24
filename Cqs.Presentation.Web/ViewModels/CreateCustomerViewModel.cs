namespace Cqs.Presentation.Web.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Cqs.Infrastructure.Dto;

    public class CreateCustomerViewModel : ViewModel<CreateCustomerDto>
    {
        [Required(ErrorMessage = "Please confirm your password.")]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
         
    }
}
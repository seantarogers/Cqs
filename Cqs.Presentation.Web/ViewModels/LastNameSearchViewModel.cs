namespace Cqs.Presentation.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    
    using Cqs.Infrastructure.Dto;

    public class LastNameSearchViewModel
    {
        public LastNameSearchViewModel()
        {
            Customers = new List<CustomerDto>();
        }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}
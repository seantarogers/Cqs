namespace Cqs.Presentation.Web.ViewModels
{
    using System.Collections.Generic;

    using Cqs.Infrastructure.Dto;

    public class PagedViewModel<TDto>
        where TDto : Dto
    {
        public IList<TDto> Data { get; set; }
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
    }
}
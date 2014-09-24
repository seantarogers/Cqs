namespace Cqs.Presentation.Web.ViewModels
{
    public abstract class ViewModel<TDto>
    {
        public TDto Data { get; set; }
    }
}
namespace MyArchitecture.PresenterLayer
{
    public class ViewModelMap<TViewModel>
    where TViewModel : BaseViewModel
    {
        readonly Dictionary<Guid, TViewModel> Map = [];

        public TViewModel? TryGetViewModel(Guid viewModelId)
        {
            Map.TryGetValue(viewModelId, out var vm);
            return vm;
        }

        public TViewModel SetNewViewModel(Guid viewModelId, TViewModel viewModel)
        {
            Map.Add(viewModelId, viewModel);
            return viewModel;
        }

        public void RemoveViewModelById(Guid viewModelId)
        {
            Map.Remove(viewModelId);
        }
    }
}

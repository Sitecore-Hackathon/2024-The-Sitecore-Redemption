using SitecoreRedemption.Foundation.Core.Models;
using SitecoreRedemption.Foundation.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SitecoreRedemption.Foundation.Core.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(
            string code,
            T viewModel = default(T),
            ValidationResult validationResult = null,
            object parameters = null,
            MessageViewModel messageViewModel = null);
    }
}

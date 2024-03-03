using SitecoreRedemption.Foundation.Core.Models;
using SitecoreRedemption.Foundation.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SitecoreRedemption.Foundation.Core.Services
{
    public class MediatorService : IMediatorService
    {
        public MediatorResponse<T> GetMediatorResponse<T>(
            string code,
            T viewModel = default(T),
            ValidationResult validationResult = null,
            object parameters = null,
            MessageViewModel messageViewModel = null)
        {
            var response = new MediatorResponse<T>
            {
                Code = code,
                ViewModel = viewModel,
                ValidationResult = validationResult,
                Parameters = parameters,
                MessageViewModel = messageViewModel
            };

            return response;
        }
    }
}

using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using boilerplate.ui.Services.Contact;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.ui.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _service;
        private readonly PagingAppSettings _pagingAppSettings;

        protected override string MainRoute() => UIRoutesHelper.Contact.GetEntries.GetUrl();

        public ContactController(IContactService service, PagingAppSettings pagingAppSettings)
        {
            _service = service ?? ThrowHelper.NullArgument<IContactService>();
            _pagingAppSettings = pagingAppSettings ?? ThrowHelper.NullArgument<PagingAppSettings>();
        }

        [HttpGet(UIRoutesHelper.Contact.GetEntries.PATH)]
        public async Task<IActionResult> GetEntries(int offset = 0, int? limit = null)
        {
            var result = await _service.GetEntries(offset, limit ?? _pagingAppSettings.DefaultPageLimit);
            return GetResult(result, true);
        }

        [HttpGet(UIRoutesHelper.Contact.CreateEntry.PATH)]
        public async Task<IActionResult> CreateEntry()
        {
            var result = await _service.GetCreateEntryViewModel();
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.Contact.CreateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntry(ViewModel<ContactCreateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            ServiceResult<ContactCreateEntryViewModel> newResult;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Data.Form.LabelIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Labels);
                    model.Data.Form.MusicianIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Musicians);
                    model.Data.Form.PlatformIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Platforms);
                }
                catch
                {
                    newResult = await _service.GetCreateEntryViewModel();
                    return await ResetFormOnErrorAsync(newResult, model.Data.Form, "Every contact must have at least one relationship");
                }
                if (string.IsNullOrEmpty(model.Data.Form.Email) && string.IsNullOrEmpty(model.Data.Form.CellPhone) && string.IsNullOrEmpty(model.Data.Form.BusinessPhone) && string.IsNullOrEmpty(model.Data.Form.Fax))
                {
                    
                    newResult = await _service.GetCreateEntryViewModel();
                    return await ResetFormOnErrorAsync(newResult, model.Data.Form, "At least one of the following fields: Email, CellPhone, BusinessPhone, Fax must be provided");
                }

                var result = await _service.CreateEntry(model.Data.Form);
                if (result.Status.Success)
                {
                    return GetRedirectResult(result);
                }
            }

            newResult = await _service.GetCreateEntryViewModel();
            return await ResetFormOnErrorAsync(newResult, model.Data.Form);
        }

        [HttpGet(UIRoutesHelper.Contact.UpdateEntry.PATH)]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id)
        {
            var result = await _service.GetUpdateEntryViewModel(id);
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.Contact.UpdateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id, ViewModel<ContactUpdateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            ServiceResult<ContactUpdateEntryViewModel> newResult;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Data.Form.LabelIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Labels);
                    model.Data.Form.MusicianIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Musicians);
                    model.Data.Form.PlatformIds = JsonConvert.DeserializeObject<int[]>(model.Data.Form.Platforms);

                    if (model.Data.Form.LabelIds.Count() == 0 || model.Data.Form.PlatformIds.Count() == 0 || model.Data.Form.MusicianIds.Count() == 0)
                    {
                        throw new System.Exception();
                    }
                }
                catch
                {
                    newResult = await _service.GetUpdateEntryViewModel(id);
                    return await ResetFormOnErrorAsync(newResult, model.Data.Form, "Every contact must have at least one relationship");
                }

                if (string.IsNullOrEmpty(model.Data.Form.Email) && string.IsNullOrEmpty(model.Data.Form.CellPhone) && string.IsNullOrEmpty(model.Data.Form.BusinessPhone) && string.IsNullOrEmpty(model.Data.Form.Fax))
                {

                    newResult = await _service.GetUpdateEntryViewModel(id);
                    return await ResetFormOnErrorAsync(newResult, model.Data.Form, "At least one of the following fields: Email, CellPhone, BusinessPhone, Fax must be provided");
                }

                var result = await _service.UpdateEntry(model.Data.Form);
                if (result.Status.Success)
                {
                    return GetRedirectResult(result);
                }
            }

            newResult = await _service.GetUpdateEntryViewModel(id);
            newResult.ViewModel.Data.Form = model.Data.Form;
            return GetResult(newResult, false);
        }

        [HttpPost(UIRoutesHelper.Contact.DeleteEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            var result = await _service.DeleteEntry(id);
            return GetRedirectResult(result);
        }

        private async Task<IActionResult> ResetFormOnErrorAsync(ServiceResult<ContactCreateEntryViewModel> newResult, 
            ContactCreateEntryViewModel.FormModel model, string errorMessage = "")
        {
            var dist = await _service.GetContactRelationshipsDictionaryAsync();

            newResult.ViewModel.Data.Form = model;
            newResult.ViewModel.Data.Relationships = _service.GetContactRelationshipsList(dist);
            newResult.ViewModel.Data.Form.ExistingPlatforms = dist[ContactService.PLATFORMS].Where(x => model.PlatformIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.Form.ExistingMusicians = dist[ContactService.MUSICIANS].Where(x => model.MusicianIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.Form.ExistingLabels = dist[ContactService.MUSIC_LABELS].Where(x => model.LabelIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.ErrorMessage = errorMessage;
            return GetResult(newResult, false);
        }

        private async Task<IActionResult> ResetFormOnErrorAsync(ServiceResult<ContactUpdateEntryViewModel> newResult,
            ContactUpdateEntryViewModel.FormModel model, string errorMessage = "")
        {
            var dist = await _service.GetContactRelationshipsDictionaryAsync();

            newResult.ViewModel.Data.Form = model;
            newResult.ViewModel.Data.Relationships = _service.GetContactRelationshipsList(dist);
            newResult.ViewModel.Data.Form.ExistingPlatforms = dist[ContactService.PLATFORMS].Where(x => model.PlatformIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.Form.ExistingMusicians = dist[ContactService.MUSICIANS].Where(x => model.MusicianIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.Form.ExistingLabels = dist[ContactService.MUSIC_LABELS].Where(x => model.LabelIds.Contains(x.Id)).ToList();
            newResult.ViewModel.Data.ErrorMessage = errorMessage;
            return GetResult(newResult, false);
        }
    }
}

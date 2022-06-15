using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.ui.Services.Contact
{
    public class ContactService : IContactService
    {
        public const string MUSIC_LABELS = "MUSIC_LABELS";
        public const string MUSICIANS = "MUSICIANS";
        public const string PLATFORMS = "PLATFORMS";

        private readonly IContactClient _contactClient;
        private readonly IMusicLabelClient _musicLabelClient;
        private readonly IMusicianClient _musicianClient;
        private readonly IPlatformClient _platformClient;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactClient client, IMusicLabelClient musicLabelClient, 
            IMusicianClient musicianClient, IPlatformClient platformClient, ILogger<ContactService> logger)
        {
            _contactClient = client ?? ThrowHelper.NullArgument<IContactClient>();
            _logger = logger ?? ThrowHelper.NullArgument<ILogger<ContactService>>();

            _musicLabelClient = musicLabelClient;
            _musicianClient = musicianClient;
            _platformClient = platformClient;
        }

        public async Task<ServiceResult> CreateEntry(ContactCreateEntryViewModel.FormModel model)
        {
            try
            {
               
                var response = await _contactClient.CreateEntry(new CreateCommandRequest<ContactCreateModel> { Entry = model });

                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> DeleteEntry(int id)
        {
            try
            {
                var response = await _contactClient.DeleteEntry(id);

                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult<ContactCreateEntryViewModel>> GetCreateEntryViewModel()
        {
            try
            {
                var relationshipsDictionary = await GetContactRelationshipsDictionaryAsync();

                return ServiceResult.CreateInstance(new BaseResponse { Success = true, Code = ResponseCode.Success }, new ContactCreateEntryViewModel
                {
                    Relationships = GetContactRelationshipsList(relationshipsDictionary)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<ContactCreateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult<ContactGetEntriesViewModel>> GetEntries(int offset, int limit)
        {
            try
            {
                var response = await _contactClient.GetEntries(new EntriesQueryRequest { Offset = offset, Limit = limit });
                return ServiceResult.CreateInstance(
                    response,
                    new ContactGetEntriesViewModel
                    {
                        Entries = response.Data
                    },
                    new Paging(response.TotalCount, offset, limit, (o, l) => UIRoutesHelper.Contact.GetEntries.GetUrl(o, l))
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<ContactGetEntriesViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult<ContactUpdateEntryViewModel>> GetUpdateEntryViewModel(int id)
        {
            try
            {
                var response = await _contactClient.GetEntry(id);
                if (!response.Success)
                {
                    return ServiceResult.CreateErrorInstance<ContactUpdateEntryViewModel>(response.ErrorMessage, response.Code);
                }

                var relationshipsDictionary = await GetContactRelationshipsDictionaryAsync();
                var existingPlatforms = relationshipsDictionary[PLATFORMS].Where(x => response.Data.Platforms.Contains(x.Id)).ToList();
                var existingMusicians = relationshipsDictionary[MUSICIANS].Where(x => response.Data.Musicians.Contains(x.Id)).ToList();
                var existingMusicLabels = relationshipsDictionary[MUSIC_LABELS].Where(x => response.Data.MusicLabels.Contains(x.Id)).ToList();

                return ServiceResult.CreateInstance(
                    response,
                    new ContactUpdateEntryViewModel
                    {
                        Form = new ContactUpdateEntryViewModel.FormModel
                        {
                            Id = response.Data.Id,
                            FirstName = response.Data.FirstName,
                            LastName = response.Data.LastName,
                            Title = response.Data.Title,
                            Company = response.Data.Company,
                            Email = response.Data.Email,
                            CellPhone = response.Data.PhoneCell,
                            BusinessPhone = response.Data.PhoneBusiness,
                            Fax = response.Data.Fax,
                            AddressLine1 = response.Data.AddressLine1,
                            AddressLine2 = response.Data.AddressLine2,
                            City = response.Data.City,
                            State = response.Data.State,
                            Zip = response.Data.Zip,
                            IsActive = response.Data.IsActive,
                            PlatformIds = response.Data.Platforms,
                            MusicianIds = response.Data.Musicians,
                            LabelIds = response.Data.MusicLabels,
                            ExistingPlatforms = existingPlatforms,
                            ExistingMusicians = existingMusicians,
                            ExistingLabels = existingMusicLabels,
                            Labels = JsonConvert.SerializeObject(existingMusicLabels.Select(x => x.Id).ToArray()),
                            Musicians = JsonConvert.SerializeObject(existingMusicians.Select(x => x.Id).ToArray()),
                            Platforms = JsonConvert.SerializeObject(existingPlatforms.Select(x => x.Id).ToArray())
                        },
                        Relationships = GetContactRelationshipsList(relationshipsDictionary),
                        
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<ContactUpdateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> UpdateEntry(ContactUpdateEntryViewModel.FormModel model)
        {
            try
            {   
                var response = await _contactClient.UpdateEntry(model.Id, new UpdateCommandRequest<ContactUpdateModel> { Entry = model });

                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
        }

        public List<Relationship> GetContactRelationshipsList(Dictionary<string, List<Relationship>> relationshipDictionary)
        {
            var relationships = new List<Relationship>();
            relationships.AddRange(relationshipDictionary[MUSIC_LABELS]);
            relationships.AddRange(relationshipDictionary[PLATFORMS]);
            relationships.AddRange(relationshipDictionary[MUSICIANS]);

            return relationships;
        }

        public async Task<Dictionary<string, List<Relationship>>> GetContactRelationshipsDictionaryAsync()
        {
            var d = new Dictionary<string, List<Relationship>>();
            //var resLabels = await _musicLabelClient.GetAllEntries();
            //if (!resLabels.Success)
            //{
            //    throw new Exception("Cannot load related music labels");
            //}
            //d.Add(MUSIC_LABELS, resLabels.Data.Select(x => new Relationship { Id = x.Id, Value = $"{x.Name} (label)", Type = "label" }).ToList());

            //var resPlatforms = await _platformClient.GetAllEntries();
            //if (!resLabels.Success)
            //{
            //    throw new Exception("Cannot load related platforms");
            //}
            //d.Add(PLATFORMS, resPlatforms.Data.Select(x => new Relationship { Id = x.Id, Value = $"{x.Name} (platform)", Type = "platform" }).ToList());

            //var resMusicians = await _musicianClient.GetAllEntries();
            //if (!resLabels.Success)
            //{
            //    throw new Exception("Cannot load related musicians");
            //}
            //d.Add(MUSICIANS, resMusicians.Data.Select(x => new Relationship { Id = x.Id, Value = $"{x.Name} (musician)", Type = "musician" }).ToList());

            //return d;

            d.Add(MUSIC_LABELS, new List<Relationship> {
                new Relationship { Id = 1, Value = "label 1 (label)", Type = "label" },
                new Relationship { Id = 2, Value = "label 2 (label)", Type = "label" }
            });

            d.Add(PLATFORMS, new List<Relationship> {
                new Relationship { Id = 1, Value = "platform 1 (platform)", Type = "platform" },
                new Relationship { Id = 2, Value = "platform 2 (platform)", Type = "platform" }
            });


            d.Add(MUSICIANS, new List<Relationship> {
                new Relationship { Id = 1, Value = "musician 1 (musician)", Type = "musician" },
                new Relationship { Id = 2, Value = "musician 2 (musician)", Type = "musician" }
            });


            return d;
        }        
    }
}

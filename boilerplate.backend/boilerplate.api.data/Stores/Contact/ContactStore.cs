using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores.Contact
{
    public class ContactStore : BaseStore, IContactStore
    {
        private readonly ApplicationDbContext _context;
        public ContactStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {
            _context = context;
        }

        protected override Type DataModelType => typeof(Models.Contact);
        protected override string EntriesProcedureName => ContactGetEntriesProcedure.Name;
        protected override string EntryProcedureName => ContactGetEntriesProcedure.Name;

        public async Task<EntryQueryResponse<Models.Contact>> GetContactById(int id)
        {
            var query = $@"SELECT   {nameof(Models.Contact.Id)}, 
                                    {nameof(Models.Contact.FirstName)}, 
                                    {nameof(Models.Contact.LastName)}, 
                                    {nameof(Models.Contact.Title)},
                                    {nameof(Models.Contact.Company)},
                                    {nameof(Models.Contact.Email)},
                                    {nameof(Models.Contact.PhoneCell)},
                                    {nameof(Models.Contact.PhoneBusiness)},
                                    {nameof(Models.Contact.Fax)},
                                    {nameof(Models.Contact.AddressLine1)},
                                    {nameof(Models.Contact.AddressLine2)},
                                    {nameof(Models.Contact.City)},
                                    {nameof(Models.Contact.State)},
                                    {nameof(Models.Contact.Zip)},
                                    {nameof(Models.Contact.IsActive)}
                                FROM {ContactExtension.TABLE_NAME} WHERE {nameof(Models.Contact.Id)} = {id}";

            return await GetEntry<Models.Contact>(query);
        }

        public async Task<bool> UpdateContact(UpdateCommandRequest<Models.Contact> request)
        {

            var entry = request.Entry;
            var existingEntry = await _context.Contacts
                .Include(x => x.PlatformContacts)
                .Include(x => x.MusicLabelContacts)
                .Include(x => x.MusicianContacts)
                .FirstOrDefaultAsync(x => x.Id == entry.Id);

            if (existingEntry == null)
            {
                return false;
            }

            await UpdateContactPlatformRelations(existingEntry.PlatformContacts, entry.PlatformContacts, _context.PlatformContacts);

            await UpdateMusicianContactRelation(existingEntry.MusicianContacts, entry.MusicianContacts, _context.MusicianContacts);

            await UpdateMusicLabelContactRelation(existingEntry.MusicLabelContacts, entry.MusicLabelContacts, _context.MusicLabelContacts);

            entry.DateCreated = existingEntry.DateCreated;
            _context.Entry(existingEntry).CurrentValues.SetValues(entry);
            
            await _context.SaveChangesAsync();
            
            
            return true;
        }

        private async Task UpdateContactPlatformRelations(ICollection<Models.PlatformContact> currentRelations, ICollection<Models.PlatformContact> relationsFromRequest, 
            DbSet<Models.PlatformContact> dbset) 
        {
            var requestIds = relationsFromRequest.Select(x => x.PlatformId);
            var currentIds = currentRelations.Select(x => x.PlatformId);

            var newContactRelationIds = requestIds.Except(currentIds).ToList();
            var obsoleteContactRelationIds = currentIds.Except(requestIds);
            var newContactRelations = relationsFromRequest.Where(x => newContactRelationIds.Contains(x.PlatformId));
            var obsoleteContactRelations = currentRelations.Where(x => obsoleteContactRelationIds.Contains(x.PlatformId));

            await UpdateContactRelation(dbset, newContactRelations, obsoleteContactRelations);
        }

        private async Task UpdateMusicianContactRelation(ICollection<Models.MusicianContact> currentRelations, ICollection<Models.MusicianContact> relationsFromRequest, DbSet<Models.MusicianContact> dbset)
        {
            var requestIds = relationsFromRequest.Select(x => x.MusicianId);
            var currentIds = currentRelations.Select(x => x.MusicianId);

            var newContactRelationIds = requestIds.Except(currentIds).ToList();
            var obsoleteContactRelationIds = currentIds.Except(requestIds);
            var newContactRelations = relationsFromRequest.Where(x => newContactRelationIds.Contains(x.MusicianId));
            var obsoleteContactRelations = currentRelations.Where(x => obsoleteContactRelationIds.Contains(x.MusicianId));

            await UpdateContactRelation(dbset, newContactRelations, obsoleteContactRelations);
        }

        private async Task UpdateMusicLabelContactRelation(ICollection<MusicLabelContact> currentRelations, ICollection<MusicLabelContact> relationsFromRequest, DbSet<MusicLabelContact> dbset)
        {
            var requestIds = relationsFromRequest.Select(x => x.MusicLabelId);
            var currentIds = currentRelations.Select(x => x.MusicLabelId);

            var newContactRelationIds = requestIds.Except(currentIds).ToList();
            var obsoleteContactRelationIds = currentIds.Except(requestIds);
            var newContactRelations = relationsFromRequest.Where(x => newContactRelationIds.Contains(x.MusicLabelId));
            var obsoleteContactRelations = currentRelations.Where(x => obsoleteContactRelationIds.Contains(x.MusicLabelId));

            await UpdateContactRelation(dbset, newContactRelations, obsoleteContactRelations);
        }

        private async Task UpdateContactRelation<T>(DbSet<T> dbset, IEnumerable<T> newContactRelations, IEnumerable<T> obsoleteContactRelations) where T: class 
        {
            foreach (var contactRelation in obsoleteContactRelations)
            {
                dbset.Remove(contactRelation);
            }

            foreach (var contactRelation in newContactRelations)
            {
                await dbset.AddAsync(contactRelation);
            }
        }
    }
}

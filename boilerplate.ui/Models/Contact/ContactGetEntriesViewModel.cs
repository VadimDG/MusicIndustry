using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using static boilerplate.ui.Models.TableViewModel;

namespace boilerplate.ui.Models
{
    public class ContactGetEntriesViewModel
    {
        public List<ContactDisplayReportModel> Entries { get; set; } = new List<ContactDisplayReportModel>();

        public TableViewModel GetTableViewModel(ViewDataDictionary ViewData)
        {
            return new TableViewModel
            {
                Name = (string)ViewData["Title"],
                CreateUrl = UIRoutesHelper.Contact.CreateEntry.GetUrl,
                UpdateUrl = UIRoutesHelper.Contact.UpdateEntry.GetUrl,
                DeleteUrl = UIRoutesHelper.Contact.DeleteEntry.GetUrl,
                Items = Entries ?? new List<ContactDisplayReportModel>(),
                Columns = new List<TableColumn>
                {
                    new TableColumn(nameof(ContactEditReportModel.Id))
                    {
                        IsIdentifier = true
                    },
                    new TableColumn(nameof(ContactDisplayReportModel.FirstName)),
                    new TableColumn(nameof(ContactDisplayReportModel.LastName)),
                    new TableColumn(nameof(ContactDisplayReportModel.Email)),
                    new TableColumn(nameof(ContactDisplayReportModel.PhoneCell)),
                    new TableColumn(nameof(ContactDisplayReportModel.PhoneBusiness)),
                    new TableColumn(nameof(ContactDisplayReportModel.Active)),
                    new TableColumn(nameof(ContactDisplayReportModel.Labels)),
                    new TableColumn(nameof(ContactDisplayReportModel.Musicians)),
                    new TableColumn(nameof(ContactDisplayReportModel.Platforms)),
                    new TableColumn
                    {
                        IsEdit = true
                    },
                    new TableColumn
                    {
                        IsRemove = true
                    }
                }
            };
        }
    }
}

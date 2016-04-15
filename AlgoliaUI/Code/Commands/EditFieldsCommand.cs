using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.WebEdit;
using Sitecore.Shell.Applications.WebEdit.Commands;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace AlgoliaUI.Code.Commands
{
    public class EditFieldsCommand: FieldEditorCommand
    {
        protected override PageEditFieldEditorOptions GetOptions(ClientPipelineArgs args, NameValueCollection form)
        {
            List<FieldDescriptor> list = new List<FieldDescriptor>();
            ItemUri uri = ItemUri.Parse(args.Parameters["uri"]);
            Item item = Database.GetItem(uri);
            Assert.IsNotNull(item, "item");

            string command = args.Parameters["command"];
            Assert.IsNotNullOrEmpty(command, "Field Editor command expects 'command' parameter");
            Item commandItem = Client.CoreDatabase.GetItem(command);
            Assert.IsNotNull(commandItem, "command item");

            list.AddRange(GetFieldsByFieldNames(args, item));
            list.AddRange(GetFieldsByFieldTypes(args, item));

            return new PageEditFieldEditorOptions(form, list)
            {
                Title = commandItem["Title"],
                Icon = commandItem["Icon"]
            };
        }

        private IEnumerable<FieldDescriptor> GetFieldsByFieldNames(ClientPipelineArgs args, Item item)
        {
            string fields = args.Parameters["fields"];
            if (string.IsNullOrEmpty(fields))
                return new List<FieldDescriptor>();

            return from current in new ListString(fields)
                   where item.Fields[current] != null
                   select new FieldDescriptor(item, current);
        }

        private IEnumerable<FieldDescriptor> GetFieldsByFieldTypes(ClientPipelineArgs args, Item item)
        {
            string fieldTypes = args.Parameters["fieldTypes"];
            if (string.IsNullOrEmpty(fieldTypes))
                return new List<FieldDescriptor>();

            var fieltTypesList = new ListString(fieldTypes);

            return item.Template.Fields
                .Where(x => fieltTypesList.Contains(x.Type, StringComparer.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Sortorder)
                .Select(x => new FieldDescriptor(item, x.Name));
        }

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length > 0 && context.Items[0] != null && !context.Items[0].Access.CanWrite())
            {
                return CommandState.Disabled;
            }
            return CommandState.Enabled;
        }
    }
}
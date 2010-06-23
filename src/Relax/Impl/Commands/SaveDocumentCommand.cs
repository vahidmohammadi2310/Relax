﻿using System;
using System.Linq;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class SaveDocumentCommand : 
        BaseCouchCommand,
        ISaveDocument
    {
        public virtual CommandResult Save<TModel>(TModel model)
        {
            var databaseName = configuration.GetDatabaseNameForType<TModel>();
            return Save(databaseName, model);
        }

        public virtual CommandResult Save(string databaseName, object model)
        {
            try
            {
                CreateUri(databaseName)
                    .Id(model.GetDocumentId());

                var body = model.ToJson();
                var result = Put(body);
                model.SetDocumentRevision(result.GetResultAs<SaveResponse>().Revision);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}",
                    model.GetType().FullName,
                    Uri.ToString(),
                    ex
                    );
            }
        }

        public SaveDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
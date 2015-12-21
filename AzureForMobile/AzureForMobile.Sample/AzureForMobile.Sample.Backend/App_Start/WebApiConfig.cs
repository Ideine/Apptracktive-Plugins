using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using AptkAms.Sample.Backend.DataObjects;
using AptkAms.Sample.Backend.Migrations;
using AptkAms.Sample.Backend.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace AptkAms.Sample.Backend
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            config.SetIsHosted(false);

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //Database.SetInitializer(new MobileServiceInitializer());

            var migrator = new DbMigrator(new Configuration());
            string errorMessage = null;
            try
            {
                migrator.Update();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = ex.EntityValidationErrors.SelectMany(entityValidationError => entityValidationError.ValidationErrors).Aggregate(errorMessage, (current, dbValidationError) => current + (dbValidationError.PropertyName + ": " + dbValidationError.ErrorMessage + " "));
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            if (errorMessage != null) throw new Exception(errorMessage);
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            /*List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }*/

            base.Seed(context);
        }
    }
}


using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.PowerPlatform
{
	public class AtualizarCampoRelacionado : IPlugin
	{
		public void Execute(IServiceProvider serviceProvider)
		{
			// Instancia do servico e 
			IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
			IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
			IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

			// Log de plugins - Trace

			ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

			tracing.Trace($"Forma de registrar um log no CDS para monitorar a execução do plug-in. {DateTime.Now}");

			// ID da Conta

			Entity accountCreated = (Entity)context.InputParameters["Target"];

			// 1 - QueryExpresion

			QueryExpression qe = new QueryExpression("account");
			qe.ColumnSet.AddColumns("websiteurl");
			qe.Criteria.AddCondition("accountid", ConditionOperator.Equal, accountCreated.Id);

			var resultSet_1 = service.RetrieveMultiple(qe);

			// 2 - FetchXml

			var fetchXml = "" +
			"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>" +
			  "<entity name='account'>" +
				"<attribute name='name' /> " +
				"<attribute name='primarycontactid' /> " +
				"<attribute name='telephone1' /> " +
				"<attribute name='accountid' /> " +
				"<order attribute='name' descending='false' /> " +
				"<filter type='and'> " +
				  "<condition attribute='accountid' operator='eq' uiname='TESTING COMPANY' uitype='account' value='" + accountCreated.Id + "' />" +
				"</filter> " +
			  "</entity> " +
			"</fetch>";

			var resutSet_2 = service.RetrieveMultiple(new FetchExpression(fetchXml));

			// 2 - Retrieve

			var accountRetrieved = service.Retrieve("account", accountCreated.Id, new ColumnSet("websiteurl"));

			Entity entity = new Entity("contact");
			entity["fullname"] = "NOME FIXO";
			entity["emailaddress1"] = accountRetrieved.GetAttributeValue<string>("websiteurl");
			entity["parentcustomerid"] = new EntityReference(accountCreated.LogicalName, accountCreated.Id);

			var contatoId = service.Create(entity);


			// Update

			var updateContato = new Entity("contact", contatoId);
			updateContato["fullname"] = "Qualquer outra coisa";

			service.Update(updateContato);

			// Delete

			service.Delete("contact", contatoId);

			// Execute - ReviseQuote

			var request = new ReviseQuoteRequest();
			request.ColumnSet = new ColumnSet(true);
			request.QuoteId = new Guid("df8004eb-303f-eb11-a813-000d3ac058c3");

			var response = (ReviseQuoteResponse)service.Execute(request);
		}
	}
}

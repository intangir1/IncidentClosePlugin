using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentClosePlugin
{
	public class Class1 : IPlugin
	{
		public void Execute(IServiceProvider serviceProvider)
		{
			IPluginExecutionContext context = (IPluginExecutionContext)
			serviceProvider.GetService(typeof(IPluginExecutionContext));

			if (context.InputParameters.Contains("IncidentResolution"))
			{
				Entity incidentResolution = (Entity)context.InputParameters["IncidentResolution"];
				Guid relatedIncidentGuid = ((EntityReference)incidentResolution.Attributes["incidentid"]).Id;
				IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
				IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

				Entity incident = new Entity(Incident.EntityLogicalName);

				incident.Id = relatedIncidentGuid;
				incident.Attributes["mtx_resolveddate"] = DateTime.Now;
				service.Update(incident);
			}
		}
	}
}

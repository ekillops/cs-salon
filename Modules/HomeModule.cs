using Nancy;
using System.Collections.Generic;

namespace Salon
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};

			// Client Pages
			Get["/clients"] = _ =>
			{
				List<Client> allClients = Client.GetAll();
				return View["all_clients.cshtml", allClients];
			};
			Get["/clients/new"] = _ =>
			{
				List<Stylist> allStylists = Stylist.GetAll();
				return View["new_client_form", allStylists];
			};
			Get["/clients/{id}"] = parameters =>
			{
				int clientId = int.Parse(parameters.id);
				Client targetClient = Client.Find(clientId);
				return View["client.cshtml", targetClient];
			};
			Get["/clients/{id}/update"] = parameters =>
			{
				int clientId = int.Parse(parameters.id);
				Client targetClient = Client.Find(clientId);
				return View["update_client.cshtml", targetClient];
			};
			Post["/clients"] = _ =>
			{
				string clientName = Request.Form["client-name"];
				string clientPhoneNumber = Request.Form["client-phone-number"];
				int stylistId = int.Parse(Request.Form["stylist-id"]);
				Client newClient = new Client(clientName, clientPhoneNumber, stylistId);
				newClient.Save();

				List<Client> allClients = Client.GetAll();
				return View["all_clients.cshtml", allClients];
			};
			Patch["/clients"] = _ =>
			{
				int targetId = int.Parse(Request.Form["client-id"]);
				string clientName = Request.Form["client-name"];
				string clientPhoneNumber = Request.Form["client-phone-number"];
				int stylistId = int.Parse(Request.Form["stylist-id"]);
				Client.Update(targetId, clientName, clientPhoneNumber, stylistId);

				List<Client> allClients = Client.GetAll();
				return View["all_clients.cshtml", allClients];
			};
			Delete["/clients"] = _ =>
			{
				int targetId = int.Parse(Request.Form["client-id"]);
				Client.Delete(targetId);
				List<Client> allClients = Client.GetAll();
				return View["all_clients.cshtml", allClients];
			};
			Delete["/clients/clear"] = _ =>
			{
				Client.DeleteAll();
				List<Client> allClients = Client.GetAll();
				return View["all_clients.cshtml", allClients];
			};
		}
	}
}

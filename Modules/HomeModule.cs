using Nancy;
using System;
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
				List <Stylist> allStylists = Stylist.GetAll();
				Dictionary<string, object> returnModel = new Dictionary<string, object>()
				{
					{"client", targetClient},
					{"stylists", allStylists}
				};
				return View["update_client.cshtml", returnModel];
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

			//Stylist Pages
			Get["/stylists"] = _ =>
			{
				List<Stylist> allStylists = Stylist.GetAll();
				return View["all_stylists.cshtml", allStylists];
			};
			Get["/stylists/new"] = _ =>
			{
				return View["new_stylist_form"];
			};
			Get["/stylists/{id}"] = parameters =>
			{
				int stylistId = int.Parse(parameters.id);
				Stylist targetStylist = Stylist.Find(stylistId);
				return View["stylist.cshtml", targetStylist];
			};
			Get["/stylists/{id}/update"] = parameters =>
			{
				int stylistId = int.Parse(parameters.id);
				Stylist targetStylist = Stylist.Find(stylistId);
				return View["update_stylist.cshtml", targetStylist];
			};
			Post["/stylists"] = _ =>
			{
				string stylistName = Request.Form["stylist-name"];
				string stylistPhoneNumber = Request.Form["stylist-phone-number"];
				Stylist newStylist = new Stylist(stylistName, stylistPhoneNumber);
				newStylist.Save();

				List<Stylist> allStylists = Stylist.GetAll();
				return View["all_stylists.cshtml", allStylists];
			};
			Patch["/stylists"] = _ =>
			{
				int targetId = int.Parse(Request.Form["stylist-id"]);
				string stylistName = Request.Form["stylist-name"];
				string stylistPhoneNumber = Request.Form["stylist-phone-number"];
				Stylist.Update(targetId, stylistName, stylistPhoneNumber);

				List<Stylist> allStylists = Stylist.GetAll();
				return View["all_stylists.cshtml", allStylists];
			};
			Delete["/stylists"] = _ =>
			{
				int targetId = int.Parse(Request.Form["stylist-id"]);
				Stylist.Delete(targetId);
				List<Stylist> allStylists = Stylist.GetAll();
				return View["all_stylists.cshtml", allStylists];
			};
			Delete["/stylists/clear"] = _ =>
			{
				Stylist.DeleteAll();
				List<Stylist> allStylists = Stylist.GetAll();
				return View["all_stylists.cshtml", allStylists];
			};

			//Search
			Get["/{searchType}/search/{searchBy}"] = parameters =>
			{
				string searchBy = parameters.searchBy;
				string searchType = parameters.searchType;
				string searchInput = Request.Query["search-input"];

				if (searchType == "clients")
				{
					List<Client> searchResults = Client.SearchByValue(searchBy, searchInput);
					return View["clients_search.cshtml", searchResults];
				}
				else if (searchType == "stylists")
				{
					List<Stylist> searchResults = Stylist.SearchByValue(searchBy, searchInput);
					return View["stylists_search.cshtml", searchResults];
				}
				else
				{
					return View["index.cshtml"];
				}
			};

		}
	}
}

using Blockchain_P2P_NetworkTest.ViewModels;
using Newtonsoft.Json;
using P2PCoreASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P2PCoreASP.Controllers
{
	public class HomeController : Controller
	{
		
		public ActionResult Index()
		{
			Parameter.PhillyCoin.InitializeChain();
			
			Parameter.Server = new P2PServer();

			Parameter.Server.Start();
			
			return View();
		}


		public ActionResult Block()
		{
			var indexViewModel =  new IndexViewModel();

			return View(indexViewModel);
		}


		[HttpPost]
		public ActionResult Block(IndexViewModel indexViewModel)
		{


			List<string> result = new List<string>();
			Parameter.PhillyCoin.InitializeChain();
			switch (indexViewModel.Menu)
			{
				case 1:
					Parameter.Client.Connect($"{indexViewModel.ServerUrlConnect}/Blockchain");
					break;
				case 2:

					//creer une transaction
					Parameter.PhillyCoin.CreateTransaction(new Transaction(indexViewModel.Name, indexViewModel.RecieveName, indexViewModel.Amount));
					// créer un block, ajouter la transaction en attente 
					Parameter.PhillyCoin.ProcessPendingTransactions(indexViewModel.Name);
					Parameter.Client.Broadcast(JsonConvert.SerializeObject(Parameter.PhillyCoin));
					break;

			}
			indexViewModel.Result = new List<string>();
			indexViewModel.Result.Add(JsonConvert.SerializeObject(Parameter.PhillyCoin, Formatting.Indented));
			return View(indexViewModel);
		}



		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
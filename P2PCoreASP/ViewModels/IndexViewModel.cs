using P2PCoreASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blockchain_P2P_NetworkTest.ViewModels
{
	public class IndexViewModel
	{
		public int Port { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string RecieveName { get; set; }
		public string ServerMessage { get; set; }
		public int Amount { get; set; }
		public List<string> Result { get; set; }

		public string ServerUrlConnect { get; set; }

		public int Menu { get; set; }
		
		public List<Dic> Menus {
			get
			{
				return new List<Dic>()
				{
					new Dic()
					{
						Id = 0,
						Name = "Faite un choix"
					},
					new Dic()
					{
						Id = 1,
						Name = " Connect to a server"
					},
					new Dic()
					{
						Id = 2,
						Name = "Add a transaction"
					},
					new Dic()
					{
						Id = 3,
						Name = " Display Blockchain"
					}

				};
				
			}
			
		}

		public IndexViewModel()
		{
			this.Address = Parameter.Address;
			this.Port = Parameter.Port;
			this.Result = new List<string>();
			this.Name = Parameter.Name;
			this.RecieveName = "Arno";
			this.ServerUrlConnect = "ws://192.168.1.21:1";
			this.Amount = 2500;
			this.ServerMessage = Parameter.ServerMessage;
		}
	}

	public class Dic
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
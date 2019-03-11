using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PCoreASP.Models
{
	public class Parameter
	{
		public static int Port = 1;
		public static P2PServer Server = null;
		public static P2PClient Client = new P2PClient();
		public static Blockchain PhillyCoin = new Blockchain();
		public static string Name = "Danick";
		//public static string Address = "192.168.1.42";
		public static string Address = "172.16.10.177";
		public static string ServerMessage = "";
		public static string ClientMessage = "";
	}
}
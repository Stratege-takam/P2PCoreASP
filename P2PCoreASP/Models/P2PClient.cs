using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace P2PCoreASP.Models
{
	public class P2PClient
	{
		IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();

		public void Connect(string url)
		{
			try
			{
				var result = "";
				if (!wsDict.ContainsKey(url))
				{
					WebSocket ws = new WebSocket(url);
					ws.OnMessage += (sender, e) =>
					{
						if (e.Data == "Hi Client")
						{

							result = e.Data;
							Parameter.ClientMessage = result;
						}
						else
						{
							Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
							if (newChain.IsValid() && newChain.Chain.Count > Parameter.PhillyCoin.Chain.Count)
							{
								List<Transaction> newTransactions = new List<Transaction>();
								newTransactions.AddRange(newChain.PendingTransactions);
								newTransactions.AddRange(Parameter.PhillyCoin.PendingTransactions);

								newChain.PendingTransactions = newTransactions;
								Parameter.PhillyCoin = newChain;
							}
						}
					};
					ws.Connect();
					ws.Send("Hi Server");
					ws.Send(JsonConvert.SerializeObject(Parameter.PhillyCoin));
					wsDict.Add(url, ws);
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		
				
		}

		public void Send(string url, string data)
		{
			try
			{
				foreach (var item in wsDict)
				{
					if (item.Key == url)
					{
						item.Value.Send(data);
					}
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public void Broadcast(string data)
		{
			try
			{
				foreach (var item in wsDict)
				{
					item.Value.Send(data);
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public IList<string> GetServers()
		{
			IList<string> servers = new List<string>();
			try
			{
				foreach (var item in wsDict)
				{
					servers.Add(item.Key);
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			return servers;
		}

		public void Close()
		{
			try
			{
				foreach (var item in wsDict)
				{
					item.Value.Close();
				}
			}
			catch (Exception ex)
			{

				throw new  Exception(ex.Message);
			}
			
		}
	}
}

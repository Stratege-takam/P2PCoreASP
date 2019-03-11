using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace P2PCoreASP.Models
{
	public class P2PServer : WebSocketBehavior
	{
		bool chainSynched = false;
		WebSocketServer wss = null;

		public string Start()
		{
			try
			{
				wss = new WebSocketServer($"ws://{Parameter.Address}:{Parameter.Port}");
				wss.AddWebSocketService<P2PServer>("/Blockchain");
				wss.Start();
				return $"Started server at ws://{Parameter.Address}:{Parameter.Port}";
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			
		}

		protected override void OnMessage(MessageEventArgs e)
		{
			try
			{
				if (e.Data == "Hi Server")
				{
					Parameter.ServerMessage = e.Data;
					Send("Hi Client");
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

					if (!chainSynched)
					{
						Send(JsonConvert.SerializeObject(Parameter.PhillyCoin));
						chainSynched = true;
					}
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}
	}
}

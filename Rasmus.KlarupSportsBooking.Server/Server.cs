using Rasmus.KlarupSportsBooking.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;
using System.IO;

namespace Rasmus.KlarupSportsBooking.Server
{
    class Server
    {/// <summary>
     /// The TcpListener used to receive the clients.
     /// </summary>
        private TcpListener listener { get; }
        /// <summary>
        /// The IP Address of the server.
        /// </summary>
        public IPAddress ServerIpAddress { get; }
        public DataHandler handler = new DataHandler();
        public List<string> validRequests = new List<string> { "CalculateCoveragePercentageByDateRange", "FindNextAvailableTime", "FindNextBookingByUnionName", "CalculateMostActiveUnionByDateRange", "Help", "Commands", "Command" };
        /// <summary>
        /// The constructor initializes the IP Address and the TcpListener.
        /// Uses either local IP Address, or the IPv4 Address of the computer.
        /// </summary>
        /// <param name="port">Which port to be used</param>
        /// <param name="useLocal">Optional, is true by default.</param>
        public Server(int port, bool useLocal = true)
        {
            if (useLocal)
                ServerIpAddress = IPAddress.Parse("127.0.0.1");
            else
            {
                var hostName = Dns.GetHostName();
                var ipAddresses = Dns.GetHostAddresses(hostName);
                ServerIpAddress = ipAddresses.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            }

            if (ServerIpAddress == null)
                throw new NullReferenceException("No IPv4 address for the server");

            listener = new TcpListener(ServerIpAddress, port);
        }

        /// <summary>
        /// This method starts the server listening for a client.
        /// It receives the request, handles it, and returns the response.
        /// It is ran in a infinite loop.
        /// </summary>
        public void Start()
        {
            listener.Start();

            while (true)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                using (NetworkStream ns = client.GetStream())
                using (StreamWriter writer = new StreamWriter(ns))
                using (StreamReader reader = new StreamReader(ns))
                {
                    try
                    {

                        writer.AutoFlush = true;

                        string request = reader.ReadLine();
                        string response = Handle(request);

                        writer.WriteLine(response);
                    }
                    catch (IOException ex)
                    {
                        writer.WriteLine(ex.Message);
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Method used to handle the request from the client.
        /// </summary>
        /// <param name="request">The request from the client. Must be CSV and atleast two values.</param>
        /// <returns>Returns the response to the client</returns>
        private string Handle(string request)
        {
            var splittedRequest = request.Split(',');
            bool isValid = VerifyRequest(splittedRequest);
            string response = "";
            try
            {
                if (isValid)
                {

                    switch (splittedRequest[0])
                    {
                        case "CalculateCoveragePercentageByDateRange":
                            response += Math.Round(handler.Reader.CalculateCoveragePercentageByDateRange(DateTime.Parse(splittedRequest[1]), DateTime.Parse(splittedRequest[2])), 2);
                            break;
                        case "CalculateMostActiveUnionByDateRange":
                            Union union = handler.Reader.CalculateMostActiveUnionByDateRange(DateTime.Parse(splittedRequest[1]), DateTime.Parse(splittedRequest[2]));
                            response += union.UnionName;
                            break;
                        default:
                            response = "Request not yet implemented";
                            break;
                    }
                }
                else
                {
                    response += "Error, request not valid";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Method used to validate the request.
        /// </summary>
        /// <param name="splittedRequest">The splitted CSV request</param>
        /// <returns>Returns true if valid</returns>
        private bool VerifyRequest(string[] splittedRequest)
        {
            if (splittedRequest[0].GetType() == typeof(string) && validRequests.Contains(splittedRequest[0]))
            {
                if (splittedRequest[0] == "CalculateCoveragePercentageByDateRange" || splittedRequest[0] == "CalculateMostActiveUnionByDateRange")
                {
                    if (splittedRequest.Length != 3)
                    {
                        return false;
                    }
                    if (!DateTime.TryParse(splittedRequest[1], out DateTime startDate) || !DateTime.TryParse(splittedRequest[2], out DateTime endDate))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

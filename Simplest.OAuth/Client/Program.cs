﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = GetClientToken();
            CallApi(response);

            response = GetUserToken();
            CallApi(response);
            Console.ReadLine();
        }

        private static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "http://localhost:5000/connect/token",
                "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            return client.RequestResourceOwnerPasswordAsync("ahasan", "123456", "api1").Result;
        }

        private static void CallApi(TokenResponse response)
        {
            Console.WriteLine("start api call.....");
            var client=new HttpClient();
            client.SetBearerToken(response.AccessToken);
            var ss=client.GetStringAsync("http://localhost:1480/test").Result;
            Console.WriteLine(client.GetStringAsync("http://localhost:1480/test").Result);
            Console.WriteLine("end api call.....");
            
        }

        private static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
         "http://localhost:5000/connect/token",
         "silicon",
         "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            return client.RequestClientCredentialsAsync("api1").Result;
        }
    }
}

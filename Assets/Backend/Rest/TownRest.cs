﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Assets.Backend.Exceptions;
using Assets.Backend.Models;
using Assets.Backend.RestModels;
using Newtonsoft.Json;

namespace Assets.Backend.Rest
{
    class TownRest
    {
        public async Task<PlayerTown> GetTown(int townId, string authenticationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestConstants.Url);

                var queries = new Dictionary<string, string>
                {
                    {"id", townId.ToString() },
                    {"token", authenticationToken}

                };

                var json = JsonConvert.SerializeObject(queries);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/town/get", data);
                var resultContent = await result.Content.ReadAsStringAsync();
                
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var town = JsonConvert.DeserializeObject<PlayerTown>(resultContent);
                        return town;
                    case HttpStatusCode.Forbidden:
                        throw new SessionExpiredException("The player's login session has expired.");
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException("The town of the specified player was not found.");
                }
            }

            throw new InvalidOperationException("Reached invalid state.");
        }

        public async Task<PlayerTown> CreateTown(string authenticationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestConstants.Url);

                var queries = new Dictionary<string, string>
                {
                    {"token", authenticationToken}

                };

                var json = JsonConvert.SerializeObject(queries);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/town/create", data);
                var resultContent = await result.Content.ReadAsStringAsync();

                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var town = JsonConvert.DeserializeObject<PlayerTown>(resultContent);
                        return town;
                    case HttpStatusCode.Forbidden:
                        throw new SessionExpiredException("The player's login session has expired.");
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException("Could not find town.");
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException("Accesstoken is not valid.");
                    case HttpStatusCode.BadRequest:
                        throw new PlayerAlreadyHasTownException("The specified player already has a town.");

                }
            }

            throw new InvalidOperationException("Reached invalid state.");
        }

        public async Task<List<PlayerTown>> GetAllTownsFromPlayer(string authenticationToken, int playerId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestConstants.Url);

                var queries = new Dictionary<string, string>
                {
                    {"token", authenticationToken},
                    {"playerId", playerId.ToString()}
                };

                var json = JsonConvert.SerializeObject(queries);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/town/all", data);
                var resultContent = await result.Content.ReadAsStringAsync();

                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        List<PlayerTown> towns = JsonConvert.DeserializeObject<List<PlayerTown>>(resultContent);
                        return towns;
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException("Could not find towns of specified player.");
                }
            }

            throw new InvalidOperationException("Reached invalid state.");
        }
    }
}

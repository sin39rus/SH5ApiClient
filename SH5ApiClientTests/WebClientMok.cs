﻿using SH5ApiClient.Core.Requests;
using SH5ApiClient.Infrastructure.Helpers;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClientTests
{
    public class WebClientMok : IWebClient
    {
        Task<string> IWebClient.WebGetAsync(string url)
        {
            throw new NotImplementedException();
        }

        Task<string> IWebClient.WebPostAsync(RequestBase request)
        {
            switch (request)
            {
                case CurrenciesRequest:
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Currencies.json", Encoding.UTF8));
                case DepartsRequest:
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Departs.json", Encoding.UTF8));
                case DepartRequest:
                    return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Depart.json", Encoding.UTF8));
                case GGroupsRequest:
                    return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\GGroups.json", Encoding.UTF8));
                case GDocRequest:
                    if(request.ProcName == "GDoc10")
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc10.json", Encoding.UTF8));
                    if(request.ProcName == "GDoc4")
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc4.json", Encoding.UTF8));
                    if(request.ProcName == "GDoc5")
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc5.json", Encoding.UTF8));
                    if(request.ProcName == "GDoc8Diffs")
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\GDoc8Diffs.json", Encoding.UTF8));
                    if(request.ProcName == "GDoc8")
                        return Task.FromResult(File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc8.json", Encoding.UTF8));
                    
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        Task<string> IWebClient.WebPostAsync(string request, ConnectionParamSH5 connectionParam)
        {
            throw new NotImplementedException();
        }
    }
}

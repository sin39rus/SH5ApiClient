﻿using SH5ApiClient.Models;

namespace SH5ApiClientTests
{
    internal class Options
    {
        public static ConnectionParamSH5 connectionParamSH5 = new ("Admin", "123456", "127.0.0.1", 9191);
        public static SH5ApiClient.ApiClient ApiClient = new SH5ApiClient.ApiClient(connectionParamSH5, new WebClientMok());
    }
}

// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;

namespace Localization.ConfigurationRequestCultureProvider
{
    public class JsonRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json");

            var config = builder.Build();
            string culture = config["culture"];
            string uiCulture = config["uiCulture"];

            culture = culture ?? "en-US";
            uiCulture = uiCulture ?? culture;

            return Task.FromResult(new ProviderCultureResult(culture, uiCulture));
        }
    }
}
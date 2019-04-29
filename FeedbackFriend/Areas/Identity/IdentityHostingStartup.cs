﻿using System;
using FeedbackFriend.Data;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FeedbackFriend.Areas.Identity.IdentityHostingStartup))]

namespace FeedbackFriend.Areas.Identity
{
    public class IdentityHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}

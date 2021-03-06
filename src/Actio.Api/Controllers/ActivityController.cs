﻿using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivityController : DefaultController
    {
        private readonly IBusClient _bus;

        public ActivityController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public IActionResult Get() => Content("Secured");

        [HttpPost]
        public async Task<IActionResult> Post(CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = (Guid)User?.Identity?.Name!.ToGuid();

            await _bus.PublishAsync(command);

            return Accepted($"  {command.Id}");
        }
    }
}

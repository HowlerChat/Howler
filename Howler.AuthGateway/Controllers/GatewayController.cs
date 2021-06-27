using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Howler.AuthGateway.Models;
using Howler.Database;
using Howler.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Howler.AuthGateway.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class GatewayController : ControllerBase
    {
        private ISigningAlgorithm _signingAlgorithm;

        private IFederationDatabaseContext _federatedDb;
        
        public GatewayController(IFederationDatabaseContext federatedDb, ISigningAlgorithm signingAlgorithm) {
            this._federatedDb = federatedDb;
            this._signingAlgorithm = signingAlgorithm;
        }

        [HttpPost("auth")]
        public IActionResult Post(string serverId)
        {
            var token = (this.HttpContext.Request.Headers["Authorization"].First())
                .Split(' ')
                .Last();
            var jwt = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token);
            var validatedServerId = (this._federatedDb.Servers).Where(s => s.ServerId == serverId).Select(s => s.ServerId).ToList().FirstOrDefault();

            if (validatedServerId == null) return this.NotFound();

            // TODO: Whitelist/blacklist
            return Ok(new GatewayJWT(this._signingAlgorithm)
            {
                Header = new GatewayJWTHeader
                {
                    Kid = jwt.Kid
                },
                Body = new GatewayJWTBody
                {
                    Sub = jwt.Subject,
                    DeviceKey = jwt.GetClaim("device_key").Value,
                    EventId = jwt.GetClaim("event_id").Value,
                    Scope = serverId,
                    AuthTime = long.Parse(jwt.GetClaim("auth_time").Value),
                    Issuer = "https://gateway.howler.chat",
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds(),
                    JTI = jwt.GetClaim("jti").Value,
                    ClientId = jwt.GetClaim("client_id").Value,
                    Username = jwt.GetClaim("username").Value
                }
            }.ToString());
            
        }
    }
}

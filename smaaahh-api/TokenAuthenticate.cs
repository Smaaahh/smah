using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace smaaahh_api
{
    public class TokenAuthenticate : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            AuthService _authService = new AuthService(new MemberShipProvider(), new RSAKeyProvider());

            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing autorization header", request);
                return;
            }
            if (authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid autorization scheme", request);
                return;
            }
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                return;
            }

            Boolean correctToken = await _authService.ValidateTokenAsync(authorization.Parameter);
            if (!correctToken)
                context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            AuthService _authService = new AuthService(new MemberShipProvider(), new RSAKeyProvider()); ;

            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                context.Result = new AuthenticationFailureResult("Missing autorization header", request);
                return;
            }
            if (authorization.Scheme != "Bearer")
            {
                context.Result = new AuthenticationFailureResult("Invalid autorization scheme", request);
                return;
            }
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.Result = new AuthenticationFailureResult("Missing Token", request);
                return;
            }

            Boolean correctToken = await _authService.ValidateTokenAsync(authorization.Parameter);
            if (!correctToken)
                context.Result = new AuthenticationFailureResult("Invalid Token", request);
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
}
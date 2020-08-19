using System;
using LoginAutentication.Models;
using LoginAutentication.Repositories;
using LoginAutentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LoginAutentication.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public TokenService tokenService { get; set; }

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
            tokenService = new TokenService(Configuration);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User user)
        {
            if (user == null) return NotFound(new { message = "Usuario ou senha invalidos." });

            var userIdent = UserRepository.Get(user.Email, user.Password);

            if (userIdent == null) return NotFound(new { message = "Usuario ou senha invalidos." });

            var token = tokenService.GenerateToken(userIdent);
            userIdent.Password = "";

            return new
            {
                user = userIdent,
                message = "Usuário logado com sucesso!",
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("teste1")]
        [Authorize(Roles = "teste1, teste2")]
        public string Teste1() => "Teste 1";

        [HttpGet]
        [Route("teste2")]
        [Authorize(Roles = "teste1, teste2")]
        public string Teste2() => "Teste 2";
    }
}

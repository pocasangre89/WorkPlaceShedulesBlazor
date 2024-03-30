using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WorkPlaceShedulesBlazor.DTO;

namespace WorkPlaceShedulesBlazor.Storage
{
    public class AutenticationExtension : AuthenticationStateProvider
    {
            private readonly ISessionStorageService _sessionStorage;
            private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

            public AutenticationExtension(ISessionStorageService sessionStorage)
            {
                _sessionStorage = sessionStorage;
            }

            public async Task ActualizarEstadoAutenticacion(TokenDTO? sesionUsuario)
            {
                ClaimsPrincipal claimsPrincipal;

                if (sesionUsuario != null)
                {
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("Token", sesionUsuario.result),
                    new Claim("UserId",sesionUsuario.id.ToString()),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));

                    await _sessionStorage.SaveStorage("TokenSession", sesionUsuario);
                }
                else
                {
                    claimsPrincipal = _sinInformacion;
                    await _sessionStorage.RemoveItemAsync("TokenSession");
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            }


            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {

              var sesionUsuario = await _sessionStorage.GetStorage<TokenDTO>("TokenSession");

                if (sesionUsuario == null)
                    return await Task.FromResult(new AuthenticationState(_sinInformacion));

                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("Token", sesionUsuario.result),
                    new Claim("UserId",sesionUsuario.id.ToString()),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));


                return await Task.FromResult(new AuthenticationState(claimPrincipal));

            }
        }
    }

using Blazored.SessionStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WorkGroupshedulesBlazor.Service;
using WorkPlaceShedulesBlazor;
using WorkPlaceShedulesBlazor.Interface;
using WorkPlaceShedulesBlazor.Service;
using WorkPlaceShedulesBlazor.Storage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7259/") });

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWorkGroupsService, WorkGroupsService>();
builder.Services.AddScoped<IWorkPlacesService, WorkPlacesService>();
builder.Services.AddScoped<IUserWorkPlaceShedulesService, UserWorkPlaceShedulesService>();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<AuthService>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticationExtension>();
builder.Services.AddScoped<AutenticationExtension>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();


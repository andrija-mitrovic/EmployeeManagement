﻿using EmployeeManagement.Client.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace EmployeeManagement.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        public async Task<IActionResult> Employees()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var response = await httpClient.GetAsync("api/employees").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var employeesString = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<EmployeeViewModel>>(employeesString, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(employees);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Privacy()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient"); 
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync(); 
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken); 
            
            var response = await idpClient.GetUserInfoAsync(new UserInfoRequest 
            { 
                Address = metaDataResponse.UserInfoEndpoint, 
                Token = accessToken 
            }); 
            
            if (response.IsError) 
            { 
                throw new Exception("Problem while fetching data from the UserInfo endpoint", response.Exception); 
            }

            var addressClaim = response.Claims.FirstOrDefault(c => c.Type.Equals("address")); 
            
            User.AddIdentity(new ClaimsIdentity(
            new List<Claim> 
            { 
                new Claim(addressClaim.Type.ToString(), addressClaim.Value.ToString()) 
            })); 
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
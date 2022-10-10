using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebClient.Models;



namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Login(LoginModel loginmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient("client");
            var jsonrequest = JsonConvert.SerializeObject(loginmodel);
            var content = new StringContent(jsonrequest, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/Authentication", content).Result;
            if (response.IsSuccessStatusCode)
            {
                
                var token1 = response.Content.ReadAsStringAsync().Result;
                var token = JsonDeserializer<TokenModel>(token1);
                var cliams = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,loginmodel.UserName),
                    new Claim("AccessToken",token.Token),
                };
                var identity = new ClaimsIdentity(cliams, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                };
                HttpContext.SignInAsync(principal, properties);
            }
            
            return Redirect("/Home");
        }
        public static T JsonDeserializer<T>(string jsonString)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var instance = JsonConvert.DeserializeObject<T>(jsonString);

                //Log.Trace("successfully completed.", sw.Elapsed.TotalMilliseconds);
                return instance;
            }
            catch (Exception ex)
            {
               // Log.Fatal(ex.ToString(), sw.Elapsed.TotalMilliseconds);
                return default;
            }
        }
        public static string JsonSerializer<T>(T jsonObject)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                var jsonString = JsonConvert.SerializeObject(jsonObject, settings);

                //Log.Trace("successfully completed.", sw.Elapsed.TotalMilliseconds);
                return jsonString;
            }
            catch (Exception ex)
            {
              //  Log.Fatal(ex.ToString(), sw.Elapsed.TotalMilliseconds);
                return null;
            }
        }
        public IActionResult Logout()
        {
            return View();
        }
    }
}

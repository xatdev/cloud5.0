﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TestSuite.WebUI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using CommonModel = TestSuite.Common.Models;

namespace TestSuite.WebUI.Controllers
{

      public class HomeController : Controller
    {

        private const string WeatherURL = "https://localhost:5008/WeatherForecast";//"http://129.214.137.123:8888/WeatherForecast";
        private const string FileUploadURL = "https://localhost:5008/FileUpload";//"http://129.214.137.123:8888/FileUpload";
        // GET: /Home
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Weather/
        public async Task<IActionResult> Weather()
        {
            var API_ENDPOINT_Weather = Environment.GetEnvironmentVariable("API_URL") + "/WeatherForecast";
            Console.WriteLine("API_ENDPOINT_Weather : " + API_ENDPOINT_Weather);
            ViewData["xxx"] = "enter...";
            List<Weather> weatherList = new List<Weather>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_ENDPOINT_Weather))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    weatherList = JsonConvert.DeserializeObject<List<Weather>>(apiResponse);
                    ViewData["xxx"] = "success!!";
                }
            }
            return View(weatherList);
        }


         // POST: /Home/BlockAnalyzer/
        public async Task<IActionResult> BlockAnalyzer(IFormFile formFile)
        {
            var API_ENDPOINT_FileAnalyzer = Environment.GetEnvironmentVariable("API_URL") + "/FileUpload";
	    Console.WriteLine("API_ENDPOINT_FileAnalyzer : " + API_ENDPOINT_FileAnalyzer);
            // Verify that the user selected a file
            List<CommonModel.BlockAnalyzer> nullResult = new List<CommonModel.BlockAnalyzer>();
            var fileName = formFile.FileName;
            var filePath = Path.GetTempFileName();
            if (formFile != null && formFile.Length > 0) 
            {

                using (var stream = System.IO.File.Create(filePath))
                {
                    // The formFile is the method parameter which type is IFormFile
                    // Saves the files to the local file system using a file name generated by the app.
                    await formFile.CopyToAsync(stream);
                }
            }
            else
                return View(nullResult);
                
            // extract only the filename
            Console.WriteLine("file = " + fileName);

            using (var httpClient = new HttpClient())
            {
                //var filePath = @"D:\CountOfElements.scl";//Path.Combine("IntegrationTests", "file.csv");
                var gg = System.IO.File.ReadAllBytes(filePath);
                var byteArrayContent = new ByteArrayContent(gg);

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(byteArrayContent, "file", fileName);
                var postResponse = await httpClient.PostAsync(API_ENDPOINT_FileAnalyzer, multipartContent);

                HttpContent responseContent = postResponse.Content;
                if (responseContent != null)
                {
                    Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
                    String jsonContent = stringContentsTask.Result;    
                    var results = JsonConvert.DeserializeObject<List<CommonModel.BlockAnalyzer>>(jsonContent);
                    return View(results);
                }
            }
            return View(nullResult);
        }
    }


}

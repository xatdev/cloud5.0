
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using TestSuite.Common.Models;

namespace TestSuite.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {

        public IActionResult Get()
        {
            return Ok("File upload API running....");
        }
        
        [HttpPost]
        public async  Task<IActionResult> Upload(IFormFile file)
        {
            string uploads = Path.Combine(Directory.GetCurrentDirectory());
            string filePath = Path.Combine(uploads, file.FileName);
             using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
             }
             Console.WriteLine("File uploaded to : " + filePath);

            List<BlockAnalyzer> blockAnalyzerList = new List<BlockAnalyzer>();
            BlockAnalyzer ba = new BlockAnalyzer();
            ba.BlockName = file.FileName;
            ba.Errors = 13;
            ba.Warnings = 17;
            ba.Summary = "File uploaded to : " + filePath;
            blockAnalyzerList.Add(ba);

            return Ok(blockAnalyzerList);
        }
    }
}

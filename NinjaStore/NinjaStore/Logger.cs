using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaStore
{
    public class Logger
    {
        RequestDelegate next;

        public Logger(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerRepo repo)
        {
            //Request handling comes here
            // create a new log object
             var log = new Log
             {
                 Path = context.Request.Path,
                 Method = context.Request.Method,
                 QueryString = context.Request.QueryString.ToString()
             };

            // check if the Request is a POST call 
            // since we need to read from the body
            if (context.Request.Method == "POST")
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body)
                                                    .ReadToEndAsync();
                context.Request.Body.Position = 0;
                log.Payload = body;
            }

            log.RequestedOn = DateTime.Now;
            await next.Invoke(context);

            using (Stream originalRequest = context.Response.Body)
            {
                try
                {
                    using (var memStream = new MemoryStream())
                    {
                        context.Response.Body = memStream;
                        // All the Request processing as described above 
                        // happens from here.
                        // Response handling starts from here
                        // set the pointer to the beginning of the 
                        // memory stream to read
                        memStream.Position = 0;
                        // read the memory stream till the end
                        var response = await new StreamReader(memStream)
                                                                .ReadToEndAsync();
                        // write the response to the log object
                        log.Response = response;
                        log.ResponseCode = context.Response.StatusCode.ToString();
                        log.IsSuccessStatusCode = (
                              context.Response.StatusCode == 200 ||
                              context.Response.StatusCode == 201);
                        log.RespondedOn = DateTime.Now;

                        // add the log object to the logger stream 
                        // via the Repo instance injected
                        repo.AddToLogs(log);

                        // since we have read till the end of the stream, 
                        // reset it onto the first position
                        memStream.Position = 0;

                        // now copy the content of the temporary memory 
                        // stream we have passed to the actual response body 
                        // which will carry the response out.
                        await memStream.CopyToAsync(originalRequest);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    // assign the response body to the actual context
                    context.Response.Body = originalRequest;
                }
            }
        }
    }
}

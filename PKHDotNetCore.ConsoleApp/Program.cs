// See https://aka.ms/new-console-template for more information

using PKHDotNetCore.ConsoleApp.AdoDotNetExamples;
using PKHDotNetCore.ConsoleApp.DapperExamples;
using PKHDotNetCore.ConsoleApp.EFCoreExamples;
using PKHDotNetCore.ConsoleApp.HomeWork;
using PKHDotNetCore.ConsoleApp.HttpClientExamples;
using PKHDotNetCore.ConsoleApp.RestClientExamples;

// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Run();

// DapperExample dapperExample = new DapperExample();
// dapperExample.Run();

// EFCoreExample eFCoreExample = new EFCoreExample();
// eFCoreExample.Run();

// UserAdoDotNet userAdoDotNet = new UserAdoDotNet();
// userAdoDotNet.Run();

// UserDapper userDapper = new UserDapper();
// userDapper.Run();

// HttpClientExample httpClientExample = new HttpClientExample();
// await httpClientExample.Run();

RestClientExample restClientExample = new RestClientExample();
await restClientExample.Run();

Console.ReadKey();


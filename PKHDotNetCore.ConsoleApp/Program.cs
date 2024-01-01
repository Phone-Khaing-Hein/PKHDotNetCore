// See https://aka.ms/new-console-template for more information

using PKHDotNetCore.ConsoleApp.AdoDotNetExamples;
using PKHDotNetCore.ConsoleApp.DapperExamples;
using PKHDotNetCore.ConsoleApp.EFCoreExamples;
using PKHDotNetCore.ConsoleApp.HomeWork;

// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Run();

// DapperExample dapperExample = new DapperExample();
// dapperExample.Run();

// EFCoreExample eFCoreExample = new EFCoreExample();
// eFCoreExample.Run();

// UserAdoDotNet userAdoDotNet = new UserAdoDotNet();
// userAdoDotNet.Run();

UserDapper userDapper = new UserDapper();
userDapper.Run();

Console.ReadKey();


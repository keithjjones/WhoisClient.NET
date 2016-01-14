# Project Description
This is .NET Class library of WHOIS client (with sample web site).

# Sample Web Site
Enbale AppHarbor build support.
The url of sample web site is http://whoisclient.apphb.com/.

# How to install

To install this library into your application, please install from NuGet repository.

```
PM> Install-Package WhoisClient.NET
```

# Sample source code (C#)

```csharp
using Whois.NET;
...
var result = WhoisClient.Query("192.41.192.40");

Console.WriteLine("{0} - {1}", result.AddressRange.Begin, result.AddressRange.End); // "199.71.0.0 - 199.71.0.255"
Console.WriteLine("{0}", result.OrganizationName); // "American Registry for Internet Numbers"
Console.WriteLine(string.Join(" > ", result.RespondedServers)); // "whois.arin.net" 
```
#Whois Client Web site API

_Powered by ASP.NET MVC4 "Web API" feature._  
[http://www.asp.net/web-api](http://www.asp.net/web-api)

---

##  1. GET /api/query
### Request headers requirements
 - Accept: application/json

__DO NOT__ specify "application/xml" accept header.

### Parameters
- __query__ (required)  
ex) "apphb.com", "184.72.232.223"
- __server__ (optional)  
If don't specified server, determine automatically.  
ex) "whois.arin.net"
- __port__ (optinal)  
default value is "43". ex) "43"
- __encoding__ (optional)  
default value is "ASCII". ex) "iso-2022-jp"

### Returns

JSON string that WhoisResponse object serialized.

---

##  2. GET /api/rawquery
### Request headers requirements
- Accept: application/json  
or
- Accept: application/xml

### Parameters
- __query__ (required)  
ex) "apphb.com", "184.72.232.223"
- __server__ (required)  
ex) "whois.arin.net"
- __port__ (optinal)  
default value is "43". ex) "43"
- __encoding__ (optional)  
default value is "ASCII". ex) "iso-2022-jp"

### Returns
Simple string that responded form WHOIS server.
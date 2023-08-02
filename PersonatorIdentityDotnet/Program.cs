﻿using Newtonsoft.Json;
using System.Security.Cryptography;

namespace PersonatorConsumerDotnet
{
  static class Program
  {
    static void Main(string[] args)
    {
      string baseServiceUrl = @"https://globalpersonator.melissadata.net/";
      string serviceEndpoint = @"v1/doContactVerify"; 
      string license = "";
      string action = "";
      string fullname = "";
      string addressline1 = "";
      string locality = "";
      string administrativeArea = "";
      string postal = "";
      string country = "";


      ParseArguments(ref license, ref action, ref fullname, ref addressline1, ref locality, ref administrativeArea, ref postal, ref country, args);
      CallAPI(baseServiceUrl, serviceEndpoint, license, action, fullname, addressline1, locality, administrativeArea, postal, country);
    }

    static void ParseArguments(ref string license, ref string action, ref string fullname, ref string addressline1, ref string locality, ref string administrativeArea, 
      ref string postal, ref string country, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--action"))
        {
          if (args[i + 1] != null)
          {
            action = args[i + 1];
          }
        }
        if (args[i].Equals("--fullname"))
        {
          if (args[i + 1] != null)
          {
            fullname = args[i + 1];
          }
        }
        if (args[i].Equals("--addressline1"))
        {
          if (args[i + 1] != null)
          {
            addressline1 = args[i + 1];
          }
        }
        if (args[i].Equals("--locality"))
        {
          if (args[i + 1] != null)
          {
            locality = args[i + 1];
          }
        }
        if (args[i].Equals("--administrativearea"))
        {
          if (args[i + 1] != null)
          {
            administrativeArea = args[i + 1];
          }
        }
        if (args[i].Equals("--postal"))
        {
          if (args[i + 1] != null)
          {
            postal = args[i + 1];
          }
        }
        if (args[i].Equals("--country"))
        {
          if (args[i + 1] != null)
          {
            country = args[i + 1];
          }
        }
      }
    }

    public static async Task GetContents(string baseServiceUrl, string requestQuery)
    {
      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(baseServiceUrl);
      HttpResponseMessage response = await client.GetAsync(requestQuery);

      string text = await response.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject(text);
      var prettyResponse = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);

      // Print output
      Console.WriteLine("\n==================================== OUTPUT ====================================\n");
      
      Console.WriteLine("API Call: ");
      string APICall = Path.Combine(baseServiceUrl, requestQuery);
      for (int i = 0; i < APICall.Length; i += 70)
      {
        try
        {
          Console.WriteLine(APICall.Substring(i, 70));
        }
        catch
        {
          Console.WriteLine(APICall.Substring(i, APICall.Length - i));
        }
      }

      Console.WriteLine("\nAPI Response:");
      Console.WriteLine(prettyResponse);
    }
    
    static void CallAPI(string baseServiceUrl, string serviceEndPoint, string license, string action, string fullname, string addressline1, string locality, 
      string administrativeArea, string postal, string country)
    {
      Console.WriteLine("\n=============== WELCOME TO MELISSA PERSONATOR IDENTITY CLOUD API ===============\n");
      
      bool shouldContinueRunning = true;
      while (shouldContinueRunning)
      {
        string inputAction = "";
        string inputFullName = "";
        string inputAddressLine1 = "";
        string inputLocality = "";
        string inputAdministrativeArea = "";
        string inputPostal = "";
        string inputCountry = "";

        if (string.IsNullOrEmpty(action) && string.IsNullOrEmpty(fullname) && string.IsNullOrEmpty(addressline1) && string.IsNullOrEmpty(locality) && string.IsNullOrEmpty(administrativeArea) 
          && string.IsNullOrEmpty(postal) && string.IsNullOrEmpty(country))
        {
          Console.WriteLine("\nFill in each value to see results");

          Console.Write("Action: ");
          inputAction = Console.ReadLine();

          Console.Write("FullName: ");
          inputFullName = Console.ReadLine();

          Console.Write("AddressLine1: ");
          inputAddressLine1 = Console.ReadLine();

          Console.Write("Locality: ");
          inputLocality = Console.ReadLine();

          Console.Write("AdministrativeArea: ");
          inputAdministrativeArea = Console.ReadLine();

          Console.Write("PostalCode: ");
          inputPostal = Console.ReadLine();

          Console.Write("Country: ");
          inputCountry = Console.ReadLine();
        }
        else
        {
          inputAction = action;
          inputFullName = fullname;
          inputAddressLine1 = addressline1;
          inputLocality = locality;
          inputAdministrativeArea = administrativeArea;
          inputPostal = postal;
          inputCountry = country;
        }

        while (string.IsNullOrEmpty(inputAction) || string.IsNullOrEmpty(inputFullName) || string.IsNullOrEmpty(inputAddressLine1) || string.IsNullOrEmpty(inputLocality) 
          || string.IsNullOrEmpty(inputAdministrativeArea) || string.IsNullOrEmpty(inputPostal)|| string.IsNullOrEmpty(inputCountry))
        {
          Console.WriteLine("\nFill in missing required parameter");

          if (string.IsNullOrEmpty(inputAction))
          {
            Console.Write("Action: ");
            inputAction = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputFullName))
          {
            Console.Write("FullName: ");
            inputFullName = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputAddressLine1))
          {
            Console.Write("AddressLine1: ");
            inputAddressLine1 = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputLocality))
          {
            Console.Write("Locality: ");
            inputLocality = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputAdministrativeArea))
          {
            Console.Write("AdministrativeArea: ");
            inputAdministrativeArea = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputPostal))
          {
            Console.Write("PostalCode: ");
            inputPostal = Console.ReadLine();
          }

          if (string.IsNullOrEmpty(inputCountry))
          {
            Console.Write("Country: ");
            inputCountry = Console.ReadLine();
          }
        }

        Dictionary<string, string> inputs = new Dictionary<string, string>()
        {
            { "format", "json"},
            { "act", inputAction},
            { "full", inputFullName},
            { "a1", inputAddressLine1},
            { "loc", inputLocality},
            { "admarea", inputAdministrativeArea},
            { "postal", inputPostal},
            { "ctry", inputCountry}     
        };

        Console.WriteLine("\n===================================== INPUTS ===================================\n");
        Console.WriteLine($"\t   Base Service Url: {baseServiceUrl}");
        Console.WriteLine($"\t  Service End Point: {serviceEndPoint}");
        Console.WriteLine($"\t             Action: {inputAction}");
        Console.WriteLine($"\t           FullName: {inputFullName}");
        Console.WriteLine($"\t     Address Line 1: {inputAddressLine1}");
        Console.WriteLine($"\t           Locality: {inputLocality}");
        Console.WriteLine($"\t AdministrativeArea: {inputAdministrativeArea}");
        Console.WriteLine($"\t         PostalCode: {inputPostal}");
        Console.WriteLine($"\t            Country: {inputCountry}");

        // Create Service Call
        // Set the License String in the Request
        string RESTRequest = "";

        RESTRequest += @"&id=" + Uri.EscapeDataString(license);

        // Set the Input Parameters
        foreach (KeyValuePair<string, string> kvp in inputs)
          RESTRequest += @"&" + kvp.Key + "=" + Uri.EscapeDataString(kvp.Value);

        // Build the final REST String Query
        RESTRequest = serviceEndPoint + @"?" + RESTRequest;

        // Submit to the Web Service. 
        bool success = false;
        int retryCounter = 0;

        do
        {
          try //retry just in case of network failure
          {
            GetContents(baseServiceUrl, $"{RESTRequest}").Wait();
            Console.WriteLine();
            success = true;
          }
          catch (Exception ex)
          {
            retryCounter++;
            Console.WriteLine(ex.ToString());
            return;
          }
        } while ((success != true) && (retryCounter < 5));

        bool isValid = false;
        if (!string.IsNullOrEmpty(action + fullname + addressline1 + locality + administrativeArea + postal + country))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        while (!isValid)
        {
          Console.WriteLine("\nTest another record? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();
            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      
      Console.WriteLine("\n===================== THANK YOU FOR USING MELISSA CLOUD API ====================\n");
    }
  }
}
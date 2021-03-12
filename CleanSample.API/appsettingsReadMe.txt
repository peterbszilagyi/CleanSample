
************************************************************************************************
Serilog:
************
In order to log EF queries the "Microsoft": should be set to "Information"

 "MinimumLevel": {
      "Default": "Verbose",
      "Override": {
        "Microsoft": "Warning", <<< here....
        "System": "Error"
      }
    },

************

To log into the security this should be inserted to the log text

$"...[$SECURITY$]...."

or

using CleanSample.App.Common.Constants
....
$"...{LoggingContants.SECU}...."

************************************************************************************************


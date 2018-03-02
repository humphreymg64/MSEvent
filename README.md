# Contoso Air Baggage App

A flight manifest Baggage App for Contoso Air, to allow the various flight personel responsible for getting a flight off the ground to:

* track the last known location of a passenger's checked bag
* see what % of bags have been loaded, and how many still are in process
* see estimates of how much longer it will take to finish loading

The app will use IoT enabled RFID readers to scan bag tags as they move through the airport, and then process the input data via IoT hub, and AzureML to store the data in CosmosDB, and estimate how much longer until the plane is fully loaded with passenger bags.

The app will also feature a chat bot client, that allows flight crew to ask simple questions in natural language, and access the baggage loading process.

Technologies used:

* Xamarin / Xamarin.Forms
* Visaul Studio / Visual Studio for Mac
* Visual Studio Team Services
* Visual Studio App Center
* Azure Functions
* Azure IoT Hub
* Azure Bot Framework
* AzureML
* Azure CosmosDB
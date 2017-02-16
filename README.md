# SampleRegistrationWebApp
Sample Web Application for App Registration

How to use this sample web application?

-	Clone or fork the Github project from https://github.com/Dynamics365Apps/SampleRegistrationWebApp.git
-	Open the solution with in Visual Studio
-	Change the settings in appsettings.json
    - Set the initial user email and password
    - Specify the app id and name, use the same id as in the manifest to create the navx package
-	Publish it on Azure as a Microsoft Azure App Service
    - Include a Azure SQL Database
    - Run the migration on publishing
    - Make a note of the public URL of the Azure App Service    
-	Use the Hello World app sample (https://github.com/Dynamics365Apps/SampleAppWithRegistration) to learn how to implement the web app

Please read the white paper for an extensive explanation how to use the sample registration web app.
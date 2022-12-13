# Overview
NTC Lego is an inventory management control project developed with an expansive admin interface allowing for CRUD operations. A minimal customer sided interface, which allows items to be added to a cart, is also included. 

The solution contains three projects:
- A frontend using Blazor, styled using Bootstrap 5
- A backend using .Net Core 6, Entity Framework, and SQL Server
- A shared directory for Models and ViewModels

## Installation

- Download git repo and open solution is visual studio. 
- Right click the Server project to open a terminal.
- Run the following command to build a local database.
```
dotnet ef database update
```
- Open your performed database management tool, such as SSMS, and locate the newly created development database - labeled "**ntcLego-Dev**". 
- Locate the file titled “repoName\Documents\BrickLinkloadXMLSeedData.sql” in the repo, then run this script within the new database. 
  - Ensure you are also following the instructions included at the top of the script file, most importantly placing the BrickLink XML files at the following location on your local machine "C:\temp\".
- With your local database setup, you can now run the solution. 

## Notes for Future Development
#### BrickLink API
- https://www.bricklink.com/v3/api.page
- The BrickLink API is used to get additional information regarding LEGO items which the XMLs did not include, such as item to color relations. 
- API tokens and secrets are included within appsettings.json.

#### Model to ViewModel Mapping
- Used to avoid circular dependencies.
- By mapping directly to a view model using the Select Linq expression, we are able to specify which fields are populated with the database call response. 

#### Database Seed Data by an External Script
- The majority of the seed data used to fill the database is contained within XML files taken from the BrickLink website. These XMl files are so large (one containing 70,000 rows worth of data), that they could not be added to the database through an ef migration.

#### Get Most Recent BrickLink XMLs
- The XML files included within the repo are likely outdated; to get the most recent ones you must go to the BrickLink website, download them, and then replace the XMLs within the repo. 
  - If you are planning to rebuild the database to load in the new XML data, you must also replace the XML files you placed at “C:\temp\”.
Login to BrickLink 
- Go to https://www.bricklink.com/siteMap.asp and click the “Download” link.
- Download Catalog Items (Sets, Parts), Item Types, Categories, and Colors.
- Replace the old XMLs in the repo at “repoName\Documents\BrickLink\XML” with these new files. 

#### Running the Seed Data Script on a Remote Database
- To populate a remote database with seed data, you must use the “loadXMLSeedData(REMOTE).sql” file.
- The largest difference is that with a remote database, you cannot upload locally stored file data directly. You must host the XMLs in a remote storage container and then link to the external data source.
- More detailed instructions are included at the top of the script file.

#### Sequence to place data on Blazor page
- Create the necessary models and viewmodels, perform an entity framework database migration if any database changes have occurred (such as a new table).
  - To create a new ef migration, open a terminal in the Server project and run:
```
dotnet ef migrations add newMigration
```
- Create a DataService method which performs basic CRUD operation on the DataContext.
  - Model to ViewModel mapping may occur in these methods.
- Create a controller method which uses the dataservice method, and specifically an endpoint.
  - Business logic and validation should be performed within the controller.
- Use an HttpClient on the Blazor page to connect to the endpoint, display the data on the page.

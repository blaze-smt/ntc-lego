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
- Locate the file titled “repoName\Documents\BrickLink\loadXMLSeedData.sql” in the repo, then run this script within the new database. 
  - Ensure you are also following the instructions included at the top of the script file, most importantly placing the BrickLink XML files at the following location on your local machine "C:\temp\".
- With your local database setup, you can now run the solution. 
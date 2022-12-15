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

## Code-Behind
Blazor projects will include a section called @code{} for users to start the C# code. Code-behind is the technique of moving that code to a seperate file for cleaner code and easier use in a shared scenario.
In order to implement code behind on an existing or new razor view, follow these steps:
- Right-click on the folder that contains the razor page and select 'new item'.
- Name the new page the exact same as the razor page with the '.cs' suffix. Ex. PageName.razor.cs
- Make the new class a 'partial' class
- Cut all code in the @code{} section of the razor page and paste in the new class
- Remove the empty @code{} for a cleaner view
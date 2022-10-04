using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

using NTC_Lego.Shared;

namespace NTC_Lego.Server
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            // Load XML Data.
            LoadCategory(context);
        }

        private static void LoadCategory(DataContext context)
        {
            // Get path to XML file
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\categories.xml");

            using (context)
            {
                // Check if table is empty 
                if (!context.Category.Any())
                {
                    // Read XML file
                    XDocument xdoc = XDocument.Load(path);

                    // Map XML file to list
                    List<Category> categories = xdoc.Descendants("ITEM").Select(x => new Category()
                    {
                        CategoryId = Convert.ToInt32(x.Element("CATEGORY").Value),
                        CategoryName = x.Element("CATEGORYNAME").Value,
                    }).ToList();

                    // Add list contents into table and save changes. 
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        categories.ForEach(x => context.Category.Add(x));
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category ON;");
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category OFF;");
                        transaction.Commit();
                    }
                    
                    Console.WriteLine("\n*** Category table loaded with XML data. ***\n");
                }
                else
                {
                    Console.WriteLine("\n*** Category table already had content, no XML was loaded. ***\n");
                }
            }
        }
    }
}

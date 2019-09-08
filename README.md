# MongoCSharpTestFramework
MongoDB, C# and NUnit Testing Framwork
To use this MongoCSharp Test Frameork follow the below steps

Pre-Requisite:
1. Microsoft .Net Framework 4.5.2 
2. MongoDB v4.0.5

Steps:
1. Search for NextgenVSIXProject extension under Tools -> Extension and Updates and install it
2. After installation is completed, Create a project using Nextgen Test Project template
3. Right click on Refrences and select Manage NuGet Packages
4. Click to restore from your online package source message is displayed on the top of the page
5. Click on Restore button, after restoring is finished
6. Click on refresh button on the top of the solution
7. Restore the mongodb sample database
8. Edit the App.config of the project and change the MongoDbContext connectionString
9. Build the created above project
10. Restore MongoTestDB into local/server machine
11. Update the TestClassName in TestClasses collection according to your project namespace
12. Update the TestCaseName respectively if required in TestCases collection
13. Update the GlobalTestData respectively if required
13. Update the TestData for the testcases respectively in TestData collection
14. After changes is completed rebuild the project to get the testcases into test explorer



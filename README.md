Cqs
===

Overview
------
A Cqs architecture implemented in MVC using Dapper, Entity Framework, Simple Injector and claims based authorisation.
Also includes a query performance benchmarking console, that compares entity framework and dapper as tools for querying.

To Run the Web Application
------
1. Update the configs to point at your Sql Server instance - configs that need updating are in Cqs.PerformanceTests, Cqs.Tools.DatabaseCreator and Cqs.Presentation.Web. Run the Cqs.Tools.DatabaseCreator - this will create your database, and add some sprocs and data.
2. Install Cqs.Presentation.Web into IIS and enable windows authentication and disable anonymous access. Other wise the Claims Transformer will not execute.
3. Browse to the route of the site which is .../Customers

To Run the Query Performance Benchmarking Console
------
1. Update the configs to point at your Sql Server instance - configs that need updating are in Cqs.PerformanceTests, Cqs.Tools.DatabaseCreator. Run the Cqs.Tools.DatabaseCreator - this will create your database, and add some sprocs and data.
2. Run the Cqs.PerformanceTests and watch for the output on the screen 

Credits
------
This prototype builds upon the excellent patterns of the .Net Junkie https://www.cuttingedge.it/blogs/steven/

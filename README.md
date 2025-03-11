The project is developed using skills of C#, .Net, MVC, and JavaScript. It has the functions to show the list of products, add new product, display product details, update product, and soft delete product. 
It contains API consumers for all of these functions using C#, async task, and dependency injection except the soft delete (The requirement is to use given API Delete method to soft delete, but C#'s HTTPPClient's Delete doesn't allow such flexibility). Additionally, it also contains 2 functions in JavaScript to consume your API Delete method and Patch method. 
The starting file is Program.cs. The rouitng information is at the bottom of it. The configuration file is appsettings.json that contains your API config info
Home page: By default, when you run the project in development mode the homepage. Source code file: Views/Home/Index.cshtml. 
SureLock's landing page/Product List: The navigation bar,SureLock Test, on the top of the Homepage can navigate to the SureLock's landing page. It is a Locks list page. Source code files: Views/SureLock/Index.cshtml. Controllers/SureLockController/Index(...) methods
Add New Product: Source code files: Views/SureLock/Create.cshtml, Controllers/SureLockController/Create(...) methods
Display Product Detail: Source code files: Views/SureLock/Edit.cshtml, Controllers/SureLockController/Detail(...) method
Update product: Source code files: Views/SureLock/Edit.cshtml, Controllers/SureLockController/Edit*(...) methods, JavaScript source code: JSEdit() method in wwwroot/js/SureLockScript.js 
Delete product: Source code files: Views/SureLock/Edit.cshtml, Controllers/SureLockController/Delete(...) methods, JavaScript source code: JSDeleteUseAPI() method in wwwroot/js/SureLockScript.js 

Mobile user friendly: using Bootstrap library

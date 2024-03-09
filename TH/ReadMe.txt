DotNet Migration
-------------------------------------------------------
Set 1. Install Packages to API project
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools

Step 2. Install Packages to Infra project
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.AspNetCore.Identity.EntityFrameworkCore [For Identity]

Step 3. Add dependency in Program in API project or
Extend the dependency to the Infra project and add
services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthDB"));
            });
            services.AddScoped<AuthDbContext>();
Step 4. Make the API project Start-Up project as it's the mother project.
Step 5. Open NuGet Power Manager and MUST select the Infra project.
Step 6. Update in API project - make it FALSE
    <InvariantGlobalization>false</InvariantGlobalization>
Step 6. Run command:
    add-migration Initial
Step 7: Run command:
    update-database



#Scaffold-DcContext Issue:
https://www.youtube.com/watch?v=9hQXhUq3dgw

1. Make the selected project "Start-Up"
2. Select the project in package console
3. Type this:
Scaffold-DbContext "Data Source=.\SQLEXPRESS;Initial Catalog=GardenDb;User ID=sa;Password=Adm!n123;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


#SEtting Up A Project
- Update csproj file: <InvariantGlobalization>true</InvariantGlobalization> --> false

#Mongo
https://nexocode.com/blog/posts/getting-started-with-mongodb-in.net-core-applications/

#How to extend IServiceCollection
- install package
	Microsoft.Extensions.DependencyInjection
	Microsoft.Extensions.Configuration
	Microsoft.Extensions.Configuration.Binder

# Exception Filter
https://www.youtube.com/watch?v=Hh5ONzZo0Bo&t=367s

New Project Setup
------------------------------------------------
- Make it false --> <InvariantGlobalization>false</InvariantGlobalization>
- Install "CoreApiResponse" in API project
- Inherit Controller from "BaseController" of CoreApiResponse. Then u can use CustomResult
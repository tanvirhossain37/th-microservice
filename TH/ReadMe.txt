#Scaffold-DcContext Issue:
https://www.youtube.com/watch?v=9hQXhUq3dgw

1. Make the selected project "Start-Up"
2. Select the project in package console
3. Type this:
Scaffold-DbContext "Data Source=.\SQLEXPRESS;Initial Catalog=GardenDb;User ID=sa;Password=Adm!n123;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


#SEtting Up A Project
- Update csproj file: <InvariantGlobalization>true</InvariantGlobalization> --> false
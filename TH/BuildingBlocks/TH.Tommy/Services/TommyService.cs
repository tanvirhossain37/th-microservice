namespace TH.Tommy;

public class TommyService:BaseService
{
    private readonly string _template;
    private readonly string _factory;
    private readonly string _mapper;

    public TommyService()
    {
        _template = $"{TommyRoot}\\Templates";
        _factory = $"{ResultDestRoot}\\Factories";
        _mapper = $"{ResultDestRoot}\\Mappers";

        Util.DeleteDirectory(ResultDestRoot, true);

        Util.CreateDirectory(_factory);
        Util.CreateDirectory(_mapper);
    }


    public void CreateBE(string projectName, string feRoot)
    {
        Console.WriteLine();
        Console.WriteLine($"Creating BE...");
        Console.WriteLine();

        


        var files = Directory.GetFiles(SourceRoot);

        foreach (var file in files)
        {
            Console.WriteLine($"Entity: {Path.GetFileNameWithoutExtension(file)}");

            CreateBeEntities(file, projectName);
            CreateBeInputModels(file, projectName);
            CreateBeViewModels(file, projectName);
            CreateBeFilterModels(file, projectName);
            CreateBeRepos(file, files, projectName);
            CreateBeServices(file, projectName);
            CreateBeControllers(file, projectName);
        }

        CreateBeUoW(files, projectName);

    }

    private void CreateBeControllers(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var dest = Util.CreateDirectory($"{ResultDestRoot}\\Controllers");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                //get content

                //replace template
                string template = FileManager.Read($"{_template}\\BeController.txt");
                template = template.Replace("$namespace$", $"{projectName}");
                template = template.Replace("$WE$", fileNameWithoutExtension);
                template = template.Replace("$WES$", Util.TryPluralize(fileNameWithoutExtension));
                template = template.Replace("$we$", FileManager.ToCamelCase(fileNameWithoutExtension));
                template = template.Replace("$Access$", $"{projectName}RestrictAccess");

                //save it
                FileManager.Write($"{dest}\\{fileNameWithoutExtension}Controller.cs", template);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeUoW(string[] files, string projectName)
    {
        try
        {
            if (files == null) throw new ArgumentNullException(nameof(files));
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var interfaceDest = Util.CreateDirectory($"{ResultDestRoot}\\RepoInterfaces");
            var repoDest = Util.CreateDirectory($"{ResultDestRoot}\\Repos");

            string uowInterfacePropertyContent = GetUowInterfacePropertyContentOfBe(files);
            string uowPropertyContent = GetUowPropertyContentOfBe(files);
            string uowConstructorInjectContent = GetUowConstructorInjectContentOfBe(files);
            string uowConstructorContent = GetUowConstructorContentOfBe(files);

            //unit of work
            var iuowTemplate = FileManager.Read($"{_template}\\IUowRepo.txt");
            iuowTemplate = iuowTemplate.Replace("$namespace$", $"{projectName}.App");
            iuowTemplate = iuowTemplate.Replace("//we todo repo", uowInterfacePropertyContent);

            var uowTemplate = FileManager.Read($"{_template}\\UowRepo.txt");
            uowTemplate = uowTemplate.Replace("$namespace$", $"{projectName}.infra");
            uowTemplate = uowTemplate.Replace("$UOW$", FileManager.ToPascalCase(projectName));
            uowTemplate = uowTemplate.Replace("//we todo repo", uowPropertyContent);
            uowTemplate = uowTemplate.Replace("//we todo constructor inject", uowConstructorInjectContent);
            uowTemplate = uowTemplate.Replace("//we todo constructor", uowConstructorContent);

            FileManager.Write($"{interfaceDest}\\IUow.cs", iuowTemplate);
            FileManager.Write($"{repoDest}\\Uow.cs", uowTemplate);

            //factory
            FileManager.Append(Path.Combine(_factory, "Repos.txt"), $"services.AddScoped<IUow, Uow>();");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeServices(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var interfaceDest = Util.CreateDirectory($"{ResultDestRoot}\\ServiceInterfaces");
            var serviceDest = Util.CreateDirectory($"{ResultDestRoot}\\Services");
            var partialDest = Util.CreateDirectory($"{serviceDest}\\Partials");
            
            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                string dependencyContent = GetDependencyContentOfBe(lines, fileNameWithoutExtension);
                string serviceConstructorInjectContent = GetServiceConstructorInjectContentOfBe(lines, fileNameWithoutExtension);
                string partialServiceConstructorInjectContent = GetPartialServiceConstructorInjectContentOfBe(lines, fileNameWithoutExtension);
                string serviceConstructorContent = GetServiceConstructorContentOfBe(lines, fileNameWithoutExtension);
                string serviceDisposeContent = GetServiceDisposeContentOfBe(lines, fileNameWithoutExtension);
                string serviceUpdateContent = GetServiceUpdateContentOfBe(lines);
                string serviceFilterContent = GetServiceFilterContentOfBe(lines);
                string serviceSortContent = GetServiceSortContentOfBe(lines, fileNameWithoutExtension);
                string serviceValidationContent = GetServiceValidationContentOfBe(lines, fileNameWithoutExtension);
                string serviceValidationListOfBe = GetServiceValidationListOfBe(lines, fileNameWithoutExtension);
                string serviceChainSaveEffect = GetServiceChainSaveEffect(lines, fileNameWithoutExtension);
                string serviceChainUpdateEffect = GetServiceChainUpdateEffect(lines, fileNameWithoutExtension);
                string serviceChainDeleteEffect = GetServiceChainDeleteEffect(lines, fileNameWithoutExtension);
                string updateQuery = GetUpdateOrDeleteQuery(lines, fileNameWithoutExtension);
                string deleteQuery = GetUpdateOrDeleteQuery(lines, fileNameWithoutExtension);
                string findQuery = GetFindQuery(lines, fileNameWithoutExtension);
                string saveDuplicate = GetSaveDuplicate(lines, fileNameWithoutExtension);
                string updateDuplicate = GetUpdateDuplicate(lines, fileNameWithoutExtension);

                //get template

                #region Interface

                var interfaceTemplate = FileManager.Read($"{_template}\\IBeService.txt");
                interfaceTemplate = interfaceTemplate.Replace("$namespace$", $"{projectName}.App");
                interfaceTemplate = interfaceTemplate.Replace("$WE$", fileNameWithoutExtension);
                interfaceTemplate = interfaceTemplate.Replace("$WES$", Util.TryPluralize(fileNameWithoutExtension));

                #endregion

                #region Service

                string serviceTemplate = FileManager.Read($"{_template}\\BeService.txt");

                serviceTemplate = serviceTemplate.Replace("$namespace$", $"{projectName}.App");
                serviceTemplate = serviceTemplate.Replace("$WE$", fileNameWithoutExtension);
                serviceTemplate = serviceTemplate.Replace("$WES$", Util.TryPluralize(fileNameWithoutExtension));
                serviceTemplate = serviceTemplate.Replace("$UOW$", FileManager.ToPascalCase(projectName));
                serviceTemplate = serviceTemplate.Replace("//we todo dependency", dependencyContent);
                serviceTemplate = serviceTemplate.Replace("//we todo constructor inject", serviceConstructorInjectContent);
                serviceTemplate = serviceTemplate.Replace("//we todo constructor", serviceConstructorContent);
                serviceTemplate = serviceTemplate.Replace("//we todo dispose", serviceDisposeContent);
                serviceTemplate = serviceTemplate.Replace("//we todo update", serviceUpdateContent);
                serviceTemplate = serviceTemplate.Replace("//we todo filter", serviceFilterContent);
                serviceTemplate = serviceTemplate.Replace("//we todo sort", serviceSortContent);
                serviceTemplate = serviceTemplate.Replace("//we todo validation", serviceValidationContent);
                serviceTemplate = serviceTemplate.Replace("//todo chain effect save methods", serviceChainSaveEffect);
                serviceTemplate = serviceTemplate.Replace("//todo chain effect update methods", serviceChainUpdateEffect);
                serviceTemplate = serviceTemplate.Replace("//todo chain effect delete methods", serviceChainDeleteEffect);
                serviceTemplate = serviceTemplate.Replace("//we todo list validation", serviceValidationListOfBe);
                serviceTemplate = serviceTemplate.Replace("//todo updateQuery", updateQuery);
                serviceTemplate = serviceTemplate.Replace("//todo deleteQuery", deleteQuery);
                serviceTemplate = serviceTemplate.Replace("//todo findQuery", findQuery);
                serviceTemplate = serviceTemplate.Replace("//todo duplicate save", saveDuplicate);
                serviceTemplate = serviceTemplate.Replace("//todo duplicate update", updateDuplicate);

                #endregion

                #region Partials

                var partialTemplate = FileManager.Read($"{_template}\\BePartialService.txt");
                partialTemplate = partialTemplate.Replace("$namespace$", $"{projectName}.App");
                partialTemplate = partialTemplate.Replace("$WE$", fileNameWithoutExtension);
                partialTemplate = partialTemplate.Replace("//we todo constructor inject", serviceConstructorInjectContent);
                partialTemplate = partialTemplate.Replace("//we todo this constructor inject", partialServiceConstructorInjectContent);

                #endregion

                //save it
                FileManager.Write($"{interfaceDest}\\I{fileNameWithoutExtension}Service.cs", interfaceTemplate);
                FileManager.Write($"{serviceDest}\\{fileNameWithoutExtension}Service.cs", serviceTemplate);
                FileManager.Write($"{partialDest}\\{fileNameWithoutExtension}Service.cs", partialTemplate);

                //factory
                FileManager.Append(Path.Combine(_factory, "Services.txt"),
                    $"services.AddScoped<I{fileNameWithoutExtension}Service, {fileNameWithoutExtension}Service>();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    private void CreateBeRepos(string file, string[] files, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var interfaceDest = Util.CreateDirectory($"{ResultDestRoot}\\RepoInterfaces");
            var repoDest = Util.CreateDirectory($"{ResultDestRoot}\\Repos");
            var partialRepoDest = Util.CreateDirectory($"{repoDest}\\Partials");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                var interfaceNameMethodContent = GetRepoInterfaceNameMethodContent(lines, fileNameWithoutExtension);
                var interfaceCodeMethodContent = GetRepoInterfaceCodeMethodContent(lines, fileNameWithoutExtension);

                var nameMethodContent = GetRepoNameMethodContent(lines, fileNameWithoutExtension);
                var codeMethodContent = GetRepoCodeMethodContent(lines, fileNameWithoutExtension);

                //get template
                var interfaceTemplate = FileManager.Read($"{_template}\\IBeRepo.txt");
                interfaceTemplate = interfaceTemplate.Replace("$namespace$", $"{projectName}.App");
                interfaceTemplate = interfaceTemplate.Replace("$WE$", fileNameWithoutExtension);
                interfaceTemplate = interfaceTemplate.Replace("//todo repo name methods", interfaceNameMethodContent);
                interfaceTemplate = interfaceTemplate.Replace("//todo repo code methods", interfaceCodeMethodContent);

                var repoTemplate = FileManager.Read($"{_template}\\BeRepo.txt");
                repoTemplate = repoTemplate.Replace("$namespace$", $"{projectName}.App");
                repoTemplate = repoTemplate.Replace("$WE$", fileNameWithoutExtension);
                repoTemplate = repoTemplate.Replace("//todo repo name methods", nameMethodContent);
                repoTemplate = repoTemplate.Replace("//todo repo code methods", codeMethodContent);

                var partialRepoTemplate = FileManager.Read($"{_template}\\BePartialRepo.txt");
                partialRepoTemplate = partialRepoTemplate.Replace("$namespace$", $"{projectName}.App");
                partialRepoTemplate = partialRepoTemplate.Replace("$WE$", fileNameWithoutExtension);

                //save it
                FileManager.Write($"{interfaceDest}\\I{fileNameWithoutExtension}Repo.cs", interfaceTemplate);
                FileManager.Write($"{repoDest}\\{fileNameWithoutExtension}Repo.cs", repoTemplate);
                FileManager.Write($"{partialRepoDest}\\{fileNameWithoutExtension}Repo.cs", partialRepoTemplate);

                //FileManager.Write($"{interfaceDest}\\IUow{FileManager.ToPascalCase(moduleName)}.cs", iuowTemplate);
                //FileManager.Write($"{repoDest}\\Uow{FileManager.ToPascalCase(moduleName)}.cs", uowTemplate);

                //factory
                FileManager.Append(Path.Combine(_factory, "Repos.txt"),
                    $"services.AddScoped<I{fileNameWithoutExtension}Repo, {fileNameWithoutExtension}Repo>();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeFilterModels(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var dest = Util.CreateDirectory($"{ResultDestRoot}\\FilterModels");
            var partialDest = Util.CreateDirectory($"{dest}\\Partials");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                string propertyContent = GetPropertyContentOfBeFilterModel(lines);

                //get template
                var template = FileManager.Read($"{_template}\\BeFilterModel.txt");
                template = template.Replace("$namespace$", $"{projectName}.App");
                template = template.Replace("$WE$", fileNameWithoutExtension);
                template = template.Replace("//we todo properties", propertyContent);

                //partials
                var partialTemplate = FileManager.Read($"{_template}\\BeFilterModelPartial.txt");
                partialTemplate = partialTemplate.Replace("$namespace$", $"{projectName}.App");
                partialTemplate = partialTemplate.Replace("$WE$", fileNameWithoutExtension);

                //save it
                FileManager.Write($"{dest}\\{fileNameWithoutExtension}FilterModel.cs", template);
                FileManager.Write($"{partialDest}\\{fileNameWithoutExtension}FilterModel.cs", partialTemplate);

                //factory
                FileManager.Append(Path.Combine(_factory, @"FilterModels.txt"), $"services.AddScoped<{fileNameWithoutExtension}FilterModel>();");

                //mapper
                FileManager.Append(Path.Combine(_mapper, "FilterModels.txt"), $"CreateMap<{fileNameWithoutExtension}, {fileNameWithoutExtension}FilterModel>().ReverseMap();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeViewModels(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var dest = Util.CreateDirectory($"{ResultDestRoot}\\ViewModels");
            var partialDest = Util.CreateDirectory($"{dest}\\Partials");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                string propertyContent = GetPropertyContentOfBeViewModel(lines);
                string partialPropertyContent = GetPartialPropertyContentOfBeViewModel(lines, fileNameWithoutExtension);

                //get template
                var template = FileManager.Read($"{_template}\\BeViewModel.txt");
                template = template.Replace("$namespace$", $"{projectName}.App");
                template = template.Replace("$WE$", fileNameWithoutExtension);
                template = template.Replace("//we todo properties", propertyContent);

                //partials
                var partialTemplate = FileManager.Read($"{_template}\\BeViewModel.txt");
                partialTemplate = partialTemplate.Replace("$namespace$", $"{projectName}.App");
                partialTemplate = partialTemplate.Replace("$WE$", fileNameWithoutExtension);
                partialTemplate = partialTemplate.Replace("//we todo properties", partialPropertyContent);

                //save it
                FileManager.Write($"{dest}\\{fileNameWithoutExtension}ViewModel.cs", template);
                if (!string.IsNullOrWhiteSpace(partialPropertyContent))
                    FileManager.Write($"{partialDest}\\{fileNameWithoutExtension}ViewModel.cs", partialTemplate);

                //factory
                FileManager.Append(Path.Combine(_factory, @"ViewModels.txt"), $"services.AddScoped<{fileNameWithoutExtension}ViewModel>();");

                //mapper
                FileManager.Append(Path.Combine(_mapper, "ViewModels.txt"), $"CreateMap<{fileNameWithoutExtension}ViewModel, {fileNameWithoutExtension}>().ReverseMap();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeInputModels(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var dest = Util.CreateDirectory($"{ResultDestRoot}\\InputModels");
            var partialDest = Util.CreateDirectory($"{dest}\\Partials");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                string propertyContent = GetPropertyContentOfBeInputModel(lines);

                //get template
                var template = FileManager.Read($"{_template}\\BeInputModel.txt");
                template = template.Replace("$namespace$", $"{projectName}.App");
                template = template.Replace("$WE$", fileNameWithoutExtension);
                template = template.Replace("//we todo properties", propertyContent);

                var partialTemplate = string.Empty;

                partialTemplate = FileManager.Read($"{_template}\\BeInputModelPartial.txt");
                partialTemplate = partialTemplate.Replace("$namespace$", $"{projectName}.App");
                partialTemplate = partialTemplate.Replace("$WE$", fileNameWithoutExtension);

                //save it
                FileManager.Write($"{dest}\\{fileNameWithoutExtension}InputModel.cs", template);
                FileManager.Write($"{partialDest}\\{fileNameWithoutExtension}InputModel.cs", partialTemplate);

                //factory
                FileManager.Append(Path.Combine(_factory, @"InputModels.txt"), $"services.AddScoped<{fileNameWithoutExtension}InputModel>();");

                //mapper
                FileManager.Append(Path.Combine(_mapper, "InputModels.txt"), $"CreateMap<{fileNameWithoutExtension}InputModel, {fileNameWithoutExtension}>().ReverseMap();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateBeEntities(string file, string projectName)
    {
        try
        {
            file = string.IsNullOrWhiteSpace(file) ? throw new ArgumentNullException(nameof(file)) : file.Trim();
            projectName = string.IsNullOrWhiteSpace(projectName) ? throw new ArgumentNullException(nameof(projectName)) : projectName.Trim();

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var dest = Util.CreateDirectory($"{ResultDestRoot}\\Entities");

            var lines = Util.ReadObjectLines(file);
            if (lines != null)
            {
                string propertyContent = GetPropertyContentOfBeEntity(lines);

                //get template
                var template = FileManager.Read($"{_template}\\BeEntity.txt");
                template = template.Replace("$namespace$", $"{projectName}.Core");
                template = template.Replace("$WE$", fileNameWithoutExtension);
                template = template.Replace("//we todo properties", propertyContent);

                ////save it
                FileManager.Write($"{dest}\\{fileNameWithoutExtension}.cs", template);

                ////factory
                FileManager.Append(Path.Combine(_factory, @"Entities.txt"), $"services.AddScoped<I{fileNameWithoutExtension}, {fileNameWithoutExtension}>();");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPropertyContentOfBeEntity(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("public string Id")) ||
                    (line.Contains("public DateTime CreatedDate")) ||
                    (line.Contains("public DateTime? ModifiedDate")) ||
                    (line.Contains("public bool Active")))
                    continue;

                content = string.Concat(content, $"\n\t{line}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPropertyContentOfBeInputModel(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")) ||
                    (line.Contains("public DateTime CreatedDate")) ||
                    (line.Contains("public DateTime? ModifiedDate")) ||
                    (line.Contains("public bool Active")))
                    continue;

                content = string.Concat(content, $"\n\t{line}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPropertyContentOfBeViewModel(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                content = string.Concat(content, $"\n\t{line}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPartialPropertyContentOfBeViewModel(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                if (fieldName.EndsWith("Id"))
                {
                    if (fieldName.Equals("Id")) continue;

                    fieldName = fieldName.Substring(0, fieldName.Length - 2);
                    content = string.Concat(content, $"\n\tpublic string {fieldName}Name {{ get; set; }}");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPropertyContentOfBeFilterModel(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                var typeName = line.Trim().Split(' ')[1];

                if (typeName.Contains("bool"))
                    content = string.Concat(content, $"\n\tpublic bool? {fieldName} {{ get; set; }}");
                else if (typeName.Contains("DateTime"))
                    content = string.Concat(content, $"\n\tpublic DateTime? {fieldName} {{ get; set; }}");
                else if (typeName.Contains("TimeSpan"))
                    content = string.Concat(content, $"\n\tpublic TimeSpan? {fieldName} {{ get; set; }}");
                else
                    content = string.Concat(content, $"\n\t{line}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetRepoInterfaceNameMethodContent(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;

            bool yes = DoesTheWordExist("CompanyId", lines);

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];

                if (fieldName.Equals("Name"))
                {
                    if (fileNameWithoutExtension.Equals("Module"))
                    {
                        content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameAsync(string name, DataFilter dataFilter);");
                        content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter);");
                    }
                    else
                    {
                        if (yes)
                        {
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameAsync(string spaceId, string companyId, string name, DataFilter dataFilter);");
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameExceptMeAsync(string id, string spaceId, string companyId, string name, DataFilter dataFilter);");
                        }
                        else
                        {
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameAsync(string spaceId, string name, DataFilter dataFilter);");
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByNameExceptMeAsync(string id, string spaceId, string name, DataFilter dataFilter);");
                        }
                    }
                    
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetRepoInterfaceCodeMethodContent(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;

            bool yes = DoesTheWordExist("CompanyId", lines);

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];

                if (fieldName.Equals("Code"))
                {
                    if (fileNameWithoutExtension.Equals("Module"))
                    {
                        content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeAsync(string code, DataFilter dataFilter);");
                        content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeExceptMeAsync(string id, string code, DataFilter dataFilter);");
                    }
                    else
                    {
                        if (yes)
                        {
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeAsync(string spaceId, string companyId, string code, DataFilter dataFilter);");
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeExceptMeAsync(string id, string spaceId, string companyId, string code, DataFilter dataFilter);");
                        }
                        else
                        {
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeAsync(string spaceId, string code, DataFilter dataFilter);");
                            content = string.Concat(content, $"\n\tTask<{fileNameWithoutExtension}> FindByCodeExceptMeAsync(string id, string spaceId, string code, DataFilter dataFilter);");
                        }
                    }
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetRepoNameMethodContent(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            string content = string.Empty;
            var helperTemplate = string.Empty;

            bool yes = DoesTheWordExist("CompanyId", lines);

            if (fileNameWithoutExtension.Equals("Module"))
            {
                helperTemplate= FileManager.Read($"{_template}\\BeRepoNameHelperTemplateForModule.txt");
            }
            else
            {
                helperTemplate = FileManager.Read(yes ? $"{_template}\\BeRepoNameHelperTemplate.txt" : $"{_template}\\BeRepoNameHelperTemplateWithOutTenant.txt");
            }

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];

                if (fieldName.Equals("Name"))
                {
                    helperTemplate = helperTemplate.Replace("$WE$", fileNameWithoutExtension);

                    content = string.Concat(content, helperTemplate);
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetRepoCodeMethodContent(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            string content = string.Empty;
            var helperTemplate = string.Empty;
            bool yes = DoesTheWordExist("CompanyId", lines);

            if (fileNameWithoutExtension.Equals("Module"))
            {
                helperTemplate = FileManager.Read($"{_template}\\BeRepoCodeHelperTemplateForModule.txt");
            }
            else
            {
                helperTemplate = FileManager.Read(yes ? $"{_template}\\BeRepoCodeHelperTemplate.txt" : $"{_template}\\BeRepoCodeHelperTemplateWithOutTenant.txt");
            }

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];

                if (fieldName.Equals("Code"))
                {
                    helperTemplate = helperTemplate.Replace("$WE$", fileNameWithoutExtension);

                    content = string.Concat(content, helperTemplate);
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUowInterfacePropertyContentOfBe(string[] files)
    {
        try
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            var content = string.Empty;

            foreach (var file in files)
            {
                content = string.Concat(content, $"\n\tI{Path.GetFileNameWithoutExtension(file)}Repo {Path.GetFileNameWithoutExtension(file)}Repo {{ get; set; }}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUowPropertyContentOfBe(string[] files)
    {
        try
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            var content = string.Empty;

            foreach (var file in files)
            {
                content = string.Concat(content, $"\n\tpublic I{Path.GetFileNameWithoutExtension(file)}Repo {Path.GetFileNameWithoutExtension(file)}Repo {{ get; set; }}");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetDependencyContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("ICollection"))
                {
                    var dependency = (line.Trim().Split('<')[1]).Split('>')[0];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(dependency));
                    if (firstOrDefault != null)
                        continue;

                    if (dependency.Equals(fileNameWithoutExtension))
                        content = string.Concat(content, $"\n\t//protected readonly I{dependency}Service {dependency}Service;");
                    else
                    {
                        content = string.Concat(content, $"\n\tprotected readonly I{dependency}Service {dependency}Service;");
                    }

                    temp.Add(dependency);
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceConstructorInjectContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("ICollection"))
                {
                    var dependency = (line.Trim().Split('<')[1]).Split('>')[0];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(dependency));
                    if (firstOrDefault != null)
                        continue;

                    if (!dependency.Equals(fileNameWithoutExtension))
                        content = string.Concat(content, $", I{dependency}Service {FileManager.ToCamelCase(dependency)}Service");

                    temp.Add(dependency);
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetPartialServiceConstructorInjectContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("ICollection"))
                {
                    var dependency = (line.Trim().Split('<')[1]).Split('>')[0];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(dependency));
                    if (firstOrDefault != null)
                        continue;

                    if (!dependency.Equals(fileNameWithoutExtension))
                        content = string.Concat(content, $", {FileManager.ToCamelCase(dependency)}Service");

                    temp.Add(dependency);
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceConstructorContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {

        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("ICollection"))
                {
                    var dependency = (line.Trim().Split('<')[1]).Split('>')[0];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(dependency));
                    if (firstOrDefault != null)
                        continue;

                    if (!dependency.Equals(fileNameWithoutExtension))
                        content = string.Concat(content,
                            $"\n\t\t{dependency}Service = {FileManager.ToCamelCase(dependency)}Service ?? throw new ArgumentNullException(nameof({FileManager.ToCamelCase(dependency)}Service));");

                    temp.Add(dependency);
                }
            }

            return content;
        }

        catch (Exception)
        {
            Console.WriteLine();
            throw;
        }
    }

    private string GetServiceDisposeContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("ICollection"))
                {
                    var dependency = (line.Trim().Split('<')[1]).Split('>')[0];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(dependency));
                    if (firstOrDefault != null)
                        continue;

                    if (!dependency.Equals(fileNameWithoutExtension))
                        content = string.Concat(content,
                            $"\n\t\t\t{dependency}Service?.Dispose();");

                    temp.Add(dependency);
                }
            }

            return content;
        }
        catch (Exception)
        {
            Console.WriteLine();
            throw;
        }
    }

    private string GetServiceUpdateContentOfBe(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                if ((fieldName.Equals("Id")) ||
                    (fieldName.Equals("CreatedDate")) ||
                    (fieldName.Equals("ModifiedDate")) ||
                    (fieldName.Equals("Active")) ||
                    (fieldName.Equals("SpaceId")) ||
                    (fieldName.Equals("CompanyId")))
                    continue;

                content = string.Concat(content, $"\n\t\texistingEntity.{fieldName} = entity.{fieldName};");
            }

            return content;
        }
        catch (Exception)
        {
            Console.WriteLine();
            throw;
        }
    }

    private string GetServiceFilterContentOfBe(IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                var typeName = line.Trim().Split(' ')[1];

                if ((typeName.Contains("int")) || (typeName.Contains("long")) || (typeName.Contains("double")) || (typeName.Contains("decimal")))
                {
                    content = string.Concat(content, line.Contains("?")
                        ? $"\n\t\t\tif (filter.{fieldName}.HasValue && filter.{fieldName}.Value > 0) predicates.Add(t => t.{fieldName} == filter.{fieldName});"
                        : $"\n\t\t\tif (filter.{fieldName} > 0) predicates.Add(t => t.{fieldName} == filter.{fieldName});");
                }
                else if (typeName.Contains("string"))
                {
                    content = string.Concat(content, $"\n\t\t\tif (!string.IsNullOrWhiteSpace(filter.{fieldName})) predicates.Add(t => t.{fieldName}.Contains(filter.{fieldName}.Trim()));");
                }
                else if (typeName.Contains("bool"))
                {
                    content = string.Concat(content, $"\n\t\t\tif (filter.{fieldName}.HasValue) predicates.Add(t => t.{fieldName} == filter.{fieldName});");
                }
                else if (typeName.Contains("DateTime"))
                {
                    content = string.Concat(content, $"\n\t\t\tif (filter.{fieldName}.HasValue) predicates.Add(t => t.{fieldName} == filter.{fieldName});");
                }
                else if (typeName.Contains("TimeSpan"))
                {
                    content = string.Concat(content, $"\n\t\t\tif (filter.{fieldName}.HasValue) predicates.Add(t => t.{fieldName} == filter.{fieldName});");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceSortContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (fileNameWithoutExtension == null) throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            var content = string.Empty;

            var count = 0;
            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                if (fieldName.EndsWith("Id"))
                {
                    if (fieldName.Equals("Id")) continue;

                    fieldName = fieldName.Substring(0, fieldName.Length - 2);

                    count++;
                    content = string.Concat(content,
                        $"\n\t\t\t\tif (sortFilter.PropertyName.Equals(\"{fieldName}Name\", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = \"{fieldName}.Name\";");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceValidationContentOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? throw new ArgumentNullException(nameof(fileNameWithoutExtension)) : fileNameWithoutExtension.Trim();

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];
                var typeName = line.Trim().Split(' ')[1];

                if ((typeName.Contains("int")) || (typeName.Contains("long")) || (typeName.Contains("double")))
                {
                    content = string.Concat(content,
                        line.Contains("?")
                            ? $"\n\t\t\tif ((!entity.{fieldName}.HasValue) || (entity.{fieldName} <= 0)) entity.{fieldName} = null;"
                            : $"\n\t\t\tif (entity.{fieldName} <= 0) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\");");

                }
                else if (typeName.Contains("decimal"))
                {
                    content = string.Concat(content,
                        line.Contains("?")
                            ? $"\n\t\t\tif ((!entity.{fieldName}.HasValue) || (entity.{fieldName} < 0)) entity.{fieldName} = null;"
                            : $"\n\t\t\tif (entity.{fieldName} < 0) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\");");
                }
                else if (typeName.Contains("string"))
                {
                    if (fieldName.Equals("Code"))
                    {
                        content = string.Concat(content,
                            $"\n\t\t\tentity.{fieldName} = string.IsNullOrWhiteSpace(entity.{fieldName}) ? Util.TryGenerateCode() : entity.{fieldName}.Trim();");
                    }
                    else
                    {
                        content = string.Concat(content,
                            line.Contains("?")
                                ? $"\n\t\t\tentity.{fieldName} = string.IsNullOrWhiteSpace(entity.{fieldName}) ? string.Empty : entity.{fieldName}.Trim();"
                                : $"\n\t\t\tentity.{fieldName} = string.IsNullOrWhiteSpace(entity.{fieldName}) ? throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\") : entity.{fieldName}.Trim();");
                    }
                }
                else if (typeName.Contains("bool"))
                {
                    if (line.Contains("?"))
                    {
                        content = string.Concat(content, $"\n\t\t\tif (!entity.{fieldName}.HasValue) entity.{fieldName} = null;");
                    }
                }
                else if (typeName.Contains("DateTime"))
                {
                    if (line.Contains("?"))
                    {
                        content = string.Concat(content,
                            $"\n\t\t\tif (entity.{fieldName}.HasValue) {{ if (!Util.TryIsValidDate((DateTime)entity.{fieldName})) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\"); }}");
                    }
                    else
                    {
                        content = string.Concat(content, $"\n\t\t\tif (!Util.TryIsValidDate(entity.{fieldName})) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\");");
                    }
                }
                else if (typeName.Contains("TimeSpan"))
                {
                    if (line.Contains("?"))
                    {
                        content = string.Concat(content,
                            $"\n\t\t\tif (entity.{fieldName}.HasValue) {{ if (!Util.DateUtil.IsValidTime((TimeSpan)entity.{fieldName})) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\"); }}");
                    }
                    else
                    {
                        content = string.Concat(content, $"\n\t\t\tif (!Util.DateUtil.IsValidTime(entity.{fieldName})) throw new CustomException($\"{{Lang.Find(\"validation_error\")}}: {fieldName}\");");
                    }
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceValidationListOfBe(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? throw new ArgumentNullException(nameof(fileNameWithoutExtension)) : fileNameWithoutExtension.Trim();

            var content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")) && (line.Contains("ICollection")))
                {

                    var childEntity = line.Trim().Split('<', '>')[1];
                    var fieldName = line.Trim().Split(' ')[3];

                    content = string.Concat(content, $"\n\t\t\tif (entity.{fieldName} == null) entity.{fieldName} = new List<{childEntity}>();");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceChainSaveEffect(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? throw new ArgumentNullException(nameof(fileNameWithoutExtension)) : fileNameWithoutExtension.Trim();

            string content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")) && (line.Contains("ICollection")))
                {
                    var childEntity = line.Trim().Split('<', '>')[1];
                    var fieldName = line.Trim().Split(' ')[3];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(fieldName));
                    if (firstOrDefault != null)
                        continue;

                    if (!childEntity.Equals(fileNameWithoutExtension))
                    {
                        var template = FileManager.Read($"{_template}\\BeServiceChainEffectSave.txt");
                        template = template.Replace("$WE$", $"{childEntity}Service");
                        template = template.Replace("$WES$", fieldName);

                        content = string.Concat(content, $"\n{template}");

                        temp.Add(fieldName);
                    }
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceChainUpdateEffect(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? throw new ArgumentNullException(nameof(fileNameWithoutExtension)) : fileNameWithoutExtension.Trim();

            string content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")) && (line.Contains("ICollection")))
                {

                    var childEntity = line.Trim().Split('<', '>')[1];
                    var fieldName = line.Trim().Split(' ')[3];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(fieldName));
                    if (firstOrDefault != null)
                        continue;

                    if (!childEntity.Equals(fileNameWithoutExtension))
                    {
                        var template = FileManager.Read($"{_template}\\BeServiceChainEffectUpdate.txt");
                        template = template.Replace("$WE$", $"{childEntity}Service");
                        template = template.Replace("$WES$", fieldName);

                        content = string.Concat(content, $"\n{template}");

                        temp.Add(fieldName);
                    }
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetServiceChainDeleteEffect(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? throw new ArgumentNullException(nameof(fileNameWithoutExtension)) : fileNameWithoutExtension.Trim();

            string content = string.Empty;
            var temp = new List<string>();

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")) && (line.Contains("ICollection")))
                {

                    var childEntity = line.Trim().Split('<', '>')[1];
                    var fieldName = line.Trim().Split(' ')[3];

                    var firstOrDefault = temp.FirstOrDefault(x => x.Equals(fieldName));
                    if (firstOrDefault != null)
                        continue;

                    if (!childEntity.Equals(fileNameWithoutExtension))
                    {
                        var template = FileManager.Read($"{_template}\\BeServiceChainEffectDelete.txt");
                        template = template.Replace("$WE$", $"{childEntity}Service");
                        template = template.Replace("$WES$", fieldName);

                        content = string.Concat(content, $"\n{template}");

                        temp.Add(fieldName);
                    }
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUowConstructorInjectContentOfBe(string[] files)
    {
        try
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            var content = string.Empty;

            foreach (var file in files)
            {
                content = string.Concat(content, $", I{Path.GetFileNameWithoutExtension(file)}Repo {FileManager.ToCamelCase(Path.GetFileNameWithoutExtension(file))}Repo");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUowConstructorContentOfBe(string[] files)
    {
        try
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            var content = string.Empty;

            foreach (var file in files)
            {
                content = string.Concat(content,
                    $"\n\t\t\t{Path.GetFileNameWithoutExtension(file)}Repo = {FileManager.ToCamelCase(Path.GetFileNameWithoutExtension(file))}Repo ?? throw new ArgumentNullException(nameof({FileManager.ToCamelCase(Path.GetFileNameWithoutExtension(file))}Repo));");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUpdateOrDeleteQuery(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;
            var hasSpace = DoesTheWordExist("SpaceId", lines);
            var hasCompany = DoesTheWordExist("CompanyId", lines);

            if (hasSpace && hasCompany)
            {
                content = string.Concat(content, $"x => (x.SpaceId.Equals(entity.SpaceId)) && (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id))");
            }
            else if(hasSpace)
            {
                content = string.Concat(content, $"x => (x.SpaceId.Equals(entity.SpaceId)) && (x.Id.Equals(entity.Id))");
            }
            else if (hasCompany)
            {
                content = string.Concat(content, $"x => (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id))");
            }
            else
            {
                content = string.Concat(content, $"x => (x.Id.Equals(entity.Id))");
            }

            return content;
        }
        catch (Exception )
        {
            throw;
        }
    }

    private string GetFindQuery(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;
            var hasSpace = DoesTheWordExist("SpaceId", lines);
            var hasCompany = DoesTheWordExist("CompanyId", lines);

            if (hasSpace && hasCompany)
            {
                content = string.Concat(content, $"x => (x.SpaceId.Equals(filter.SpaceId)) && (x.CompanyId.Equals(filter.CompanyId)) && (x.Id.Equals(filter.Id))");
            }
            else if (hasSpace)
            {
                content = string.Concat(content, $"x => (x.SpaceId.Equals(filter.SpaceId)) && (x.Id.Equals(filter.Id))");
            }
            else if (hasCompany)
            {
                content = string.Concat(content, $"x => (x.CompanyId.Equals(filter.CompanyId)) && (x.Id.Equals(filter.Id))");
            }
            else
            {
                content = string.Concat(content, $"x => (x.Id.Equals(filter.Id))");
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool DoesTheWordExist(string word, IEnumerable<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            string content = string.Empty;

            foreach (var line in lines)
            {
                if ((line.Contains("virtual")))
                    continue;

                var fieldName = line.Trim().Split(' ')[2];

                if (fieldName.Equals(word)) return true;
            }

            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetSaveDuplicate(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;
            var hasName = DoesTheWordExist("Name", lines);
            var hasCode = DoesTheWordExist("Code", lines);
            var hasSpace = DoesTheWordExist("SpaceId", lines);
            var hasCompany = DoesTheWordExist("CompanyId", lines);

            if (hasName)
            {
                if (hasSpace && hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameAsync(entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else if (hasSpace)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameAsync(entity.SpaceId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else if (hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameAsync(entity.CompanyId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameAsync(entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
            }

            if(hasCode)
            {
                if (hasSpace && hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeAsync(entity.SpaceId, entity.CompanyId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else if (hasSpace)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeAsync(entity.SpaceId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else if (hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeAsync(entity.CompanyId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeAsync(entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GetUpdateDuplicate(IEnumerable<string> lines, string fileNameWithoutExtension)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            fileNameWithoutExtension = string.IsNullOrWhiteSpace(fileNameWithoutExtension)
                ? throw new ArgumentNullException(nameof(fileNameWithoutExtension))
                : fileNameWithoutExtension.Trim();

            var content = string.Empty;
            var hasName = DoesTheWordExist("Name", lines);
            var hasCode = DoesTheWordExist("Code", lines);
            var hasSpace = DoesTheWordExist("SpaceId", lines);
            var hasCompany = DoesTheWordExist("CompanyId", lines);

            if (hasName)
            {
                if (hasSpace && hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameExceptMeAsync(entity.Id, entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else if (hasSpace)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameExceptMeAsync(entity.Id, entity.SpaceId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else if (hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameExceptMeAsync(entity.Id, entity.CompanyId, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
                else
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByName = await Repo.{fileNameWithoutExtension}Repo.FindByNameExceptMeAsync(entity.Id, entity.Name, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByName is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Name\");");
                }
            }

            if (hasCode)
            {
                if (hasSpace && hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeExceptMeAsync(entity.Id, entity.SpaceId, entity.CompanyId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else if (hasSpace)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeExceptMeAsync(entity.Id, entity.SpaceId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else if (hasCompany)
                {
                    content = string.Concat(content, $"\n\t\tvar existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeExceptMeAsync(entity.Id, entity.CompanyId, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
                else
                {
                    content = string.Concat(content, $"\n\t\t var existingEntityByCode = await Repo.{fileNameWithoutExtension}Repo.FindByCodeExceptMeAsync(entity.Id, entity.Code, dataFilter);");
                    content = string.Concat(content, $"\n\t\tif (existingEntityByCode is not null) throw new CustomException($\"{{Lang.Find(\"error_duplicate\")}}: Code\");");
                }
            }

            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
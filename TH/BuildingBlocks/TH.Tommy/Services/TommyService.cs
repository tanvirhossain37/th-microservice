namespace TH.Tommy;

public class TommyService:BaseService
{
    private readonly string _template;
    private readonly string _factory;
    private readonly string _mapper;

    public TommyService()
    {
        _template = $"{TommyRoot}\\Templates";
        _factory = $"{TommyRoot}\\Factories";
        _mapper = $"{TommyRoot}\\Mappers";

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
}
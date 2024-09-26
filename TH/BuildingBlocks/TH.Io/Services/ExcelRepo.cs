using System.Linq.Expressions;
using Ganss.Excel;

namespace TH.Io;

public class ExcelRepo : IExcelRepo
{
    public IEnumerable<TEntity> Fetch<TEntity>(string path) where TEntity : class, new()
    {
        path = string.IsNullOrWhiteSpace(path) ? throw new ArgumentNullException(nameof(path)) : path.Trim();

        return new ExcelMapper(path).Fetch<TEntity>();
    }

    public IEnumerable<TEntity> FetchMapColumnIndex<TEntity>(string path) where TEntity : class, new()
    {
        path = string.IsNullOrWhiteSpace(path) ? throw new ArgumentNullException(nameof(path)) : path.Trim();

        return new ExcelMapper(path) { HeaderRow = false }.Fetch<TEntity>();
    }

    public CustomFile Save<TEntity>(IEnumerable<TEntity> entities, string name, bool xlsx = true,
        IList<Expression<Func<TEntity, object>>> ignores = null) where TEntity : class
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim();

        //############# test - tanvir #############

        //var path = @"C:\Users\tanvir\Desktop\excel-test\" + Guid.NewGuid().ToString() + ".xlsx";
        //var excel = new ExcelMapper();
        //if (ignores != null)
        //{
        //    foreach (var ignore in ignores)
        //    {
        //        excel.Ignore(ignore);
        //    }
        //}

        //excel.Save(path, entities, name, xlsx);

        //############# end test #############

        var msReport = new MemoryStream();
        var mapper = new ExcelMapper();
        if (ignores != null)
        {
            foreach (var ignore in ignores)
                mapper.Ignore(ignore);
        }

        mapper.Save(msReport, entities, name, xlsx);
        var bytes = msReport.ToArray();

        var fileData = Convert.ToBase64String(bytes);

        //.xls	Microsoft Excel	application/vnd.ms-excel

        var customFile = new CustomFile
        {
            //Name = $"Attendances - {DateTime.Now:d MMM yyyy}.xlsx",
            Size = 0,
            DocTypeId = 0,
            Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            LastModifiedDate = null,
            Tags = null,
            FileData = fileData
        };

        return customFile;
    }
}
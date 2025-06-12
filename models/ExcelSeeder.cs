using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using LoginApi.Data;
using LoginApi.Models;

public static class ExcelSeeder
{
    public static void ImportFromExcel(AppDbContext context, string excelPath)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);
        var result = reader.AsDataSet();

        ImportUsers(context, result.Tables["ccUsers"]);
        ImportAreas(context, result.Tables["ccRIACat_Areas"]);
        ImportLogins(context, result.Tables["ccLogLogin"]);

        context.SaveChanges();
    }

    private static void ImportUsers(AppDbContext context, DataTable table)
    {
        for (int i = 1; i < table.Rows.Count; i++) // omitir encabezado
        {
            var row = table.Rows[i];

            var user = new User
            {
                User_id = Convert.ToInt32(row[0]),
                Login = row[1]?.ToString(),
                Nombres = row[2]?.ToString(),
                ApellidoPaterno = row[3]?.ToString(),
                ApellidoMaterno = row[4]?.ToString(),
                Password = row[5]?.ToString(),
                TipoUser_id = Convert.ToInt32(row[6]),
                Status = Convert.ToInt32(row[7]),
                fCreate = Convert.ToDateTime(row[8]),
                IDArea = Convert.ToInt32(row[9]),
                LastLoginAttempt = Convert.ToDateTime(row[10])
            };

            context.ccUsers.Add(user);
        }
    }

    private static void ImportAreas(AppDbContext context, DataTable table)
    {
        for (int i = 1; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];
            var idArea = Convert.ToInt32(row[0]);

            // Verifica en el ChangeTracker y en la base de datos
            bool exists = context.ccRIACat_Areas.Local.Any(a => a.IDArea == idArea) ||
                          context.ccRIACat_Areas.Any(a => a.IDArea == idArea);

            if (exists)
                continue;

            var area = new RIACatArea
            {
                IDArea = idArea,
                AreaName = row[1]?.ToString(),
                StatusArea = Convert.ToInt32(row[2]),
                CreateDate = Convert.ToDateTime(row[3]),
            };

            context.ccRIACat_Areas.Add(area);
        }
    }
    private static void ImportLogins(AppDbContext context, DataTable table)
    {
        for (int i = 1; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            var login = new Login
            {
                Id = Convert.ToInt32(row[0]),
                User_id = Convert.ToInt32(row[1]),
                Extension = Convert.ToInt32(row[2]),
                TipoMov = Convert.ToInt32(row[3]),
                Fecha = Convert.ToDateTime(row[4])
            };

            context.ccloglogin.Add(login);
        }
    }
}


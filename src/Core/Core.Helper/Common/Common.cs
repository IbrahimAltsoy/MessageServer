﻿namespace Core.Helper.Common;
public static class Common
{
    public static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }
    public static string GetStaticContentDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\");
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
    public static string GetFilePath(string FileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(_GetStaticContentDirectory, FileName);
        return result;
    }
}

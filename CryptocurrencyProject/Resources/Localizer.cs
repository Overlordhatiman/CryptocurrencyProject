using System;
using System.Globalization;
using System.Resources;
using System.Windows;

public class Localizer
{
    private static ResourceManager resourceManager;
    private static CultureInfo cultureInfo;

    static Localizer()
    {
        resourceManager = new ResourceManager("CryptocurrencyProject.Resources.Strings", typeof(Localizer).Assembly);
        cultureInfo = new CultureInfo("en-US");
    }

    public static string GetString(string key)
    {
        return resourceManager.GetString(key, cultureInfo);
    }

    public static void SetCulture(string cultureName)
    {
        cultureInfo = new CultureInfo(cultureName);
    }
}

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
}

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

// https://dev.to/_patrickgod/net-core-3-1-web-api-entity-framework-jumpstart-part-1-4jla
//https://www.udemy.com/course/net-core-31-web-api-entity-framework-core-jumpstart/
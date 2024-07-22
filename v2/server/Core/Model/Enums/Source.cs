namespace server.Core.Model.Enums;

public enum Source {
    User,
    Frida,
}

public static class SourceExtensions {
    public static string GetDescription(this Source source)
        => source switch {
            Source.User => "User Created",
            Source.Frida => "DTU Fødevareinstituttet - frida.fooddata.dk",
            _ => throw new StringReprentationForSourceNotDefinedException(source)
        };

    public static Source ToSource(this string source)
        => source switch {
            "User Created" => Source.User,
            "DTU Fødevareinstituttet - frida.fooddata.dk" => Source.Frida,
            _ => throw new SourceNotDefinedException(source)
        };
}

public class StringReprentationForSourceNotDefinedException(Source source) 
: Exception($"String representation not defined for source: {source}") 
{}

public class SourceNotDefinedException(string source) 
: Exception($"Source not defined: {source}") 
{}
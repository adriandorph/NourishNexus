namespace server.Core.Model.Enums;

public enum Accessibility {
    Public,
    Private,
    Friends,
    Followers,
    Restricted
}

public static class AccessibilityExtensions {
    public static string GetDescription(this Accessibility accessibility) {
        return accessibility switch {
            Accessibility.Public => "Public",
            Accessibility.Private => "Private",
            Accessibility.Friends => "Friends",
            Accessibility.Followers => "Followers",
            Accessibility.Restricted => "Restricted",
            _ => throw new AccessibilityDescriptionNotDefinedException(accessibility)
        };
    }

    public static Accessibility GetAccessibilityEnum(this string accessibility) {
        return accessibility switch {
            "Public" => Accessibility.Public,
            "Private" => Accessibility.Private,
            "Friends" => Accessibility.Friends,
            "Followers" => Accessibility.Followers,
            "Restricted" => Accessibility.Restricted,
            _ => throw new AccessibilityNotDefinedException(accessibility)
        };
    }
}

public class AccessibilityNotDefinedException(string accessibility) : 
Exception($"Accessibility not defined: {accessibility}")
{}

public class AccessibilityDescriptionNotDefinedException(Accessibility accessibility) : 
Exception($"Description not defined for accessibility: {accessibility.GetDescription()}")
{}
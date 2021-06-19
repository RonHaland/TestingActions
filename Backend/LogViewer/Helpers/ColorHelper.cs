namespace LogViewer.Helpers
{
    public static class ColorHelper
    {
        public static string GetColor(string level)
        {
            switch (level)
            {
                case "Verbose":
                    return "khaki";
                case "Debug":
                    return "greenyellow";
                case "Warning":
                    return "yellow";
                case "Error":
                    return "crimson";
                case "Fatal":
                    return "darkred";
                case "Information":
                default:
                    return "azure";
            }
        }

        public static string GetBootstrapClass(string level, bool isDarkMode = false)
        {
            var txt = isDarkMode ? "bg-" : "table-";
            switch (level)
            {
                case "Verbose":
                    return $"{txt}secondary";
                case "Debug":
                    return $"{txt}success";
                case "Warning":
                    return $"{txt}warning";
                case "Error":
                    return $"{txt}danger";
                case "Fatal":
                    return $"{txt}danger";
                case "Information":
                default:
                    return $"{txt}info";
            }
        }
    }
}

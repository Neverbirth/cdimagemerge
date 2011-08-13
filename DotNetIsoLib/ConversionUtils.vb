Friend NotInheritable Class ConversionUtils

    Public Shared Function GetRegExFromPattern(ByVal searchPattern As String) As Text.RegularExpressions.Regex
        'Anyone feel free to add missing characters that need replacing
        searchPattern = searchPattern.Replace(".", "\.")
        searchPattern = searchPattern.Replace("[", "\[")
        searchPattern = searchPattern.Replace("]", "\]")
        searchPattern = searchPattern.Replace("$", "\$")
        searchPattern = searchPattern.Replace("(", "\(")
        searchPattern = searchPattern.Replace(")", "\)")
        searchPattern = searchPattern.Replace("{", "\{")
        searchPattern = searchPattern.Replace("}", "\}")
        searchPattern = searchPattern.Replace("#", "\#")
        searchPattern = searchPattern.Replace("?", ".")
        searchPattern = searchPattern.Replace("*", ".+")

        Return New Text.RegularExpressions.Regex(searchPattern, Text.RegularExpressions.RegexOptions.IgnoreCase)
    End Function

End Class

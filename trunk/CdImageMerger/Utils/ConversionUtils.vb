Namespace Utils

    Public NotInheritable Class ConversionUtils

        Public Shared Function ConvertFromBytes(ByVal size As Double) As String
            Const format As String = "{0:0.00} {1}"

            If size < 1024 Then Return String.Format(format, size, "B")

            If size < (1024 * 1024) Then
                Return String.Format(format, size / 1024, "KB")
            End If

            If size < (1024 * 1024 * 1024) Then
                Return String.Format(format, size / (1024 * 1024), "MB")
            End If

            Return String.Format(format, size / (1024 * 1024 * 1024), "GB")
        End Function

    End Class

End Namespace
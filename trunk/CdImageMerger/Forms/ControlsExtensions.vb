Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<EditorBrowsable(EditorBrowsableState.Never)> _
Module ControlsExtensions

    <Extension()> _
    Public Sub SetShellTheme(ByVal source As ListView)
        CheckAndSetShellTheme(source)
    End Sub

    <Extension()> _
    Public Sub SetShellTheme(ByVal source As TreeView)
        CheckAndSetShellTheme(source)
    End Sub

    Private Sub CheckAndSetShellTheme(ByVal source As Control)
        If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= 6 Then
            NativeMethods.SetWindowTheme(source.Handle, "Explorer", Nothing)
        End If
    End Sub

    Private Class NativeMethods

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
        End Function


    End Class

End Module

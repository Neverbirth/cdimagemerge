Public Class PathTableInfo


    Private _rootDirectory As PathTableEntryInfo
    Public ReadOnly Property RootDirectory() As PathTableEntryInfo
        Get
            Return _rootDirectory
        End Get
    End Property

    Friend Sub New(ByVal rootPathElement As ImageInfo.PathTableRecord)

    End Sub

End Class

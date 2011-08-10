Public Class PathTableInfo

    Private pathTableList As IList(Of PathTableEntryInfo)

    Private _rootDirectory As PathTableEntryInfo
    Public ReadOnly Property RootDirectory() As PathTableEntryInfo
        Get
            Return _rootDirectory
        End Get
    End Property

    Friend Sub New()

    End Sub

    Public Function GetPathTableList() As IEnumerable(Of PathTableEntryInfo)
        Return New ObjectModel.ReadOnlyCollection(Of PathTableEntryInfo)(pathTableList)
    End Function

    Friend Sub SetPathTableList(ByVal pathTableList As IList(Of ImageInfo.PathTableRecord))
        _rootDirectory = New PathTableEntryInfo(pathTableList(0))

        Dim pathTables As New List(Of PathTableEntryInfo)(pathTableList.Count)
        pathTables.Add(_rootDirectory)

        ' Can a path table entry point to a parent with an index greater than its own?
        For i As Integer = 1 To pathTableList.Count - 1
            Dim pathTableEntry As New PathTableEntryInfo(pathTableList(i))

            pathTableEntry.SetParent(pathTables(pathTableList(i).parentDN - 1))
            pathTables.Add(pathTableEntry)
        Next

        Me.pathTableList = pathTables
    End Sub

End Class

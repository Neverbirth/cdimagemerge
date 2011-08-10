Public Class PathTableEntryInfo

#Region " Properties "

    Private _children As New List(Of PathTableEntryInfo)()
    Public ReadOnly Property Children() As IEnumerable(Of PathTableEntryInfo)
        Get
            Return _children
        End Get
    End Property

    Private _lba As Long
    Public ReadOnly Property LBA() As Long
        Get
            Return _lba
        End Get
    End Property

    Private _name As String
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Private _parent As PathTableEntryInfo
    Public ReadOnly Property Parent() As PathTableEntryInfo
        Get
            Return _parent
        End Get
    End Property

#End Region

#Region " Constructor "

    Friend Sub New(ByVal pathTableRecord As ImageInfo.PathTableRecord)
        _lba = pathTableRecord.dirLocation
        _name = pathTableRecord.dirID
    End Sub

#End Region

#Region " Methods"

    Friend Sub AddChildRecord(ByVal record As PathTableEntryInfo)
        _children.Add(record)
    End Sub

    Friend Sub RemoveChildRecord(ByVal record As PathTableEntryInfo)
        _children.Remove(record)
    End Sub

    Friend Sub SetParent(ByVal parent As PathTableEntryInfo)
        If _parent IsNot parent Then
            If _parent IsNot Nothing Then _
                _parent.RemoveChildRecord(Me)

            _parent = parent

            If _parent IsNot Nothing Then _
                _parent.AddChildRecord(Me)
        End If
    End Sub

#End Region

End Class

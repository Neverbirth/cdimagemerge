Public Class PathTableEntryInfo

    Private _children As IEnumerable(Of PathTableEntryInfo)
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

    Friend Sub New()

    End Sub

End Class

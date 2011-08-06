'TODO: Media flag bit?

<Flags()> _
Public Enum DirectoryRecordFlags
    NormalFile = 0
    Hidden = 1
    Directory = 2
    Associated = 4
    NotFinalRecord = 128
End Enum

Public Class DirectoryRecord

#Region " Properties "

    Private _creationDate As DateTimeOffset
    Public ReadOnly Property CreationDate() As DateTimeOffset
        Get
            Return _creationDate
        End Get
    End Property

    Private _lba As Integer
    Public ReadOnly Property LBA() As Integer
        Get
            Return _lba
        End Get
    End Property

    Private _length As Long
    Public ReadOnly Property Length() As Long
        Get
            Return _length
        End Get
    End Property

    Private _name As String
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Private _parent As DirectoryRecord
    Public ReadOnly Property Parent() As DirectoryRecord
        Get
            Return _parent
        End Get
    End Property

#End Region

#Region " Constructor "

    Friend Sub New()

    End Sub

#End Region

End Class

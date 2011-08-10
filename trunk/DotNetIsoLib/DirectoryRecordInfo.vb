'TODO: Media flag bit?

<Flags()> _
Public Enum DirectoryRecordFlags
    NormalFile = 0
    Hidden = 1
    Directory = 2
    Associated = 4
    NotFinalRecord = 128
End Enum

Public Class DirectoryRecordInfo

#Region " Fields "

    Private parentLba As Long

#End Region

#Region " Properties "

    Private _creationDate As DateTimeOffset
    Public ReadOnly Property CreationDate() As DateTimeOffset
        Get
            Return _creationDate
        End Get
    End Property

    Private _flags As DirectoryRecordFlags
    Public ReadOnly Property Flags() As DirectoryRecordFlags
        Get
            Return _flags
        End Get
    End Property

    Private _imageInfo As ImageInfo
    Public ReadOnly Property ImageInfo() As ImageInfo
        Get
            Return _imageInfo
        End Get
    End Property

    Public ReadOnly Property IsDirectory() As Boolean
        Get
            Return _flags And DirectoryRecordFlags.Directory = DirectoryRecordFlags.Directory
        End Get
    End Property

    Private _lba As Long
    Public ReadOnly Property LBA() As Long
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

    Private _parent As DirectoryRecordInfo
    Public ReadOnly Property Parent() As DirectoryRecordInfo
        Get
            If _parent Is Nothing And parentLba > 0 Then
                _parent = _imageInfo.GetDirectoryRecord(parentLba)
            End If

            Return _parent
        End Get
    End Property

#End Region

#Region " Constructor "

    Friend Sub New(ByVal directoryRecord As ImageInfo.DirectoryRecord)
        ParseDirectoryRecordData(directoryRecord)
    End Sub

#End Region

#Region " Methods "

    Public Function GetDirectories() As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        'Return _directoryRecords.Values.Where(Function(x) x.IsDirectory)
    End Function

    Public Function GetDirectories(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetDirectories(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Public Function GetDirectoryRecords() As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

    End Function

    Public Function GetDirectoryRecords(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetDirectoryRecords(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Public Function GetFiles() As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Public Function GetFiles(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetFiles(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Private Sub ParseDirectoryRecordData(ByVal value As ImageInfo.DirectoryRecord)
        _flags = value.fileFlags
        _lba = value.lsbStart
        _length = value.lsbDataLength
        _name = value.fi

        If Not IsDirectory AndAlso _name.Length > 2 AndAlso _name.Chars(_name.Length - 2) = ";"c Then
            _name = _name.Substring(0, _name.Length - 2)
        End If

        _creationDate = New DateTimeOffset(1900 + value.year, value.month, value.day, _
                                           value.hour, value.minute, value.second, _
                                           TimeSpan.FromMinutes(value.gmtOffset * 15))
    End Sub

    Friend Sub SetImageOwner(ByVal image As ImageInfo)
        _imageInfo = image
    End Sub

    Friend Sub SetParent(ByVal directoryRecord As DirectoryRecordInfo)
        _parent = directoryRecord
    End Sub

    Friend Sub SetParentLba(ByVal value As Long)
        parentLba = value
    End Sub

#End Region

End Class

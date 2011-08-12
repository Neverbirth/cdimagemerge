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

    Private childRecords As List(Of DirectoryRecordInfo)
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

    Friend Sub AddChildRecord(ByVal record As DirectoryRecordInfo)
        If Not IsDirectory Then
            Throw New InvalidOperationException("Cannot add child records to a directory record that is a file")
        End If

        If childRecords Is Nothing Then childRecords = New List(Of DirectoryRecordInfo)()

        childRecords.Add(record)
    End Sub

    Public Function GetDirectories() As IEnumerable(Of DirectoryRecordInfo)
        Return GetDirectoryRecords(Function(x) x.IsDirectory, False)
    End Function

    Public Function GetDirectories(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetDirectories(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Public Function GetDirectoryRecords() As IEnumerable(Of DirectoryRecordInfo)
        Dim predicate As Func(Of DirectoryRecordInfo, Boolean) = Nothing

        Return GetDirectoryRecords(predicate, False)
    End Function

    Public Function GetDirectoryRecords(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetDirectoryRecords(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        'Return _directoryRecords.Values.Where(Function(x) Not x.IsDirectory)
    End Function

    Private Function GetDirectoryRecords(ByVal predicate As Func(Of DirectoryRecordInfo, Boolean), ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
        If Not IsDirectory Then Return Enumerable.Empty(Of DirectoryRecordInfo)()

        If childRecords Is Nothing Then ImageInfo.ParseDirectoryRecord(LBA)

        Dim recordList As IEnumerable(Of DirectoryRecordInfo) = If(recursive, childRecords.SelectMany(Function(x) x.GetDirectoryRecords(predicate, True)), childRecords)

        Return If(predicate IsNot Nothing, recordList.Where(predicate), childRecords)
    End Function

    Public Function GetFiles() As IEnumerable(Of DirectoryRecordInfo)
        Return GetDirectoryRecords(Function(x) Not x.IsDirectory, False)
    End Function

    Public Function GetFiles(ByVal searchPattern As String) As IEnumerable(Of DirectoryRecordInfo)
        Return GetFiles(searchPattern, False)
    End Function

    Public Function GetFiles(ByVal searchPattern As String, ByVal recursive As Boolean) As IEnumerable(Of DirectoryRecordInfo)
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

    Friend Sub RemoveChildRecord(ByVal record As DirectoryRecordInfo)
        If Not IsDirectory Then
            Throw New InvalidOperationException("Cannot remove child records from a directory record that is a file")
        End If

        If childRecords Is Nothing Then Return

        childRecords.Remove(record)
    End Sub

    Friend Sub SetImageOwner(ByVal image As ImageInfo)
        _imageInfo = image
    End Sub

    Friend Sub SetName(ByVal value As String)
        _name = value
    End Sub

    Friend Sub SetParent(ByVal directoryRecord As DirectoryRecordInfo)
        If _parent IsNot directoryRecord Then
            If _parent IsNot Nothing AndAlso _parent.IsDirectory Then _
                _parent.RemoveChildRecord(Me)

            _parent = directoryRecord

            If _parent IsNot Nothing AndAlso _parent.IsDirectory Then _
                _parent.AddChildRecord(Me)
        End If
    End Sub

    Friend Sub SetParentLba(ByVal value As Long)
        parentLba = value
    End Sub

#End Region

End Class

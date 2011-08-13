Imports DotNetIsoLib
Imports System.Runtime.InteropServices

Namespace Forms

    'TODO: Add column sorting
    Public Class ExplorerView

#Region " Constants "

        Private Const FOLDER_KEY As String = "folder"

#End Region

#Region " Eventos "

        Public Event SelectedDirectoryChanged As EventHandler
        Public Event SelectedFileChanged As EventHandler

#End Region

#Region " Fields "

        Private Shared ReadOnly fileInfoCache As New Dictionary(Of String, NativeMethods.SHFILEINFO)()
        Private Shared ReadOnly fileInfoImageList As New ImageList()

#End Region

#Region " Properties "

        Private _imageInfo As ImageInfo
        Public Property ImageInfo() As ImageInfo
            Get
                Return _imageInfo
            End Get
            Set(ByVal value As DotNetIsoLib.ImageInfo)
                If _imageInfo IsNot value Then
                    FolderTree.Nodes.Clear()
                    FileList.Items.Clear()

                    _imageInfo = value

                    If _imageInfo IsNot Nothing Then
                        FolderTree.BeginUpdate()
                        FillFolderTree(_imageInfo.PathTableInfo.RootDirectory, Nothing)
                        FolderTree.EndUpdate()
                    End If
                End If
            End Set
        End Property

        Private _selectedDirectory As DirectoryRecordInfo
        Public ReadOnly Property SelectedDirectory() As DirectoryRecordInfo
            Get
                Return _selectedDirectory
            End Get
        End Property

        Private _selectedFile As DirectoryRecordInfo
        Public ReadOnly Property SelectedFile() As DirectoryRecordInfo
            Get
                Return _selectedFile
            End Get
        End Property

#End Region

#Region " Constructors "

        Shared Sub New()
            fileInfoImageList.ColorDepth = ColorDepth.Depth32Bit
            GetFileInfo(FOLDER_KEY)
        End Sub

        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            FileList.SmallImageList = fileInfoImageList
            FolderTree.ImageList = fileInfoImageList
        End Sub

#End Region

#Region " Event Handlers "

        Private Sub FileList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileList.DoubleClick
            If _selectedFile IsNot Nothing AndAlso _selectedFile.IsDirectory Then
                FolderTree.SelectedNode = FolderTree.SelectedNode.Nodes.Find(CStr(FolderTree.SelectedNode.Tag) + _selectedFile.Name + "/", False).FirstOrDefault()
            End If
        End Sub

        Private Sub FileList_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileList.HandleCreated
            FileList.SetShellTheme()
        End Sub

        Private Sub FileList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileList.SelectedIndexChanged
            If FileList.SelectedItems.Count > 0 Then
                _selectedFile = DirectCast(FileList.SelectedItems(0).Tag, DirectoryRecordInfo)
            Else
                _selectedFile = Nothing
            End If

            OnSelectedFileChanged(EventArgs.Empty)
        End Sub

        Private Sub FolderTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterSelect
            Dim directoryRecord = _imageInfo.GetDirectoryRecord(CStr(e.Node.Tag))

            _selectedDirectory = directoryRecord

            FileList.BeginUpdate()
            FileList.Items.Clear()
            For Each record In directoryRecord.GetDirectoryRecords()
                FillFileList(record)
            Next
            FileList.EndUpdate()

            OnSelectedDirectoryChanged(EventArgs.Empty)
        End Sub

        Private Sub FolderTree_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.HandleCreated
            FolderTree.SetShellTheme()
        End Sub

#End Region

#Region " Methods "

        Private Sub FillFileList(ByVal record As DirectoryRecordInfo)
            Dim item As New ListViewItem(record.Name)
            Dim key As String

            If record.IsDirectory Then
                key = FOLDER_KEY
            Else
                Dim ex As String = IO.Path.GetExtension(record.Name)

                key = If(String.IsNullOrEmpty(ex), "dummy", ex)

                If record.Flags And DirectoryRecordFlags.Hidden Then key += ".hidden"
            End If

            Dim info As NativeMethods.SHFILEINFO = GetFileInfo(key)

            item.ImageKey = key
            item.SubItems.Add(If(record.IsDirectory, String.Empty, Utils.ConversionUtils.ConvertFromBytes(record.Length)))
            item.SubItems.Add(record.LBA)
            item.SubItems.Add(info.szTypeName)
            item.SubItems.Add(record.CreationDate.ToString())
            item.SubItems.Add(record.Flags)
            item.Tag = record

            FileList.Items.Add(item)
        End Sub

        Private Sub FillFolderTree(ByVal pathEntry As PathTableEntryInfo, ByVal parentNode As TreeNode)
            Dim node As New TreeNode(pathEntry.Name)

            node.ImageKey = FOLDER_KEY

            If parentNode IsNot Nothing Then
                node.Tag = CStr(parentNode.Tag) + pathEntry.Name + "/"

                parentNode.Nodes.Add(node)
            Else
                node.Tag = pathEntry.Name

                FolderTree.Nodes.Add(node)
            End If

            node.Name = CStr(node.Tag)

            For Each child In pathEntry.Children
                FillFolderTree(child, node)
            Next
        End Sub

        Private Shared Function GetFileInfo(ByVal key As String) As NativeMethods.SHFILEINFO
            Dim info As NativeMethods.SHFILEINFO = Nothing

            If Not fileInfoCache.TryGetValue(key, info) Then
                info = New NativeMethods.SHFILEINFO()

                Dim attrs As UInteger = If(key = FOLDER_KEY, NativeMethods.FILE_ATTRIBUTE_DIRECTORY, NativeMethods.FILE_ATTRIBUTE_NORMAL)

                If key.EndsWith(".hidden") Then attrs = attrs Or NativeMethods.FILE_ATTRIBUTE_HIDDEN

                NativeMethods.SHGetFileInfo(key, attrs, info, Marshal.SizeOf(info), NativeMethods.SHGFI_TYPENAME Or NativeMethods.SHGFI_USEFILEATTRIBUTES Or NativeMethods.SHGFI_ICON Or NativeMethods.SHGFI_SMALLICON)

                fileInfoImageList.Images.Add(key, Icon.FromHandle(info.hIcon))

                fileInfoCache(key) = info
            End If

            Return info
        End Function

        Protected Overridable Sub OnSelectedDirectoryChanged(ByVal e As EventArgs)
            RaiseEvent SelectedDirectoryChanged(Me, e)
        End Sub

        Protected Overridable Sub OnSelectedFileChanged(ByVal e As EventArgs)
            RaiseEvent SelectedFileChanged(Me, e)
        End Sub

        Public Sub SetFoldersVisible(ByVal value As Boolean)
            If Me.FolderTree.Visible AndAlso Not value Then
                Me.FolderTree.Hide()
                Me.SplitContainer1.Panel1Collapsed = True
            ElseIf Not Me.FolderTree.Visible AndAlso value Then
                Me.FolderTree.Show()
                Me.SplitContainer1.Panel1Collapsed = False
            End If
        End Sub

#End Region

#Region " Nested Classes "

        Private Class NativeMethods

            Public Const FILE_ATTRIBUTE_HIDDEN As UInteger = &H2
            Public Const FILE_ATTRIBUTE_DIRECTORY As UInteger = &H10
            Public Const FILE_ATTRIBUTE_NORMAL As UInteger = &H80

            Public Const SHGFI_TYPENAME As UInteger = &H400
            Public Const SHGFI_USEFILEATTRIBUTES As UInteger = &H10
            Public Const SHGFI_ICON As UInteger = &H100
            Public Const SHGFI_SMALLICON As UInteger = &H1

            <StructLayout(LayoutKind.Sequential)> _
            Public Structure SHFILEINFO
                Public hIcon As IntPtr
                Public iIcon As Integer
                Public dwAttributes As UInteger
                <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
                Public szDisplayName As String
                <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
                Public szTypeName As String
            End Structure

            <DllImport("shell32.dll")> _
            Public Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As UInteger, ByRef psfi As SHFILEINFO, ByVal cbSizeFileInfo As UInteger, ByVal uFlags As UInteger) As Integer
            End Function

        End Class

#End Region

    End Class

End Namespace
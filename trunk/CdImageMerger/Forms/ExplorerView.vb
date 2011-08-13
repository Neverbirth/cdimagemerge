Imports DotNetIsoLib
Imports System.Runtime.InteropServices

Namespace Forms

    Public Class ExplorerView

#Region " Eventos "

        Public Event SelectedDirectoryChanged As EventHandler
        Public Event SelectedFileChanged As EventHandler

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

#Region " Event Handlers "

        Private Sub FolderTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterSelect
            Dim directoryRecord = _imageInfo.GetDirectoryRecord(CStr(e.Node.Tag))

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

        Private Sub FileList_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileList.HandleCreated
            FileList.SetShellTheme()
        End Sub

        Private Sub FileList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileList.SelectedIndexChanged
            OnSelectedFileChanged(EventArgs.Empty)
        End Sub

#End Region

#Region " Methods "

        Private Sub FillFileList(ByVal record As DirectoryRecordInfo)
            Dim item As New ListViewItem(record.Name)

            Dim info = New NativeMethods.SHFILEINFO()
            Dim attrs As UInteger = If(record.IsDirectory, NativeMethods.FILE_ATTRIBUTE_DIRECTORY, NativeMethods.FILE_ATTRIBUTE_NORMAL)

            If record.Flags And DirectoryRecordFlags.Hidden Then attrs = attrs Or NativeMethods.FILE_ATTRIBUTE_HIDDEN

            NativeMethods.SHGetFileInfo(record.Name, attrs, info, Marshal.SizeOf(info), NativeMethods.SHGFI_TYPENAME Or NativeMethods.SHGFI_USEFILEATTRIBUTES Or NativeMethods.SHGFI_ICON Or NativeMethods.SHGFI_SMALLICON)

            SmallImageList.Images.Add(record.Name, Icon.FromHandle(info.hIcon))

            item.ImageKey = record.Name
            item.SubItems.Add(If(record.IsDirectory, String.Empty, record.Length.ToString()))
            item.SubItems.Add(record.LBA)
            item.SubItems.Add(info.szTypeName)
            item.SubItems.Add(record.CreationDate.ToString())
            item.SubItems.Add(record.Flags)

            FileList.Items.Add(item)
        End Sub

        Private Sub FillFolderTree(ByVal pathEntry As PathTableEntryInfo, ByVal parentNode As TreeNode)
            Dim node As New TreeNode(pathEntry.Name)

            If parentNode IsNot Nothing Then
                node.Tag = CStr(parentNode.Tag) + pathEntry.Name + "/"

                parentNode.Nodes.Add(node)
            Else
                node.Tag = pathEntry.Name

                FolderTree.Nodes.Add(node)
            End If

            For Each child In pathEntry.Children
                FillFolderTree(child, node)
            Next
        End Sub

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
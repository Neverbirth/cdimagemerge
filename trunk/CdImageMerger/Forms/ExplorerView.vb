Public Class ExplorerView

#Region " Eventos "

    Public Event SelectedDirectoryChanged As EventHandler
    Public Event SelectedFileChanged As EventHandler

#End Region

#Region " Properties "

    Private _imageInfo As DotNetIsoLib.ImageInfo
    Public Property ImageInfo() As DotNetIsoLib.ImageInfo
        Get
            Return _imageInfo
        End Get
        Set(ByVal value As DotNetIsoLib.ImageInfo)
            If _imageInfo IsNot value Then
                FolderTree.Nodes.Clear()
                FileList.Items.Clear()

                _imageInfo = value
            End If
        End Set
    End Property

    Private _selectedDirectory As DotNetIsoLib.DirectoryRecord
    Public ReadOnly Property SelectedDirectory() As DotNetIsoLib.DirectoryRecord
        Get
            Return _selectedDirectory
        End Get
    End Property

    Private _selectedFile As DotNetIsoLib.DirectoryRecord
    Public ReadOnly Property SelectedFile() As DotNetIsoLib.DirectoryRecord
        Get
            Return _selectedFile
        End Get
    End Property

#End Region

#Region " Event Handlers "

    Private Sub FolderTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterSelect
        OnSelectedDirectoryChanged(EventArgs.Empty)
    End Sub

    Private Sub FileList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileList.SelectedIndexChanged
        OnSelectedFileChanged(EventArgs.Empty)
    End Sub

#End Region

#Region " Methods "

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

End Class

Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ExplorerView
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.FolderTree = New System.Windows.Forms.TreeView
            Me.SmallImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.FileList = New System.Windows.Forms.ListView
            Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.SuspendLayout()
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.FolderTree)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.FileList)
            Me.SplitContainer1.Size = New System.Drawing.Size(448, 382)
            Me.SplitContainer1.SplitterDistance = 145
            Me.SplitContainer1.SplitterWidth = 1
            Me.SplitContainer1.TabIndex = 16
            '
            'FolderTree
            '
            Me.FolderTree.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FolderTree.FullRowSelect = True
            Me.FolderTree.HideSelection = False
            Me.FolderTree.ImageIndex = 0
            Me.FolderTree.ImageList = Me.SmallImageList
            Me.FolderTree.Location = New System.Drawing.Point(0, 0)
            Me.FolderTree.Name = "FolderTree"
            Me.FolderTree.SelectedImageIndex = 0
            Me.FolderTree.ShowLines = False
            Me.FolderTree.Size = New System.Drawing.Size(145, 382)
            Me.FolderTree.TabIndex = 16
            '
            'SmallImageList
            '
            Me.SmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            Me.SmallImageList.ImageSize = New System.Drawing.Size(16, 16)
            Me.SmallImageList.TransparentColor = System.Drawing.Color.Transparent
            '
            'FileList
            '
            Me.FileList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
            Me.FileList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FileList.FullRowSelect = True
            Me.FileList.HideSelection = False
            Me.FileList.Location = New System.Drawing.Point(0, 0)
            Me.FileList.Name = "FileList"
            Me.FileList.Size = New System.Drawing.Size(302, 382)
            Me.FileList.SmallImageList = Me.SmallImageList
            Me.FileList.TabIndex = 14
            Me.FileList.UseCompatibleStateImageBehavior = False
            Me.FileList.View = System.Windows.Forms.View.Details
            '
            'ColumnHeader1
            '
            Me.ColumnHeader1.Text = "Name"
            '
            'ColumnHeader2
            '
            Me.ColumnHeader2.Text = "Size"
            Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'ColumnHeader3
            '
            Me.ColumnHeader3.Text = "LBA"
            Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'ColumnHeader4
            '
            Me.ColumnHeader4.Text = "Type"
            '
            'ColumnHeader5
            '
            Me.ColumnHeader5.Text = "Creation Date"
            '
            'ColumnHeader6
            '
            Me.ColumnHeader6.Text = "Flags"
            '
            'ExplorerView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "ExplorerView"
            Me.Size = New System.Drawing.Size(448, 382)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents FolderTree As System.Windows.Forms.TreeView
        Friend WithEvents FileList As System.Windows.Forms.ListView
        Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
        Friend WithEvents SmallImageList As System.Windows.Forms.ImageList

    End Class

End Namespace
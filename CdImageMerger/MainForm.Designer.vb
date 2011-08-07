<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.ImageInfoGroupBox = New System.Windows.Forms.GroupBox
        Me.ImageInfoSplitContainer = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.Label1 = New System.Windows.Forms.Label
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.TreeView2 = New System.Windows.Forms.TreeView
        Me.ListView2 = New System.Windows.Forms.ListView
        Me.Label2 = New System.Windows.Forms.Label
        Me.OpenBtn = New System.Windows.Forms.Button
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CdMageOffsetChk = New System.Windows.Forms.CheckBox
        Me.StartBtn = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.OriginStartNumeric = New System.Windows.Forms.NumericUpDown
        Me.DestinationStartNumeric = New System.Windows.Forms.NumericUpDown
        Me.OriginEndNumeric = New System.Windows.Forms.NumericUpDown
        Me.ActionCombo = New System.Windows.Forms.ComboBox
        Me.ImageInfoGroupBox.SuspendLayout()
        Me.ImageInfoSplitContainer.Panel1.SuspendLayout()
        Me.ImageInfoSplitContainer.Panel2.SuspendLayout()
        Me.ImageInfoSplitContainer.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.OriginStartNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DestinationStartNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OriginEndNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageInfoGroupBox
        '
        Me.ImageInfoGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImageInfoGroupBox.Controls.Add(Me.ImageInfoSplitContainer)
        Me.ImageInfoGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.ImageInfoGroupBox.Name = "ImageInfoGroupBox"
        Me.ImageInfoGroupBox.Size = New System.Drawing.Size(704, 330)
        Me.ImageInfoGroupBox.TabIndex = 5
        Me.ImageInfoGroupBox.TabStop = False
        Me.ImageInfoGroupBox.Text = "Information"
        '
        'ImageInfoSplitContainer
        '
        Me.ImageInfoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImageInfoSplitContainer.Location = New System.Drawing.Point(3, 16)
        Me.ImageInfoSplitContainer.Name = "ImageInfoSplitContainer"
        '
        'ImageInfoSplitContainer.Panel1
        '
        Me.ImageInfoSplitContainer.Panel1.Controls.Add(Me.SplitContainer1)
        Me.ImageInfoSplitContainer.Panel1.Controls.Add(Me.Label1)
        '
        'ImageInfoSplitContainer.Panel2
        '
        Me.ImageInfoSplitContainer.Panel2.Controls.Add(Me.SplitContainer2)
        Me.ImageInfoSplitContainer.Panel2.Controls.Add(Me.Label2)
        Me.ImageInfoSplitContainer.Size = New System.Drawing.Size(698, 311)
        Me.ImageInfoSplitContainer.SplitterDistance = 343
        Me.ImageInfoSplitContainer.SplitterWidth = 2
        Me.ImageInfoSplitContainer.TabIndex = 13
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(633, 505)
        Me.SplitContainer1.SplitterDistance = 206
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 15
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(206, 505)
        Me.TreeView1.TabIndex = 16
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(426, 505)
        Me.ListView1.TabIndex = 14
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(282, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Source Image"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TreeView2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.ListView2)
        Me.SplitContainer2.Size = New System.Drawing.Size(606, 505)
        Me.SplitContainer2.SplitterDistance = 196
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 16
        '
        'TreeView2
        '
        Me.TreeView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView2.Location = New System.Drawing.Point(0, 0)
        Me.TreeView2.Name = "TreeView2"
        Me.TreeView2.Size = New System.Drawing.Size(196, 505)
        Me.TreeView2.TabIndex = 16
        '
        'ListView2
        '
        Me.ListView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView2.Location = New System.Drawing.Point(0, 0)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(409, 505)
        Me.ListView2.TabIndex = 14
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(252, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Destination Image"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OpenBtn
        '
        Me.OpenBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.OpenBtn.Location = New System.Drawing.Point(464, 97)
        Me.OpenBtn.Name = "OpenBtn"
        Me.OpenBtn.Size = New System.Drawing.Size(75, 23)
        Me.OpenBtn.TabIndex = 6
        Me.OpenBtn.Text = "&Open"
        Me.OpenBtn.UseVisualStyleBackColor = True
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.AutoSize = False
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(151, 17)
        Me.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.AutoSize = False
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 477)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(728, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBox1.Controls.Add(Me.ActionCombo)
        Me.GroupBox1.Controls.Add(Me.OpenBtn)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.CdMageOffsetChk)
        Me.GroupBox1.Controls.Add(Me.StartBtn)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.OriginStartNumeric)
        Me.GroupBox1.Controls.Add(Me.DestinationStartNumeric)
        Me.GroupBox1.Controls.Add(Me.OriginEndNumeric)
        Me.GroupBox1.Location = New System.Drawing.Point(33, 348)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(663, 126)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Action"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(226, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Destination Start Sector"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Source Start Sector"
        '
        'CdMageOffsetChk
        '
        Me.CdMageOffsetChk.AutoSize = True
        Me.CdMageOffsetChk.Checked = True
        Me.CdMageOffsetChk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CdMageOffsetChk.Location = New System.Drawing.Point(229, 72)
        Me.CdMageOffsetChk.Name = "CdMageOffsetChk"
        Me.CdMageOffsetChk.Size = New System.Drawing.Size(155, 17)
        Me.CdMageOffsetChk.TabIndex = 4
        Me.CdMageOffsetChk.Text = "Use CDMage Sector Offset"
        Me.CdMageOffsetChk.UseVisualStyleBackColor = True
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(464, 42)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 23)
        Me.StartBtn.TabIndex = 0
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Source End Sector"
        '
        'OriginStartNumeric
        '
        Me.OriginStartNumeric.Location = New System.Drawing.Point(112, 45)
        Me.OriginStartNumeric.Name = "OriginStartNumeric"
        Me.OriginStartNumeric.Size = New System.Drawing.Size(108, 20)
        Me.OriginStartNumeric.TabIndex = 2
        '
        'DestinationStartNumeric
        '
        Me.DestinationStartNumeric.Location = New System.Drawing.Point(352, 45)
        Me.DestinationStartNumeric.Name = "DestinationStartNumeric"
        Me.DestinationStartNumeric.Size = New System.Drawing.Size(108, 20)
        Me.DestinationStartNumeric.TabIndex = 1
        '
        'OriginEndNumeric
        '
        Me.OriginEndNumeric.Location = New System.Drawing.Point(112, 71)
        Me.OriginEndNumeric.Name = "OriginEndNumeric"
        Me.OriginEndNumeric.Size = New System.Drawing.Size(108, 20)
        Me.OriginEndNumeric.TabIndex = 3
        '
        'ActionCombo
        '
        Me.ActionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ActionCombo.FormattingEnabled = True
        Me.ActionCombo.Location = New System.Drawing.Point(9, 19)
        Me.ActionCombo.Name = "ActionCombo"
        Me.ActionCombo.Size = New System.Drawing.Size(211, 21)
        Me.ActionCombo.TabIndex = 8
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 499)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ImageInfoGroupBox)
        Me.MinimumSize = New System.Drawing.Size(695, 503)
        Me.Name = "MainForm"
        Me.Text = "CDImageMerger"
        Me.ImageInfoGroupBox.ResumeLayout(False)
        Me.ImageInfoSplitContainer.Panel1.ResumeLayout(False)
        Me.ImageInfoSplitContainer.Panel1.PerformLayout()
        Me.ImageInfoSplitContainer.Panel2.ResumeLayout(False)
        Me.ImageInfoSplitContainer.Panel2.PerformLayout()
        Me.ImageInfoSplitContainer.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.OriginStartNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DestinationStartNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OriginEndNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageInfoGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents OpenBtn As System.Windows.Forms.Button
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents StartBtn As System.Windows.Forms.Button
    Friend WithEvents OriginEndNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents OriginStartNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents DestinationStartNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents CdMageOffsetChk As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ImageInfoSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView2 As System.Windows.Forms.TreeView
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ActionCombo As System.Windows.Forms.ComboBox

End Class

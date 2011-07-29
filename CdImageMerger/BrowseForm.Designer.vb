<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrowseForm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OriginTextBox = New System.Windows.Forms.TextBox
        Me.BrowseDestinationButton = New System.Windows.Forms.Button
        Me.BrowseOriginButton = New System.Windows.Forms.Button
        Me.DestinationTextBox = New System.Windows.Forms.TextBox
        Me.OkBtn = New System.Windows.Forms.Button
        Me.CancelBtn = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.OriginTextBox)
        Me.GroupBox1.Controls.Add(Me.BrowseDestinationButton)
        Me.GroupBox1.Controls.Add(Me.BrowseOriginButton)
        Me.GroupBox1.Controls.Add(Me.DestinationTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(450, 101)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Image files"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Destination file:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Source file:"
        '
        'OriginTextBox
        '
        Me.OriginTextBox.Location = New System.Drawing.Point(32, 32)
        Me.OriginTextBox.Name = "OriginTextBox"
        Me.OriginTextBox.Size = New System.Drawing.Size(373, 20)
        Me.OriginTextBox.TabIndex = 1
        '
        'BrowseDestinationButton
        '
        Me.BrowseDestinationButton.Location = New System.Drawing.Point(411, 69)
        Me.BrowseDestinationButton.Name = "BrowseDestinationButton"
        Me.BrowseDestinationButton.Size = New System.Drawing.Size(31, 23)
        Me.BrowseDestinationButton.TabIndex = 3
        Me.BrowseDestinationButton.Text = "..."
        Me.BrowseDestinationButton.UseVisualStyleBackColor = True
        '
        'BrowseOriginButton
        '
        Me.BrowseOriginButton.Location = New System.Drawing.Point(411, 30)
        Me.BrowseOriginButton.Name = "BrowseOriginButton"
        Me.BrowseOriginButton.Size = New System.Drawing.Size(31, 23)
        Me.BrowseOriginButton.TabIndex = 0
        Me.BrowseOriginButton.Text = "..."
        Me.BrowseOriginButton.UseVisualStyleBackColor = True
        '
        'DestinationTextBox
        '
        Me.DestinationTextBox.Location = New System.Drawing.Point(32, 71)
        Me.DestinationTextBox.Name = "DestinationTextBox"
        Me.DestinationTextBox.Size = New System.Drawing.Size(373, 20)
        Me.DestinationTextBox.TabIndex = 2
        '
        'OkBtn
        '
        Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OkBtn.Location = New System.Drawing.Point(387, 118)
        Me.OkBtn.Name = "OkBtn"
        Me.OkBtn.Size = New System.Drawing.Size(75, 23)
        Me.OkBtn.TabIndex = 6
        Me.OkBtn.Text = "&Ok"
        Me.OkBtn.UseVisualStyleBackColor = True
        '
        'CancelBtn
        '
        Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelBtn.Location = New System.Drawing.Point(306, 118)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(75, 23)
        Me.CancelBtn.TabIndex = 7
        Me.CancelBtn.Text = "&Cancel"
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'BrowseForm
        '
        Me.AcceptButton = Me.OkBtn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelBtn
        Me.ClientSize = New System.Drawing.Size(471, 152)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.OkBtn)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BrowseForm"
        Me.Text = "Carga de archivos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OriginTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BrowseDestinationButton As System.Windows.Forms.Button
    Friend WithEvents BrowseOriginButton As System.Windows.Forms.Button
    Friend WithEvents DestinationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OkBtn As System.Windows.Forms.Button
    Friend WithEvents CancelBtn As System.Windows.Forms.Button
End Class

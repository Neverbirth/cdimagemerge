Imports DotNetIsoLib

Imports System.IO
Imports System.Reflection

'TODO: Let user choose image mode

Public Class MainForm

#Region " Constants "

    Private Const CDMAGE_OFFSET As Integer = 149

#End Region

#Region " Fields "

    Private _origImage As String
    Private _destImage As String

    Private _actionCollection As CdImageActionCollection

#End Region

#Region " Event Handlers "

    Private Sub StatusStrip_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusStrip.Resize
        With StatusStrip
            .Items(0).Width = CInt(.Width * 0.75)
            .Items(1).Width = .Width - .Items(0).Width - .SizeGripBounds.Width - 5
        End With
    End Sub

    Private Sub Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenBtn.Click
        OpenImages()
    End Sub

    Private Sub ImportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartBtn.Click
        Dim currentAction As ICdImageAction = DirectCast(ActionCombo.SelectedValue, ICdImageAction)

        If Not CanExecuteAction(currentAction) Then
            MessageBox.Show("Cannot execute action, it needs more data than the provided", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)

            Return
        End If

        Me.StartBtn.Enabled = False

        Cursor.Current = Cursors.WaitCursor

        Dim origStartPos As Long = OriginStartNumeric.Value - 1
        Dim count As Long = If(OriginEndNumeric.Value = 0, OriginEndNumeric.Maximum, OriginEndNumeric.Value - origStartPos) - 1
        Dim destPos As Long = DestinationStartNumeric.Value - 1

        If CdMageOffsetChk.Checked Then
            origStartPos -= CDMAGE_OFFSET
            destPos -= CDMAGE_OFFSET
        End If

        Try
            Me.ToolStripStatusLabel1.Text = currentAction.Description

            currentAction.Execute(_origImage, _destImage, origStartPos, destPos, count)

            MessageBox.Show("Operation finished succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Me.ToolStripStatusLabel1.Text = "Operation successfully completed"
        Catch ex As Exception
            MessageBox.Show("Error performing action: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Me.ToolStripStatusLabel1.Text = ex.Message
        End Try

        Cursor.Current = Cursors.Default

        Me.StartBtn.Enabled = True
    End Sub

    Private Sub CdMageOffsetChk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CdMageOffsetChk.CheckedChanged
        If DirectCast(sender, CheckBox).Checked Then
            Me.OriginStartNumeric.Minimum = CDMAGE_OFFSET + 1
            Me.DestinationStartNumeric.Minimum = CDMAGE_OFFSET + 1
            Me.OriginEndNumeric.Maximum += CDMAGE_OFFSET
            Me.OriginStartNumeric.Maximum += CDMAGE_OFFSET
            Me.DestinationStartNumeric.Maximum += CDMAGE_OFFSET
        Else
            Me.OriginStartNumeric.Minimum = 1
            Me.DestinationStartNumeric.Minimum = 1
            Me.OriginEndNumeric.Maximum -= CDMAGE_OFFSET
            Me.OriginStartNumeric.Maximum -= CDMAGE_OFFSET
            Me.DestinationStartNumeric.Maximum -= CDMAGE_OFFSET
        End If
    End Sub

#End Region

#Region " Methods "

    Private Sub ActionProgress_Callback(ByVal progress As Double)
        Me.ToolStripProgressBar1.Value = progress
        Application.DoEvents()
    End Sub

    Private Function CanExecuteAction(ByVal action As ICdImageAction) As Boolean
        Select Case action.NeedsImage
            Case ImageNeed.None
                Return True

            Case ImageNeed.Source
                Return Not String.IsNullOrEmpty(_origImage)

            Case ImageNeed.Destination
                Return Not String.IsNullOrEmpty(_destImage)

            Case ImageNeed.Both
                Return Not String.IsNullOrEmpty(_destImage) AndAlso Not String.IsNullOrEmpty(_origImage)

            Case Else
                Throw New InvalidOperationException("Action with unknown value")
        End Select
    End Function

    Private Function GetImageSectors(ByVal imagePath As String, ByVal imageMode As Integer) As Integer
        Dim fileInfo As IO.FileInfo
        Dim fileSize As Long

        fileInfo = New IO.FileInfo(imagePath)
        fileSize = fileInfo.Length

        If fileSize Mod imageMode <> 0 Then
            Return -1
        End If

        Return fileSize / imageMode
    End Function

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        _actionCollection = New CdImageActionCollection()
        _actionCollection.AddFrom(Assembly.GetExecutingAssembly())

        For Each file In Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.CdImageAction.dll")
            Dim asm As Assembly = Assembly.LoadFrom(file)

            _actionCollection.AddFrom(asm)
        Next

        ActionCombo.DataSource = _actionCollection.ToArray()
        ActionCombo.DisplayMember = "Name"

        OpenImages()

        MyBase.OnLoad(e)
    End Sub

    Private Sub OpenImages()
        Using browseFrm As BrowseForm = New BrowseForm()
            If browseFrm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim imgInfo As ImageInfo
                Dim sectors As Integer

                _origImage = browseFrm.OriginImage
                _destImage = browseFrm.DestinationImage

                imgInfo = New ImageInfo(_origImage, ImageInfo.ImageModes.ModeTwo2352)

                sectors = GetImageSectors(_origImage, 2352)

                If sectors = -1 Then
                    _origImage = Nothing
                    imgInfo = Nothing
                    Me.OriginStartNumeric.Enabled = False
                    Me.OriginEndNumeric.Enabled = False
                    MessageBox.Show("Source image total size doesn't coincide with block size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Else
                    '                Me.OrigPathLabel.Text = _origImage
                    '                Me.OrigSectorsLabel.Text = sectors.ToString()
                    Me.OriginStartNumeric.Enabled = True
                    Me.OriginEndNumeric.Enabled = True
                    Me.OriginStartNumeric.Maximum = sectors - 1 + If(CdMageOffsetChk.Checked, 150, 0)
                    Me.OriginEndNumeric.Maximum = sectors + If(CdMageOffsetChk.Checked, 150, 0)
                End If

                SourceImageExplorerView.ImageInfo = imgInfo

                imgInfo = New ImageInfo(_destImage, ImageInfo.ImageModes.ModeTwo2352)
                sectors = GetImageSectors(_destImage, 2352)

                If sectors = -1 Then
                    _destImage = Nothing
                    imgInfo = Nothing
                    Me.DestinationStartNumeric.Enabled = False
                    MessageBox.Show("Destination image total size doesn't coincide with block size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Else
                    '                Me.DestPathLabel.Text = _destImage
                    '                Me.DestSectorsLabel.Text = sectors.ToString()
                    Me.DestinationStartNumeric.Enabled = True
                    Me.DestinationStartNumeric.Maximum = sectors - 1 + If(CdMageOffsetChk.Checked, 150, 0)
                End If

                DestinationImageExplorerView.ImageInfo = imgInfo
            End If
        End Using
    End Sub

#End Region

End Class

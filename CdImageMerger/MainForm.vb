Public Class MainForm

    Private Const CDMAGE_OFFSET As Integer = 149

    Private _origImage As String
    Private _destImage As String

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenImages()

        Me.StartBtn.Enabled = (_origImage <> String.Empty AndAlso _destImage <> String.Empty)
    End Sub

    Private Sub StatusStrip_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusStrip.Resize
        With StatusStrip
            .Items(0).Width = CInt(.Width * 0.75)
            .Items(1).Width = .Width - .Items(0).Width - .SizeGripBounds.Width - 5
        End With
    End Sub

    Private Sub Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenBtn.Click
        OpenImages()

        Me.StartBtn.Enabled = (_origImage <> String.Empty AndAlso _destImage <> String.Empty)
    End Sub

    Private Sub OpenImages()
        Using browseFrm As BrowseForm = New BrowseForm
            If browseFrm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim imgInfo As ImageInfo
                Dim sectors As Integer

                _origImage = browseFrm.OriginImage
                _destImage = browseFrm.DestinationImage

                imgInfo = New ImageInfo(_origImage, ImageInfo.ImageModes.ModeTwo2352)

                sectors = GetImageSectors(_origImage, 2352)

                If sectors = -1 Then
                    MessageBox.Show("El tamaño de bloque no coincide con el tamaño total de la imagen de origen", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                '                Me.OrigPathLabel.Text = _origImage
                '                Me.OrigSectorsLabel.Text = sectors.ToString()
                Me.OriginStartNumeric.Maximum = sectors - 1 + If(CdMageOffsetChk.Checked, 150, 0)
                Me.OriginEndNumeric.Maximum = sectors + If(CdMageOffsetChk.Checked, 150, 0)

                sectors = GetImageSectors(_destImage, 2352)

                If sectors = -1 Then
                    MessageBox.Show("El tamaño de bloque no coincide con el tamaño total de la imagen de destino", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                '                Me.DestPathLabel.Text = _destImage
                '                Me.DestSectorsLabel.Text = sectors.ToString()
                Me.DestinationStartNumeric.Maximum = sectors - 1 + If(CdMageOffsetChk.Checked, 150, 0)
            End If
        End Using
    End Sub

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

    Private Sub ImportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartBtn.Click
        Dim retVal As Integer

        Me.StartBtn.Enabled = False

        If ActionCompareRadio.Checked Then
            Me.ToolStripStatusLabel1.Text = "Comparing sectors..."
            retVal = CompareSectors()
        ElseIf ActionImportImgRadio.Checked Then
            Me.ToolStripStatusLabel1.Text = "Importing sectors from source image..."
            retVal = ImportSectors()
        ElseIf ActionFillSector.Checked Then
            Me.ToolStripStatusLabel1.Text = "Filling sectors of source image..."
            retVal = FillSectors(0)
        End If

        Select Case retVal
            Case 0
                MessageBox.Show("Operation finished succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.ToolStripStatusLabel1.Text = "Operation successfully completed"
            Case 1
                MessageBox.Show("Operation didn't finish succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.ToolStripStatusLabel1.Text = "Source file is in use by another program"
            Case 2
                MessageBox.Show("Operation didn't finish succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.ToolStripStatusLabel1.Text = "Destination file is in use by another program"
        End Select

        Me.StartBtn.Enabled = True
    End Sub

    Private Function CompareSectors() As Integer
        Using originalFileStream As IO.FileStream = New IO.FileStream(_origImage, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 2352)
            Using destinationFileStream As IO.FileStream = New IO.FileStream(_destImage, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 2352)
                Using originalBinReader = New IO.BinaryReader(originalFileStream)
                    Using destinationBinReader = New IO.BinaryReader(destinationFileStream)
                        Dim buffer As Byte(), destBuffer As Byte()
                        Dim i As Long, progressStart As Long
                        Dim origStartPos As Long = OriginStartNumeric.Value - 1
                        Dim origEndPos As Long = If(OriginEndNumeric.Value = 0, OriginEndNumeric.Maximum, OriginEndNumeric.Value) - 1
                        Dim destPos As Long = DestinationStartNumeric.Value - 1
                        Dim differentSectors As New List(Of String)()

                        If CdMageOffsetChk.Checked Then
                            origStartPos -= CDMAGE_OFFSET
                            origEndPos -= CDMAGE_OFFSET
                            destPos -= CDMAGE_OFFSET
                        End If

                        originalFileStream.Seek(origStartPos * 2352, IO.SeekOrigin.Begin)
                        destinationFileStream.Seek(destPos * 2352, IO.SeekOrigin.Begin)

                        progressStart = originalFileStream.Position

                        i = origStartPos

                        Do
                            buffer = originalBinReader.ReadBytes(2352)
                            destBuffer = destinationBinReader.ReadBytes(2352)

                            For j As Integer = 0 To destBuffer.GetUpperBound(0)
                                If buffer(j) <> destBuffer(j) Then
                                    differentSectors.Add((If(CdMageOffsetChk.Checked, i + 150, i)).ToString())
                                    Exit For
                                End If
                            Next

                            Me.ToolStripProgressBar1.Value = Math.Round((originalFileStream.Position - progressStart) / originalFileStream.Length * 100)
                            i += 1
                            Application.DoEvents()
                        Loop While i <= origEndPos AndAlso originalFileStream.Position <= originalFileStream.Length

                        Dim log As New LogForm
                        log.TextBox1.Text = String.Join(System.Environment.NewLine, differentSectors.ToArray())
                        log.Text += " - " + differentSectors.Count.ToString() + " different sectors"
                        log.Show(Me)
                        Return 0
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Function ImportSectors() As Integer
        Using originalFileStream As IO.FileStream = New IO.FileStream(_origImage, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 2352)
            Using destinationFileStream As IO.FileStream = New IO.FileStream(_destImage, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.Write, 2352)
                Using originalBinReader = New IO.BinaryReader(originalFileStream)
                    Using destinationBinWriter = New IO.BinaryWriter(destinationFileStream)
                        Dim buffer As Byte()
                        Dim i As Long, progressStart As Long
                        Dim origStartPos As Long = OriginStartNumeric.Value - 1
                        Dim origEndPos As Long = If(OriginEndNumeric.Value = 0, OriginEndNumeric.Maximum, OriginEndNumeric.Value) - 1
                        Dim destPos As Long = DestinationStartNumeric.Value - 1

                        If CdMageOffsetChk.Checked Then
                            origStartPos -= CDMAGE_OFFSET
                            origEndPos -= CDMAGE_OFFSET
                            destPos -= CDMAGE_OFFSET
                        End If

                        originalFileStream.Seek(origStartPos * 2352, IO.SeekOrigin.Begin)
                        destinationFileStream.Seek(destPos * 2352, IO.SeekOrigin.Begin)

                        progressStart = originalFileStream.Position

                        i = origStartPos

                        Do
                            buffer = originalBinReader.ReadBytes(2352)
                            destinationBinWriter.Write(buffer)

                            Me.ToolStripProgressBar1.Value = Math.Round((originalFileStream.Position - progressStart) / originalFileStream.Length * 100)
                            i += 1
                            Application.DoEvents()
                        Loop While i <= origEndPos AndAlso originalFileStream.Position <= originalFileStream.Length

                        destinationBinWriter.Flush()

                        Return 0
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Function FillSectors(ByVal character As Byte) As Integer
        Using destinationFileStream As IO.FileStream = New IO.FileStream(_destImage, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.Write, 2352)
            Using destinationBinWriter = New IO.BinaryWriter(destinationFileStream)
                Dim buffer As Byte()
                Dim i As Long, progressStart As Long
                Dim origStartPos As Long = OriginStartNumeric.Value - 1
                Dim origEndPos As Long = If(OriginEndNumeric.Value = 0, OriginEndNumeric.Maximum, OriginEndNumeric.Value) - 1
                Dim destPos As Long = DestinationStartNumeric.Value - 1

                If CdMageOffsetChk.Checked Then
                    origStartPos -= CDMAGE_OFFSET
                    origEndPos -= CDMAGE_OFFSET
                    destPos -= CDMAGE_OFFSET
                End If

                destinationFileStream.Seek(destPos * 2352, IO.SeekOrigin.Begin)

                progressStart = destinationFileStream.Position

                buffer = New Byte(2352) {}
                buffer = Enumerable.Range(0, 2352).Select(Function(value As Integer) character).ToArray()

                i = origStartPos
                Me.ToolStripProgressBar1.Value = Math.Round((destinationFileStream.Position - progressStart) / destinationFileStream.Length * 100)
                Do
                    destinationBinWriter.Write(buffer)

                    Me.ToolStripProgressBar1.Value = Math.Round((destinationFileStream.Position - progressStart) / destinationFileStream.Length * 100)
                    i += 1
                    Application.DoEvents()
                Loop While i <= origEndPos AndAlso destinationFileStream.Position <= destinationFileStream.Length

                destinationBinWriter.Flush()

                Return 0
            End Using
        End Using
    End Function

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

End Class

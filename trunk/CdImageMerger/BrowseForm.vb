Public Class BrowseForm

    Public ReadOnly Property OriginImage() As String
        Get
            Return OriginTextBox.Text
        End Get
    End Property

    Public ReadOnly Property DestinationImage() As String
        Get
            Return DestinationTextBox.Text
        End Get
    End Property

    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseDestinationButton.Click, BrowseOriginButton.Click
        Using openDialog As OpenFileDialog = New OpenFileDialog
            With openDialog
                .CheckFileExists = True
                .CheckPathExists = True
                .Filter = "Archivos de imagenes (*.bin,*.img)|*.bin;*.img"
                .FilterIndex = 0
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If sender.Equals(BrowseOriginButton) Then
                        Me.OriginTextBox.Text = .FileName
                    Else
                        Me.DestinationTextBox.Text = .FileName
                    End If
                End If
            End With
        End Using
    End Sub

    Private Sub BrowseForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult = Windows.Forms.DialogResult.OK Then
            If OriginTextBox.Text.Trim().Length = 0 AndAlso DestinationTextBox.Text.Trim().Length = 0 Then
                MessageBox.Show("Por favor, seleccione las dos imágenes a cargar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
            End If
        End If
    End Sub

End Class
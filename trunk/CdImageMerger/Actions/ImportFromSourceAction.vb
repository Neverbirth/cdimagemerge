Public Class ImportFromSourceAction
    Implements ICdImageAction

    Public ReadOnly Property Description() As String Implements ICdImageAction.Description
        Get
            Return "Import Sectors from Image"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ICdImageAction.Name
        Get
            Return "Importing sectors from source image..."
        End Get
    End Property

    Public ReadOnly Property NeedsImage() As ImageNeed Implements ICdImageAction.NeedsImage
        Get
            Return ImageNeed.Both
        End Get
    End Property

    Private _progressCallback As ProgressCallback
    Public Property ProgressCallback() As ProgressCallback Implements ICdImageAction.ProgressCallback
        Get
            Return _progressCallback
        End Get
        Set(ByVal value As ProgressCallback)
            _progressCallback = value
        End Set
    End Property

    Public Sub Execute(ByVal sourceImagePath As String, ByVal destinationImagePath As String, ByVal sourceSector As Integer, ByVal destinationSector As Integer, ByVal sectorCount As Integer) Implements ICdImageAction.Execute
        Using originalFileStream As IO.FileStream = New IO.FileStream(sourceImagePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 2352)
            Using destinationFileStream As IO.FileStream = New IO.FileStream(destinationImagePath, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.Write, 2352)
                Using originalBinReader = New IO.BinaryReader(originalFileStream)
                    Using destinationBinWriter = New IO.BinaryWriter(destinationFileStream)
                        Dim buffer As Byte()
                        Dim i As Long, progressStart As Long

                        originalFileStream.Seek(sourceSector * 2352, IO.SeekOrigin.Begin)
                        destinationFileStream.Seek(destinationSector * 2352, IO.SeekOrigin.Begin)

                        progressStart = originalFileStream.Position

                        Do
                            buffer = originalBinReader.ReadBytes(2352)
                            destinationBinWriter.Write(buffer)

                            If ProgressCallback IsNot Nothing Then ProgressCallback.Invoke(Math.Round((originalFileStream.Position - progressStart) / originalFileStream.Length * 100))

                            i += 1
                        Loop While i <= sectorCount AndAlso originalFileStream.Position <= originalFileStream.Length

                        destinationBinWriter.Flush()
                    End Using
                End Using
            End Using
        End Using
    End Sub

End Class

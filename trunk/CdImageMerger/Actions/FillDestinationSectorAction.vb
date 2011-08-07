Public Class FillDestinationSectorAction
    Implements ICdImageAction

    'TODO: Change so user can select this value
    Private Const character As Byte = 0

    Public ReadOnly Property Description() As String Implements ICdImageAction.Description
        Get
            Return "Filling sectors of source image..."
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ICdImageAction.Name
        Get
            Return "Fill Sectors"
        End Get
    End Property

    Public ReadOnly Property NeedsImage() As ImageNeed Implements ICdImageAction.NeedsImage
        Get
            Return ImageNeed.Destination
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
        Using destinationFileStream As IO.FileStream = New IO.FileStream(destinationImagePath, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.Write, 2352)
            Using destinationBinWriter = New IO.BinaryWriter(destinationFileStream)
                Dim buffer As Byte()
                Dim i As Long, progressStart As Long

                destinationFileStream.Seek(destinationSector * 2352, IO.SeekOrigin.Begin)

                progressStart = destinationFileStream.Position

                buffer = Enumerable.Range(0, 2352).Select(Function(value As Integer) character).ToArray()

                If ProgressCallback IsNot Nothing Then ProgressCallback.Invoke(Math.Round((destinationFileStream.Position - progressStart) / destinationFileStream.Length * 100))
                Do
                    destinationBinWriter.Write(buffer)

                    If ProgressCallback IsNot Nothing Then ProgressCallback.Invoke(Math.Round((destinationFileStream.Position - progressStart) / destinationFileStream.Length * 100))
                    i += 1
                Loop While i <= sectorCount AndAlso destinationFileStream.Position <= destinationFileStream.Length

                destinationBinWriter.Flush()
            End Using
        End Using
    End Sub

End Class

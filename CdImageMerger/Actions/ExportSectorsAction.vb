Public Class ExportSectorsAction
    Implements ICdImageAction

    Public ReadOnly Property Description() As String Implements ICdImageAction.Description
        Get
            Return "Exporting sectors..."
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ICdImageAction.Name
        Get
            Return "Export Sectors to File"
        End Get
    End Property

    Public ReadOnly Property NeedsImage() As ImageNeed Implements ICdImageAction.NeedsImage
        Get
            Return ImageNeed.Source
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
        Throw New NotImplementedException()
    End Sub

End Class

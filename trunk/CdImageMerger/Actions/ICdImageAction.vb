<Flags()> _
Public Enum ImageNeed As Byte
    None = 0
    Source = 1
    Destination = 2
    Both = Source Or Destination
End Enum

Public Delegate Sub ProgressCallback(ByVal progress As Double)

Public Interface ICdImageAction

    ReadOnly Property Description() As String
    ReadOnly Property Name() As String
    ReadOnly Property NeedsImage() As ImageNeed

    Property ProgressCallback() As ProgressCallback

    Sub Execute(ByVal sourceImagePath As String, ByVal destinationImagePath As String, ByVal sourceSector As Integer, ByVal destinationSector As Integer, ByVal sectorCount As Integer)

End Interface
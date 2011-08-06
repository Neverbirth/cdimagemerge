<Flags()> _
Public Enum ImageNeed As Byte
    None = 0
    Source = 1
    Destination = 2
End Enum

Public Interface ICdImageAction

    ReadOnly Property Description() As String
    ReadOnly Property Name() As String
    ReadOnly Property NeedsImage() As ImageNeed

    Sub Execute()

End Interface
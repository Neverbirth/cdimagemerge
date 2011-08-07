Imports System.Reflection

Friend Class CdImageActionCollection
    Implements IEnumerable(Of ICdImageAction)

    Private _actions As New List(Of ICdImageAction)(6)

    Public Sub AddFrom(ByVal asm As Assembly)
        For Each t As Type In asm.GetExportedTypes().Where(Function(x) Not x.IsAbstract AndAlso GetType(ICdImageAction).IsAssignableFrom(x))
            Dim action As ICdImageAction = DirectCast(Activator.CreateInstance(t), ICdImageAction)

            _actions.Add(action)
        Next
    End Sub

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of ICdImageAction) Implements System.Collections.Generic.IEnumerable(Of ICdImageAction).GetEnumerator
        Return _actions.GetEnumerator()
    End Function

    Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _actions.GetEnumerator()
    End Function

End Class

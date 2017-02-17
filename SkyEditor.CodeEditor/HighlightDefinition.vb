''' <summary>
''' Defines a set of code highlighting rules
''' </summary>
Public Class HighlightDefinition

    Public Sub New()
        NamedHighlightColors = New List(Of HighlightingStyle)
        Rules = New List(Of HighlightRule)
    End Sub

    ''' <summary>
    ''' The name of the code highlight definition
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String

    ''' <summary>
    ''' Pre-defined colors referenced by individual highlight rules
    ''' </summary>
    Public Property NamedHighlightColors As List(Of HighlightingStyle)

    ''' <summary>
    ''' The highlight rules
    ''' </summary>
    Public Property Rules As List(Of HighlightRule)

End Class

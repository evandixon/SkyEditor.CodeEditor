''' <summary>
''' Defines how to highlight a particular kind of expression
''' </summary>
Public Class HighlightRule

    ''' <summary>
    ''' Regular expression that represents what should be highlighted.
    ''' </summary>
    Public Property Regex As String

    ''' <summary>
    ''' Name of the Named Highlight Color that's already registered.
    ''' </summary>
    ''' <returns></returns>
    Public Property ColorName As String

    ''' <summary>
    ''' Whether or not the regular expression should be case sensitive
    ''' </summary>
    ''' <returns></returns>
    Public Property CaseSensitive As Boolean

End Class

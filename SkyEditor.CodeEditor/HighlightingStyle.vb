''' <summary>
''' A style that can be applied to text
''' </summary>
Public Class HighlightingStyle

    Public Sub New()
        Foreground = "Transparent"
        Background = "Transparent"
        FontStyle = "Normal"
        FontWeight = "Normal"
        Underline = False
    End Sub

    ''' <summary>
    ''' Name of the style
    ''' </summary>
    Public Property Name As String

    ''' <summary>
    ''' The foreground color of the text
    ''' </summary>
    ''' <remarks>Should be in HTML format (#RRGGBB)</remarks>
    Public Property Foreground As String

    ''' <summary>
    ''' The background color of the text
    ''' </summary>
    ''' <remarks>Should be in HTML format (#RRGGBB)</remarks>
    Public Property Background As String

    ''' <summary>
    ''' Weight of the font
    ''' </summary>
    ''' <returns></returns>
    Public Property FontWeight As String

    ''' <summary>
    ''' Style of the font
    ''' </summary>
    Public Property FontStyle As String

    ''' <summary>
    ''' Whether or not the text should be underlined
    ''' </summary>
    Public Property Underline As Boolean

End Class

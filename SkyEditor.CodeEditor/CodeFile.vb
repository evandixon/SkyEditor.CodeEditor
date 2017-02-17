Imports SkyEditor.Core.IO

''' <summary>
''' A text file that contains code highlighting information
''' </summary>
Public MustInherit Class CodeFile
    Inherits TextFile
    Public MustOverride Function GetCodeHighlightRules() As HighlightDefinition
End Class

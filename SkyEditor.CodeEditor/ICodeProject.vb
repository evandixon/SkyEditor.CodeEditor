''' <summary>
''' Represents a project that defines extra data for a code file
''' </summary>
Public Interface ICodeProject
    Function GetExtraData(code As CodeFile) As CodeExtraData
End Interface

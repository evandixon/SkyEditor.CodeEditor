''' <summary>
''' Documentation for a function
''' </summary>
Public Class FunctionDocumentation

    Public Sub New()
        ParameterSets = New List(Of ParameterSet)
    End Sub

    ''' <summary>
    ''' The name of the function
    ''' </summary>
    Public Property FunctionName As String

    ''' <summary>
    ''' A brief description of what the function does
    ''' </summary>
    Public Property FunctionDescription As String

    ''' <summary>
    ''' A list of valid parameter overloads
    ''' </summary>
    Public Property ParameterSets As List(Of ParameterSet)

    ''' <summary>
    ''' The return type of the function
    ''' </summary>
    Public Property ReturnTypeName As String

    ''' <summary>
    ''' Additional discussion about a function and its usage
    ''' </summary>
    Public Property Remarks As String

    Public Overrides Function ToString() As String
        If String.IsNullOrEmpty(FunctionName) Then
            Return MyBase.ToString
        Else
            Return FunctionName
        End If
    End Function

End Class
''' <summary>
''' A set of parameters, for use with function documentation
''' </summary>
Public Class ParameterSet

    Public Sub New()
        Parameters = New List(Of ParameterInfo)
    End Sub

    ''' <summary>
    ''' The parameters in the set
    ''' </summary>
    Public Property Parameters As List(Of ParameterInfo)

End Class

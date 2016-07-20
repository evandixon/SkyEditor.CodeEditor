Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input
Imports ICSharpCode.AvalonEdit.CodeCompletion
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF

Public Class AvalonEditControl
    Inherits DataBoundObjectControl

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler txtCode.TextArea.TextEntering, AddressOf txtCode_TextEntering
        AddHandler txtCode.TextArea.TextEntered, AddressOf txtCode_TextEntered
    End Sub

    Dim extraData As CodeExtraData
    Private WithEvents autoComplete As CompletionWindow

    Private Sub txtCode_TextEntered(sender As Object, e As TextCompositionEventArgs)
        If extraData IsNot Nothing Then
            If extraData.GetAutoCompleteChars.Contains(e.Text) Then
                autoComplete = New CompletionWindow(txtCode.TextArea)
                With autoComplete.CompletionList.CompletionData
                    For Each item In extraData.GetAutoCompleteData(GetLastPart(" "))
                        .Add(New AutoCompleteData(item, extraData.GetAutoCompleteChars))
                    Next
                End With
                autoComplete.Show()
            End If
        End If
    End Sub

    Private Sub txtCode_TextEntering(sender As Object, e As TextCompositionEventArgs)
        If autoComplete IsNot Nothing AndAlso e.Text.Length > 0 Then
            If Not Char.IsLetterOrDigit(e.Text(0)) Then
                autoComplete.CompletionList.RequestInsertion(e)
            End If
        End If
    End Sub

    Private Function GetLastPart(PrecedingChar As String)
        Dim partStart = txtCode.Text.LastIndexOf(PrecedingChar, txtCode.CaretOffset)
        If partStart = -1 Then
            partStart = 0
        End If
        Dim part As String = txtCode.Text.Substring(partStart + PrecedingChar.Length, txtCode.CaretOffset - partStart).Trim
        Return part
    End Function

    Private Sub autoComplete_Closed(sender As Object, e As EventArgs) Handles autoComplete.Closed
        autoComplete = Nothing
    End Sub

    Private Sub AvalonEditControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Header = My.Resources.Language.Code
        txtCode.ShowLineNumbers = True
    End Sub

    Public Overrides Property ObjectToEdit As Object
        Get
            Return MyBase.ObjectToEdit
        End Get
        Set(value As Object)
            MyBase.ObjectToEdit = value

            Dim highlighter As New AvalonCodeHighlighter(DirectCast(value, CodeFile).GetCodeHighlightRules)

            'Dim p = PluginManager.GetInstance.GetOpenedFileProject(GetEditingObject)
            'If p IsNot Nothing AndAlso TypeOf p Is ICodeProject Then
            '    extraData = DirectCast(p, ICodeProject).GetExtraData(GetEditingObject(Of CodeFile))
            'Else
            'extraData = New DebugExtraData
            'End If

            If extraData IsNot Nothing AndAlso extraData.AdditionalHighlightRules IsNot Nothing Then
                highlighter.AddRuleSet("Project Rules", extraData.AdditionalHighlightRules)
            End If

            txtCode.SyntaxHighlighting = highlighter
        End Set
    End Property
End Class

Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input
Imports ICSharpCode.AvalonEdit.CodeCompletion
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.UI.WPF

Public Class AvalonEditControl
    Inherits DataBoundViewControl

    Public Sub New(applicationViewModel As ApplicationViewModel)
        If applicationViewModel Is Nothing Then
            Throw New ArgumentNullException(NameOf(applicationViewModel))
        End If
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler txtCode.TextArea.TextEntering, AddressOf txtCode_TextEntering
        AddHandler txtCode.TextArea.TextEntered, AddressOf txtCode_TextEntered
        CurrentApplicationViewModel = applicationViewModel
    End Sub

    Dim extraData As CodeExtraData
    Private WithEvents autoComplete As CompletionWindow

    Protected ReadOnly Property CurrentApplicationViewModel As ApplicationViewModel

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

    Private Sub txtCode_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged
        DirectCast(ViewModel, CodeFile).Contents = txtCode.Text
        IsModified = True
    End Sub

    Public Overrides Property ViewModel As Object
        Get
            Return MyBase.ViewModel
        End Get
        Set(value As Object)
            MyBase.ViewModel = value

            txtCode.Text = DirectCast(ViewModel, CodeFile).Contents

            Dim highlighter As New AvalonCodeHighlighter(DirectCast(value, CodeFile).GetCodeHighlightRules)

            Dim p = CurrentApplicationViewModel.GetFileViewModelForModel(value)?.ParentProject
            If p IsNot Nothing AndAlso TypeOf p Is ICodeProject Then
                extraData = DirectCast(p, ICodeProject).GetExtraData(value)
            End If

            If extraData IsNot Nothing AndAlso extraData.AdditionalHighlightRules IsNot Nothing Then
                highlighter.AddRuleSet("Project Rules", extraData.AdditionalHighlightRules)
            End If

            txtCode.SyntaxHighlighting = highlighter
        End Set
    End Property
End Class

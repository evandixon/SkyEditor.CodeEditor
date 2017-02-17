﻿Imports System.Runtime.Serialization
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Utilities

''' <summary>
''' An instance of CodeExtraData that reads its information from disk.
''' </summary>
Public Class CodeExtraDataFile
    Inherits CodeExtraData
    Implements iNamed
    Implements IOpenableFile
    Implements iOnDisk
    Implements ISavableAs

    Private Class JsonStructure
        Public Property Database As List(Of FunctionDocumentation)
        Public Property AutoCompleteChars As List(Of Char)
    End Class

    Public Property Database As List(Of FunctionDocumentation)
    Public Property AutoCompleteChars As List(Of Char)
    Public Property Filename As String Implements iOnDisk.Filename

    Public ReadOnly Property Name As String Implements iNamed.Name
        Get
            If _name Is Nothing Then
                Return IO.Path.GetFileNameWithoutExtension(Filename)
            Else
                Return _name
            End If
        End Get
    End Property
    Dim _name As String

    Public Overrides Function GetAutoCompleteChars() As IEnumerable(Of Char)
        Return Me.AutoCompleteChars
    End Function

    Public Overrides Function GetAutoCompleteData(CurrentWord As String) As IEnumerable(Of FunctionDocumentation)
        Return (From d In Database Where d.FunctionName.ToLowerInvariant.StartsWith(CurrentWord.ToLowerInvariant) Select d)
    End Function

    Public Overrides Function GetDocumentation(FunctionName As String) As FunctionDocumentation
        Return (From d In Database Where String.Compare(d.FunctionName, FunctionName, True, Globalization.CultureInfo.InvariantCulture) = 0 Select d).FirstOrDefault
    End Function

    Public Overrides Function AdditionalHighlightRules() As HighlightDefinition
        Dim out As New HighlightDefinition
        out.Name = "Function Names"
        out.NamedHighlightColors.Add(New HighlightingStyle() With {.Name = "FunctionName", .Foreground = "#009999"})
        For Each item In GetAutoCompleteData("")
            Dim rule As New HighlightRule
            rule.CaseSensitive = True
            rule.ColorName = "FunctionName"
            rule.Regex = item.FunctionName
            out.Rules.Add(rule)
        Next
        Return out
    End Function

    Public Async Function Save(provider As IIOProvider) As Task Implements ISavable.Save
        Await Save(Filename, provider)
    End Function

    Public Function Save(Filename As String, provider As IIOProvider) As Task Implements ISavableAs.Save
        Dim j As New JsonStructure With {.Database = Me.Database, .AutoCompleteChars = Me.AutoCompleteChars}
        Json.SerializeToFile(Filename, j, provider)
        Return Task.CompletedTask
    End Function

    Public Sub New(Filename As String, provider As IIOProvider)
        Me.New
        OpenFileInternal(Filename, provider)
    End Sub
    Public Sub New()
        MyBase.New()
        Database = New List(Of FunctionDocumentation)
        AutoCompleteChars = New List(Of Char)
    End Sub

    Public Sub CreateFile(Name As String) 'Implements iCreatableFile.CreateFile
        Database = New List(Of FunctionDocumentation)
        AutoCompleteChars = New List(Of Char)
    End Sub

    Public Async Function OpenFile(Filename As String, Provider As IIOProvider) As Task Implements IOpenableFile.OpenFile
        Await OpenFileInternal(Filename, Provider)
    End Function

    Private Function OpenFileInternal(Filename As String, provider As IIOProvider) As Task
        Dim j = Json.DeserializeFromFile(Of JsonStructure)(Filename, provider)
        Me.Database = j.Database
        Me.AutoCompleteChars = j.AutoCompleteChars
        Return Task.CompletedTask
    End Function

    Public Event FileSaved As EventHandler Implements ISavable.FileSaved

    Public Function GetDefaultExtension() As String Implements ISavableAs.GetDefaultExtension
        Return "*.fdd"
    End Function

    Public Function GetSupportedExtensions() As IEnumerable(Of String) Implements ISavableAs.GetSupportedExtensions
        Return {"*.fdd"}
    End Function
End Class

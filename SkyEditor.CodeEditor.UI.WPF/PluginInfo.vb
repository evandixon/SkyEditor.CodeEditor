Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.UI.WPF

Public Class PluginInfo
    Inherits WPFCoreSkyEditorPlugin

    Public Overrides ReadOnly Property Credits As String
        Get
            Return ""
        End Get
    End Property

    Public Overrides ReadOnly Property PluginAuthor As String
        Get
            Return "evandixon"
        End Get
    End Property

    Public Overrides ReadOnly Property PluginName As String
        Get
            Return My.Resources.Language.TextEditor
        End Get
    End Property

    Public Overrides Sub Load(Manager As PluginManager)
        MyBase.Load(Manager)
        Manager.LoadRequiredPlugin(New SkyEditor.CodeEditor.PluginDefinition, Me)
    End Sub

End Class

﻿Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class TestController
        Inherits ApiController

        ' GET: api/Test
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/Test/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/Test
        <HttpPost()>
        Public Function PostValue(<FromBody()> ByVal value As String) As Integer
            Return (5 + Convert.ToInt32(value))
        End Function

        ' PUT: api/Test/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/Test/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace
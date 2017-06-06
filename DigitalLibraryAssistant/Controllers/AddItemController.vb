Imports System.IO
Imports System.Net
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Namespace Controllers
    Public Class AddItemController
        Inherits ApiController

        ' GET: api/AddItem
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/AddItem/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/AddItem
        Public Function PostValue(<FromBody()> ByVal item As Item) As Object
            Dim url As String = "http://localhost:8983/solr/Users/select?q=*:*&stats=true&stats.field=BookId&indent=on&wt=json"

            item.BookId = Convert.ToInt32(WebInvoke(url).Last().Last().Last().Last().Last().Last()("max")) + 1
            Dim js As New JavaScriptSerializer

            url = "http://localhost:8983/solr/Users/update?wt=json&commitWithin=500&boost=1"
            Dim items = New List(Of Item)
            items.Add(item)
            Return WebInvoke(url, js.Serialize(items).Replace("IsNew", "New"))
        End Function

        ' PUT: api/AddItem/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/AddItem/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub

        Public Function WebInvoke(ByVal url As String, Optional data As String = "")
            'Return New String() {"value1", "value2"}
            Dim request As WebRequest = WebRequest.Create(url)
            ' Set the Method property of the request to POST.  
            request.Method = "POST"
            ' Create POST data and convert it to a byte array.  
            'Dim postData As String = "This is a test that posts this string to a Web server."
            'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            ' Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json"
            ' Set the ContentLength property of the WebRequest.  
            'request.ContentLength = byteArray.Length
            Dim dataStream As Stream
            If (Not String.IsNullOrEmpty(data)) Then
                Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
                request.ContentLength = byteArray.Length
                dataStream = request.GetRequestStream()
                ' Write the data to the request stream.  
                dataStream.Write(byteArray, 0, byteArray.Length)
                ' Close the Stream object.  
                dataStream.Close()
            End If

            ' Get the request stream.  

            ' Get the response.  
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.  
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.  
            Dim reader As New StreamReader(dataStream)
            ' Read the content.  
            Dim responseFromServer As String = reader.ReadToEnd()
            ' Display the content.  
            Console.WriteLine(responseFromServer)
            ' Clean up the streams.  
            reader.Close()
            dataStream.Close()
            response.Close()

            Dim json As JObject = JObject.Parse(responseFromServer)

            Return json
        End Function
    End Class
End Namespace

Public Class Item
    Public Property BookId As Integer
    Public Property Title As String
    Public Property Author As String
    Public Property ISBN As String
    Public Property id As String
    Public Property IsNew As String
    Public Property DataTableName As String
    Public Property CreateDate As String
    'Public Property Loaned As String
    Public Property StartDate As String
    Public Property EndDate As String
End Class
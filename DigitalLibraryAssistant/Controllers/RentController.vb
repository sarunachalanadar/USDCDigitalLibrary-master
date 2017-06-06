Imports System.IO
Imports System.Net
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Namespace Controllers
    Public Class RentController
        Inherits ApiController

        ' GET: api/Rent
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/Rent/5
        Public Function PostValue(<FromBody()> ByVal item As Item) As Object
            Dim js As New JavaScriptSerializer
            item.StartDate = Date.Today
            item.EndDate = Date.Today.AddDays(14)
            Dim Url = "http://localhost:8983/solr/Users/update?wt=json&commitWithin=500&boost=1"
            Dim items = New List(Of Item)
            items.Add(item)
            Return WebInvoke(Url, js.Serialize(items))
        End Function

        ' POST: api/Rent
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/Rent/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/Rent/5
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
Imports System.IO
Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json.Linq

Public Class SolrController
    Inherits ApiController

    ' GET api/<controller>
    Public Function GetValues() As String
        Return "Get method called."
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As String) As String
        Return "value"
    End Function

    ' POST api/<controller>
        Public Function PostValue(<FromBody()> ByVal value As String) As JArray
        Dim url As String = "http://localhost:8080/solr/Users/select?wt=json&indent=true"
        If Not String.IsNullOrEmpty(value) OrElse value IsNot Nothing Then
            url = url + "&q=" + value
        Else
            url = url + "&q=*:*"
        End If
        Return GetSolrSearchResults(url)
    End Function

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub

    Public Function GetSolrSearchResults(ByVal url As String)
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
        ' Get the request stream.  
        Dim dataStream As Stream = request.GetRequestStream()
        ' Write the data to the request stream.  
        'dataStream.Write(byteArray, 0, byteArray.Length)
        ' Close the Stream object.  
        dataStream.Close()
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

        Return json.SelectToken("response").SelectToken("docs")
    End Function
End Class

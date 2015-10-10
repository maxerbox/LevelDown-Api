
Namespace LevelDown
    Public Class Client

        Public Async Function LoginAsync(ByVal username As String, ByVal password As String) As Task(Of WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType))
            Return Await Task.Run(Function() As WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)
                                      Dim httpclient As New System.Net.WebClient()
                                      httpclient.BaseAddress = "http://leveldown.fr/"

                                      Dim Data As New Specialized.NameValueCollection
                                      Dim Message As New WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)
                                      Message = GetTokenAsync().Result
                                      InputBox("Token",, Message.Result.Token)
                                      With Data
                                          .Add("tk", Message.Result.Token)
                                          .Add("login_username", username)
                                          .Add("login_password", password)
                                      End With
                                      Try
                                          httpclient.UploadValues("http://leveldown.fr/connexion", Data)
                                          Dim SetCookie = httpclient.ResponseHeaders.GetValues("Set-Cookie").ElementAt(0)
                                          Dim session As String = SetCookie.Substring(SetCookie.IndexOf("sess=") + 5, SetCookie.IndexOf(";", SetCookie.IndexOf("sess=") + 5) - (SetCookie.IndexOf("sess=") + 5))
                                          Message.Result.Session = session
                                          InputBox("Session",, Message.Result.Session)
                                      Catch ex As System.Net.WebException
                                          MsgBox(ex.ToString)
                                          Message = New WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)(True, ex.Message, ex.Status)
                                      End Try

                                      Return Message
                                  End Function)
        End Function
        Private Async Function GetTokenAsync() As Task(Of WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType))

            Return Await Task.Run(Function() As WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)
                                      Dim message As New WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)
                                      Dim httpclient As New Net.WebClient()
                                      Dim source = ""
                                      Try
                                          source = httpclient.DownloadString("http://leveldown.fr/connexion")
                                      Catch ex As System.Net.WebException
                                          message = New WebTypeResult.WebyBaseResult(Of Types.WebTokenSessionType)(True, ex.Message, ex.Status)
                                      End Try
                                      If message.IsErronous = False Then
                                          Dim Mot1 As String = "value="""
                                          Dim indexofmot1 As Integer = source.IndexOf(Mot1, source.IndexOf("name=""tk"""))
                                          Dim indexoflastquote = source.IndexOf("""", indexofmot1 + Mot1.Length)
                                          Dim resultat = source.Substring(indexofmot1 + Mot1.Length, indexoflastquote - (indexofmot1 + Mot1.Length))
                                          message.Result.Token = resultat
                                      End If
                                      Return message
                                  End Function)

        End Function

    End Class
    Namespace WebTypeResult
        Public Class WebyBaseResult(Of TResult As {Class, New})
            Private _TResult As New TResult
            Public Property Result() As TResult
                Get
                    Return _TResult
                End Get
                Set(ByVal value As TResult)
                    _TResult = value
                End Set
            End Property
            Private _IsErronous As Boolean
            Public Property IsErronous() As Boolean
                Get
                    Return _IsErronous
                End Get
                Set(ByVal value As Boolean)
                    _IsErronous = value
                End Set
            End Property
            Private _Message As String
            Public Property Message() As String
                Get
                    Return _Message
                End Get
                Set(ByVal value As String)
                    _Message = value
                End Set
            End Property
            Private _Status As Net.WebExceptionStatus
            Public Property Status() As Net.WebExceptionStatus
                Get
                    Return _Status
                End Get
                Set(ByVal value As Net.WebExceptionStatus)
                    _Status = value
                End Set
            End Property
            Public Sub New(ByVal __message As String)
                _IsErronous = False
                _Message = __message
                _Status = Net.WebExceptionStatus.Success
            End Sub
            Public Sub New(ByVal __IsErronous As Boolean, ByVal __message As String, ByVal __status As Net.WebExceptionStatus)
                _IsErronous = __IsErronous
                _Message = __message
                _Status = __status
            End Sub
            Public Sub New()
                _IsErronous = False
                _Message = "Ok"
                _Status = Net.WebExceptionStatus.Success

            End Sub
        End Class


        Public Class WebyBaseResult
            Private _IsErronous As Boolean
            Public Property IsErronous() As Boolean
                Get
                    Return _IsErronous
                End Get
                Set(ByVal value As Boolean)
                    _IsErronous = value
                End Set
            End Property
            Private _Message As String
            Public Property Message() As String
                Get
                    Return _Message
                End Get
                Set(ByVal value As String)
                    _Message = value
                End Set
            End Property
            Private _Status As Net.WebExceptionStatus
            Public Property Status() As Net.WebExceptionStatus
                Get
                    Return _Status
                End Get
                Set(ByVal value As Net.WebExceptionStatus)
                    _Status = value
                End Set
            End Property
            Public Sub New(ByVal __message As String)
                _IsErronous = False
                _Message = __message
                _Status = Net.WebExceptionStatus.Success
            End Sub
            Public Sub New(ByVal __IsErronous As Boolean, ByVal __message As String, ByVal __status As Net.WebExceptionStatus)
                _IsErronous = __IsErronous
                _Message = __message
                _Status = __status
            End Sub
            Public Sub New()
                _IsErronous = False
                _Message = "Ok"
                _Status = Net.WebExceptionStatus.Success
            End Sub
        End Class

    End Namespace
    Namespace Types
        Public Class WebTokenSessionType
            Private _Token As String = ""
            Public Property Token() As String
                Get
                    Return _Token
                End Get
                Set(ByVal value As String)
                    _Token = value
                End Set
            End Property
            Private _Session As String = ""
            Public Property Session() As String
                Get
                    Return _Session
                End Get
                Set(ByVal value As String)
                    _Session = value
                End Set
            End Property
            Public Sub New(ByVal __token As String)

                _Token = __token
            End Sub
            Public Sub New(ByVal __token As String, ByVal __Session As String)
                _Token = __token
                _Session = __Session
            End Sub
            Public Sub New()
            End Sub
        End Class
    End Namespace
End Namespace

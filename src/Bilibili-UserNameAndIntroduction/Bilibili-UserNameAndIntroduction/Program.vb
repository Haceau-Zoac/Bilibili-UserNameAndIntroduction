Imports System.Net.Http
Imports System.Text.RegularExpressions

Module Program
    ReadOnly client As HttpClient = New HttpClient()
    Sub Main()
        While True
            ' ����uid
            Console.Write("UID:")
            Dim uid As String = Console.ReadLine()
            Dim html
            ' ��ȡhtml
            Try
                html = GetHtml($"https://space.bilibili.com/{uid}")
            Catch
                Console.WriteLine("δ�ҵ����û�")
                Continue While
            End Try
            ' ʹ��������ʽ����html
            Dim user = Regex.Match(html, "<meta name=""description"" content=""[^""]+""/>").Value
            ' ��ȡ�ַ�������ý��
            If user IsNot "" Then
                Dim userName = user.Substring(34, user.IndexOf("��") - 34)
                Console.WriteLine("���û�����Ϊ��" + userName)
                If (user.Length - 68 - user.IndexOf("��") - 2) = 0 Then
                    Console.WriteLine("���û��޼��")
                Else
                    Dim introduction = user.Substring(user.IndexOf("��") + 1, user.Length - 68 - user.IndexOf("��") - 2)
                    Console.WriteLine("���û����Ϊ��" + introduction)
                End If

            Else
                Console.WriteLine("δ�ҵ����û�")
            End If
        End While
    End Sub

    ''' <summary>
    ''' ��ȡhtml
    ''' </summary>
    ''' <param name="url">��ַ</param>
    ''' <returns>html</returns>
    Function GetHtml(ByVal url As String) As String
        Dim response As HttpResponseMessage = client.GetAsync(url).Result
        response.EnsureSuccessStatusCode()
        Return response.Content.ReadAsStringAsync().Result
    End Function
End Module
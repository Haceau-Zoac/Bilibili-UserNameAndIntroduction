Imports System.Net.Http
Imports System.Text.RegularExpressions

Module Program
    ReadOnly client As HttpClient = New HttpClient()
    Sub Main()
        While True
            ' 输入uid
            Console.Write("UID:")
            Dim uid As String = Console.ReadLine()
            Dim html
            ' 获取html
            Try
                html = GetHtml($"https://space.bilibili.com/{uid}")
            Catch
                Console.WriteLine("未找到该用户")
                Continue While
            End Try
            ' 使用正则表达式分析html
            Dim user = Regex.Match(html, "<meta name=""description"" content=""[^""]+""/>").Value
            ' 截取字符串，获得结果
            If user IsNot "" Then
                Dim userName = user.Substring(34, user.IndexOf("，") - 34)
                Console.WriteLine("该用户名称为：" + userName)
                If (user.Length - 68 - user.IndexOf("，") - 2) = 0 Then
                    Console.WriteLine("该用户无简介")
                Else
                    Dim introduction = user.Substring(user.IndexOf("，") + 1, user.Length - 68 - user.IndexOf("，") - 2)
                    Console.WriteLine("该用户简介为：" + introduction)
                End If

            Else
                Console.WriteLine("未找到该用户")
            End If
        End While
    End Sub

    ''' <summary>
    ''' 获取html
    ''' </summary>
    ''' <param name="url">地址</param>
    ''' <returns>html</returns>
    Function GetHtml(ByVal url As String) As String
        Dim response As HttpResponseMessage = client.GetAsync(url).Result
        response.EnsureSuccessStatusCode()
        Return response.Content.ReadAsStringAsync().Result
    End Function
End Module
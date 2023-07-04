Imports System.DirectoryServices
Imports System.Runtime.InteropServices
Imports ProcessBank.Agent.Tools

''' <summary>
''' Контейнер инструментов для работы с LDAP протоколом.
''' </summary>
Public NotInheritable Class Ldap

    Public Shared Function GetEntryNativeName(entry As DirectoryEntry) As String
        Return IIf(Mid(ValToStr(entry.Name), 3, 1) = "=", Mid(entry.Name, 4), entry.Name).ToString()
    End Function

    Public Shared Function GetUserPropertyValue(rootEntry As DirectoryEntry, samAccountName As String, propertyName As String) As Object
        Dim searcher = New DirectorySearcher(rootEntry)
        If String.IsNullOrWhiteSpace(samAccountName) Then Throw New ArgumentException("SamAccountName can't be Null or White Space!")

        ' Искать запись
        Dim queryFormat = "(&(objectClass=user)(objectCategory=person)(|(SamAccountName=*{0}*)))"
        searcher.Filter = String.Format(queryFormat, samAccountName)
        For Each result As SearchResult In searcher.FindAll()
            If result.Properties(propertyName).Count > 0 Then Return result.Properties(propertyName)(0)
        Next

        Return Nothing
    End Function

    Public Shared Function FindUser(rootEntry As DirectoryEntry, searchString As String) As String
        Dim searcher = New DirectorySearcher(rootEntry)
        Dim queryFormat = "(&(objectClass=user)(objectCategory=person)(|(SamAccountName=*{0}*)(cn=*{0}*)(gn=*{0}*)(sn=*{0}*)(email=*{0}*)))"
        searcher.Filter = String.Format(queryFormat, searchString)
        Dim userInfo As String = String.Empty
        For Each result As SearchResult In searcher.FindAll()
            Dim san As String = "NA"
            Dim cn As String = "NA"
            If result.Properties("SamAccountName").Count > 0 Then userInfo = ValToStr(result.Properties("SamAccountName")(0))
            If result.Properties("cn").Count > 0 Then cn = ValToStr(result.Properties("cn")(0))
            userInfo &= $"{san}{vbTab}{cn}{vbCrLf}"
        Next
        Return userInfo
    End Function

    Public Shared Function ConvertToDate(parameterValue As Object) As Date?
        Dim li = TryCast(parameterValue, IAdsLargeInteger)
        If li Is Nothing Then Return Nothing
        If li.HighPart.Equals(2147483647) Then Return Nothing
        If li.HighPart.Equals(0) And li.LowPart.Equals(0) Then Return Nothing
        Return DateTime.FromFileTime((li.HighPart << 32) + CType(li.LowPart, UInteger))
    End Function

    <ComImport(), Guid("9068270b-0939-11d1-8be1-00c04fd8d503"), InterfaceType(ComInterfaceType.InterfaceIsDual)>
    Interface IAdsLargeInteger
        Property HighPart As Long
        Property LowPart As Long
    End Interface
End Class

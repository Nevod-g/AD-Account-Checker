Imports ProcessBank.Agent.Tools

Public Class frmAdObject
    Private AdObject As AdObject

    Public Overloads Sub ShowDialog(adObject As AdObject)
        If adObject Is Nothing Then
            Message.Show("No data.", "Show Directory Entry Information")
            Exit Sub
        End If

        Me.AdObject = adObject
        meInfo.EditValue =
            $"{NameOf(adObject.Name)}: {adObject.Name}{vbCrLf}" &
            $"{NameOf(adObject.ObjectClass)}: {adObject.ObjectClass}{vbCrLf}" &
            $"{NameOf(adObject.ObjectCategory)}: {adObject.ObjectCategory}{vbCrLf}" &
            $"{NameOf(adObject.ObjectGUID)}: {adObject.ObjectGUID}{vbCrLf}" &
            $"{NameOf(adObject.EmployeeId)}: {adObject.EmployeeId}{vbCrLf}" &
            $"{NameOf(adObject.SamAccountName)}: {adObject.SamAccountName}{vbCrLf}" &
            $"{NameOf(adObject.GivenName)}: {adObject.GivenName}{vbCrLf}" &
            $"{NameOf(adObject.Surname)}: {adObject.Surname}{vbCrLf}" &
            $"{NameOf(adObject.Title)}: {adObject.Title}{vbCrLf}" &
            $"{NameOf(adObject.Department)}: {adObject.Department}{vbCrLf}" &
            $"{NameOf(adObject.Company)}: {adObject.Company}{vbCrLf}" &
            $"{NameOf(adObject.Enabled)}: {adObject.Enabled}{vbCrLf}" &
            $"{NameOf(adObject.AccountExpirationDate)}: {adObject.AccountExpirationDate}{vbCrLf}" &
            $"{NameOf(adObject.DistinguishedName)}: {adObject.DistinguishedName}{vbCrLf}" &
            $"{NameOf(adObject.LastLogon)}: {adObject.LastLogon}{vbCrLf}" &
            $"{NameOf(adObject.NativePath)}: {adObject.NativePath}{vbCrLf}"
        meInfo.DeselectAll()
        tePropertyValue.EditValue = Nothing
        tePropertyName.EditValue = Nothing
        MyBase.ShowDialog()
    End Sub

    Private Sub sbGetPropertyValue_Click(sender As Object, e As EventArgs) Handles sbGetPropertyValue.Click
        Dim result = AdObject.GetPropertyValue(tePropertyName.Text)

        'If String.IsNullOrWhiteSpace(tePropertyValue.Text) Then
        '    result = ValToStr(
        '        Ldap.GetUserPropertyValue(AdObject.RootEntry, AdObject.SamAccountName, tePropertyName.Text)
        '    )
        'End If

        tePropertyValue.EditValue = result
    End Sub
End Class
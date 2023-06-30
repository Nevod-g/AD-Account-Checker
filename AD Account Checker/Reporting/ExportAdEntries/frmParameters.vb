
Imports System.ComponentModel

Namespace Reporting
    Namespace ExportAdEntries
        Public Class frmParameters
            Private Sub frmParameters_Load(sender As Object, e As EventArgs) Handles Me.Load
                meExceptCompanies.EditValue = My.Settings.ReportingExceptCompanies
            End Sub

            Private Sub ceExceptCompanies_CheckedChanged(sender As Object, e As EventArgs) Handles ceExceptCompanies.CheckedChanged
                meExceptCompanies.Enabled = ceExceptCompanies.Checked
            End Sub

            Private Sub frmParameters_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
                If My.Settings.ReportingExceptCompanies <> meExceptCompanies.Text Then
                    My.Settings.ReportingExceptCompanies = meExceptCompanies.Text.Trim()
                    My.Settings.Save()
                End If
            End Sub

            Private Sub sbOk_Click(sender As Object, e As EventArgs) Handles sbOk.Click
                ExcelTechnology.IsExceptComparedEntries = ceExceptComparedEntries.Checked
                ExcelTechnology.IsExceptCompanies = ceExceptCompanies.Checked
                ExcelTechnology.ExceptCompanies = meExceptCompanies.Text.Trim()
            End Sub
        End Class
    End Namespace
End Namespace
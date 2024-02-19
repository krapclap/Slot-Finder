Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not ListBox2.Items.Contains(ListBox1.SelectedItem) Then
            Status1.Text = "Color Added To Project"
            Status1.Image = Slot_Finder.My.Resources.add_list
            ListBox2.Items.Add(ListBox1.SelectedItem.ToString)
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ListBox1.Refresh()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CheckBox2.Checked = False
        Panel5.BackColor = Color.White
        TextBox1.Text = ""
        Status1.Text = "Adding New Color"
        Status1.Image = Slot_Finder.My.Resources.add_list
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.BringToFront()
        GroupBox1.Visible = Not GroupBox1.Visible

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not ListBox1.Items.Contains(TextBox1.Text) Then
            Status1.Text = "Color Added To Library"
            Status1.Image = Slot_Finder.My.Resources.add_list
            ListBox1.Items.Add(TextBox1.Text)
            My.Settings.Library.Clear()
            For Each item In ListBox1.Items
                My.Settings.Library.Add(item)
            Next
        Else
            Status1.Text = "Color Already In Library!"
            Status1.Image = Slot_Finder.My.Resources.alert2
        End If
        If Not ListBox7.Items.Contains(TextBox1.Text) Then
            ListBox7.Items.Add(TextBox1.Text)
            My.Settings.History.Clear()
            For Each item In ListBox7.Items
                My.Settings.History.Add(item)
            Next
        End If
        If CheckBox2.Checked = True Then
            'If not in system colors?
            If Not ListBox4.Items.Contains(TextBox1.Text) Then
                If Not ListBox8.Items.Contains(TextBox1.Text) And Not ListBox9.Items.Contains(Panel5.BackColor.ToArgb) Then
                    ListBox8.Items.Add(TextBox1.Text)
                    ListBox9.Items.Add(Panel5.BackColor.ToArgb)
                    Status1.Text = "Custom Color Added To Library"
                    Status1.Image = Slot_Finder.My.Resources.add_list
                Else
                    Status1.Text = "A custom color with this label or shade already exists"
                    Status1.Image = Slot_Finder.My.Resources.alert2
                End If
            End If
            My.Settings.CustomName.Clear()
            For Each item In ListBox8.Items
                My.Settings.CustomName.Add(item)
            Next
            My.Settings.CustomColor.Clear()
            For Each item In ListBox9.Items
                My.Settings.CustomColor.Add(item)
            Next
        End If
        TextBox1.Text = ""
        GroupBox1.Visible = Not GroupBox1.Visible

    End Sub
    Private Sub ListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox1.DragDrop
        Dim index As Integer = ListBox1.IndexFromPoint(ListBox1.PointToClient(New Point(e.X, e.Y)))
        If index > -1 Then
            ListBox1.Items.Insert(index, e.Data.GetData(DataFormats.Text))
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            ListBox1.SelectedIndex = index

        End If
    End Sub

    Private Sub ListBox1_DragOver(sender As Object, e As DragEventArgs) Handles ListBox1.DragOver
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub ListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDown
        Label2.Text = "1"
        Refresh()
        ListBox1.DoDragDrop(ListBox1.Text, DragDropEffects.All)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If SaveFile.FileName = "" And CloseProjectMenuItem.Visible = True Then CloseProjectMenuItem.Visible = False
        If Not SaveFile.FileName = "" And CloseProjectMenuItem.Visible = False Then CloseProjectMenuItem.Visible = True
        Status2.Text = "Project Contains: " + ListBox2.Items.Count.ToString + " Colors"
        Status3.Text = "Project Performs: " + Str(ListBox3.Items.Count - 1) + " Color Changes"
        If ListBox1.SelectedIndex >= 0 Then Button4.Enabled = True Else Button4.Enabled = False
        If ListBox1.SelectedIndex >= 0 Then Button1.Enabled = True Else Button1.Enabled = False
        If ListBox2.SelectedIndex >= 0 Then Button10.Enabled = True Else Button10.Enabled = False
        If ListBox2.SelectedIndex >= 0 Then Button5.Enabled = True Else Button5.Enabled = False
        If ListBox3.SelectedIndex >= 0 Then Button6.Enabled = True Else Button6.Enabled = False
        If ListBox3.Items.Count > 0 Then Button7.Enabled = True Else Button7.Enabled = False
        If ListBox5.Items.Count > 0 Then
            For i = 0 To ListBox3.Items.Count - 1
                If Not ListBox3.Items(i) = ListBox5.Items(i) Then
                    ListView1.Items.Clear()
                    Dim blankitem As New ListViewItem
                    blankitem.SubItems.Add("")
                    blankitem.SubItems.Add("")
                    blankitem.SubItems.Add("")
                    blankitem.SubItems.Add("")
                    ListView1.Items.Add(blankitem)
                    ListBox5.Items.Clear()
                    ListBox6.Items.Clear()
                    Exit For
                End If
            Next
        End If


        If ComboBox2.Text = "" Or ComboBox3.Text = "" Or ComboBox4.Text = "" Or ComboBox5.Text = "" Or ListBox3.Items.Count < 4 Then
            Button11.Enabled = False
            ListView1.Items.Clear()
            Dim blankitem As New ListViewItem
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            ListView1.Items.Add(blankitem)
            ListBox5.Items.Clear()
            ListBox6.Items.Clear()

        ElseIf Not ComboBox2.Text = "" And Not ComboBox3.Text = "" And Not ComboBox4.Text = "" And Not ComboBox5.Text = "" And ListBox3.Items.Count > 0 And ListBox3.Items.Count > 0 Then
            Button11.Enabled = True
        End If

    End Sub


    Private Sub ListBox2_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox2.DragDrop
        Dim index As Integer
        If Label2.Text = "1" Then
            Button1.PerformClick()
        ElseIf Label2.Text = "2" Then
            index = ListBox2.IndexFromPoint(ListBox2.PointToClient(New Point(e.X, e.Y)))
            If index > -1 Then
                ListBox2.Items.Insert(ListBox2.IndexFromPoint(ListBox2.PointToClient(New Point(e.X, e.Y))), e.Data.GetData(DataFormats.Text))
                ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
                ListBox2.SelectedIndex = index

            End If

        End If
        If CheckBox1.Checked = True Then

            Dim I As Integer
            ComboBox2.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox2.Items.Add(ListBox2.Items(I))
            Next
            ComboBox3.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox3.Items.Add(ListBox2.Items(I))
            Next
            ComboBox4.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox4.Items.Add(ListBox2.Items(I))
            Next
            ComboBox5.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox5.Items.Add(ListBox2.Items(I))
            Next
            If ListBox3.Items.Count > 0 Then ComboBox2.Text = ListBox3.Items(0).ToString
            If ListBox3.Items.Count > 1 Then ComboBox3.Text = ListBox3.Items(1).ToString
            If ListBox3.Items.Count > 2 Then ComboBox4.Text = ListBox3.Items(2).ToString
            If ListBox3.Items.Count > 3 Then ComboBox5.Text = ListBox3.Items(3).ToString
        End If
    End Sub

    Private Sub ListBox2_DragOver(sender As Object, e As DragEventArgs) Handles ListBox2.DragOver
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub ListBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDown
        Label2.Text = "2"
        Refresh()
        ListBox2.DoDragDrop(ListBox2.Text, DragDropEffects.All)
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox2.Refresh()

    End Sub

    Private Sub ComboBox2_MouseEnter(sender As Object, e As EventArgs) Handles ComboBox2.MouseEnter
    End Sub

    Private Sub ListBox3_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox3.MouseDown
        Label2.Text = 3
        Refresh()
        ListBox3.DoDragDrop(ListBox3.Text, DragDropEffects.All)
    End Sub

    Private Sub ListBox3_DragOver(sender As Object, e As DragEventArgs) Handles ListBox3.DragOver
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub ListBox3_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox3.DragDrop
        Dim index As Integer

        If Label2.Text = "2" Then
            index = ListBox3.IndexFromPoint(ListBox3.PointToClient(New Point(e.X, e.Y)))
            Label7.Text = index
            If index > -1 Then
                ListBox3.Items.Add(e.Data.GetData(DataFormats.Text))
                ListBox3.SelectedIndex = index
            ElseIf index = -1 Then
                ListBox3.Items.Add(ListBox2.SelectedItem.ToString)
            End If
        ElseIf Label2.Text = "3" Then
            index = ListBox3.IndexFromPoint(ListBox3.PointToClient(New Point(e.X, e.Y)))
            If index > -1 Then
                ListBox3.Items.Insert(index, e.Data.GetData(DataFormats.Text))
                ListBox3.Items.RemoveAt(ListBox3.SelectedIndex)
                ListBox3.SelectedIndex = index
            End If
        End If
        If CheckBox1.Checked = True Then

            Dim I As Integer
            ComboBox2.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox2.Items.Add(ListBox2.Items(I))
            Next
            ComboBox3.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox3.Items.Add(ListBox2.Items(I))
            Next
            ComboBox4.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox4.Items.Add(ListBox2.Items(I))
            Next
            ComboBox5.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox5.Items.Add(ListBox2.Items(I))
            Next
            If ListBox3.Items.Count > 0 Then ComboBox2.Text = ListBox3.Items(0).ToString
            If ListBox3.Items.Count > 1 Then ComboBox3.Text = ListBox3.Items(1).ToString
            If ListBox3.Items.Count > 2 Then ComboBox4.Text = ListBox3.Items(2).ToString
            If ListBox3.Items.Count > 3 Then ComboBox5.Text = ListBox3.Items(3).ToString
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ListBox2.SelectedIndex >= 0 Then ListBox3.Items.Add(ListBox2.Text)
        Status1.Text = "New Color Added to Cycle List"
        Status1.Image = Slot_Finder.My.Resources.add_list
        If CheckBox1.Checked = True Then

            Dim I As Integer
            ComboBox2.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox2.Items.Add(ListBox2.Items(I))
            Next
            ComboBox3.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox3.Items.Add(ListBox2.Items(I))
            Next
            ComboBox4.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox4.Items.Add(ListBox2.Items(I))
            Next
            ComboBox5.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox5.Items.Add(ListBox2.Items(I))
            Next
            If ListBox3.Items.Count > 0 Then ComboBox2.Text = ListBox3.Items(0).ToString
            If ListBox3.Items.Count > 1 Then ComboBox3.Text = ListBox3.Items(1).ToString
            If ListBox3.Items.Count > 2 Then ComboBox4.Text = ListBox3.Items(2).ToString
            If ListBox3.Items.Count > 3 Then ComboBox5.Text = ListBox3.Items(3).ToString
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim result As DialogResult = MessageBox.Show("Clear Purge Block Items?", "caption", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
        ElseIf result = DialogResult.Yes Then
            Status1.Image = Slot_Finder.My.Resources.paper
            Status1.Text = "Color Cycle List Caleared"
            ListBox3.Items.Clear()
            ComboBox2.Items.Clear()
            ComboBox3.Items.Clear()
            ComboBox4.Items.Clear()
            ComboBox5.Items.Clear()
            Dim blankitem As New ListViewItem
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            ListView1.Items.Add(blankitem)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        ListBox3.Items.RemoveAt(ListBox3.SelectedIndex)
        Status1.Text = "Color Removed From Color Cycle"
        Status1.Image = Slot_Finder.My.Resources.delete_List
        If CheckBox1.Checked = True Then

            Dim I As Integer
            ComboBox2.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox2.Items.Add(ListBox2.Items(I))
            Next
            ComboBox3.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox3.Items.Add(ListBox2.Items(I))
            Next
            ComboBox4.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox4.Items.Add(ListBox2.Items(I))
            Next
            ComboBox5.Items.Clear()
            For I = 0 To ListBox2.Items.Count - 1
                ComboBox5.Items.Add(ListBox2.Items(I))
            Next
            If ListBox3.Items.Count > 0 Then ComboBox2.Text = ListBox3.Items(0).ToString
            If ListBox3.Items.Count > 1 Then ComboBox3.Text = ListBox3.Items(1).ToString
            If ListBox3.Items.Count > 2 Then ComboBox4.Text = ListBox3.Items(2).ToString
            If ListBox3.Items.Count > 3 Then ComboBox5.Text = ListBox3.Items(3).ToString
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Status1.Image = Slot_Finder.My.Resources.alert2
        Status1.Text = "Calculating"
        Refresh()
        Dim i, o, p As Integer
        Dim T As String
        Dim data(5) As String
        Dim z As Int32
        ListView1.Items.Clear()
        If CheckBox1.Checked Then
            ComboBox2.Items.Clear()
            For i = 0 To ListBox2.Items.Count - 1
                ComboBox2.Items.Add(ListBox2.Items(i))
            Next
            ComboBox3.Items.Clear()
            For i = 0 To ListBox2.Items.Count - 1
                ComboBox3.Items.Add(ListBox2.Items(i))
            Next
            ComboBox4.Items.Clear()
            For i = 0 To ListBox2.Items.Count - 1
                ComboBox4.Items.Add(ListBox2.Items(i))
            Next
            ComboBox5.Items.Clear()
            For i = 0 To ListBox2.Items.Count - 1
                ComboBox5.Items.Add(ListBox2.Items(i))
            Next
            ComboBox4.Text = ListBox3.Items(0).ToString
            ComboBox2.Text = ListBox3.Items(1).ToString
            ComboBox3.Text = ListBox3.Items(2).ToString
            ComboBox5.Text = ListBox3.Items(3).ToString
            i = 0
        End If

        ListView1.Items.Clear()
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        For i = 0 To ListBox2.Items.Count - 1
            For o = 0 To ListBox3.Items.Count - 1

                If ListBox3.Items(o) = ListBox2.Items(i) Then
                    If o > 0 Then
                        ListBox5.Items.Add(ListBox3.Items(o - 1))
                        T = ListBox3.Items(o - 1)
                    End If
                    If o < ListBox3.Items.Count - 1 Then
                        ListBox5.Items.Add(ListBox3.Items(o + 1))
                        T = T + " " + ListBox3.Items(o + 1)
                    End If
                    ListBox6.Items.Add(CStr(o) + " " + ListBox3.Items(o) + ": " + T)
                End If
            Next

            For p = 0 To ListBox2.Items.Count - 1
                If Not ListBox2.Items(i) = ListBox2.Items(p) Then
                    If Not ListBox5.Items.Contains(ListBox2.Items(p)) Then
                        If ComboBox2.Text = ListBox2.Items(p) Then data(2) = ListBox2.Items(p)
                        If ComboBox3.Text = ListBox2.Items(p) Then data(3) = ListBox2.Items(p)
                        If ComboBox4.Text = ListBox2.Items(p) Then data(1) = ListBox2.Items(p)
                        If ComboBox5.Text = ListBox2.Items(p) Then data(4) = ListBox2.Items(p)
                    End If
                End If

            Next
            If ComboBox2.Text = ListBox2.Items(i) Or ComboBox3.Text = ListBox2.Items(i) Or ComboBox4.Text = ListBox2.Items(i) Or ComboBox5.Text = ListBox2.Items(i) Then

                ListBox5.Items.Clear()
            Else

                Dim item As New ListViewItem(ListBox2.Items(i).ToString)
                item.Font = New Font(ListView1.Font, FontStyle.Bold)
                item.UseItemStyleForSubItems = False
                Dim subitem = item.SubItems.Add(data(1))
                subitem.Font = New Font(ListView1.Font, FontStyle.Regular)
                item.SubItems.Add(data(2))
                item.SubItems.Add(data(3))
                item.SubItems.Add(data(4))
                ListView1.Items.Add(item)
                For z = 1 To 4
                    If ListView1.Items(ListView1.Items.Count - 1).SubItems(z).Text = "" Then
                    Else
                        ListView1.Items(ListView1.Items.Count - 1).SubItems(z).BackColor = Color.Lime
                    End If
                Next
                ListBox5.Items.Clear()
                T = ""
            End If
            data(0) = ""
            data(1) = ""
            data(2) = ""
            data(3) = ""
            data(4) = ""
        Next
        ListBox5.Items.Clear()
        For i = 0 To ListBox3.Items.Count - 1
            ListBox5.Items.Add(ListBox3.Items(i))
        Next
        Status1.Image = Slot_Finder.My.Resources.Finished
        Status1.Text = "Color Chart Completed:  " + System.DateTime.Now
    End Sub

    Private Sub ComboBox3_DropDown(sender As Object, e As EventArgs) Handles ComboBox3.DropDown
        Dim I As Integer
        ComboBox3.Items.Clear()
        For I = 0 To ListBox2.Items.Count - 1
            ComboBox3.Items.Add(ListBox2.Items(I))
        Next
    End Sub

    Private Sub ComboBox2_DropDown(sender As Object, e As EventArgs) Handles ComboBox2.DropDown
        Dim I As Integer
        ComboBox2.Items.Clear()
        For I = 0 To ListBox2.Items.Count - 1
            ComboBox2.Items.Add(ListBox2.Items(I))
        Next
    End Sub

    Private Sub ComboBox4_DropDown(sender As Object, e As EventArgs) Handles ComboBox4.DropDown
        Dim I As Integer
        ComboBox4.Items.Clear()
        For I = 0 To ListBox2.Items.Count - 1
            ComboBox4.Items.Add(ListBox2.Items(I))
        Next
    End Sub

    Private Sub ComboBox5_DropDown(sender As Object, e As EventArgs) Handles ComboBox5.DropDown
        Dim I As Integer
        ComboBox5.Items.Clear()
        For I = 0 To ListBox2.Items.Count - 1
            ComboBox5.Items.Add(ListBox2.Items(I))
        Next
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Status1.Image = Slot_Finder.My.Resources.alert2
        Status1.Text = "Adding New Color Canceled"
        GroupBox1.Visible = False
        TextBox1.Text = ""
        Panel5.BackColor = SystemColors.Control
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Panel3.BackColor = Color.FromName(ComboBox2.Text)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            If ListBox3.Items.Count > 3 Then


                Dim I As Integer
                ComboBox4.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox4.Items.Add(ListBox2.Items(I))
                Next
                ComboBox2.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox2.Items.Add(ListBox2.Items(I))
                Next
                ComboBox3.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox3.Items.Add(ListBox2.Items(I))
                Next
                ComboBox5.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox5.Items.Add(ListBox2.Items(I))
                Next

                ComboBox4.Text = ListBox3.Items(0).ToString
                ComboBox2.Text = ListBox3.Items(1).ToString
                ComboBox3.Text = ListBox3.Items(2).ToString
                ComboBox5.Text = ListBox3.Items(3).ToString
            End If
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
        Else
            ComboBox2.Enabled = True
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
        End If


    End Sub

    Private Sub CheckBox1_ChangeUICues(sender As Object, e As UICuesEventArgs) Handles CheckBox1.ChangeUICues

    End Sub

    Private Sub ComboBox2_ValueMemberChanged(sender As Object, e As EventArgs) Handles ComboBox2.ValueMemberChanged

    End Sub

    Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
    End Sub

    Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
    End Sub

    Private Sub ComboBox4_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedValueChanged
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
    End Sub

    Private Sub ComboBox5_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedValueChanged
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Panel2.BackColor = Color.FromName(ComboBox5.Text)
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
    End Sub

    Private Sub ComboBox5_Leave(sender As Object, e As EventArgs) Handles ComboBox5.Leave
        'If ComboBox5.Text = "" Then
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
        'End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If Not ListBox2.Items.Contains(ListBox1.Items(ListBox1.SelectedIndex)) Then
            Dim result As DialogResult = MessageBox.Show("Delete color from your library?", "caption", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
            ElseIf result = DialogResult.Yes Then
                Status1.Text = "Color Deleted From Library"
                Status1.Image = Slot_Finder.My.Resources.add_list
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                My.Settings.Library.Clear()
                For Each item In ListBox1.Items
                    My.Settings.Library.Add(item)
                Next
            End If
        Else
            MessageBox.Show("This color must be removed from Colors Being Used first!", "caption", MessageBoxButtons.OK)
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 482
        For Each item In My.Settings.CustomColor
            ListBox9.Items.Add(item)
        Next
        For Each item In My.Settings.CustomName
            ListBox8.Items.Add(item)
        Next
        For Each item In My.Settings.Library
            ListBox1.Items.Add(item)
        Next
        For Each item In My.Settings.History
            ListBox7.Items.Add(item)
        Next
        ListView1.SmallImageList = ImageList1
        ListView1.Columns(1).ImageIndex = 0
        ListView1.Columns(2).ImageIndex = 1
        ListView1.Columns(3).ImageIndex = 2
        ListView1.Columns(4).ImageIndex = 3

        For Each i In GetType(System.Drawing.Color).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
            ListBox4.Items.Add(i.Name)
        Next
        If CheckBox1.Checked Then
            If ListBox3.Items.Count > 3 Then


                Dim I As Integer
                ComboBox2.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox2.Items.Add(ListBox2.Items(I))
                Next
                ComboBox3.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox3.Items.Add(ListBox2.Items(I))
                Next
                ComboBox4.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox4.Items.Add(ListBox2.Items(I))
                Next
                ComboBox5.Items.Clear()
                For I = 0 To ListBox2.Items.Count - 1
                    ComboBox5.Items.Add(ListBox2.Items(I))
                Next

                ComboBox4.Text = ListBox3.Items(0).ToString
                ComboBox2.Text = ListBox3.Items(1).ToString
                ComboBox3.Text = ListBox3.Items(2).ToString
                ComboBox5.Text = ListBox3.Items(3).ToString
            End If
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
        Else
            ComboBox2.Enabled = True
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
        End If
        ListView1.Items.Clear()
        Dim blankitem As New ListViewItem
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        blankitem.SubItems.Add("")
        ListView1.Items.Add(blankitem)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim i As Integer
        Dim result As DialogResult = MessageBox.Show("Removing this color will also remove all entries from your color swap list. Continue?", "Warning", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
        ElseIf result = DialogResult.Yes Then
            Status1.Text = "Color Removed From Project"
            Status1.Image = Slot_Finder.My.Resources.delete_List
            ListView1.Items.Clear()
            Dim blankitem As New ListViewItem
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            blankitem.SubItems.Add("")
            ListView1.Items.Add(blankitem)
            ListBox5.Items.Clear()
            ListBox6.Items.Clear()
            For i = 0 To ListBox3.Items.Count - 1
                ListBox5.Items.Add(ListBox3.Items(i))
            Next
            ListBox3.Items.Clear()
            For i = 0 To ListBox5.Items.Count - 1
                If Not ListBox5.Items(i) = ListBox2.SelectedItem Then ListBox3.Items.Add(ListBox5.Items(i))
            Next
            ListBox5.Items.Clear()
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        My.Computer.Audio.Play(My.Resources.bgm, AudioPlayMode.BackgroundLoop)
        Form2.Location = Location
        Form2.Focus()
        Form2.Show()
    End Sub

    Private Sub ListBox4_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox4.DrawItem
        If ListBox4.Items.Count > 0 Then
            Using customBrush As New SolidBrush(Color.FromName(ListBox4.Items(e.Index)))
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
            End Using
            Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
            e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        If CheckBox2.Checked = False Then
            TextBox1.Text = ListBox4.Items(ListBox4.SelectedIndex)
            Panel5.BackColor = Color.FromName(ListBox4.Items(ListBox4.SelectedIndex).ToString)
        End If
        Refresh()
    End Sub

    Private Sub ListBox3_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox3.DrawItem
        If ListBox3.Items.Count > 0 Then


            'If e.Index Mod 2 = 0 Then
            Using customBrush As New SolidBrush(Color.FromName(ListBox3.Items(e.Index)))
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                If ListBox8.Items.Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                    For i = 0 To ListBox1.Items.Count - 1
                        If ListBox8.GetItemText(ListBox8.Items(i)).Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                            Using CustomColorBrush As New SolidBrush(Color.FromArgb(ListBox9.Items(i)))
                                e.Graphics.FillRectangle(Brushes.DeepSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                                e.Graphics.FillRectangle(CustomColorBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                            End Using
                            Exit For
                        End If
                    Next
                Else
                    e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                End If
            End Using
            Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
            e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox1.DrawItem
        If ListBox1.Items.Count > 0 Then
            Using customBrush As New SolidBrush(Color.FromName(ListBox1.Items(e.Index)))
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                If ListBox8.Items.Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                    For i = 0 To ListBox1.Items.Count - 1
                        If ListBox8.GetItemText(ListBox8.Items(i)).Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                            Using CustomColorBrush As New SolidBrush(Color.FromArgb(ListBox9.Items(i)))
                                e.Graphics.FillRectangle(Brushes.DeepSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                                e.Graphics.FillRectangle(CustomColorBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                            End Using
                            Exit For
                        End If
                    Next
                Else
                    e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                End If
            End Using
            Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
            e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListBox2_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox2.DrawItem
        If ListBox2.Items.Count > 0 Then
            Using customBrush As New SolidBrush(Color.FromName(ListBox2.Items(e.Index)))
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                If ListBox8.Items.Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                    For i = 0 To ListBox1.Items.Count - 1
                        If ListBox8.GetItemText(ListBox8.Items(i)).Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                            Using CustomColorBrush As New SolidBrush(Color.FromArgb(ListBox9.Items(i)))
                                e.Graphics.FillRectangle(Brushes.DeepSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                                e.Graphics.FillRectangle(CustomColorBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                            End Using
                            Exit For
                        End If
                    Next
                Else
                    e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                End If
            End Using
            Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
            e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        ListBox3.Refresh()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Panel4.BackColor = Color.FromName(ComboBox3.Text)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Panel1.BackColor = Color.FromName(ComboBox4.Text)
    End Sub

    Private Sub ListBox7_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox7.DrawItem
        If ListBox7.Items.Count > 0 Then
            Using customBrush As New SolidBrush(Color.FromName(ListBox7.Items(e.Index)))

                If ListBox8.Items.Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                    For i = 0 To ListBox1.Items.Count - 1
                        If ListBox8.GetItemText(ListBox8.Items(i)).Contains(CType(sender, ListBox).Items(e.Index).ToString()) Then
                            Using CustomColorBrush As New SolidBrush(Color.FromArgb(ListBox9.Items(i)))
                                e.Graphics.FillRectangle(Brushes.DeepSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                                e.Graphics.FillRectangle(CustomColorBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                            End Using
                            Exit For
                        End If
                    Next
                Else
                    e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20))
                    e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16))
                End If
            End Using
            Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
            e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox7.SelectedIndexChanged
        If CheckBox2.Checked = False Then
            TextBox1.Text = ListBox7.Items(ListBox7.SelectedIndex)
            Panel5.BackColor = Color.FromName(ListBox7.Items(ListBox7.SelectedIndex).ToString)
        End If
        Refresh()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        TextBox1.ReadOnly = Not CheckBox2.Checked
        If TextBox1.ReadOnly = True Then TextBox1.Text = ""
        If CheckBox2.Checked = True Then Else Panel5.Enabled = False
        ListBox4.Enabled = Not CheckBox2.Checked
        ListBox7.Enabled = Not CheckBox2.Checked
        If CheckBox2.Checked Then
            Panel5.BackgroundImage = Slot_Finder.My.Resources.icons8_color_palette_48
            Panel5.Enabled = True
        Else
            Panel5.Enabled = False
            Panel5.BackgroundImage = Slot_Finder.My.Resources.bwColors
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            Button3.Enabled = False
        Else
            Button3.Enabled = True
        End If
    End Sub

    Private Sub ListBox7_DoubleClick(sender As Object, e As EventArgs) Handles ListBox7.DoubleClick
        ListBox7.Items.RemoveAt(ListBox7.SelectedIndex)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        My.Settings.History.Clear()
        ListBox7.Items.Clear()
        Status1.Text = "Color Library History Cleared"
        Status1.Image = Slot_Finder.My.Resources.paper

    End Sub

    Private Sub ListView1_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles ListView1.DrawSubItem
        If e.ItemIndex = 0 Then

            e.Graphics.FillRectangle(Brushes.DeepSkyBlue, e.Bounds.X, e.Bounds.Y + 20, e.Bounds.Width, e.Bounds.Height - 20)
        Else
            If e.SubItem.Text = "" Then
                e.Graphics.DrawImage(ImageList1.Images.Item(5), CInt(e.Bounds.X + e.Bounds.Width / 2 - 10), e.Bounds.Y + 1, 20, 20)
            Else

                If e.ColumnIndex > 0 Then


                    If ListView1.Items.Count > 0 Then

                        Using customBrush As New SolidBrush(Color.FromName(e.SubItem.Text))
                            e.Graphics.FillRectangle(Brushes.Lime, e.Bounds)
                            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds.X + 2, e.Bounds.Y + 2, ListView1.Columns(e.ColumnIndex).Width - 4, 18)
                            e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 20, e.Bounds.Y + 1, 20, 20))
                            e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 22, e.Bounds.Y + 3, 16, 16))
                            e.Graphics.DrawImage(ImageList1.Images.Item(e.ColumnIndex), e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16)
                            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 42, e.Bounds.Y + 3)
                            e.Graphics.DrawString(e.SubItem.Text, ListView1.Font, Brushes.Black, textLocation)
                        End Using
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub ListView1_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles ListView1.DrawItem
        If ListView1.Items.Count > 0 And e.ItemIndex > 0 Then


            Using customBrush As New SolidBrush(Color.FromName(e.Item.Text))
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 1, e.Bounds.Y, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 3, e.Bounds.Y + 2, 16, 16))
            End Using
            Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y + 3)
            e.Graphics.DrawString(e.Item.Text, ListView1.Font, Brushes.Black, textLocation)
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Sub ListView1_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles ListView1.DrawColumnHeader
        Dim sf As New StringFormat()
        Dim customBrush As New SolidBrush(SystemColors.Window)
        Try
            Select Case e.Header.TextAlign
                Case HorizontalAlignment.Center
                    sf.Alignment = StringAlignment.Center
                Case HorizontalAlignment.Right
                    sf.Alignment = StringAlignment.Far
            End Select
            e.DrawBackground()
            If e.ColumnIndex = 1 Then
                customBrush.Color = Panel1.BackColor
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 70, e.Bounds.Y + 3, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 72, e.Bounds.Y + 5, 16, 16))
            ElseIf e.ColumnIndex = 2 Then
                customBrush.Color = Panel3.BackColor
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 70, e.Bounds.Y + 3, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 72, e.Bounds.Y + 5, 16, 16))
            ElseIf e.ColumnIndex = 3 Then
                customBrush.Color = Panel4.BackColor
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 70, e.Bounds.Y + 3, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 72, e.Bounds.Y + 5, 16, 16))
            ElseIf e.ColumnIndex = 4 Then
                customBrush.Color = Panel2.BackColor
                e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(e.Bounds.X + 70, e.Bounds.Y + 3, 20, 20))
                e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 72, e.Bounds.Y + 5, 16, 16))
            End If


            Dim headerFont As New Font("Helvetica", 10, FontStyle.Bold)
            Try
                Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 27.5, e.Bounds.Y + 5)
                e.Graphics.DrawString(e.Header.Text, headerFont,
                Brushes.Black, textLocation, sf)
            Finally
                e.Graphics.DrawImage(ImageList1.Images.Item(e.ColumnIndex), e.Bounds.X + 5, e.Bounds.Y + 2, 22, 22)
                headerFont.Dispose()
            End Try

        Finally
            sf.Dispose()
        End Try

    End Sub

    Private Sub ListView1_ColumnWidthChanging(sender As Object, e As ColumnWidthChangingEventArgs) Handles ListView1.ColumnWidthChanging
        If e.NewWidth < 100 Then
            e.NewWidth = 100
            e.Cancel = True
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Status1.Text = ""
        Timer2.Enabled = False
    End Sub

    Private Sub Status1_TextChanged(sender As Object, e As EventArgs) Handles Status1.TextChanged
        Timer2.Enabled = False
        Timer2.Enabled = True
        If Status1.Text = "" Then Status1.DisplayStyle = ToolStripItemDisplayStyle.Text Else Status1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

    End Sub

    Private Sub SaveSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMenuItem.Click
        If SaveFile.ShowDialog() = DialogResult.OK Then
            Using sWriter As New StreamWriter(SaveFile.FileName)
                For Each item In ListBox3.Items
                    sWriter.WriteLine(item)
                Next
            End Using
            Status1.Text = "Project Saved " + SaveFile.FileName
            GroupBox3.Text = "Results    " + SaveFile.FileName
        End If
    End Sub

    Private Sub ListView1_Resize(sender As Object, e As EventArgs) Handles ListView1.Resize
        If Not ListView1.Columns(4).Width <= 99 - 5 Then ListView1.Columns(4).Width = ListView1.Width - ListView1.Columns(0).Width - ListView1.Columns(1).Width - ListView1.Columns(2).Width - ListView1.Columns(3).Width - 5
    End Sub

    Private Sub StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles StatusStrip1.ItemClicked

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Button8.Left = GroupBox3.Width - Button8.Width - 10

    End Sub

    Private Sub Panel5_Click(sender As Object, e As EventArgs) Handles Panel5.Click
        CD.Color = Panel5.BackColor
        If CheckBox2.Checked = True And CD.ShowDialog <> Windows.Forms.DialogResult.Cancel Then Panel5.BackColor = CD.Color
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListBox8_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox8.DrawItem
        If ListBox8.Items.Count > 0 Then
            Try
                Using customBrush As New SolidBrush(Color.FromArgb(ListBox9.Items(e.Index)))
                    e.Graphics.FillRectangle(Brushes.DeepSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, 20, 20)) 'draw custom background
                    e.Graphics.FillRectangle(customBrush, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16)) 'draw custom background
                End Using
                Dim text As String = CType(sender, ListBox).Items(e.Index).ToString()
                Dim textLocation As New System.Drawing.PointF(e.Bounds.X + 20, e.Bounds.Y)
                e.Graphics.DrawString(text, e.Font, Brushes.Black, textLocation)
                e.DrawFocusRectangle()
            Finally

            End Try

        End If
    End Sub

    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles GroupBox1.VisibleChanged
        Panel5.Enabled = CheckBox2.Checked
    End Sub

    Private Sub ListBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox8.SelectedIndexChanged
        TextBox1.Text = ListBox8.Items(ListBox8.SelectedIndex)
        Panel5.BackColor = Color.FromArgb(ListBox9.Items(ListBox8.SelectedIndex).ToString)
    End Sub

    Private Sub Form1_ResizeBegin(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin

    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton1.ButtonClick
        If SaveFile.FileName = "" Then
            SaveMenuItem.PerformClick()
        Else
            Using sWriter As New StreamWriter(SaveFile.FileName)
                For Each item In ListBox3.Items
                    sWriter.WriteLine(item)
                Next
            End Using
            My.Settings.ColorList.Clear()
            Status1.Text = "Project Saved " + SaveFile.FileName
        End If

    End Sub

    Private Sub LoadSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadMenuItem.Click
        Dim oDialog As New OpenFileDialog
        oDialog.Filter = "AMS Swapper Project|*.swap"
        If oDialog.ShowDialog() = DialogResult.OK Then
            ListBox3.Items.Clear()
            ListBox2.Items.Clear()
            SaveFile.FileName = oDialog.FileName
            Using sReader As New StreamReader(oDialog.FileName)
                While sReader.Peek() > 0
                    Dim input = sReader.ReadLine()
                    ListBox3.Items.Add(input)
                    If Not ListBox2.Items.Contains(input) Then ListBox2.Items.Add(input)
                End While
            End Using
            Status3.Text = "Project loaded"
            GroupBox3.Text = "Results  " + SaveFile.FileName
            CheckBox1.Checked = True
            Button11.PerformClick()
            Refresh()
        End If

    End Sub

    Private Sub CloseProjectMenuItem_Click(sender As Object, e As EventArgs) Handles CloseProjectMenuItem.Click
        ListBox3.Items.Clear()
        ListBox2.Items.Clear()
        GroupBox3.Text = "Results"
        SaveFile.FileName = ""
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()
        ComboBox5.Items.Clear()
        Panel1.BackColor = Color.White
        Panel2.BackColor = Color.White
        Panel3.BackColor = Color.White
        Panel4.BackColor = Color.White

    End Sub

    Private Sub ComboBox5_TextChanged(sender As Object, e As EventArgs) Handles ComboBox5.TextChanged
    End Sub
End Class

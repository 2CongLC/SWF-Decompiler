Imports System
Imports System.IO
Imports System.Text


'https://github.com/SFTtech/openage/blob/master/doc/media/drs-files.md
Public Class DRSFile


Private source as ByteArray
Private info as string '40 bytes
Private version as string '4 bytes
Private ftype as string '12 bytes
Private tablecount as integer '4 bytes
Private foffset as integer '4 bytes


Public Sub New(Byval buffer as Byte())

source = New ByteArray(buffer)
source.Position = 0
info = Encosing.ASCII.Getstring(source.ReadBytesEndian(40))
source.Position = 40
version = Encoding.ASCII.Getstring(source.ReadBytesEndian(4))
source.Position = 44
ftype =  Encoding.ASCII.Getstring(source.ReadBytesEndian(12))
source.Position = 56
tablecount = source.ReadInt()
source.Position = 60
foffset = source.ReadInt()

Dim subfolders As New List(Of Subfolder)()
For i As Integer = 0 To tablecount - 1
 subfolders.Add(New Subfile() With {
   .Extensions = source.Readchars(4),
   .Offset = source.ReadInt(),
   .NumFiles = source.ReadInt() })                
Next


End Sub

Public Structure SUBFOLDER
    Public Extension as string
    Public Offset as integer
    Public NumFiles as integer
End Structure
  
Public Structure SUBFILE
    Public ID as integer
    Public Offset As Integer
    Public size as integer
End Structure








End Class 

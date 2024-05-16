Imports System.IO
Imports System.Buffers

Public Class ChromeUnPack

  Private data as ByteArray
  Private _version as UInteger
  Private _enc as Byte
  Private resourceCount as UShort
  Private aliasCount as UShort
  
  Public Sub New(Byval buffer as Byte())
   Dim source as ByteArray = New ByteArray(buffer)
   _version = source.ReadUnsignedInt() 
   _enc = source.ReadByte()
   data = New ByteArray()
   data.WriteBytes(source,3)
  End Sub

  Public sub UnPack(Byval outPut as String)
    data.Position = 0
    resourceCount = data.ReadUnsignedShort()
    aliasCount = data.ReadUnsignedShort()
    
    Dim entries As EntryData() = New EntryData(resourceCount + 1 - 1) {}
    Dim aliases As AliasData() = New AliasData(aliasCount - 1) {}
    For i As Integer = 0 To resourceCount
            Dim resourceId As UShort = data.ReadUnsignedShort()
            Dim fileOffset As UInteger = data.ReadUnsignedInt()
            entries(i) = New EntryData(resourceId, fileOffset)         
        Next
  For i As Integer = 0 To aliasCount - 1
            Dim resourceId As UShort = data.ReadUnsignedShort()
            Dim entryIndex As UShort = data.ReadUnsignedShort()
            aliases(i) = New AliasData(resourceId, entryIndex)       
        Next
    
Const outputDirectory As String = "resources"
        Directory.CreateDirectory(outputDirectory)
For i As Integer = 0 To resourceCount - 1
            data.WriteUnsignedInt(entries(i).FileOffset)
            
            Dim length As Integer = CInt(entries(i + 1).FileOffset - entries(i).FileOffset)
            Dim buff As Byte() = ArrayPool(Of Byte).Shared.Rent(length)
            Dim bytesRead As Integer = stream.Read(buff, 0, length)
            Using file As FileStream = File.Create(Path.Combine(outputDirectory, $"f_{entries(i).ResourceId:X4}"))
                file.Write(buff, 0, length)
            End Using
            ArrayPool(Of Byte).Shared.Return(buff)
        Next


    End Sub
  
 Public Structure EntryData
    Public ReadOnly ResourceId As UShort
    Public ReadOnly FileOffset As UInteger

    Public Sub New(resourceId As UShort, fileOffset As UInteger)
        Me.ResourceId = resourceId
        Me.FileOffset = fileOffset
    End Sub
End Structure
  
  Public Structure AliasData
    Public ReadOnly ResourceId As UShort
    Public ReadOnly EntryIndex As UShort

    Public Sub New(resourceId As UShort, entryIndex As UShort)
        Me.ResourceId = resourceId
        Me.EntryIndex = entryIndex
    End Sub
End Structure

End Class
  







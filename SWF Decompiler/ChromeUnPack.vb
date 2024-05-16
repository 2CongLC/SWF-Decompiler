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
    
Const outputDirectory As String = outPut
Directory.CreateDirectory(outputDirectory)
For i As Integer = 0 To resourceCount - 1
    Dim outData as ByteArray = New ByteArray()
    outData.WrireBytes(data,entries(i).FileOffset)       
    IO.File.WriteAllBytes(outputDirectory,outData.ToArray())
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
  







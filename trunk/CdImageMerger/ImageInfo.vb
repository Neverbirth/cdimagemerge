Imports System.IO

Public Class ImageInfo

    Private _fileName As String
    Private _sectorSize As Integer
    Private _length As Long
    Private _mode As ImageModes
    Private _totalSectors As Integer
    Private _totalTime As String

    Private _primaryVolumeDescriptor As PrimaryVolumeDescriptor
    Private _pathTableRecord As PathTableRecord
    Private _directories As Dictionary(Of Integer, Directory)

    Private Const HSVOLSTART As Integer = 16    ' where we expect a Primary Volume Descriptor */
    Private Const HSTERMSTART As Integer = 17    ' where we expect the Volume Descriptor Terminator */

    Private Const StdVolType As Byte = 1    ' Primary Volume Descriptor type
    Private Const VolEndType As Byte = 255    ' Volume Descriptor Set Terminator type 

    '
    ' File Flags for Directory Records
    '
    Private Const existenceBit As Integer = &H1
    Private Const directoryBit As Integer = &H2
    Private Const associatedBit As Integer = &H4
    Private Const recordBit As Integer = &H8
    Private Const protectionBit As Integer = &H10
    Private Const multiextentBit As Integer = &H80

    '****************************************************************************/
    '*  Exported Types                                                          */
    '****************************************************************************/

    'typedef unsigned char byte;



    '****************************************************************************/
    '*  ISO 9660 (pANS Z39.86 198x) standard format                             */
    '****************************************************************************/

    ' 
    ' Primary Volume Descriptor (PVD)
    '
    Private Structure PrimaryVolumeDescriptor
        Public VDType As Byte           ' Must be 1 for PVD
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> _
        Public VSStdId As String        ' Must be "CD001"
        Public VSStdVersion As Byte     ' Must be 1
        Public Reserved1 As Byte        ' Must be 0's
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public systemIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, sizeconst:=32)> _
        Public volumeIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, sizeconst:=8)> _
        Public Reserved2 As String      ' Must be 0's
        Public lsbVolumeSpaceSize As Integer
        Public msbVolumeSpaceSize As Integer
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public Reserved3 As String      ' Must be 0's
        Public lsbVolumeSetSize As Short
        Public msbVolumeSetSize As Short
        Public lsbVolumeSetSequenceNumber As Short
        Public msbVolumeSetSequenceNumber As Short
        Public lsbLogicalBlockSize As Short
        Public msbLogicalBlockSize As Short
        Public lsbPathTableSize As Integer
        Public msbPathTableSize As Integer
        Public lsbPathTable1 As Integer ' mandatory occurrence
        Public lsbPathTable2 As Integer ' optional occurrence
        Public msbPathTable1 As Integer ' mandatory occurrence
        Public msbPathTable2 As Integer ' optional occurrence
        Public rootDirectoryRecord As DirectoryRecord
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public volumeSetIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public publisherIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public dataPreparerIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public applicationIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public copyrightFileIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public abstractFileIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public bibliographicFileIdentifier As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeCreation As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeModification As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeExpiration As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeEffective As String
        '  char  FileStructureStandardVersion;
        '  char  Reserved4;        ' Must be 0's
        '  char  ApplicationUse[512];
        '  char  FutureStandardization[653];
    End Structure

    '
    ' Path Table
    '

    'typedef char  dirIDArray[8];

    Private Structure PathTableRecord
        Public len_di As Byte   ' length of directory identifier
        Public XARlength As Byte    ' Extended Attribute Record Length
        Public dirLocation As Long  ' 1st logical block where directory is stored
        Public parentDN As Short    ' parent directory number
        '  dirIDArray  dirID;    ' directory identifier: actual length is
        '  9 - [8+Len_di]; there is an extra blank
        '  byte if Len_di is odd.
    End Structure

    '
    ' Directory Record
    '  There exists one of these for each file in the directory.
    '  
    Private Structure OldAppleExtension
        '        Public macFlag(2) As Char    ' $42 $41 - 'BA' famous value
        Public systemUseID As Byte  ' 06 = HFS
        '  Public  fileType[4] as byte  ' such as 'TEXT' or 'STAK'
        '  Public  fileCreator[4] as byte  ' such as 'hscd' or 'WILD'
        '  Public  finderFlags[2] as byte
    End Structure


    Public Structure AppleExtension
        '  char  signature[2];    ' $41 $41 - 'AA' famous value
        Public extensionLength As Byte  ' $0E for this ID
        Public systemUseID As Byte  ' 02 = HFS
        '        Public fileType() As Byte = New Byte(4) {}  ' such as 'TEXT' or 'STAK'
        '        Public fileCreator(4) As Byte  ' such as 'hscd' or 'WILD'
        '        Public finderFlags(2) As Byte
    End Structure

    Public Structure DirectoryRecord
        Public len_dr As Byte       ' directory record length
        Public XARlength As Byte    ' Extended Attribute Record Length
        Public lsbStart As Integer
        Public msbStart As Integer  ' 1st logical block where file starts
        Public lsbDataLength As Integer
        Public msbDataLength As Integer
        Public year As Byte         ' since 1900
        Public month As Byte
        Public day As Byte
        Public hour As Byte
        Public minute As Byte
        Public second As Byte
        Public gmtOffset As Byte
        Public fileFlags As Byte
        Public interleaveSize As Byte
        Public interleaveSkip As Byte
        Public lsbVolSetSeqNum As Short
        Public msbVolSetSeqNum As Short  ' which volume in volume set contains this file.
        Public len_fi As Byte       ' length of file identifier which follows
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public fi As String         ' file identifier: actual is 37-[36+Len_fi], contains extra blank byte if Len_fi odd
        Public apple As AppleExtension ' this actually fits immediately after the fi[]
        ' field, or after its padding byte. */
    End Structure

    Public Structure Directory
        Private parentDirectoryLba As Integer
        Public records() As DirectoryRecord

        Public ReadOnly Property Parent() As Directory
            Get
                '                Return GetDirectoryRecords(parentDirectoryLba)
            End Get
        End Property
    End Structure

    Public Structure ImageDirectory
        Private a As String
        Private Sub New(ByVal a As String)

        End Sub
    End Structure

    Public Class ImageDirectory2
        Private a As String
        Private Sub New(ByVal a As String)

        End Sub
    End Class

    Public Enum ImageModes As Integer
        ModeOne2048 = 0
        ModeOne2448 = 1
        ModeOne2368 = 2
        ModeOne2352 = 3
        ModeTwo2336 = 4
        ModeTwo2352 = 5
        ModeTwo2368 = 6
        ModeTwo2448 = 7
    End Enum
    '    Public prueba As New ImageDirectory2("ASD")
    Public Sub New(ByVal fileName As String, ByVal mode As ImageModes)
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        _fileName = fileName
        _length = fileInfo.Length
        _mode = mode
        _sectorSize = 2352
        _directories = New Dictionary(Of Integer, Directory)

        'GetPrimaryVolumeDescriptor()
    End Sub

    Private Function GetPrimaryVolumeDescriptor() As Boolean
        Using imageStream As FileStream = New FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read, _sectorSize)
            Using binReader As BinaryReader = New BinaryReader(imageStream, System.Text.Encoding.Default)
                imageStream.Seek(HSVOLSTART * _sectorSize + 24, SeekOrigin.Begin)
                With _primaryVolumeDescriptor
                    .VDType = binReader.ReadByte()

                    If .VDType <> StdVolType Then
                        Return False
                    End If

                    .VSStdId = System.Text.Encoding.Default.GetString(binReader.ReadBytes(5))
                    .VSStdVersion = binReader.ReadByte()
                    .Reserved1 = binReader.ReadByte()
                    .systemIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .volumeIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .Reserved2 = System.Text.Encoding.Default.GetString(binReader.ReadBytes(8))
                    .lsbVolumeSpaceSize = binReader.ReadInt32()
                    .msbVolumeSpaceSize = binReader.ReadInt32()
                    .Reserved3 = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .lsbVolumeSetSize = binReader.ReadInt16()
                    .msbVolumeSetSize = binReader.ReadInt16()
                    .lsbVolumeSetSequenceNumber = binReader.ReadInt16()
                    .msbVolumeSetSequenceNumber = binReader.ReadInt16()
                    .lsbLogicalBlockSize = binReader.ReadInt16()
                    .msbLogicalBlockSize = binReader.ReadInt16()
                    .lsbPathTableSize = binReader.ReadInt32()
                    .msbPathTableSize = binReader.ReadInt32()
                    .lsbPathTable1 = binReader.ReadInt32()
                    .lsbPathTable2 = binReader.ReadInt32()
                    .msbPathTable1 = binReader.ReadInt32()
                    .msbPathTable2 = binReader.ReadInt32()
                    .rootDirectoryRecord = GetDirectoryRecord(binReader)
                    .volumeSetIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .publisherIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .dataPreparerIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .applicationIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .copyrightFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .abstractFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .bibliographicFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .volumeCreation = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeModification = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeExpiration = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeEffective = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))

                    ParseDirectoryRecord(.rootDirectoryRecord.lsbStart)
                End With
            End Using
        End Using

        Return True
    End Function

    'TODO: Add to collection
    'TODO: Add IsDirectory check?
    'TODO: Add Parent property?
    'TODO: Check .
    Private Sub ParseDirectoryRecord(ByVal lba As Integer)
        Using imageStream As FileStream = New FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read, _sectorSize)
            Using binReader As BinaryReader = New BinaryReader(imageStream)
                Dim lDirectoryRecord As DirectoryRecord

                imageStream.Seek(lba * _sectorSize + 24, SeekOrigin.Begin)

                Do
                    lDirectoryRecord = GetDirectoryRecord(binReader)
                Loop While lDirectoryRecord.len_dr <> 0
            End Using
        End Using
    End Sub

    Private Function GetDirectoryRecord(ByRef binReader As BinaryReader) As DirectoryRecord
        Dim returnDirectory As DirectoryRecord
        Dim restBytes As Short

        With returnDirectory
            .len_dr = binReader.ReadByte()
            If .len_dr <> 0 Then
                .XARlength = binReader.ReadByte()
                .lsbStart = binReader.ReadInt32()
                .msbStart = binReader.ReadInt32()
                .lsbDataLength = binReader.ReadInt32()
                .msbDataLength = binReader.ReadInt32()
                .year = binReader.ReadByte()
                .month = binReader.ReadByte()
                .day = binReader.ReadByte()
                .hour = binReader.ReadByte()
                .minute = binReader.ReadByte()
                .second = binReader.ReadByte()
                .gmtOffset = binReader.ReadByte()
                .fileFlags = binReader.ReadByte()
                .interleaveSize = binReader.ReadByte()
                .interleaveSkip = binReader.ReadByte()
                .lsbVolSetSeqNum = binReader.ReadInt16()
                .msbVolSetSeqNum = binReader.ReadInt16()
                .len_fi = binReader.ReadByte()
                .fi = System.Text.Encoding.Default.GetString(binReader.ReadBytes(.len_fi))
                restBytes = .len_dr - 33 - .len_fi
                If .len_fi Mod 2 = 0 Then
                    binReader.ReadByte()
                    restBytes -= 1
                End If
                If restBytes > 0 Then
                    binReader.ReadBytes(restBytes)
                End If
            End If
        End With

        Return returnDirectory

    End Function

    Public Function GetDirectoryRecords(ByVal lba As Integer) As Directory

    End Function

End Class

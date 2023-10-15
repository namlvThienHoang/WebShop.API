using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EPS.Utils.Common
{
    public class objData
    {
    }
    public class ClsExcel
    {
        public bool isHeader { get; set; }
        public bool isBold { get; set; }
        public int row { get; set; }
        public int cell { get; set; }
        public object values { get; set; }
        public bool isBorder { get; set; }
        public bool isMersg { get; set; }
        public int firstRow { get; set; }
        public int lastRow { get; set; }
        public int firstCol { get; set; }
        public int lastCol { get; set; }
        public int fontSize { get; set; }
        public string fontName { get; set; }
        public bool isWrapText { get; set; }
        public ExcelHorizontalAlignment horAlign { get; set; }
        public ExcelVerticalAlignment verAlign { get; set; }
        public double width { get; set; }
        public object color { get; set; }
        public ClsExcel(int row, int cell, object values, ExcelHorizontalAlignment horAlign = ExcelHorizontalAlignment.Left, ExcelVerticalAlignment verAlign = ExcelVerticalAlignment.Center, bool isHeader = false, bool isBold = false, bool isBorder = true, bool isWrapText = false, int fontSize = 12, string fontName = "Times New Roman", bool isMersg = false, int firstRow = 0, int lastRow = 0, int firstCol = 0, int lastCol = 0, double width = 0, object color = null)
        {
            this.row = row;
            this.cell = cell;
            this.values = values;
            this.isMersg = isMersg;
            this.firstRow = firstRow;
            this.lastRow = lastRow;
            this.firstCol = firstCol;
            this.lastCol = lastCol;
            this.fontSize = fontSize;
            this.isBorder = isBorder;
            this.isBold = isBold;
            this.isHeader = isHeader;
            this.isWrapText = isWrapText;
            this.verAlign = verAlign;
            this.horAlign = horAlign;
            this.fontName = fontName;
            this.width = width;
            this.color = color;

        }
    }
    public class objMessage
    {
        public string message { get; set; }
        public bool error { get; set; }
        public objMessage() { }
        public objMessage(bool stt=false, string mes= "") {
            message = mes;
            error = stt;
        }
    }
    public class objExportResult
    {
        public string message { get; set; }
        public bool error { get; set; }
        public MemoryStream data { get; set; }

        public objExportResult( MemoryStream data, bool error=false, string message="")
        {
            this.message = message;
            this.error = error;
            this.data = data;
        }
        public objExportResult( string message )
        {
            this.message = message;
            this.error = true;
            
        }
    }
    public class CauHinhExport
    {
        public int index { get; set; }
        public string value { get; set; }
        public string align { get; set; }
        public double width { get; set; }

    }
    public class CellValue
    {

        public object value { get; set; }
        public ExcelHorizontalAlignment alignH { get; set; }


    }
}

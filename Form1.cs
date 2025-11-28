using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
namespace math_gen
{
    // 运算符枚举：表示可用的四则运算符
    public enum OperatorSymbol
    {
        Add,        // 加
        Subtract,   // 减
        Multiply,   // 乘
        Divide      // 除
    }
    public partial class Form1 : Form
    {

       
        public Form1()
        {
            InitializeComponent();

            // 在构造函数中运行时绑定 Leave 事件以触发 "光标移开" 校验
            // 整数输入框使用 ValidateIntTextBoxOnLeave
            this.txt_max.Leave += ValidateIntTextBoxOnLeave;
            this.txt_opter.Leave += ValidateIntOperTextBoxOnLeave;
            this.txt_total.Leave += ValidateIntTextBoxOnLeave;
           // this.txt_rowclom.Leave += ValidateIntTextBoxOnLeave;
           // this.txt_split.Leave += ValidateIntTextBoxOnLeave;
        }

        private OperatorSymbol PickRandomOperator(List<OperatorSymbol> ops)
        {
            if (ops == null || ops.Count == 0)
                throw new ArgumentException("ops 不能为空且至少包含一个运算符", nameof(ops));
            return ops[_rand.Next(ops.Count)];
        }
        // 判断字符串是否为数值（支持整数与小数，使用当前区域设置）
        private bool IsNumeric(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            double _;
            return double.TryParse(s.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out _);
        }

        // 从 TextBox 尝试解析为 double
        private bool TryParseDoubleFromTextBox(TextBox tb, out double value)
        {
            value = 0;
            if (tb == null)
                return false;
            var s = tb.Text?.Trim();
            if (string.IsNullOrEmpty(s))
                return false;
            return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value);
        }

        // 从 TextBox 尝试解析为 int
        private bool TryParseIntFromTextBox(TextBox tb, out int value)
        {
            value = 0;
            if (tb == null)
                return false;
            var s = tb.Text?.Trim();
            if (string.IsNullOrEmpty(s))
                return false;
            return int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out value);
        }
        // 在 Form1 类内添加：根据操作数列表与已选运算符构建表达式（两个重载，整型与小数）
        private string BuildExpression(List<int> operands, List<OperatorSymbol> ops)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < operands.Count; j++)
            {
                sb.Append(operands[j].ToString(CultureInfo.CurrentCulture));
                if (j < operands.Count - 1)
                {
                    var op = PickRandomOperator(ops);
                    sb.Append(' ').Append(GetOperatorSymbol(op)).Append(' ');
                }
            }
            return sb.ToString();
        }

        private string BuildExpression(List<double> operands, List<OperatorSymbol> ops)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < operands.Count; j++)
            {
                sb.Append(operands[j].ToString("0.##", CultureInfo.CurrentCulture));
                if (j < operands.Count - 1)
                {
                    var op = PickRandomOperator(ops);
                    sb.Append(' ').Append(GetOperatorSymbol(op)).Append(' ');
                }
            }
            return sb.ToString();
        }
        private void AppendLog(string text)
        {
            if (this.txt_log == null)
                return;
            
            // 线程安全地更新 txt_log
            if (this.txt_log.InvokeRequired)
            {
                this.txt_log.Invoke((Action)(() => AppendLog(text)));
                return;
            }

            // 确保为多行并只读（可选）
            this.txt_log.Multiline = true;
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = ScrollBars.Vertical;
            this.txt_log.BackColor = this.txt_log.BackColor; // 保持原有背景色
            this.txt_log.ForeColor = Color.Red;

            // 使用 AppendText 保留现有内容并移动光标
            if (string.IsNullOrEmpty(this.txt_log.Text))
                this.txt_log.Text = text;
            else
                this.txt_log.AppendText(Environment.NewLine + text);
        }

        private void ClearLog()
        {
            if (this.txt_log == null)
                return;

            if (this.txt_log.InvokeRequired)
            {
                this.txt_log.Invoke((Action)(ClearLog));
                return;
            }

            this.txt_log.Clear();
        }

        public static void CreateSinglePdf()
        {
           // // 创建一个PDF文档对象
           // PdfDocument document = new PdfDocument();
           // document.Info.Title = "Created with PDFsharp";
           // // 创建一个空的Page
           // PdfPage page = document.AddPage();

           // // 创建一个画布对象，用于绘制
           // XGraphics gfx = XGraphics.FromPdfPage(page);

           // // 创建字体
           //// XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
           // // 绘制文本
           // gfx.DrawString("Hello, World!", font, XBrushes.Black,
           // new XRect(0, 0, page.Width, page.Height),
           // XStringFormats.Center);

           // // 保存文档
           // const string filename = "HelloWorld.pdf";
           // document.Save(filename);
        }

        // 统计传入复选框中被选中的数量
        private int CountChecked(params CheckBox[] boxes)
        {
            if (boxes == null || boxes.Length == 0)
                return 0;
            return boxes.Count(b => b != null && b.Checked);
        }
        // 使用枚举返回当前被选中的运算符列表
        private List<OperatorSymbol> GetSelectedOperators()
        {
            var list = new List<OperatorSymbol>();
            if (this.chk_jia != null && this.chk_jia.Checked) list.Add(OperatorSymbol.Add);
            if (this.chk_jian != null && this.chk_jian.Checked) list.Add(OperatorSymbol.Subtract);
            if (this.chk_cheng != null && this.chk_cheng.Checked) list.Add(OperatorSymbol.Multiply);
            if (this.chk_chu != null && this.chk_chu.Checked) list.Add(OperatorSymbol.Divide);
            return list;
        }

        private int CheckOperatorCount()
        {
            // 改为使用枚举结果计数
            return GetSelectedOperators().Count;
        }

        // 将枚举转换为显示符号（用于显示或生成题目）
        private static string GetOperatorSymbol(OperatorSymbol op)
        {
            switch (op)
            {
                case OperatorSymbol.Add: return "+";
                case OperatorSymbol.Subtract: return "-";
                case OperatorSymbol.Multiply: return "×";
                case OperatorSymbol.Divide: return "÷";
                default: return "?";
            }
        }


        // 通用：当光标从整数类型的 TextBox 移开时校验（Leave 事件）
        private void ValidateIntOperTextBoxOnLeave(object sender, EventArgs e)
        {
            ValidateIntTextBoxOnLeave(sender, e);

            if (!TryParseIntFromTextBox(this.txt_opter, out int numoperator))
            {
                MessageBox.Show("请输入合法的运算符数量（数字）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txt_opter.Focus();
                return;
            }

            int checkedOps = CountChecked(this.chk_jia, this.chk_jian, this.chk_cheng, this.chk_chu);
            if (checkedOps == 0)
            {
                MessageBox.Show("请至少选择一个运算符（加、减、乘、除）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



        }
        // 通用：当光标从整数类型的 TextBox 移开时校验（Leave 事件）
        private void ValidateIntTextBoxOnLeave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null)
                return;

            var text = tb.Text?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                tb.BackColor = Color.MistyRose; // 可视提醒：空内容视为不合法
                // 非强制弹窗，仅显示提示
                var ttEmpty = new ToolTip();
                ttEmpty.Show("不能为空，请输入整数", tb, 0, -20, 1500);
                tb.Focus();
                return;
            }

            if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out _))
            {
                tb.BackColor = Color.MistyRose;
                var tt = new ToolTip();
                tt.Show("请输入合法整数", tb, 0, -20, 2000);
                // 根据需求可以选择自动聚焦回该控件
                tb.Focus();
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }

        // 通用：当光标从小数/数值类型的 TextBox 移开时校验（Leave 事件）
        private void ValidateDoubleTextBoxOnLeave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null)
                return;

            var text = tb.Text?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                tb.BackColor = Color.MistyRose;
                var ttEmpty = new ToolTip();
                ttEmpty.Show("不能为空，请输入数字", tb, 0, -20, 1500);
                tb.Focus();
                return;
            }

            if (!double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out _))
            {
                tb.BackColor = Color.MistyRose;
                var tt = new ToolTip();
                tt.Show("请输入合法数字（可带小数）", tb, 0, -20, 2000);
                tb.Focus();
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }
        private static readonly Random _rand = new Random();

        // 生成两个非 0、位于 [1, max] 范围内的随机整数（若希望包含负数或 0，请调整逻辑）
        private List<int> GenerateNonZeroIntegers(int max,int oper)
        {


            var list = new List<int>();
            if (max < 1)
                throw new ArgumentException("max 必须大于等于 1", nameof(max));

            for (int i = 0; i < oper + 1; i++)
            {
                list.Add(_rand.Next(1, max + 1));
            }

            // 示例：显示生成的两个数
           // MessageBox.Show($"生成的两个非 0 随机整数：{list}", "随机结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 后续：可以把值赋到界面控件上，例如 textBox 用于显示（如有对应控件）
            // 如果希望两个数不相等，可以解开下面注释
            // while (b == a && max > 1) b = _rand.Next(1, max + 1);

            return list;
        }
        // 生成两个非 0、位于 [1, max] 范围内的随机整数（若希望包含负数或 0，请调整逻辑）
        private List<double> GenerateNonZeroDoubles(int max, int oper)
        {
            if (max < 1)
                throw new ArgumentException("max 必须大于等于 1", nameof(max));

            var list = new List<double>();
            for (int i = 0; i < oper + 1; i++)
            {
                double val;
                if (max == 1)
                {
                    val = 1.0;
                }
                else
                {
                    // 生成 [1.00, max] 的随机小数，保留两位
                    val = 1.0 + _rand.NextDouble() * (max - 1.0);
                }
                list.Add(Math.Round(val, 2));
            }
            return list;
        }
        //// 生成：根据运算符序列构造满足整除要求的整数操作数列表
        //private List<int> GenerateIntegersForOperators(int max, int operandCount, List<OperatorSymbol> opsSequence)
        //{
        //    if (max < 1) throw new ArgumentException("max 必须大于等于 1", nameof(max));
        //    if (opsSequence == null) throw new ArgumentNullException(nameof(opsSequence));
        //    if (operandCount != opsSequence.Count + 1) throw new ArgumentException("operandCount 应等于 opsSequence.Count + 1");

        //    var operands = new List<int>(new int[operandCount]);

        //    // 从右向左生成：先随机最后一个操作数（1..max）
        //    operands[operandCount - 1] = _rand.Next(1, max + 1);

        //    for (int i = operandCount - 2; i >= 0; i--)
        //    {
        //        var op = opsSequence[i];
        //        if (op == OperatorSymbol.Divide)
        //        {
        //            // 要求 operands[i] 能被 operands[i+1] 整除，并且 operands[i] <= max
        //            int divisor = operands[i + 1];
        //            if (divisor <= 0) divisor = 1;
        //            int maxQuotient = Math.Max(1, max / divisor); // 至少为1
        //            int quotient = _rand.Next(1, maxQuotient + 1);
        //            operands[i] = divisor * quotient;
        //        }
        //        else
        //        {
        //            operands[i] = _rand.Next(1, max + 1);
        //        }
        //    }

        //    return operands;
        //}
        private List<int> GenerateIntegersForOperators(int max, int operandCount, List<OperatorSymbol> opsSequence)
        {
            if (max < 1) throw new ArgumentException("max 必须大于等于 1", nameof(max));
            if (opsSequence == null) throw new ArgumentNullException(nameof(opsSequence));
            if (operandCount != opsSequence.Count + 1) throw new ArgumentException("operandCount 应等于 opsSequence.Count + 1");

            const int maxAttempts = 200;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                var operands = new List<int>(new int[operandCount]);
                bool failed = false;

                // 从右向左生成
                for (int i = operandCount - 1; i >= 0; i--)
                {
                    // 最右端元素：若左侧是除法则缩小范围，否则全范围
                    if (i == operandCount - 1)
                    {
                        if (i - 1 >= 0 && opsSequence[i - 1] == OperatorSymbol.Divide)
                        {
                            int upper = Math.Max(1, max / 2);
                            operands[i] = _rand.Next(1, upper + 1);
                        }
                        else
                        {
                            operands[i] = _rand.Next(1, max + 1);
                        }
                    }
                    else
                    {
                        var op = opsSequence[i];

                        if (op == OperatorSymbol.Divide)
                        {
                            int divisor = operands[i + 1];
                            if (divisor <= 0) { failed = true; break; }

                            int maxQuotient = max / divisor;
                            // 要保证整除且（可选）被除数 != 除数：这里确保商至少为 2
                            if (maxQuotient < 2) { failed = true; break; }

                            int quotient = _rand.Next(2, maxQuotient + 1);
                            operands[i] = divisor * quotient;
                        }
                        else if (op == OperatorSymbol.Subtract)
                        {
                            // 要求被减数 > 减数
                            int minVal = operands[i + 1] + 1;
                            if (minVal > max) { failed = true; break; }
                            operands[i] = _rand.Next(minVal, max + 1);
                        }
                        else
                        {
                            // 加、乘：普通随机，但需考虑下一位是否为除法，防止后续无解
                            if (i - 1 >= 0 && opsSequence[i - 1] == OperatorSymbol.Divide)
                            {
                                int upper = Math.Max(1, max / 2);
                                operands[i] = _rand.Next(1, upper + 1);
                            }
                            else
                            {
                                operands[i] = _rand.Next(1, max + 1);
                            }
                        }
                    }
                }

                if (failed) continue;

                // 校验所有约束：除法整除且不等于，减法被减数大于减数，值在范围内
                bool ok = true;
                for (int k = 0; k < opsSequence.Count; k++)
                {
                    int a = operands[k];
                    int b = operands[k + 1];

                    if (opsSequence[k] == OperatorSymbol.Divide)
                    {
                        if (b == 0 || a % b != 0 || a == b || a > max || b > max)
                        {
                            ok = false; break;
                        }
                    }
                    else if (opsSequence[k] == OperatorSymbol.Subtract)
                    {
                        if (!(a > b) || a > max || b > max)
                        {
                            ok = false; break;
                        }
                    }
                    else
                    {
                        if (a < 1 || a > max || b < 1 || b > max)
                        {
                            ok = false; break;
                        }
                    }
                }

                // 检查最后一个操作数范围
                if (operands[operandCount - 1] < 1 || operands[operandCount - 1] > max) ok = false;

                if (ok) return operands;
            }

            throw new InvalidOperationException("无法生成满足约束的操作数序列：请增大 max 或调整运算符设置。");
        }
        // 使用给定运算符序列构建整数表达式（不再随机选运算符）
        private string BuildExpressionWithOps(List<int> operands, List<OperatorSymbol> opsSequence)
        {
            if (operands == null) throw new ArgumentNullException(nameof(operands));
            if (opsSequence == null) throw new ArgumentNullException(nameof(opsSequence));
            if (operands.Count != opsSequence.Count + 1) throw new ArgumentException("operands.Count 必须等于 opsSequence.Count + 1");

            var sb = new StringBuilder();
            for (int j = 0; j < operands.Count; j++)
            {
                sb.Append(operands[j].ToString(CultureInfo.CurrentCulture));
                if (j < operands.Count - 1)
                {
                    var op = opsSequence[j];
                    sb.Append(' ').Append(GetOperatorSymbol(op)).Append(' ');
                }
            }
            return sb.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            bool jia, jian, cheng, chu, is_xiaoshu;

            jia = this.chk_jia.Checked;
            jian = this.chk_jian.Checked;
            cheng = this.chk_cheng.Checked;
            chu = this.chk_chu.Checked;
            is_xiaoshu = this.checkBox1.Checked;

            // int maxnum, numoperator, total, row_clomn, split;
            // 验证并解析文本框为数字
            ClearLog();
            if (!TryParseIntFromTextBox(this.txt_max, out int maxnum))
            {
                MessageBox.Show("请输入合法的最大值（数字）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txt_max.Focus();
                return;
            }


            if (!TryParseIntFromTextBox(this.txt_total, out int total))
            {
                MessageBox.Show("请输入合法的题目总数（数字）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txt_total.Focus();
                return;
            }


            // 遍历已选运算符（使用枚举）
            var ops = GetSelectedOperators();
            if (ops == null || ops.Count == 0)
            {
                MessageBox.Show("未选择任何运算符。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool is_exdivision  = is_xiaoshu? true : false;
            // 弹出保存对话框
            if (is_exdivision)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF 文件 (*.pdf)|*.pdf";
                    sfd.FileName = "练习题.pdf";
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;

                    var savePath = sfd.FileName;

                    try
                    {
                        // 创建并写入 PDF（横向 A4，四周留白 36）
                        using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                        {
                            var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 36, 36, 36, 36);
                            var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                            doc.Open();
                            if (!TryParseIntFromTextBox(this.txt_row, out int rows))
                            {
                                AppendLog("行 输入错误，使用默认行");
                            }
                            if (!TryParseIntFromTextBox(this.txt_clome, out int cols))
                            {
                                AppendLog("列 输入错误，使用默认列");
                            }
                            if (!TryParseIntFromTextBox(this.txt_hanjianju, out int hanju))
                            {
                                AppendLog("行间距 输入错误，使用默认行");
                            }
                            rows = rows <= 0 ? 10 : rows;
                            cols = cols <= 0 ? 4 : cols;
                            int perPage = cols * rows; // 50
                            hanju = hanju <= 0 ? 48 : hanju;
                            int current = 0; // 已生成题目计数


                            BaseFont bfHei = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font contentFont = new iTextSharp.text.Font(bfHei, 14);
                            float lineLeading = contentFont.Size + hanju;
                            float cellPadding = 6f;
                            float minCellHeight = lineLeading + cellPadding * 2f;

                            // 分页生成：每页建立一个 PdfPTable 并在页头写入表头
                            while (current < total)
                            {
                                // 页头
                                var header = new iTextSharp.text.Paragraph("班级  __________    姓名  __________    时间  __________    成绩  __________", contentFont);
                                header.SpacingAfter = 8f;
                                doc.Add(header);

                                var table = new PdfPTable(cols) { WidthPercentage = 100 };
                                // 每列均分宽度
                                float[] widths = new float[cols];
                                for (int i = 0; i < cols; i++) widths[i] = 1f;
                                table.SetWidths(widths);
                                table.DefaultCell.Padding = 6f;
                                table.DefaultCell.Border = PdfPCell.NO_BORDER;
                                table.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                                table.HorizontalAlignment = Element.ALIGN_CENTER;

                                //int itemsThisPage = Math.Min(perPage, total - current);

                                for (int i = 0; i < total; i++)
                                {
                                    int questionIndex = current + 1;
                                    string line;
                                    if (is_xiaoshu)
                                    {
                                        var operands = GenerateNonZeroDoubles(maxnum, CheckOperatorCount());
                                        if (operands == null || operands.Count == 0)
                                        {
                                            MessageBox.Show("生成随机小数失败。", "生成错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            doc.Close();
                                            return;
                                        }
                                        string sbExpr = BuildExpression(operands, ops);
                                        line = $"{questionIndex}. {sbExpr} =";
                                    }
                                    else
                                    {
                                        var operands = GenerateNonZeroIntegers(maxnum, CheckOperatorCount());
                                        if (operands == null || operands.Count == 0)
                                        {
                                            MessageBox.Show("生成随机整数失败。", "生成错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            doc.Close();
                                            return;
                                        }
                                        string sbExpr = BuildExpression(operands, ops);
                                        line = $"{questionIndex}. {sbExpr} =";
                                    }
                                    // 使用 Paragraph + SetLeading 实现分散（两端）对齐，并保证行间距
                                    var para = new Paragraph(line, contentFont);
                                    para.SetLeading(lineLeading, 0f);
                                    para.Alignment = Element.ALIGN_JUSTIFIED;

                                    var cell = new PdfPCell(para)
                                    {
                                        Border = PdfPCell.NO_BORDER,
                                        PaddingTop = cellPadding,
                                        PaddingBottom = cellPadding,
                                        PaddingLeft = 6f,
                                        PaddingRight = 6f,
                                        VerticalAlignment = Element.ALIGN_MIDDLE,
                                        HorizontalAlignment = Element.ALIGN_LEFT,
                                        MinimumHeight = minCellHeight
                                    };
                                    table.AddCell(cell);
                                    current++;
                                }

                                // 如果本页单元格不到 cols*rows，最后一行需要补空单元格以保证布局整齐
                                int addedCells = total % cols;
                                if (addedCells != 0)
                                {
                                    int toAdd = cols - addedCells;
                                    for (int k = 0; k < toAdd; k++)
                                    {
                                        var emptyCell = new PdfPCell(new Phrase("", contentFont))
                                        {
                                            Border = PdfPCell.NO_BORDER,
                                            MinimumHeight = 30f
                                        };
                                        table.AddCell(emptyCell);
                                    }
                                }

                                doc.Add(table);

                                if (current < total)
                                {
                                    doc.NewPage();
                                }
                            }

                            doc.Close();
                            writer.Close();

                        }

                        MessageBox.Show("PDF 已保存到：" + savePath, "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AppendLog("已生成文件：" + savePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("生成或保存 PDF 时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else {

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF 文件 (*.pdf)|*.pdf";
                    sfd.FileName = "练习题.pdf";
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;

                    var savePath = sfd.FileName;

                    try
                    {
                        // 创建并写入 PDF（横向 A4，四周留白 36）
                        using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                        {
                            var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 36, 36, 36, 36);
                            var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                            doc.Open();
                            if (!TryParseIntFromTextBox(this.txt_row, out int rows))
                            {
                                AppendLog("行 输入错误，使用默认行");
                            }
                            if (!TryParseIntFromTextBox(this.txt_clome, out int cols))
                            {
                                AppendLog("列 输入错误，使用默认列");
                            }
                            if (!TryParseIntFromTextBox(this.txt_hanjianju, out int hanju))
                            {
                                AppendLog("行间距 输入错误，使用默认行");
                            }
                            rows = rows <= 0 ? 10 : rows;
                            cols = cols <= 0 ? 4 : cols;
                            hanju = hanju <= 0 ? 48 : hanju;
                            int perPage = cols * rows; // 50
                            int current = 0; // 已生成题目计数


                            BaseFont bfHei = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font contentFont = new iTextSharp.text.Font(bfHei, 14);
                            float lineLeading = contentFont.Size + hanju;
                            float cellPadding = 6f;
                            float minCellHeight = lineLeading + cellPadding * 2f;

                            // 分页生成：每页建立一个 PdfPTable 并在页头写入表头
                            while (current < total)
                            {
                                // 页头
                                var header = new iTextSharp.text.Paragraph("班级  __________    姓名  __________    时间  __________    成绩  __________", contentFont);
                                header.SpacingAfter = 8f;
                                doc.Add(header);

                                var table = new PdfPTable(cols) { WidthPercentage = 100 };
                                // 每列均分宽度
                                float[] widths = new float[cols];
                                for (int i = 0; i < cols; i++) widths[i] = 1f;
                                table.SetWidths(widths);
                                table.DefaultCell.Padding = 6f;
                                table.DefaultCell.Border = PdfPCell.NO_BORDER;
                                table.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;

                                //int itemsThisPage = Math.Min(perPage, total - current);

                                for (int i = 0; i < total; i++)
                                {
                                    int questionIndex = current + 1;
                                    string line;

                                    int operandCount = CheckOperatorCount() + 1;
                                    var opsSequence = new List<OperatorSymbol>();
                                    for (int k = 0; k < operandCount - 1; k++)
                                        opsSequence.Add(PickRandomOperator(ops));

                                    // 根据 opsSequence 生成满足整除要求的整数操作数
                                    var operands = GenerateIntegersForOperators(maxnum, operandCount, opsSequence);
                                    if (operands == null || operands.Count == 0)
                                    {
                                        MessageBox.Show("生成随机整数失败。", "生成错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        doc.Close();
                                        return;
                                    }
                                    string sbExpr = BuildExpressionWithOps(operands, opsSequence);

                                    line = $"{questionIndex}. {sbExpr} =";

                                    // 使用 Paragraph + SetLeading 实现分散（两端）对齐，并保证行间距
                                    var para = new Paragraph(line, contentFont);
                                    para.SetLeading(lineLeading, 0f);
                                    para.Alignment = Element.ALIGN_JUSTIFIED;

                                    var cell = new PdfPCell(para)
                                    {
                                        Border = PdfPCell.NO_BORDER,
                                        PaddingTop = cellPadding,
                                        PaddingBottom = cellPadding,
                                        PaddingLeft = 6f,
                                        PaddingRight = 6f,
                                        VerticalAlignment = Element.ALIGN_MIDDLE,
                                        HorizontalAlignment = Element.ALIGN_LEFT,
                                        MinimumHeight = minCellHeight
                                    };
                                    table.AddCell(cell);
                                    current++;

                                }

                                // 如果本页单元格不到 cols*rows，最后一行需要补空单元格以保证布局整齐
                                int addedCells = total % cols;
                                if (addedCells != 0)
                                {
                                    int toAdd = cols - addedCells;
                                    for (int k = 0; k < toAdd; k++)
                                    {
                                        var emptyCell = new PdfPCell(new Phrase("", contentFont))
                                        {
                                            Border = PdfPCell.NO_BORDER,
                                            MinimumHeight = 30f
                                        };
                                        table.AddCell(emptyCell);
                                    }
                                }

                                doc.Add(table);

                                if (current < total)
                                {
                                    doc.NewPage();
                                }


                            }

                            doc.Close();
                            writer.Close();

                        }

                        MessageBox.Show("PDF 已保存到：" + savePath, "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AppendLog("已生成文件：" + savePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("生成或保存 PDF 时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }






        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {


            //if(checkBox1.Checked)
            //    this.textBox1.Enabled = false;
            //else
            //   this.textBox1.Enabled = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

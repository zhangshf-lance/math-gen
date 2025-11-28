namespace math_gen
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.chk_jia = new System.Windows.Forms.CheckBox();
            this.chk_jian = new System.Windows.Forms.CheckBox();
            this.chk_cheng = new System.Windows.Forms.CheckBox();
            this.chk_chu = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_clome = new System.Windows.Forms.TextBox();
            this.txt_row = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.txt_opter = new System.Windows.Forms.TextBox();
            this.txt_max = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txt_log = new System.Windows.Forms.TextBox();
            this.txt_hanjianju = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("华文新魏", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(290, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成PDF导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_jia
            // 
            this.chk_jia.AutoSize = true;
            this.chk_jia.Location = new System.Drawing.Point(112, 31);
            this.chk_jia.Name = "chk_jia";
            this.chk_jia.Size = new System.Drawing.Size(55, 29);
            this.chk_jia.TabIndex = 1;
            this.chk_jia.Text = "加";
            this.chk_jia.UseVisualStyleBackColor = true;
            // 
            // chk_jian
            // 
            this.chk_jian.AutoSize = true;
            this.chk_jian.Location = new System.Drawing.Point(183, 31);
            this.chk_jian.Name = "chk_jian";
            this.chk_jian.Size = new System.Drawing.Size(55, 29);
            this.chk_jian.TabIndex = 1;
            this.chk_jian.Text = "减";
            this.chk_jian.UseVisualStyleBackColor = true;
            // 
            // chk_cheng
            // 
            this.chk_cheng.AutoSize = true;
            this.chk_cheng.Location = new System.Drawing.Point(252, 31);
            this.chk_cheng.Name = "chk_cheng";
            this.chk_cheng.Size = new System.Drawing.Size(55, 29);
            this.chk_cheng.TabIndex = 1;
            this.chk_cheng.Text = "乘";
            this.chk_cheng.UseVisualStyleBackColor = true;
            // 
            // chk_chu
            // 
            this.chk_chu.AutoSize = true;
            this.chk_chu.Location = new System.Drawing.Point(320, 31);
            this.chk_chu.Name = "chk_chu";
            this.chk_chu.Size = new System.Drawing.Size(55, 29);
            this.chk_chu.TabIndex = 1;
            this.chk_chu.Text = "除";
            this.chk_chu.UseVisualStyleBackColor = true;
            this.chk_chu.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "最大数：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "行：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "运算符数量：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "题目总数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "列：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_hanjianju);
            this.groupBox1.Controls.Add(this.txt_clome);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_row);
            this.groupBox1.Font = new System.Drawing.Font("华文新魏", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(267, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 247);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成格式";
            // 
            // txt_clome
            // 
            this.txt_clome.Location = new System.Drawing.Point(91, 125);
            this.txt_clome.Name = "txt_clome";
            this.txt_clome.Size = new System.Drawing.Size(100, 32);
            this.txt_clome.TabIndex = 3;
            // 
            // txt_row
            // 
            this.txt_row.Location = new System.Drawing.Point(91, 62);
            this.txt_row.Name = "txt_row";
            this.txt_row.Size = new System.Drawing.Size(100, 32);
            this.txt_row.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_total);
            this.groupBox2.Controls.Add(this.txt_opter);
            this.groupBox2.Controls.Add(this.txt_max);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("华文新魏", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(38, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 247);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "题目控制";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(101, 204);
            this.txt_total.Name = "txt_total";
            this.txt_total.Size = new System.Drawing.Size(100, 32);
            this.txt_total.TabIndex = 3;
            // 
            // txt_opter
            // 
            this.txt_opter.Location = new System.Drawing.Point(101, 141);
            this.txt_opter.Name = "txt_opter";
            this.txt_opter.Size = new System.Drawing.Size(100, 32);
            this.txt_opter.TabIndex = 3;
            // 
            // txt_max
            // 
            this.txt_max.Location = new System.Drawing.Point(101, 78);
            this.txt_max.Name = "txt_max";
            this.txt_max.Size = new System.Drawing.Size(100, 32);
            this.txt_max.TabIndex = 3;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(175, 29);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "是否包含小数";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_jia);
            this.groupBox3.Controls.Add(this.chk_jian);
            this.groupBox3.Controls.Add(this.chk_cheng);
            this.groupBox3.Controls.Add(this.chk_chu);
            this.groupBox3.Font = new System.Drawing.Font("华文新魏", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(38, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(431, 66);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "运算符号";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("华文新魏", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(35, 338);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 61);
            this.label6.TabIndex = 5;
            this.label6.Text = "提示:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(85, 338);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(386, 61);
            this.txt_log.TabIndex = 7;
            this.txt_log.WordWrap = false;
            // 
            // txt_hanjianju
            // 
            this.txt_hanjianju.Location = new System.Drawing.Point(91, 195);
            this.txt_hanjianju.Name = "txt_hanjianju";
            this.txt_hanjianju.Size = new System.Drawing.Size(100, 32);
            this.txt_hanjianju.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "行间距：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 485);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "小学数学练习 ver:1.0.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chk_jia;
        private System.Windows.Forms.CheckBox chk_jian;
        private System.Windows.Forms.CheckBox chk_cheng;
        private System.Windows.Forms.CheckBox chk_chu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_clome;
        private System.Windows.Forms.TextBox txt_row;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.TextBox txt_opter;
        private System.Windows.Forms.TextBox txt_max;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_hanjianju;
    }
}


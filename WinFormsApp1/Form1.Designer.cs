namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            order_button = new Button();
            payment_button = new Button();
            invoice_button = new Button();
            workingwithxml = new Button();
            Transaction = new Button();
            genObj = new Button();
            add_udt = new Button();
            udf = new Button();
            del_udt = new Button();
            add_udt_rec = new Button();
            service_obj = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.Location = new Point(21, 28);
            button1.Name = "button1";
            button1.Size = new Size(140, 45);
            button1.TabIndex = 0;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ButtonFace;
            button2.Location = new Point(321, 34);
            button2.Name = "button2";
            button2.Size = new Size(140, 39);
            button2.TabIndex = 1;
            button2.Text = "Disconnect";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // order_button
            // 
            order_button.BackColor = SystemColors.ButtonFace;
            order_button.Location = new Point(21, 95);
            order_button.Name = "order_button";
            order_button.Size = new Size(140, 39);
            order_button.TabIndex = 2;
            order_button.Text = "Order";
            order_button.UseVisualStyleBackColor = false;
            order_button.Click += button3_Click;
            // 
            // payment_button
            // 
            payment_button.BackColor = SystemColors.ButtonFace;
            payment_button.Location = new Point(321, 95);
            payment_button.Name = "payment_button";
            payment_button.Size = new Size(140, 42);
            payment_button.TabIndex = 3;
            payment_button.Text = "Payment";
            payment_button.UseVisualStyleBackColor = false;
            payment_button.Click += payment_button_Click;
            // 
            // invoice_button
            // 
            invoice_button.BackColor = SystemColors.ButtonFace;
            invoice_button.Location = new Point(167, 95);
            invoice_button.Name = "invoice_button";
            invoice_button.Size = new Size(140, 39);
            invoice_button.TabIndex = 4;
            invoice_button.Text = "Invoice";
            invoice_button.UseVisualStyleBackColor = false;
            invoice_button.Click += invoice_button_Click;
            // 
            // workingwithxml
            // 
            workingwithxml.BackColor = SystemColors.ButtonFace;
            workingwithxml.Location = new Point(21, 155);
            workingwithxml.Name = "workingwithxml";
            workingwithxml.Size = new Size(140, 43);
            workingwithxml.TabIndex = 5;
            workingwithxml.Text = "WorkingWithXML";
            workingwithxml.UseVisualStyleBackColor = false;
            workingwithxml.Click += workingwithxml_Click;
            // 
            // Transaction
            // 
            Transaction.BackColor = SystemColors.ButtonFace;
            Transaction.Location = new Point(167, 155);
            Transaction.Name = "Transaction";
            Transaction.Size = new Size(140, 43);
            Transaction.TabIndex = 6;
            Transaction.Text = "Transaction";
            Transaction.TextImageRelation = TextImageRelation.TextAboveImage;
            Transaction.UseVisualStyleBackColor = false;
            Transaction.Click += Transaction_Click;
            // 
            // genObj
            // 
            genObj.BackColor = SystemColors.ButtonFace;
            genObj.Location = new Point(321, 155);
            genObj.Name = "genObj";
            genObj.Size = new Size(140, 43);
            genObj.TabIndex = 7;
            genObj.Text = "General Objects";
            genObj.TextImageRelation = TextImageRelation.TextAboveImage;
            genObj.UseVisualStyleBackColor = false;
            genObj.Click += genObj_Click;
            // 
            // add_udt
            // 
            add_udt.BackColor = SystemColors.ButtonFace;
            add_udt.Location = new Point(21, 218);
            add_udt.Name = "add_udt";
            add_udt.Size = new Size(140, 39);
            add_udt.TabIndex = 9;
            add_udt.Text = "Add UDT";
            add_udt.UseVisualStyleBackColor = false;
            add_udt.Click += add_udt_Click;
            // 
            // udf
            // 
            udf.BackColor = SystemColors.ButtonFace;
            udf.Location = new Point(321, 218);
            udf.Name = "udf";
            udf.Size = new Size(140, 39);
            udf.TabIndex = 10;
            udf.Text = "UDF";
            udf.UseVisualStyleBackColor = false;
            udf.Click += udf_Click;
            // 
            // del_udt
            // 
            del_udt.BackColor = SystemColors.ButtonFace;
            del_udt.Location = new Point(167, 218);
            del_udt.Name = "del_udt";
            del_udt.Size = new Size(140, 39);
            del_udt.TabIndex = 11;
            del_udt.Text = "Delete UDT";
            del_udt.UseVisualStyleBackColor = false;
            del_udt.Click += del_udt_Click;
            // 
            // add_udt_rec
            // 
            add_udt_rec.BackColor = SystemColors.ButtonFace;
            add_udt_rec.Location = new Point(21, 279);
            add_udt_rec.Name = "add_udt_rec";
            add_udt_rec.Size = new Size(140, 39);
            add_udt_rec.TabIndex = 12;
            add_udt_rec.Text = "Add UDT Records";
            add_udt_rec.UseVisualStyleBackColor = false;
            add_udt_rec.Click += add_udt_rec_Click;
            // 
            // service_obj
            // 
            service_obj.BackColor = SystemColors.ButtonFace;
            service_obj.Location = new Point(167, 279);
            service_obj.Name = "service_obj";
            service_obj.Size = new Size(140, 39);
            service_obj.TabIndex = 13;
            service_obj.Text = "Service Object";
            service_obj.UseVisualStyleBackColor = false;
            service_obj.Click += service_obj_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 330);
            Controls.Add(service_obj);
            Controls.Add(add_udt_rec);
            Controls.Add(del_udt);
            Controls.Add(udf);
            Controls.Add(add_udt);
            Controls.Add(genObj);
            Controls.Add(Transaction);
            Controls.Add(workingwithxml);
            Controls.Add(invoice_button);
            Controls.Add(payment_button);
            Controls.Add(order_button);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "01_DIAPI_Connection";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button order_button;
        private Button payment_button;
        private Button invoice_button;
        private Button workingwithxml;
        private Button Transaction;
        private Button genObj;
        private Button add_udt;
        private Button udf;
        private Button del_udt;
        private Button add_udt_rec;
        private Button service_obj;
    }
}

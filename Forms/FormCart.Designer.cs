namespace E_commerance_System.Forms
{
    partial class FormCart
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.btnUpdateQty = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label();
            this.txtShippingAddress = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPay = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.cmbBankList = new System.Windows.Forms.ComboBox();
            this.lblTID = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblEst = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.btnBrowseProof = new System.Windows.Forms.Button();
            this.btnClearPayment = new System.Windows.Forms.Button();
            this.lblProofPath = new System.Windows.Forms.Label();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(26, 35, 126);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(850, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(317, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🛒  Your Shopping Cart";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeight = 38;
            this.dgvCart.Location = new System.Drawing.Point(20, 75);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(790, 280);
            this.dgvCart.TabIndex = 1;
            // 
            // btnUpdateQty
            // 
            this.btnUpdateQty.BackColor = System.Drawing.Color.FromArgb(63, 81, 181);
            this.btnUpdateQty.FlatAppearance.BorderSize = 0;
            this.btnUpdateQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateQty.ForeColor = System.Drawing.Color.White;
            this.btnUpdateQty.Location = new System.Drawing.Point(20, 365);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new System.Drawing.Size(140, 38);
            this.btnUpdateQty.TabIndex = 2;
            this.btnUpdateQty.Text = "📝 Update Qty";
            this.btnUpdateQty.UseVisualStyleBackColor = false;
            this.btnUpdateQty.Click += new System.EventHandler(this.BtnUpdateQty_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(211, 47, 47);
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(170, 365);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(140, 38);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "❌ Remove Item";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.FromArgb(120, 120, 140);
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(320, 365);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(130, 38);
            this.btnClearCart.TabIndex = 4;
            this.btnClearCart.Text = "🗑️ Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.BtnClearCart_Click);
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.Controls.Add(this.lblSubtotal);
            this.pnlSummary.Controls.Add(this.lblTotal);
            this.pnlSummary.Controls.Add(this.lblAddr);
            this.pnlSummary.Controls.Add(this.txtShippingAddress);
            this.pnlSummary.Controls.Add(this.lblPhone);
            this.pnlSummary.Controls.Add(this.txtPhone);
            this.pnlSummary.Controls.Add(this.lblPay);
            this.pnlSummary.Controls.Add(this.cmbPaymentMethod);
            this.pnlSummary.Controls.Add(this.lblBank);
            this.pnlSummary.Controls.Add(this.cmbBankList);
            this.pnlSummary.Controls.Add(this.lblTID);
            this.pnlSummary.Controls.Add(this.txtTransactionId);
            this.pnlSummary.Controls.Add(this.lblEst);
            this.pnlSummary.Controls.Add(this.dtpDeliveryDate);
            this.pnlSummary.Controls.Add(this.btnBrowseProof);
            this.pnlSummary.Controls.Add(this.btnClearPayment);
            this.pnlSummary.Controls.Add(this.lblProofPath);
            this.pnlSummary.Controls.Add(this.btnPlaceOrder);
            this.pnlSummary.Location = new System.Drawing.Point(20, 415);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(790, 350);
            this.pnlSummary.TabIndex = 5;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtotal.Location = new System.Drawing.Point(20, 12);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(124, 25);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal: 0.00";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(0, 137, 123);
            this.lblTotal.Location = new System.Drawing.Point(20, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(133, 32);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: 0.00";
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddr.Location = new System.Drawing.Point(20, 75);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(171, 23);
            this.lblAddr.TabIndex = 2;
            this.lblAddr.Text = "📍 Shipping Address:";
            // 
            // txtShippingAddress
            // 
            this.txtShippingAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShippingAddress.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtShippingAddress.Location = new System.Drawing.Point(20, 98);
            this.txtShippingAddress.Name = "txtShippingAddress";
            this.txtShippingAddress.Size = new System.Drawing.Size(350, 32);
            this.txtShippingAddress.TabIndex = 3;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(20, 135);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(155, 23);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "📞 Contact Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPhone.Location = new System.Drawing.Point(20, 158);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(350, 32);
            this.txtPhone.TabIndex = 5;
            // 
            // lblPay
            // 
            this.lblPay.AutoSize = true;
            this.lblPay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPay.Location = new System.Drawing.Point(420, 75);
            this.lblPay.Name = "lblPay";
            this.lblPay.Size = new System.Drawing.Size(171, 23);
            this.lblPay.TabIndex = 6;
            this.lblPay.Text = "💳 Payment Method:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(420, 98);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(340, 31);
            this.cmbPaymentMethod.TabIndex = 7;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.CmbPaymentMethod_SelectedIndexChanged);
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBank.Location = new System.Drawing.Point(420, 135);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(130, 23);
            this.lblBank.TabIndex = 8;
            this.lblBank.Text = "🏦 Select Bank:";
            this.lblBank.Visible = false;
            // 
            // cmbBankList
            // 
            this.cmbBankList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankList.Location = new System.Drawing.Point(420, 158);
            this.cmbBankList.Name = "cmbBankList";
            this.cmbBankList.Size = new System.Drawing.Size(340, 31);
            this.cmbBankList.TabIndex = 9;
            this.cmbBankList.Visible = false;
            // 
            // lblTID
            // 
            this.lblTID.AutoSize = true;
            this.lblTID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTID.Location = new System.Drawing.Point(420, 200);
            this.lblTID.Name = "lblTID";
            this.lblTID.Size = new System.Drawing.Size(193, 23);
            this.lblTID.TabIndex = 10;
            this.lblTID.Text = "🔑 Reference Number:";
            this.lblTID.Visible = false;
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionId.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTransactionId.Location = new System.Drawing.Point(420, 223);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(340, 32);
            this.txtTransactionId.TabIndex = 11;
            this.txtTransactionId.Visible = false;
            // 
            // lblEst
            // 
            this.lblEst.AutoSize = true;
            this.lblEst.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEst.Location = new System.Drawing.Point(20, 205);
            this.lblEst.Name = "lblEst";
            this.lblEst.Size = new System.Drawing.Size(183, 23);
            this.lblEst.TabIndex = 12;
            this.lblEst.Text = "📅 Estimated Delivery:";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(20, 228);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(350, 30);
            this.dtpDeliveryDate.TabIndex = 13;
            // 
            // 
            // btnBrowseProof
            // 
            this.btnBrowseProof.BackColor = System.Drawing.Color.FromArgb(100, 100, 120);
            this.btnBrowseProof.FlatAppearance.BorderSize = 0;
            this.btnBrowseProof.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProof.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseProof.ForeColor = System.Drawing.Color.White;
            this.btnBrowseProof.Location = new System.Drawing.Point(420, 260);
            this.btnBrowseProof.Name = "btnBrowseProof";
            this.btnBrowseProof.Size = new System.Drawing.Size(180, 30);
            this.btnBrowseProof.TabIndex = 14;
            this.btnBrowseProof.Text = "📸 Attach Proof";
            this.btnBrowseProof.UseVisualStyleBackColor = false;
            this.btnBrowseProof.Click += new System.EventHandler(this.BtnBrowseProof_Click);
            // 
            // btnClearPayment
            // 
            this.btnClearPayment.BackColor = System.Drawing.Color.FromArgb(120, 120, 140);
            this.btnClearPayment.FlatAppearance.BorderSize = 0;
            this.btnClearPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearPayment.ForeColor = System.Drawing.Color.White;
            this.btnClearPayment.Location = new System.Drawing.Point(610, 260);
            this.btnClearPayment.Name = "btnClearPayment";
            this.btnClearPayment.Size = new System.Drawing.Size(80, 30);
            this.btnClearPayment.TabIndex = 17;
            this.btnClearPayment.Text = "🧹 Clear";
            this.btnClearPayment.UseVisualStyleBackColor = false;
            this.btnClearPayment.Click += new System.EventHandler(this.BtnClearPayment_Click);
            // 
            // lblProofPath
            // 
            this.lblProofPath.AutoSize = true;
            this.lblProofPath.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblProofPath.ForeColor = System.Drawing.Color.Gray;
            this.lblProofPath.Location = new System.Drawing.Point(700, 265);
            this.lblProofPath.Name = "lblProofPath";
            this.lblProofPath.Size = new System.Drawing.Size(107, 20);
            this.lblProofPath.TabIndex = 15;
            this.lblProofPath.Text = "No file selected";
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(0, 137, 123);
            this.btnPlaceOrder.FlatAppearance.BorderSize = 0;
            this.btnPlaceOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.White;
            this.btnPlaceOrder.Location = new System.Drawing.Point(420, 295);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(340, 45);
            this.btnPlaceOrder.TabIndex = 16;
            this.btnPlaceOrder.Text = "🚀 Pay";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.BtnPlaceOrder_Click);
            // 
            // FormCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.ClientSize = new System.Drawing.Size(850, 800);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.btnClearCart);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpdateQty);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormCart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🛒 Shopping Cart";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Button btnUpdateQty;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.TextBox txtShippingAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPay;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.ComboBox cmbBankList;
        private System.Windows.Forms.Label lblTID;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblEst;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Button btnBrowseProof;
        private System.Windows.Forms.Label lblProofPath;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Button btnClearPayment;
    }
}

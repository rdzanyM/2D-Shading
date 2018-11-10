﻿namespace Light
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.positionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constantLight = new System.Windows.Forms.ToolStripMenuItem();
            this.variableLight = new System.Windows.Forms.ToolStripMenuItem();
            this.triangle1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangle2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flatToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.steelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.normalMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metalFloorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalMapToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fromImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.metalFloorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightToolStripMenuItem,
            this.triangle1ToolStripMenuItem,
            this.triangle2ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem2,
            this.positionToolStripMenuItem});
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.lightToolStripMenuItem.Text = "Light";
            // 
            // colorToolStripMenuItem2
            // 
            this.colorToolStripMenuItem2.Name = "colorToolStripMenuItem2";
            this.colorToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.colorToolStripMenuItem2.Text = "Color";
            this.colorToolStripMenuItem2.Click += new System.EventHandler(this.colorToolStripMenuItem2_Click);
            // 
            // positionToolStripMenuItem
            // 
            this.positionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantLight,
            this.variableLight});
            this.positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            this.positionToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.positionToolStripMenuItem.Text = "Position";
            // 
            // constantLight
            // 
            this.constantLight.Name = "constantLight";
            this.constantLight.Size = new System.Drawing.Size(122, 22);
            this.constantLight.Text = "Constant";
            this.constantLight.Click += new System.EventHandler(this.constantToolStripMenuItem_Click);
            // 
            // variableLight
            // 
            this.variableLight.Name = "variableLight";
            this.variableLight.Size = new System.Drawing.Size(122, 22);
            this.variableLight.Text = "Variable";
            this.variableLight.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // triangle1ToolStripMenuItem
            // 
            this.triangle1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.normalMapToolStripMenuItem});
            this.triangle1ToolStripMenuItem.Name = "triangle1ToolStripMenuItem";
            this.triangle1ToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.triangle1ToolStripMenuItem.Text = "Triangle1";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flatToolStripMenuItem,
            this.textureToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // flatToolStripMenuItem
            // 
            this.flatToolStripMenuItem.Name = "flatToolStripMenuItem";
            this.flatToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.flatToolStripMenuItem.Text = "Flat";
            this.flatToolStripMenuItem.Click += new System.EventHandler(this.flatToolStripMenuItem_Click);
            // 
            // textureToolStripMenuItem
            // 
            this.textureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem,
            this.steelToolStripMenuItem});
            this.textureToolStripMenuItem.Name = "textureToolStripMenuItem";
            this.textureToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.textureToolStripMenuItem.Text = "Texture";
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            this.addImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addImageToolStripMenuItem.Text = "Add Image";
            this.addImageToolStripMenuItem.Click += new System.EventHandler(this.addImageToolStripMenuItem_Click);
            // 
            // steelToolStripMenuItem
            // 
            this.steelToolStripMenuItem.Name = "steelToolStripMenuItem";
            this.steelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.steelToolStripMenuItem.Text = "Steel";
            this.steelToolStripMenuItem.Click += new System.EventHandler(this.steelToolStripMenuItem_Click);
            // 
            // triangle2ToolStripMenuItem
            // 
            this.triangle2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem1,
            this.normalMapToolStripMenuItem1});
            this.triangle2ToolStripMenuItem.Name = "triangle2ToolStripMenuItem";
            this.triangle2ToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.triangle2ToolStripMenuItem.Text = "Triangle2";
            // 
            // colorToolStripMenuItem1
            // 
            this.colorToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flatToolStripMenuItem1,
            this.textureToolStripMenuItem1});
            this.colorToolStripMenuItem1.Name = "colorToolStripMenuItem1";
            this.colorToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.colorToolStripMenuItem1.Text = "Color";
            // 
            // flatToolStripMenuItem1
            // 
            this.flatToolStripMenuItem1.Name = "flatToolStripMenuItem1";
            this.flatToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.flatToolStripMenuItem1.Text = "Flat";
            this.flatToolStripMenuItem1.Click += new System.EventHandler(this.flatToolStripMenuItem1_Click);
            // 
            // textureToolStripMenuItem1
            // 
            this.textureToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem1,
            this.steelToolStripMenuItem1});
            this.textureToolStripMenuItem1.Name = "textureToolStripMenuItem1";
            this.textureToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.textureToolStripMenuItem1.Text = "Texture";
            // 
            // addImageToolStripMenuItem1
            // 
            this.addImageToolStripMenuItem1.Name = "addImageToolStripMenuItem1";
            this.addImageToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.addImageToolStripMenuItem1.Text = "Add Image";
            this.addImageToolStripMenuItem1.Click += new System.EventHandler(this.addImageToolStripMenuItem1_Click);
            // 
            // steelToolStripMenuItem1
            // 
            this.steelToolStripMenuItem1.Name = "steelToolStripMenuItem1";
            this.steelToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.steelToolStripMenuItem1.Text = "Steel";
            this.steelToolStripMenuItem1.Click += new System.EventHandler(this.steelToolStripMenuItem1_Click);
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 493);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // normalMapToolStripMenuItem
            // 
            this.normalMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromImageToolStripMenuItem,
            this.metalFloorToolStripMenuItem});
            this.normalMapToolStripMenuItem.Name = "normalMapToolStripMenuItem";
            this.normalMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.normalMapToolStripMenuItem.Text = "Normal Map";
            // 
            // metalFloorToolStripMenuItem
            // 
            this.metalFloorToolStripMenuItem.Name = "metalFloorToolStripMenuItem";
            this.metalFloorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.metalFloorToolStripMenuItem.Text = "Metal";
            // 
            // fromImageToolStripMenuItem
            // 
            this.fromImageToolStripMenuItem.Name = "fromImageToolStripMenuItem";
            this.fromImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fromImageToolStripMenuItem.Text = "From Image";
            this.fromImageToolStripMenuItem.Click += new System.EventHandler(this.fromImageToolStripMenuItem_Click);
            // 
            // normalMapToolStripMenuItem1
            // 
            this.normalMapToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromImageToolStripMenuItem1,
            this.metalFloorToolStripMenuItem1});
            this.normalMapToolStripMenuItem1.Name = "normalMapToolStripMenuItem1";
            this.normalMapToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.normalMapToolStripMenuItem1.Text = "Normal Map";
            // 
            // fromImageToolStripMenuItem1
            // 
            this.fromImageToolStripMenuItem1.Name = "fromImageToolStripMenuItem1";
            this.fromImageToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.fromImageToolStripMenuItem1.Text = "From Image";
            this.fromImageToolStripMenuItem1.Click += new System.EventHandler(this.fromImageToolStripMenuItem1_Click);
            // 
            // metalFloorToolStripMenuItem1
            // 
            this.metalFloorToolStripMenuItem1.Name = "metalFloorToolStripMenuItem1";
            this.metalFloorToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.metalFloorToolStripMenuItem1.Text = "Metal";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 517);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangle1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangle2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem flatToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem textureToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem positionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constantLight;
        private System.Windows.Forms.ToolStripMenuItem variableLight;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem steelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem steelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem normalMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metalFloorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalMapToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fromImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem metalFloorToolStripMenuItem1;
    }
}


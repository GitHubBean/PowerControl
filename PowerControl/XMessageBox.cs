﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PowerControl
{
    /// <summary>
    /// 消息框
    /// </summary>
    public sealed partial class XMessageBox : XForm
    {
        #region 常量

        private const float TextMaxWidth = 430;
        private const float TextMinWidth = 90;

        private const float TextMarginX = 10;
        private const float TextMarginY = 30;

        private const float IconMarginX = 20;

        private const float WindowMinHeight = 80 + ButtonHeight + ButtonMargin * 2;

        private const int ButtonMargin = 15;
        private const int ButtonWidth = 70;
        private const int ButtonHeight = 30;

        #endregion 常量

        #region 字段

        // 内容文本
        private readonly string _content;
        // 图标引用
        private readonly Icon _icon;
        // 文本尺寸
        private readonly SizeF _szText;

        #endregion 字段

        #region 实例

        /// <summary>
        /// 初始化消息框的实例
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        private XMessageBox(
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None)
        {
            InitializeComponent();

            if (Parent == null)
                StartPosition = FormStartPosition.CenterScreen;

            _content = text;
            Text = caption;

            base.TitleBarStartColor = TitleBarStartColor;
            base.TitleBarEndColor = TitleBarEndColor;
            base.TitleBarForeColor = TitleForeColor;
            base.Shadow = Shadow;

            switch (icon)
            {
                case MessageBoxIcon.None:
                    break;
                case MessageBoxIcon.Hand:
                    _icon = SystemIcons.Hand;
                    break;
                case MessageBoxIcon.Question:
                    _icon = SystemIcons.Question;
                    break;
                case MessageBoxIcon.Exclamation:
                    _icon = SystemIcons.Exclamation;
                    break;
                case MessageBoxIcon.Asterisk:
                    _icon = SystemIcons.Asterisk;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(icon), icon, null);
            }

            XButton btnOK, btnCancel, btnAbort, btnRetry, btnIgnore, btnYes, btnNo;

            int btnTotalWidth;

            using (Graphics g = CreateGraphics())
                _szText = g.MeasureString(text, Font,
                    new SizeF(TextMaxWidth, Screen.PrimaryScreen.Bounds.Height),
                    StringFormat.GenericDefault);

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btnOK = CreateButton("确定", 'O', DialogResult.OK);
                    btnTotalWidth = ButtonWidth + ButtonMargin * 2;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2) + TitleBarHeight;
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnOK.Location = new Point((int)(Width / 2F - btnOK.Width / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnOK });
                    break;
                case MessageBoxButtons.OKCancel:
                    btnOK = CreateButton("确定", 'O', DialogResult.OK);
                    btnCancel = CreateButton("取消", '\0', DialogResult.Cancel);

                    btnTotalWidth = ButtonWidth * 2 + ButtonMargin * 3;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2)
                             + TitleBarHeight;
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnOK.Location = new Point((int)(Width / 2F - ButtonWidth - ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnCancel.Location = new Point((int)(Width / 2F + ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnOK, btnCancel });
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    btnAbort = CreateButton("中止", 'A', DialogResult.Abort);
                    btnRetry = CreateButton("重试", 'R', DialogResult.Retry);
                    btnIgnore = CreateButton("忽略", 'I', DialogResult.Ignore);

                    btnTotalWidth = ButtonWidth * 3 + ButtonMargin * 4;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2)
                             + TitleBarHeight;
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnAbort.Location = new Point((int)(Width / 2F - ButtonWidth - ButtonMargin - ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnRetry.Location = new Point((int)(Width / 2F - ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnIgnore.Location = new Point((int)(Width / 2F + ButtonMargin + ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnAbort, btnRetry, btnIgnore });
                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnYes = CreateButton("是", 'Y', DialogResult.Yes);
                    btnNo = CreateButton("否", 'N', DialogResult.No);
                    btnCancel = CreateButton("取消", '\0', DialogResult.Cancel);

                    btnTotalWidth = ButtonWidth * 3 + ButtonMargin * 4;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2);
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnYes.Location = new Point((int)(Width / 2F - ButtonWidth - ButtonMargin - ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnNo.Location = new Point((int)(Width / 2F - ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnCancel.Location = new Point((int)(Width / 2F + ButtonMargin + ButtonWidth / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnYes, btnNo, btnCancel });
                    break;
                case MessageBoxButtons.YesNo:
                    btnYes = CreateButton("是", 'Y', DialogResult.Yes);
                    btnNo = CreateButton("否", 'N', DialogResult.No);

                    btnTotalWidth = ButtonWidth * 2 + ButtonMargin * 3;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2)
                             + TitleBarHeight;
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnYes.Location = new Point((int)(Width / 2F - ButtonWidth - ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnNo.Location = new Point((int)(Width / 2F + ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnYes, btnNo });
                    break;
                case MessageBoxButtons.RetryCancel:
                    btnRetry = CreateButton("重试", 'R', DialogResult.Retry);
                    btnCancel = CreateButton("取消", '\0', DialogResult.Cancel);

                    btnTotalWidth = ButtonWidth * 2 + ButtonMargin * 3;

                    if (_szText.Width < TextMinWidth)
                        _szText.Width = TextMinWidth;

                    Width = Math.Max((int)(_szText.Width + TextMarginX * 2), btnTotalWidth) + (int)(_icon?.Width + IconMarginX ?? 0);
                    Height = (int)(ButtonHeight + ButtonMargin * 2 + _szText.Height + TextMarginY * 2)
                             + TitleBarHeight;
                    if (Height < WindowMinHeight)
                        Height = (int)WindowMinHeight;

                    btnRetry.Location = new Point((int)(Width / 2F - ButtonWidth - ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    btnCancel.Location = new Point((int)(Width / 2F + ButtonMargin / 2F),
                        Height - ButtonMargin - ButtonHeight - TitleBarHeight);

                    Controls.AddRange(new Control[] { btnRetry, btnCancel });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null);
            }
        }

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_icon != null)
            {
                e.Graphics.DrawIcon(_icon, (int)IconMarginX, (int)TextMarginY);
                e.Graphics.DrawString(_content, Font, new SolidBrush(Color.FromArgb(80, 80, 80)),
                    new RectangleF(IconMarginX + _icon.Width + TextMarginX, TextMarginY, _szText.Width, _szText.Height),
                    StringFormat.GenericDefault);
            }
            else
                e.Graphics.DrawString(_content, Font, new SolidBrush(Color.FromArgb(80, 80, 80)),
                    new RectangleF(TextMarginX, TextMarginY, _szText.Width, _szText.Height),
                    StringFormat.GenericDefault);

            base.OnPaint(e);
        }

        /// <inheritdoc />
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }

        #endregion 实例

        #region 静态

        /// <summary>
        /// 获取或设置消息框标题栏的渐变起始颜色。
        /// </summary>
        public new static Color TitleBarStartColor { get; set; } = Color.PaleVioletRed;

        /// <summary>
        /// 获取或设置消息框标题栏的渐变结束颜色。
        /// </summary>
        public new static Color TitleBarEndColor { get; set; } = Color.Pink;

        /// <summary>
        /// 获取或设置消息框标题的前景色。
        /// </summary>
        public new static Color TitleForeColor { get; set; } = Color.White;

        /// <summary>
        /// 获取或设置一个值，该值指示是否渲染消息框的阴影。
        /// </summary>
        public new static bool Shadow { get; set; } = true;

        /// <summary>
        /// 按钮渐变起始颜色
        /// </summary>
        public static Color ButtonStartColor { get; set; } = Color.White;

        /// <summary>
        /// 按钮渐变结束颜色
        /// </summary>
        public static Color ButtonEndColor { get; set; } = Color.FromArgb(232, 232, 232);

        /// <summary>
        /// 按钮前景颜色
        /// </summary>
        public static Color ButtonForeColor { get; set; } = Color.FromArgb(80, 80, 80);

        /// <summary>
        /// 按钮鼠标按下状态渐变起始颜色
        /// </summary>
        public static Color ButtonHoldingStartColor { get; set; } = Color.FromArgb(109, 169, 255);

        /// <summary>
        /// 按钮鼠标按下状态渐变结束颜色
        /// </summary>
        public static Color ButtonHoldingEndColor { get; set; } = Color.FromArgb(83, 128, 252);

        /// <summary>
        /// 按钮鼠标按下状态前景颜色
        /// </summary>
        public static Color ButtonHoldingForeColor { get; set; } = Color.White;

        /// <summary>
        /// 显示具有指定文本的消息框
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">按钮</param>
        /// <param name="icon">图标</param>
        /// <returns>消息框的返回结果</returns>
        public static DialogResult Show(
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None)
        {
            using (XMessageBox mbox = new XMessageBox(text, caption, buttons, icon))
                return mbox.ShowDialog();
        }

        /// <summary>
        /// 创建消息框按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="shortcut"></param>
        /// <param name="dialogResult">按钮触发的消息框返回值</param>
        /// <returns>按钮实例</returns>
        private static XButton CreateButton(string text, char shortcut, DialogResult dialogResult)
        {
            XButton btn = new XButton
            {
                Text = text,
                Size = new Size(ButtonWidth, ButtonHeight),
                Shortcut = shortcut,
                RoundedRectangleCornerRadius = 4,
                BorderColor = Color.White,
                BorderWidth = 0,
                StartColor = ButtonStartColor,
                EndColor = ButtonEndColor,
                ForeColor = ButtonForeColor,
                HoldingStartColor = ButtonHoldingStartColor,
                HoldingEndColor = ButtonHoldingEndColor,
                HoldingForeColor = ButtonHoldingForeColor
            };

            btn.Click += (s1, e1) =>
            {
                Form f = ((XButton)s1)?.FindForm();
                if (f == null) return;
                f.DialogResult = dialogResult;
            };

            return btn;
        }

        #endregion 静态
    }
}

﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PowerControl
{
    /// <summary>
    /// 实用工具类
    /// </summary>
    static class Utilities
    {
        /// <summary>
        /// 递归获取控件所在的父窗口
        /// </summary>
        /// <param name="c">控件</param>
        /// <returns>父窗口引用</returns>
        public static Form GetParentForm(Control c)
        {
            if (c.Parent == null) return null;
            if (c.Parent is Form) return c.Parent as Form;

            return GetParentForm(c.Parent);
        }

        /// <summary>
        /// 获取更深的颜色
        /// </summary>
        /// <param name="baseColor">基色</param>
        /// <param name="diff">色差</param>
        /// <returns></returns>
        public static Color GetDeeperColor(Color baseColor, int diff = 32)
        {
            int r, g, b;

            r = baseColor.R - diff;
            g = baseColor.G - diff;
            b = baseColor.B - diff;
            r = r < 0 ? 0 : r;
            g = g < 0 ? 0 : g;
            b = b < 0 ? 0 : b;

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 获取更浅的颜色
        /// </summary>
        /// <param name="baseColor">基色</param>
        /// <param name="diff">色差</param>
        /// <returns></returns>
        public static Color GetLighterColor(Color baseColor, int diff = 32)
        {
            int r, g, b;

            r = baseColor.R + diff;
            g = baseColor.G + diff;
            b = baseColor.B + diff;
            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 获取圆角矩形路径
        /// </summary>
        /// <param name="rect">参考矩形</param>
        /// <param name="radius">圆角半径</param>
        /// <returns></returns>
        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;

            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            //左上角
            path.AddArc(arcRect, 180, 90);

            //右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            //右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            //左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// 根据半径、角度求圆上坐标
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="radius">半径</param>
        /// <param name="angle">角度</param>
        /// <returns>坐标</returns>
        public static PointF GetDotLocationByAngle(PointF center, float radius, int angle)
        {
            var x = (float)(center.X + radius * Math.Cos(angle * Math.PI / 180));
            var y = (float)(center.Y + radius * Math.Sin(angle * Math.PI / 180));

            return new PointF(x, y);
        }
    }
}
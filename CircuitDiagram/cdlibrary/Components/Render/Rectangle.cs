﻿// Rectangle.cs
//
// Circuit Diagram http://www.circuit-diagram.org/
//
// Copyright (C) 2012  Sam Fisher
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CircuitDiagram.Components.Render
{
    class Rectangle : IRenderCommand
    {
        public ComponentPoint Location { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double StrokeThickness { get; set; }
        public bool Fill { get; set; }

        public RenderCommandType Type
        {
            get { return RenderCommandType.Rect; }
        }

        public Rectangle()
        {
            Location = new ComponentPoint();
            Width = 0d;
            Height = 0d;
            StrokeThickness = 2d;
            Fill = false;
        }

        public Rectangle(ComponentPoint location, double width, double height)
        {
            Location = location;
            Width = width;
            Height = height;
            StrokeThickness = 2d;
            Fill = false;
        }

        public Rectangle(ComponentPoint location, double width, double height, double strokeThickness, bool fill)
        {
            Location = location;
            Width = width;
            Height = height;
            StrokeThickness = strokeThickness;
            Fill = fill;
        }

        public void Render(Component component, CircuitDiagram.Render.IRenderContext dc)
        {
            Rect drawRect = new System.Windows.Rect(Location.Resolve(component), new Size(Width, Height));
            if (component.IsFlipped && component.Horizontal)
                drawRect = new Rect(drawRect.X - Width, drawRect.Y, Width, Height);
            else if (component.IsFlipped && !component.Horizontal)
                drawRect = new Rect(drawRect.X, drawRect.Y - Height, Width, Height);

            if (dc.Absolute)
                dc.DrawRectangle(Point.Add(drawRect.TopLeft, component.Offset), drawRect.Size, StrokeThickness, Fill);
            else
                dc.DrawRectangle(drawRect.TopLeft, drawRect.Size, StrokeThickness, Fill);
        }
    }
}

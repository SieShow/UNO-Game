﻿using MauMau.Classes.Background.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using MauMau.Classes.Background.Enum;
using System.Windows.Input;

namespace MauMau.Classes.Background
{
    public abstract class Carta : ICompatible
    {
        protected ImageBrush frontImage;
        protected ImageBrush backImage = new ImageBrush(((ImageSource)Application.Current.Resources["Card_other_back"]));
        protected Rectangle elementUI;

        public ImageBrush FrontImage { get { return this.frontImage; } }
        public Rectangle ElementUI { get { return this.elementUI; } set { this.elementUI = value; } }
        public ImageBrush BackImage { get { return this.backImage; } }

        public Carta(ImageBrush image, string id)
        {
            this.frontImage = image;

            this.elementUI = new Rectangle();
            this.elementUI.Fill = this.frontImage;
            this.elementUI.RadiusX = 10;
            this.elementUI.RadiusY = 10;
            this.elementUI.Height = 180;
            this.elementUI.Width = 114;
            this.elementUI.Name = "newcard";
            this.elementUI.Uid = id;
            this.elementUI.Cursor = Cursors.Hand;
        }
        public string GetID()
        {
            return this.elementUI.Uid;
        }
        public abstract bool Compatible(ICompatible card);

        public abstract bool Compatible(ICompatible card, Cor color);
    }
}

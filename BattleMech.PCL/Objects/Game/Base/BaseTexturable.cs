using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleMech.PCL.Objects.Game.Base {
    public class BaseTexturable : BaseRenderable, ICollidable
    {
        protected ContentManager _content;
        protected List<ActiveAssetsVIEW> _assetInfos;

        public Texture2D Texture;

        public TEXTURABLE_ITEM_TYPES ItemType;
        
        public bool IsFullScreen;

        public bool IsColliding { get; set; }

        public Func<Rectangle, bool> CheckOnScreen;

        public BaseTexturable() { }

        public BaseTexturable(ContentManager content, List<ActiveAssetsVIEW> assetInfos, TEXTURABLE_ITEM_TYPES itemType) : base() {
            this._content = content;
            this._assetInfos = assetInfos;
            Texture = content.Load<Texture2D>(assetInfos.FirstOrDefault().Filename);

            ItemType = itemType;
        }

        public void Init(List<ActiveAssetsVIEW> assetInfos, Vector2 position)
        {
            _assetInfos = assetInfos;
            Texture = _content.Load<Texture2D>(assetInfos.FirstOrDefault().Filename);
            PositionX = position.X;
            PositionY = position.Y;
            IsActive = true;
        }

        public override Rectangle GetRectange() {
            //if (IsFullScreen) {
            //    return new Rectangle(0, 0, (int) PositionX, (int) PositionY);
            //}

            return new Rectangle((int) PositionX, (int) PositionY, Texture.Width, Texture.Height);
        }

        public virtual bool GetCollision(Rectangle other)
        {
            //check collision against other rectangle
            Rectangle bounds = GetRectange();

            if ((bounds.Right >= other.Left && bounds.Left < other.Right ||
                bounds.Left <= other.Right && bounds.Right > other.Left) &&
                (bounds.Bottom >= other.Top && bounds.Top < other.Bottom ||
                bounds.Top <= other.Bottom && bounds.Bottom > other.Top))
            {
                return true;
            }

            return false;
        }
    }
}
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Interfaces;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Objects.Game
{
    class VisualEffect : BaseTexturable, IAnimable
    {
        public List<string> animations { get; set; }
        public string currentAnimation { get; set; }
        public int currentFrame { get; set; }
        public int totalFrames { get; set; }
        public bool loops { get; set; }
        public double? duration { get; set; }
        public double frameDuration { get; set; }

        public VisualEffect(ContentManager content, List<ActiveAssetsVIEW> assetInfos) : base(content, assetInfos, TEXTURABLE_ITEM_TYPES.EFFECT){
            
        }

        public override void Update(GameTime gameTime){
            duration += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (duration >= frameDuration) NextFrame();
            
            base.Update(gameTime);

            //if (CheckOnScreen != null && !CheckOnScreen(GetRectange())) IsActive = false;
        }

        public void Start(double? totalDuration = null, bool loops = false, bool playBackwards = false)
        {
            duration = 0;
            if (totalDuration.HasValue) frameDuration = totalDuration.Value / _assetInfos.Count;
            else frameDuration = 0;     //default frame rate

            currentFrame = 1;

            var assetInfo = _assetInfos.FirstOrDefault(asset => asset.Filename.Contains(currentFrame.ToString()));
            Texture = _content.Load<Texture2D>(assetInfo.Filename);
        }

        public void NextFrame() {
            currentFrame++;

            //check if the animation has reached the end
            if (currentFrame > _assetInfos.Count())
            {
                if(loops) currentFrame = 1;
                else IsActive = false;
            }

            //if animation is still active by this point, render the next frame
            if (IsActive)
            {
                var assetInfo = _assetInfos.FirstOrDefault(asset => asset.Filename.Contains(currentFrame.ToString()));
                Texture = _content.Load<Texture2D>(assetInfo.Filename);
            }

            duration = 0;   //reset duration
        }
        public void PrevFrame() { }
    }
}

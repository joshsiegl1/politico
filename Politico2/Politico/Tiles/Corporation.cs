using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Tiles
{
    public class Corporation : Tile
    {
        public struct Buildings
        {
            static Texture2D corp1small;
            public static Texture2D Corp1Small { get { return corp1small; } set { corp1small = value; } }

            static Texture2D corp1mediumbottom; 
            public static Texture2D Corp1MediumBottom { get { return corp1mediumbottom; } set { corp1mediumbottom = value; } }

            static Texture2D corp1mediumtop;
            public static Texture2D Corp1MediumTop { get { return corp1mediumtop; } set { corp1mediumtop = value; } }

            static Texture2D corp1largemid; 
            public static Texture2D Corp1LargeMid { get { return corp1largemid; } set { corp1largemid = value; } }

            static Texture2D corp1largetop; 
            public static Texture2D Corp1LargeTop { get { return corp1largetop; } set { corp1largetop = value; } }


            static Texture2D corp2small;
            public static Texture2D Corp2Small { get { return corp2small; } set { corp2small = value; } }

            static Texture2D corp2mediumbottom;
            public static Texture2D Corp2MediumBottom { get { return corp2mediumbottom; } set { corp2mediumbottom = value; } }

            static Texture2D corp2mediumtop;
            public static Texture2D Corp2MediumTop { get { return corp2mediumtop; } set { corp2mediumtop = value; } }

            static Texture2D corp2largemid;
            public static Texture2D Corp2LargeMid { get { return corp2largemid; } set { corp2largemid = value; } }

            static Texture2D corp2largetop;
            public static Texture2D Corp2LargeTop { get { return corp2largetop; } set { corp2largetop = value; } }


            static Texture2D corp3small;
            public static Texture2D Corp3Small { get { return corp3small; } set { corp3small = value; } }

            static Texture2D corp3mediumbottom;
            public static Texture2D Corp3MediumBottom { get { return corp3mediumbottom; } set { corp3mediumbottom = value; } }

            static Texture2D corp3mediumtop;
            public static Texture2D Corp3MediumTop { get { return corp3mediumtop; } set { corp3mediumtop = value; } }

            static Texture2D corp3largemid;
            public static Texture2D Corp3LargeMid { get { return corp3largemid; } set { corp3largemid = value; } }

            static Texture2D corp3largetop;
            public static Texture2D Corp3LargeTop { get { return corp3largetop; } set { corp3largetop = value; } }
        }

        public struct BuildingsNight
        {
            static Texture2D corp1smallnight;
            public static Texture2D Corp1SmallNight { get { return corp1smallnight; } set { corp1smallnight = value; } }

            static Texture2D corp1mediumbottomnight;
            public static Texture2D Corp1MediumBottomNight { get { return corp1mediumbottomnight; } set { corp1mediumbottomnight = value; } }

            static Texture2D corp1mediumtopnight;
            public static Texture2D Corp1MediumTopNight { get { return corp1mediumtopnight; } set { corp1mediumtopnight = value; } }

            static Texture2D corp1largemidnight;
            public static Texture2D Corp1LargeMidNight { get { return corp1largemidnight; } set { corp1largemidnight = value; } }

            static Texture2D corp1largetopnight;
            public static Texture2D Corp1LargeTopNight { get { return corp1largetopnight; } set { corp1largetopnight = value; } }


            static Texture2D corp2smallnight;
            public static Texture2D Corp2SmallNight { get { return corp2smallnight; } set { corp2smallnight = value; } }

            static Texture2D corp2mediumbottomnight;
            public static Texture2D Corp2MediumBottomNight { get { return corp2mediumbottomnight; } set { corp2mediumbottomnight = value; } }

            static Texture2D corp2mediumtopnight;
            public static Texture2D Corp2MediumTopNight { get { return corp2mediumtopnight; } set { corp2mediumtopnight = value; } }

            static Texture2D corp2largemidnight;
            public static Texture2D Corp2LargeMidNight { get { return corp2largemidnight; } set { corp2largemidnight = value; } }

            static Texture2D corp2largetopnight;
            public static Texture2D Corp2LargeTopNight { get { return corp2largetopnight; } set { corp2largetopnight = value; } }


            static Texture2D corp3smallnight;
            public static Texture2D Corp3SmallNight { get { return corp3smallnight; } set { corp3smallnight = value; } }

            static Texture2D corp3mediumbottomnight;
            public static Texture2D Corp3MediumBottomNight { get { return corp3mediumbottomnight; } set { corp3mediumbottomnight = value; } }

            static Texture2D corp3mediumtopnight;
            public static Texture2D Corp3MediumTopNight { get { return corp3mediumtopnight; } set { corp3mediumtopnight = value; } }

            static Texture2D corp3largemidnight;
            public static Texture2D Corp3LargeMidNight { get { return corp3largemidnight; } set { corp3largemidnight = value; } }

            static Texture2D corp3largetopnight;
            public static Texture2D Corp3LargeTopNight { get { return corp3largetopnight; } set { corp3largetopnight = value; } }
        }

        public struct Lights
        {
            static Texture2D corp1smalllights; 
            public static Texture2D Corp1SmallLights { get { return corp1smalllights; } set { corp1smalllights = value; } }

            static Texture2D corp1mediumlightsbottom; 
            public static Texture2D Corp1MediumLightsBottom { get { return corp1mediumlightsbottom; } set { corp1mediumlightsbottom = value; } }

            static Texture2D corp1mediumlightstop; 
            public static Texture2D Corp1MediumLightsTop { get { return corp1mediumlightstop; } set { corp1mediumlightstop = value; } }


            static Texture2D corp2smalllights;
            public static Texture2D Corp2SmallLights { get { return corp2smalllights; } set { corp2smalllights = value; } }

            static Texture2D corp2mediumlightsbottom;
            public static Texture2D Corp2MediumLightsBottom { get { return corp2mediumlightsbottom; } set { corp2mediumlightsbottom = value; } }

            static Texture2D corp2mediumlightstop;
            public static Texture2D Corp2MediumLightsTop { get { return corp2mediumlightstop; } set { corp2mediumlightstop = value; } }


            static Texture2D corp3smalllights;
            public static Texture2D Corp3SmallLights { get { return corp3smalllights; } set { corp3smalllights = value; } }

            static Texture2D corp3mediumlightsbottom;
            public static Texture2D Corp3MediumLightsBottom { get { return corp3mediumlightsbottom; } set { corp3mediumlightsbottom = value; } }

            static Texture2D corp3mediumlightstop;
            public static Texture2D Corp3MediumLightsTop { get { return corp3mediumlightstop; } set { corp3mediumlightstop = value; } }
        }

        private int level;
        private float levelTimer; 

        private enum CorpType
        {
            corp1, 
            corp2, 
            corp3
        }

        private CorpType corpType;

        static Random random = new Random();  

        public Corporation(Vector2 position) : base(Buildings.Corp1Small, position, BuildingsNight.Corp1SmallNight)
        {
            corpType = (CorpType)random.Next(3);

            if (corpType == CorpType.corp2)
                UpdateBaseTextureNight(BuildingsNight.Corp2SmallNight);
            else if (corpType == CorpType.corp3) UpdateBaseTextureNight(BuildingsNight.Corp3SmallNight);

            if (corpType == CorpType.corp2)
                UpdateBaseTexture(Buildings.Corp2Small);
            else if (corpType == CorpType.corp3) UpdateBaseTexture(Buildings.Corp3Small);

        }

        private void UpdateBaseTexture(CorpType corpType)
        {
            if (corpType == CorpType.corp1)
                UpdateBaseTextureNight(BuildingsNight.Corp1MediumBottomNight);
            else if (corpType == CorpType.corp2)
                UpdateBaseTextureNight(BuildingsNight.Corp2MediumBottomNight);
            else UpdateBaseTextureNight(BuildingsNight.Corp3MediumBottomNight);


            if (corpType == CorpType.corp1)
                UpdateBaseTexture(Buildings.Corp1MediumBottom);
            else if (corpType == CorpType.corp2)
                UpdateBaseTexture(Buildings.Corp2MediumBottom);
            else UpdateBaseTexture(Buildings.Corp3MediumBottom);
        }

        public override void Update(GameTime gametime)
        {
            if (level <= 4)
            {
                levelTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (levelTimer >= 3000)
                {
                    level++;
                    levelTimer = 0f;
                    UpdateBaseTexture(corpType);
                }
            }
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            base.Draw(sbatch, offsetX, offsetY);

            if (corpType == CorpType.corp1)
                DrawBuilding(sbatch, offsetX, offsetY, BuildingsNight.Corp1MediumTopNight, BuildingsNight.Corp1LargeMidNight, BuildingsNight.Corp1LargeTopNight, Night.NightAdditive, Night.NightColor);
            else if (corpType == CorpType.corp2)
                DrawBuilding(sbatch, offsetX, offsetY, BuildingsNight.Corp2MediumTopNight, BuildingsNight.Corp2LargeMidNight, BuildingsNight.Corp2LargeTopNight, Night.NightAdditive, Night.NightColor);
            else DrawBuilding(sbatch, offsetX, offsetY, BuildingsNight.Corp3MediumTopNight, BuildingsNight.Corp3LargeMidNight, BuildingsNight.Corp3LargeTopNight, Night.NightAdditive, Night.NightColor);

            if (corpType == CorpType.corp1)
                DrawBuilding(sbatch, offsetX, offsetY, Buildings.Corp1MediumTop, Buildings.Corp1LargeMid, Buildings.Corp1LargeTop, Night.DayAdditive, Night.DayColor);
            else if (corpType == CorpType.corp2)
                DrawBuilding(sbatch, offsetX, offsetY, Buildings.Corp2MediumTop, Buildings.Corp2LargeMid, Buildings.Corp2LargeTop, Night.DayAdditive, Night.DayColor);
            else DrawBuilding(sbatch, offsetX, offsetY, Buildings.Corp3MediumTop, Buildings.Corp3LargeMid, Buildings.Corp3LargeTop, Night.DayAdditive, Night.DayColor);

        }

        private void DrawBuilding(SpriteBatch sbatch, int offsetX, int offsetY, Texture2D mediumtop, Texture2D largemid, Texture2D largetop, float additive, float timeColor)
        {
            if (level == 1)
            {
                float layerDepth = (Y * 0.01f) + 0.02f;
                sbatch.Draw(mediumtop, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * timeColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + additive);
            }
            else if (level >= 1)
            {
                float layerDepth = (Y * 0.01f) + 0.02f;
                sbatch.Draw(largemid, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * timeColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + additive);
                layerDepth = (Y * 0.01f) + 0.03f;
                sbatch.Draw(largetop, new Rectangle(bounds.X, bounds.Y - 128, bounds.Width, bounds.Height), null, selectedTint * timeColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + additive);
            }
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            if (haspower)
            {
                if (corpType == CorpType.corp1)
                    DrawBuildingLights(sbatch, offsetX, offsetY, Lights.Corp1SmallLights, Lights.Corp1MediumLightsBottom, Lights.Corp1MediumLightsTop); 
                else if (corpType == CorpType.corp2)
                    DrawBuildingLights(sbatch, offsetX, offsetY, Lights.Corp2SmallLights, Lights.Corp2MediumLightsBottom, Lights.Corp2MediumLightsTop);
                else DrawBuildingLights(sbatch, offsetX, offsetY, Lights.Corp3SmallLights, Lights.Corp3MediumLightsBottom, Lights.Corp3MediumLightsTop);

            }
        }

        private void DrawBuildingLights(SpriteBatch sbatch, int offsetX, int offsetY, Texture2D smalllights, Texture2D mediumlightsbottom, Texture2D mediumlightstop)
        {
            if (level <= 0)
            {
                Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
                float layerDepth = Y * 0.01f;
                sbatch.Draw(smalllights, imageRect, null, selectedTint, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + 0.00001f);
            }
            else
            {

                float layerDepth = Y * 0.01f;
                sbatch.Draw(mediumlightsbottom, new Vector2(position.X - offsetX, position.Y - offsetY), null, selectedTint, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.00001f);
                layerDepth = (Y * 0.01f) + 0.02f;
                sbatch.Draw(mediumlightstop, new Vector2(position.X - offsetX, position.Y - offsetY - 64), null, selectedTint, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.00001f);
            }
        }

        public override int TileNumber()
        {
            return 7; 
        }
    }
}

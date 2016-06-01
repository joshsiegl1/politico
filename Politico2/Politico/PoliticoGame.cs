using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.GUI;
using Politico2.Politico.TrafficSystem;
using Politico2.Politico.Effects;
using Politico2.Politico.Disasters;
using Politico2.Politico.Tiles;

namespace Politico2.Politico
{
    public class PoliticoGame
    {
        private Grid grid;
        private Traffic traffic; 
        private Cursor cursor;
        private MainUserInterface gui;
        private Night night;
        private EffectsManager effectsManager;
        private DisasterManager disasterManager; 

        public PoliticoGame(MenuSystem.ScreenManager ScreenManger)
        {
            grid = new Grid();
            grid.onTilePlaced += Grid_onTilePlaced;
            grid.onTileDestroyed += Grid_onTileDestroyed;
            grid.onFirePlaced += Grid_onFirePlaced;
            grid.onRiotPlaced += Grid_onRiotPlaced;
            cursor = new Cursor(ScreenManger);

            gui = new MainUserInterface();
            gui.onTileSelect += Gui_onTileSelect;
            gui.onBulldozerClicked += Gui_onBulldozerClicked;
            gui.onOptionsButtonClicked += Gui_onOptionsButtonClicked;


            traffic = new Traffic();
            night = new Night();
            effectsManager = new EffectsManager();
            cursor.SetSelectedTile(new Tiles.Grass(Vector2.Zero), false);

            traffic.onFireOut += Traffic_onFireOut;
            traffic.onRiotDone += Traffic_onRiotDone;
            traffic.onWaterEffect += Traffic_onWaterEffect;
            traffic.onChopperRemove += Traffic_onChopperRemove;

            disasterManager = new DisasterManager();
            disasterManager.onDustHandle += DisasterManager_onDustHandle;
            disasterManager.onNukeHit += DisasterManager_onNukeHit;
            disasterManager.onShootTileDestroy += DisasterManager_onShootTileDestroy;
            disasterManager.onWarTileBombed += DisasterManager_onWarTileBombed;
            disasterManager.onStormTileStruck += DisasterManager_onStormTileStruck;

            //effectsManager.AddCloud(Vector2.Zero, new Vector2(0, 1920), new Vector2(0, 400), new Vector2(0.1f, 0)); 
        }

        private void DisasterManager_onStormTileStruck(object sender, EventArgs e, Tile t)
        {
            if (t.CanBeBurnt())
                StartFire(t); 
        }

        private void DisasterManager_onWarTileBombed(object sender, EventArgs e, Tile t)
        {
            effectsManager.Add(EffectsManager.EffectType.Explosion, t.Position);
            Grid_onTileDestroyed(sender, e, t, grid.getTiles());
            grid.KillTile(t.X, t.Y); 
        }

        private void DisasterManager_onShootTileDestroy(object sender, EventArgs e, Tile killTile)
        {
                effectsManager.Add(EffectsManager.EffectType.Explosion, killTile.Position);
                Grid_onTileDestroyed(sender, e, killTile, grid.getTiles());
                grid.KillTile(killTile.X, killTile.Y);
        }

        private void DisasterManager_onNukeHit(object sender, EventArgs e)
        {
            grid.SetTiles(grid.OriginalTiles);
            effectsManager.KillAllEffects();  
            traffic.ClearAllTraffic(); 
        }

        private void DisasterManager_onDustHandle(object sender, EventArgs e, Tile t)
        {
            effectsManager.Add(EffectsManager.EffectType.Dust, t.Position);
        }

        private void Traffic_onChopperRemove(object sender, EventArgs e, Vector2 position)
        {
            effectsManager.Add(EffectsManager.EffectType.Explosion, position); 
        }

        private void Traffic_onWaterEffect(object sender, EventArgs e, Vector2 position)
        {
            effectsManager.Add(EffectsManager.EffectType.Water, position);
        }

        private void Traffic_onRiotDone(object sender, EventArgs e, Tile t)
        {
            disasterManager.KillRiot(t);
        }

        private void Traffic_onFireOut(object sender, EventArgs e, Tiles.Tile t)
        {
            effectsManager.KillFire(t);
        }

        private void Grid_onFirePlaced(object sender, EventArgs e, Tiles.Tile t)
        {
            StartFire(t); 
        }

        void StartFire(Tiles.Tile t)
        {
            effectsManager.Add(EffectsManager.EffectType.Fire, t);
            t.onFire = true; 
            if (t.FireZone)
                traffic.DispatchFireCopter(t); 
        }

        private void Grid_onRiotPlaced(object sender, EventArgs e, Tiles.Tile t)
        {
            StartRiot(t); 
        }

        private void StartRiot(Tile t)
        {
            disasterManager.Add(DisasterManager.DisasterType.Riot, t); 
            if (t.PoliceZone)
                traffic.DispatchPoliceCopter(t);
        }

        private void Gui_onBulldozerClicked(object sender, EventArgs e)
        {
            cursor.SetSelectedTile(new Tiles.Grass(Vector2.Zero), !cursor.isBulldozer); 
        }

        private void Gui_onTileSelect(Tiles.Tile t, EventArgs e)
        {
            if (cursor.SelectedTile.TileNumber() == t.TileNumber())
                cursor.SetSelectedTile(new Tiles.Grass(Vector2.Zero), false); 
            else
                cursor.SetSelectedTile(t); 
        }

        public event EventHandler onOptionsButtonClicked;
        private void Gui_onOptionsButtonClicked(object sender, EventArgs e)
        {
            if (onOptionsButtonClicked != null)
                onOptionsButtonClicked(sender, e);
        }

        int carCount = 0;
        bool addCar = false; 
        private void Grid_onTilePlaced(object sender, EventArgs e, Tiles.Tile t, Tiles.Tile[,] Grid)
        {

            if (carCount <= 25)
            {
                if (t is Tiles.Road)
                {
                    if (!addCar)
                    {
                        traffic.AddCar(t as Tiles.Road);
                        carCount++;
                    }
                    addCar = !addCar; 
                }
            }

            if (t is Tiles.FireStation)
            {
                traffic.AddFireCopter(t as Tiles.FireStation); 
            }

            if (t is Tiles.PoliceStation)
            {
                traffic.AddPoliceCopter(t as Tiles.PoliceStation); 
            }

            if (t is Tiles.Coal)
            {
                effectsManager.Add(EffectsManager.EffectType.CoalStack, t);
            }

            traffic.UpdateRoadMap(Grid);
        }

        private void Grid_onTileDestroyed(object sender, EventArgs e, Tiles.Tile t, Tiles.Tile[,] grid)
        {

            effectsManager.Add(EffectsManager.EffectType.Fract, t);

            if (t is PoliceStation || t is FireStation)
            {
                traffic.RemoveChopper(t.UniqueKey); 
            }
            
            if (t is Coal)
            {
                effectsManager.KillSmokeStack(t); 
            }
        }

        public void Update(GameTime gametime)
        {
            Camera.Update(gametime); 

            if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.helicopter)
            {
                traffic.RemoveChopperRandom(); 
                MainUserInterface._DisasterSelection = MainUserInterface.DisasterSelection.None; 
            }

            if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.Nuclear)
            {
                disasterManager.Add(DisasterManager.DisasterType.Nuke, null); 
                MainUserInterface._DisasterSelection = MainUserInterface.DisasterSelection.None; 
            }

            if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.Alien)
            {
                disasterManager.Add(DisasterManager.DisasterType.Alien, null);
                MainUserInterface._DisasterSelection = MainUserInterface.DisasterSelection.None; 
            }

            if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.War)
            {
                disasterManager.Add(DisasterManager.DisasterType.War, null);
                MainUserInterface._DisasterSelection = MainUserInterface.DisasterSelection.None; 
            }

            if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.Storm)
            {
                disasterManager.Add(DisasterManager.DisasterType.Storm, null);
                MainUserInterface._DisasterSelection = MainUserInterface.DisasterSelection.None; 
            }

            cursor.Update(gametime);
            grid.Update(gametime, cursor);
            traffic.Update(gametime); 
            gui.Update(gametime, cursor);
            effectsManager.Update(gametime, grid.getTiles());
            night.Update(gametime); 
            disasterManager.Update(gametime, grid.getTiles());
        }

        public void Draw(SpriteBatch sbatch, Matrix ScreenMatrix, GraphicsDevice device, MenuSystem.ScreenManager ScreenManager)
        {
            sbatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, ScreenMatrix);
            night.Draw(sbatch, ScreenMatrix);
            sbatch.End();
            sbatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Camera.get_transformation(device, ScreenMatrix, ScreenManager));
            grid.Draw(sbatch);
            cursor.DrawTransparentTile(sbatch, (int)-Camera.Pos.X, (int)-Camera.Pos.Y);
            traffic.Draw(sbatch);
            disasterManager.DrawRiots(sbatch);
            disasterManager.DrawAlien(sbatch);
            disasterManager.DrawWar(sbatch);
            disasterManager.DrawStorm(sbatch); 
            effectsManager.Draw(sbatch);
            sbatch.End();
            sbatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, null, null, null, null, Camera.get_transformation(device, ScreenMatrix, ScreenManager));
            effectsManager.DrawWithAlphaBlend(sbatch);
            disasterManager.DrawNuke(sbatch);
            disasterManager.DrawAlienBeams(sbatch); 
            sbatch.End();
            sbatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, ScreenMatrix);
            gui.Draw(sbatch);
            cursor.Draw(sbatch);
            cursor.DrawDebug(sbatch);
            night.DrawDebug(sbatch); 
            sbatch.End(); 
        }
    }
}

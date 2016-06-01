using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.GUI; 

namespace Politico2.Politico
{
    public class MainUserInterface
    {
        private TitleBar titlebar;
        private ConstructionBar constructbar;
        private GridBar gridbar;
        private DisasterBar disasterbar;
        private BottomBar bottombar; 
        public MainUserInterface()
        {
            titlebar = new TitleBar();
            constructbar = new ConstructionBar();
            gridbar = new GridBar();
            disasterbar = new DisasterBar();
            bottombar = new BottomBar(); 

            titlebar.onHammerClicked += Titlebar_onHammerClicked;
            titlebar.onBulldozerClicked += Titlebar_onBulldozerClicked;
            titlebar.onGridClicked += Titlebar_onGridClicked;
            titlebar.onDisasterClicked += Titlebar_onDisasterClicked;
            titlebar.onOptionsButtonClicked += Titlebar_onOptionsButtonClicked;
            titlebar.onWindowButtonClicked += Titlebar_onWindowButtonClicked;
            titlebar.onHiddenWindowClicked += Titlebar_onHiddenWindowClicked;

            constructbar.onTileSelect += Constructbar_onTileSelect;
            gridbar.onPowerClicked += Gridbar_onPowerClicked;
            gridbar.onFireClicked += Gridbar_onFireClicked;
            gridbar.onPoliceClicked += Gridbar_onPoliceClicked;

            disasterbar.onFireClicked += Disasterbar_onFireClicked;
            disasterbar.onRiotClicked += Disasterbar_onRiotClicked;
            disasterbar.onHelicopterClicked += Disasterbar_onHelicopterClicked;
            disasterbar.onEarthQuakeClicked += Disasterbar_onEarthQuakeClicked;
            disasterbar.onAlienClicked += Disasterbar_onAlienClicked;
            disasterbar.onNuclearClicked += Disasterbar_onNuclearClicked;
            disasterbar.onWarClicked += Disasterbar_onWarClicked;
            disasterbar.onStormClicked += Disasterbar_onStormClicked;
        }

        private void Titlebar_onHiddenWindowClicked(object sender, EventArgs e)
        {
            if (!titlebar.Show) titlebar.Toggle();
            if (!bottombar.Show) bottombar.Toggle(); 
        }

        private void Titlebar_onWindowButtonClicked(object sender, EventArgs e)
        {
            if (titlebar.Show)
            {
                titlebar.Toggle();
                titlebar.DeselectAll(); 
            }
            if (disasterbar.Show)
            {
                disasterbar.Toggle();
                disasterbar.DeselectAll();
                SetDisasterSelection(DisasterSelection.None); 
            }
            if (constructbar.Show)
            {
                constructbar.Toggle();
                constructbar.DeselectAll(); 
                
            }
            if (bottombar.Show)
            {
                bottombar.Toggle();
            }
            if (gridbar.Show)
            {
                gridbar.Toggle();
                gridbar.DeselectAll();
                SetGridSelection(GridSelection.None); 
            }
        }

        public event EventHandler onOptionsButtonClicked; 
        private void Titlebar_onOptionsButtonClicked(object sender, EventArgs e)
        {
            if (onOptionsButtonClicked != null)
                onOptionsButtonClicked(sender, e); 
        }

        private void Disasterbar_onStormClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Storm); 
        }

        private void Disasterbar_onWarClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.War); 
        }

        private void Disasterbar_onNuclearClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Nuclear); 
        }

        private void Disasterbar_onAlienClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Alien); 
        }

        private void Disasterbar_onEarthQuakeClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Earthquake);
            Camera.Shake(5000f, true); 
        }

        private void Disasterbar_onHelicopterClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.helicopter); 
        }

        public static DisasterSelection _DisasterSelection = DisasterSelection.None; 
        public enum DisasterSelection
        {
            None, 
            Fire, 
            Riot, 
            Earthquake, 
            helicopter, 
            Alien, 
            Nuclear, 
            War, 
            Storm 
        }

        private void Disasterbar_onRiotClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Riot); 
        }

        private void Disasterbar_onFireClicked(object sender, EventArgs e)
        {
            SetDisasterSelection(DisasterSelection.Fire); 
        }

        public static GridSelection _GridSelection = GridSelection.None; 
        public enum GridSelection
        {
            Power, 
            Fire, 
            Police, 
            None
        }

        void SetDisasterSelection(DisasterSelection selection)
        {
            constructbar.DeselectAll(); 
            if (_DisasterSelection == selection)
                _DisasterSelection = DisasterSelection.None;
            else _DisasterSelection = selection;
        }

        private void Gridbar_onPowerClicked(object sender, EventArgs e)
        {
            SetGridSelection( GridSelection.Power ); 
        }

        private void Gridbar_onPoliceClicked(object sender, EventArgs e)
        {
            SetGridSelection( GridSelection.Police ); 
        }

        private void Gridbar_onFireClicked(object sender, EventArgs e)
        {
            SetGridSelection( GridSelection.Fire ); 
        }

        void SetGridSelection(GridSelection selection)
        {
            disasterbar.DeselectAll();
            _DisasterSelection = DisasterSelection.None; 
            if (_GridSelection == selection)
                _GridSelection = GridSelection.None;
            else _GridSelection = selection; 
        }

        private void Titlebar_onDisasterClicked(object sender, EventArgs e)
        {
            disasterbar.Toggle();
            disasterbar.DeselectAll();
            if (gridbar.Show)
                gridbar.Toggle(); 

        }

        public EventHandler onGridClicked; 
        private void Titlebar_onGridClicked(object sender, EventArgs e)
        {
            gridbar.Toggle();
            if (disasterbar.Show)
                disasterbar.Toggle();
            _GridSelection = GridSelection.None;
            gridbar.DeselectAll(); 
            if (onGridClicked != null)
            {
                onGridClicked(this, e);
            }
        }

        public event EventHandler onBulldozerClicked; 
        private void Titlebar_onBulldozerClicked(object sender, EventArgs e)
        {
            if (onBulldozerClicked != null)
            {
                constructbar.DeselectAll(); 
                onBulldozerClicked(this, e);
            }
        }

        public delegate void TileSelectEvent(Tiles.Tile t, EventArgs e);
        public event TileSelectEvent onTileSelect; 

        private void Constructbar_onTileSelect(Tiles.Tile t, EventArgs e)
        {
            disasterbar.DeselectAll();
            _DisasterSelection = DisasterSelection.None; 
            if (onTileSelect != null)
            {
                titlebar.Deselect("Bulldozer"); 
                onTileSelect(t, e);
            }
        }

        private void Titlebar_onHammerClicked(object sender, EventArgs e)
        {
            constructbar.Toggle(); 
        }

        public void Update(GameTime gametime, Cursor cursor)
        {
            titlebar.Update(gametime, cursor);
            constructbar.Update(gametime, cursor);
            bottombar.Update(gametime); 
            gridbar.Update(gametime, cursor);
            disasterbar.Update(gametime, cursor); 
        }

        public void Draw(SpriteBatch sbatch)
        {
            titlebar.Draw(sbatch);
            bottombar.Draw(sbatch);
            constructbar.Draw(sbatch);
            gridbar.Draw(sbatch);
            disasterbar.Draw(sbatch); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Content;

using Politico2.Politico.Tiles;
using Politico2.Politico.GUI;
using Politico2.Politico.TrafficSystem;
using Politico2.Politico.Effects;
using Politico2.Politico.Disasters; 

namespace Politico2.Politico
{
    public static class Assets
    {
        public static SpriteFont debugFont; 

        public static Texture2D Base, BaseNight;

        private static void LoadNightContent(ContentManager Content)
        {
            Capital.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/capitol_night");
            House.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/house_night");
            Grass.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/grass_night");
            Coal.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/coalfactory_night");
            Apartment.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/apartment_night");
            Condo.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Condo/Night/condo_bottom_night");
            Condo.Texture_Top_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Condo/Night/condo_top_night");
            FireStation.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/firestation_night");
            PoliceStation.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/policestation_night");
            Tree.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/tree_night");
            CorpFactory.Buildings.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Night/corpfactory1_night");

            Road.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_up_right_night");
            Road.PiecesNight.Add("up_right", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_up_right_night"));
            Road.PiecesNight.Add("up_left", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_up_left_night"));
            Road.PiecesNight.Add("tri_topright-bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_tri_topright-bottomright-bottomleft_night"));
            Road.PiecesNight.Add("tri_topleft-topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_tri_topleft-topright-bottomright_night"));
            Road.PiecesNight.Add("tri_bottomright-bottomleft-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_tri_bottomright-bottomleft-topleft_night"));
            Road.PiecesNight.Add("tri_bottomleft-topleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_tri_bottomleft-topleft-topright_night"));
            Road.PiecesNight.Add("cross", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_cross_night"));
            Road.PiecesNight.Add("angle_topright-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_angle_topright-topleft_night"));
            Road.PiecesNight.Add("angle_topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_angle_topright-bottomright_night"));
            Road.PiecesNight.Add("angle_topleft-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_angle_topleft-bottomleft_night"));
            Road.PiecesNight.Add("angle_bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/road_angle_bottomright-bottomleft_night"));

            Water.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_full_night");
            Water.PiecesNight.Add("tri_topright-bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_tri_topright-bottomright-bottomleft_night"));
            Water.PiecesNight.Add("tri_topleft-topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_tri_topleft-topright-bottomright_night"));
            Water.PiecesNight.Add("tri_bottomright-bottomleft-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_tri_bottomright-bottomleft-topleft_night"));
            Water.PiecesNight.Add("tri_bottomleft-topleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_tri_bottomleft-topleft-topright_night"));
            Water.PiecesNight.Add("cross", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_center_night"));
            Water.PiecesNight.Add("angle_topright-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_angle_topright-topleft_night"));
            Water.PiecesNight.Add("angle_topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_angle_topright-bottomright_night"));
            Water.PiecesNight.Add("angle_topleft-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_angle_topleft-bottomleft_night"));
            Water.PiecesNight.Add("angle_bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_angle_bottomright-bottomleft_night"));
            Water.PiecesNight.Add("full", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_full_night"));
            Water.PiecesNight.Add("straight_bottomleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_straight_bottomleft-topright_night"));
            Water.PiecesNight.Add("straight_topleft-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_straight_topleft-bottomright_night"));
            Water.PiecesNight.Add("single_bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_single_bottomleft_night"));
            Water.PiecesNight.Add("single_bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_single_bottomright_night"));
            Water.PiecesNight.Add("single_topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_single_topleft_night"));
            Water.PiecesNight.Add("single_topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/Night/water_single_topright_night"));

            WindTurbine.Texture_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/Night/windturbine_base_night");
            WindTurbine.Texture_Top_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/Night/windturbine_top_night");
            WindTurbine.Texture_Blade_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/Night/windturbine_blade_night"); 

            Corporation.BuildingsNight.Corp1SmallNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp1_small_night");
            Corporation.BuildingsNight.Corp1MediumBottomNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp1_medium_bottom_night");
            Corporation.BuildingsNight.Corp1MediumTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp1_medium_top_night");
            Corporation.BuildingsNight.Corp1LargeMidNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp1_large_mid_night");
            Corporation.BuildingsNight.Corp1LargeTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp1_large_top_night");
                                 
            Corporation.BuildingsNight.Corp2SmallNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp2_small_night");
            Corporation.BuildingsNight.Corp2MediumBottomNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp2_medium_bottom_night");
            Corporation.BuildingsNight.Corp2MediumTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp2_medium_top_night");
            Corporation.BuildingsNight.Corp2LargeMidNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp2_large_mid_night");
            Corporation.BuildingsNight.Corp2LargeTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp2_large_top_night");
                                 
            Corporation.BuildingsNight.Corp3SmallNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp3_small_night");
            Corporation.BuildingsNight.Corp3MediumBottomNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp3_medium_bottom_night");
            Corporation.BuildingsNight.Corp3MediumTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp3_medium_top_night");
            Corporation.BuildingsNight.Corp3LargeMidNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp3_large_mid_night");
            Corporation.BuildingsNight.Corp3LargeTopNight = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/Night/corp3_large_top_night");

            Capital.FlagTextureNight = Content.Load<Texture2D>("Game/Graphics/Tiles/flag_sheet_night"); 

            BaseNight = Content.Load<Texture2D>("Game/Graphics/base_night"); 

        }

        public static void LoadContent(ContentManager Content)
        {
            LoadNightContent(Content);
            LoadFonts(Content); 

            debugFont = Content.Load<SpriteFont>("Menu/Fonts/LoginFont"); 

            Base = Content.Load<Texture2D>("Game/Graphics/base");

            Sun.Texture = Content.Load<Texture2D>("Game/Graphics/Particles/sunparticle");
            Night.Moon = Content.Load<Texture2D>("Game/Graphics/moon"); 

            Night.BackgroundTexture = Content.Load<Texture2D>("Game/Graphics/night-background");
            Night.StormBackgroundTexture = Content.Load<Texture2D>("Game/Graphics/Other/stormBackground"); 
            Night.Star.Texture = Content.Load<Texture2D>("Game/Graphics/Particles/fireparticle");

            Cursor.Texture = Content.Load<Texture2D>("cursor");
            Grass.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/grass");
            Capital.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/capitol");
            Capital.FlagTexture = Content.Load<Texture2D>("Game/Graphics/Tiles/flag_sheet");
            Road.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_up_right");
            House.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/house");

            WindTurbine.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/windturbine_base");
            WindTurbine.Texture_Top = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/windturbine_top");
            WindTurbine.Texture_Blade = Content.Load<Texture2D>("Game/Graphics/Tiles/WindTurbine/windturbine_blade");

            Coal.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/coalfactory");
            Coal.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/CorpFactory/corpfactory_lights"); 

            Corporation.Buildings.Corp1Small = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp1_small");
            Corporation.Buildings.Corp1MediumBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp1_medium_bottom");
            Corporation.Buildings.Corp1MediumTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp1_medium_top");
            Corporation.Buildings.Corp1LargeMid = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp1_large_mid");
            Corporation.Buildings.Corp1LargeTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp1_large_top");

            Corporation.Buildings.Corp2Small = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp2_small");
            Corporation.Buildings.Corp2MediumBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp2_medium_bottom");
            Corporation.Buildings.Corp2MediumTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp2_medium_top");
            Corporation.Buildings.Corp2LargeMid = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp2_large_mid");
            Corporation.Buildings.Corp2LargeTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp2_large_top");

            Corporation.Buildings.Corp3Small = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp3_small");
            Corporation.Buildings.Corp3MediumBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp3_medium_bottom");
            Corporation.Buildings.Corp3MediumTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp3_medium_top");
            Corporation.Buildings.Corp3LargeMid = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp3_large_mid");
            Corporation.Buildings.Corp3LargeTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Corporations/corp3_large_top");

            Corporation.Lights.Corp1SmallLights = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp1_small_lights");
            Corporation.Lights.Corp1MediumLightsBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp1_medium_lights_bottom");
            Corporation.Lights.Corp1MediumLightsTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp1_medium_lights_top");

            Corporation.Lights.Corp2SmallLights = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp2_small_lights");
            Corporation.Lights.Corp2MediumLightsBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp2_medium_lights_bottom");
            Corporation.Lights.Corp2MediumLightsTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp2_medium_lights_top");

            Corporation.Lights.Corp3SmallLights = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp3_small_lights");
            Corporation.Lights.Corp3MediumLightsBottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp3_medium_lights_bottom");
            Corporation.Lights.Corp3MediumLightsTop = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Corporations/corp3_medium_lights_top");

            CorpFactory.Buildings.CorpFactory1 = Content.Load<Texture2D>("Game/Graphics/Tiles/CorpFactory/corpfactory1");

            CorpFactory.Lights.CorpFactory1Lights = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/CorpFactory/corpfactory_lights"); 


            Road.Pieces.Add("up_right", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_up_right"));
            Road.Pieces.Add("up_left", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_up_left"));
            Road.Pieces.Add("tri_topright-bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_tri_topright-bottomright-bottomleft"));
            Road.Pieces.Add("tri_topleft-topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_tri_topleft-topright-bottomright"));
            Road.Pieces.Add("tri_bottomright-bottomleft-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_tri_bottomright-bottomleft-topleft"));
            Road.Pieces.Add("tri_bottomleft-topleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_tri_bottomleft-topleft-topright"));
            Road.Pieces.Add("cross", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_cross"));
            Road.Pieces.Add("angle_topright-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_angle_topright-topleft"));
            Road.Pieces.Add("angle_topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_angle_topright-bottomright"));
            Road.Pieces.Add("angle_topleft-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_angle_topleft-bottomleft"));
            Road.Pieces.Add("angle_bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Road/road_angle_bottomright-bottomleft"));
            Road.TrafficLight = Content.Load<Texture2D>("Game/Graphics/Tiles/Road/trafficlight");
            Road.TrafficLight_Night = Content.Load<Texture2D>("Game/Graphics/Tiles/Road/Night/trafficlight_night"); 

            Water.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_full");
            Water.Pieces.Add("tri_topright-bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_tri_topright-bottomright-bottomleft"));
            Water.Pieces.Add("tri_topleft-topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_tri_topleft-topright-bottomright"));
            Water.Pieces.Add("tri_bottomright-bottomleft-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_tri_bottomright-bottomleft-topleft"));
            Water.Pieces.Add("tri_bottomleft-topleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_tri_bottomleft-topleft-topright"));
            Water.Pieces.Add("cross", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_center"));
            Water.Pieces.Add("angle_topright-topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_angle_topright-topleft"));
            Water.Pieces.Add("angle_topright-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_angle_topright-bottomright"));
            Water.Pieces.Add("angle_topleft-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_angle_topleft-bottomleft"));
            Water.Pieces.Add("angle_bottomright-bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_angle_bottomright-bottomleft"));
            Water.Pieces.Add("full", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_full"));
            Water.Pieces.Add("straight_bottomleft-topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_straight_bottomleft-topright"));
            Water.Pieces.Add("straight_topleft-bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_straight_topleft-bottomright"));
            Water.Pieces.Add("single_bottomleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_single_bottomleft"));
            Water.Pieces.Add("single_bottomright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_single_bottomright"));
            Water.Pieces.Add("single_topleft", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_single_topleft"));
            Water.Pieces.Add("single_topright", Content.Load<Texture2D>("Game/Graphics/Tiles/Water/water_single_topright"));

            Tree.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/tree"); 

            Apartment.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/apartment");
            Apartment.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/apartment_lights");

            Condo.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/Condo/condo_bottom");
            Condo.Texture_Top = Content.Load<Texture2D>("Game/Graphics/Tiles/Condo/condo_top");

            Condo.Texture_Lights_Bottom = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Condo/condo_lights_bottom");
            Condo.Texture_Lights_Top = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/Condo/condo_lights_top");

            PoliceStation.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/policestation");
            PoliceStation.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/policefire_lights");

            FireStation.Texture = Content.Load<Texture2D>("Game/Graphics/Tiles/firestation");
            FireStation.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/policefire_lights"); 

            Empty.Texture = Grass.Texture;

            Button.Textures.BackgroundButton = Content.Load<Texture2D>("Game/GUI/Buttons/background_btn"); 
            Button.Textures.HammerButton = Content.Load<Texture2D>("Game/GUI/Buttons/hammer_btn");
            Button.Textures.RoadButton = Content.Load<Texture2D>("Game/GUI/Buttons/road_btn");
            Button.Textures.HouseButton = Content.Load<Texture2D>("Game/GUI/Buttons/house_btn");
            Button.Textures.WindTurbineButton = Content.Load<Texture2D>("Game/GUI/Buttons/windturbine_btn");
            Button.Textures.CoalButton = Content.Load<Texture2D>("Game/GUI/Buttons/coal_btn");
            Button.Textures.CorporationButton = Content.Load<Texture2D>("Game/GUI/Buttons/corporation_btn");
            Button.Textures.FactoryButton = Content.Load<Texture2D>("Game/GUI/Buttons/factory_btn"); 
            Button.Textures.BulldozerButton = Content.Load<Texture2D>("Game/GUI/Buttons/bulldozer_btn");
            Button.Textures.GridButton = Content.Load<Texture2D>("Game/GUI/Buttons/grid_btn"); 
            Button.Textures.ApartmentButton = Content.Load<Texture2D>("Game/GUI/Buttons/apartment_btn");
            Button.Textures.CondoButton = Content.Load<Texture2D>("Game/GUI/Buttons/condo_btn");
            Button.Textures.PoliceStationButton = Content.Load<Texture2D>("Game/GUI/Buttons/policestation_btn");
            Button.Textures.FireStationButton = Content.Load<Texture2D>("Game/GUI/Buttons/firestation_btn");
            Button.Textures.PowerGridButton = Content.Load<Texture2D>("Game/GUI/Buttons/powergrid_btn");
            Button.Textures.FireGridButton = Content.Load<Texture2D>("Game/GUI/Buttons/firegrid_btn");
            Button.Textures.PoliceGridButton = Content.Load<Texture2D>("Game/GUI/Buttons/policegrid_btn");
            Button.Textures.DisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/disaster_btn");
            Button.Textures.TreeButton = Content.Load<Texture2D>("Game/GUI/Buttons/tree_btn");
            Button.Textures.WaterButton = Content.Load<Texture2D>("Game/GUI/Buttons/water_btn");
            Button.Textures.PoliceDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/policedisaster_btn");
            Button.Textures.FireDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/firedisaster_btn");
            Button.Textures.HelicopterDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/helicopterdisaster_btn");
            Button.Textures.EarthquakeDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/earthquakedisaster_btn");
            Button.Textures.AlienAttackDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/alienattackdisaster_btn");
            Button.Textures.NuclearAttackDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/nuclearattackdisaster_btn");
            Button.Textures.WarDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/wardisaster_btn");
            Button.Textures.StormDisasterButton = Content.Load<Texture2D>("Game/GUI/Buttons/stormdisaster_btn");
            Button.Textures.WindowButton = Content.Load<Texture2D>("Game/GUI/Buttons/window_btn");
            Button.Textures.OptionsButton = Content.Load<Texture2D>("Game/GUI/Buttons/options_btn"); 

            TitleBar.Textures.TitleBarBackground = Content.Load<Texture2D>("Game/GUI/titlebar_placeholder");
            ConstructionBar.Textures.SideBarBackground = Content.Load<Texture2D>("Game/GUI/constructionbar_placeholder");
            GridBar.Textures.SideBarBackground = ConstructionBar.Textures.SideBarBackground;
            DisasterBar.Textures.SideBarBackground = GridBar.Textures.SideBarBackground;
            BottomBar.Textures.Background = Content.Load<Texture2D>("Game/GUI/bottombar"); 

            Car.Cars.Texture_downleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car_down_left");
            Car.Cars.Texture_downright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car_down_right");
            Car.Cars.Texture_upleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car_up_left");
            Car.Cars.Texture_upright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car_up_right");

            Car.Cars.Texture2_downleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car2_down_left");
            Car.Cars.Texture2_downright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car2_down_right");
            Car.Cars.Texture2_upleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car2_up_left");
            Car.Cars.Texture2_upright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car2_up_right");

            Car.Cars.Texture3_downleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car3_down_left");
            Car.Cars.Texture3_downright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car3_down_right");
            Car.Cars.Texture3_upleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car3_up_left");
            Car.Cars.Texture3_upright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car3_up_right");

            Car.Cars.Texture4_downleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car4_down_left");
            Car.Cars.Texture4_downright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car4_down_right");
            Car.Cars.Texture4_upleft = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car4_up_left");
            Car.Cars.Texture4_upright = Content.Load<Texture2D>("Game/Graphics/Vehicles/Cars/car4_up_right");

            FireCopter.Textures.DownLeftTexture = Content.Load<Texture2D>("Game/Graphics/Vehicles/Helicopters/firecopter_downleft");
            FireCopter.Textures.DownRightTexture = Content.Load<Texture2D>("Game/Graphics/Vehicles/Helicopters/firecopter_downright");

            PoliceCopter.Textures.DownLeftTexture = Content.Load<Texture2D>("Game/Graphics/Vehicles/Helicopters/policecopter_downleft");
            PoliceCopter.Textures.DownRightTexture = Content.Load<Texture2D>("Game/Graphics/Vehicles/Helicopters/policecopter_downright");

            Helicopter.Shadow = Content.Load<Texture2D>("Game/Graphics/Vehicles/Helicopters/helicopter_shadow"); 

            Capital.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/capital_lights");
            House.Texture_Light = Content.Load<Texture2D>("Game/Graphics/Tiles/Lights/house_lights");

            EffectsManager.ParticleTextures.Smoke = Content.Load<Texture2D>("Game/Graphics/Particles/smokeparticle");
            EffectsManager.ParticleTextures.Smoke_Night = Content.Load<Texture2D>("Game/Graphics/Particles/Night/smokeparticle_night");
            EffectsManager.ParticleTextures.Dust_Night = Content.Load<Texture2D>("Game/Graphics/Particles/Night/dust_particle_night"); 
            EffectsManager.ParticleTextures.Dust = Content.Load<Texture2D>("Game/Graphics/Particles/dust_particle");  
            EffectsManager.ParticleTextures.Fire = Content.Load<Texture2D>("Game/Graphics/Particles/fireparticle");
            EffectsManager.ParticleTextures.Water = EffectsManager.ParticleTextures.Fire;
            EffectsManager.ParticleTextures.Explosion = Content.Load<Texture2D>("Game/Graphics/Particles/explosion");
            EffectsManager.ParticleTextures.Cloud = Content.Load<Texture2D>("Game/Graphics/Particles/cloud_particle");
            EffectsManager.ParticleTextures.Cloud_Night = Content.Load<Texture2D>("Game/Graphics/Particles/Night/cloud_particle_night");
            EffectsManager.ParticleTextures.Rain = Content.Load<Texture2D>("Game/Graphics/Particles/raindrop"); 

            DisasterManager.Textures.RiotTexture = Content.Load<Texture2D>("Game/Graphics/Tiles/Road/riot");
            DisasterManager.Textures.MissleTexuture = Content.Load<Texture2D>("Game/Graphics/Other/missle");
            DisasterManager.Textures.SpaceShip = Content.Load<Texture2D>("Game/Graphics/Other/spaceship");
            DisasterManager.Textures.LazerBeam = Content.Load<Texture2D>("Game/Graphics/Other/lazerbeam");
            DisasterManager.Textures.Jet = Content.Load<Texture2D>("Game/Graphics/Other/jet");
            DisasterManager.Textures.Bomb = Content.Load<Texture2D>("Game/Graphics/Other/bomb");
            DisasterManager.Textures.Lightning = Content.Load<Texture2D>("Game/Graphics/Other/lightning"); 
        }

        private static void LoadFonts(ContentManager Content)
        {
            BottomBar.Fonts.MessageFont = Content.Load<SpriteFont>("Fonts/messagefont"); 
        }
    }
}
